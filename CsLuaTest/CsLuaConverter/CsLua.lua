CsLuaMeta = {};
CsLuaMeta.Foreach = function(obj)
	if type(obj) == 'table' and obj.GetEnumerator then
		return obj.GetEnumerator();
	end
	return pairs(obj);
end
local function SetAddMeta(f)
	local mt = { __add = function(self, b) return f(self[1], b) end }
	return setmetatable({}, { __add = function(a, _) return setmetatable({ a }, mt) end })
end
CsLuaMeta.add = SetAddMeta(function(a, b)
	assert(a and b, "Add called on a nil value.");
	if type(a) == "number" and type(b) == "number" then return a + b; end
	return tostring(a)..tostring(b);
end);

CsLuaMeta._not = setmetatable({}, { __add = function(_, value)
	return not(value);
end});

local DotMeta = function(f1, f2) 
	return setmetatable({}, {
		__mod = function(obj, _) 
			return setmetatable({}, {
				__index = function(_, index)
					return f1(obj,index);
				end,
				__newindex = function(_, index, value)
					return f2(obj,index, value);
				end,
			})
		end
	});
end

local typeBasedMethods = {
	ToString = function(obj)
		if type(obj) == "table" and obj.__fullTypeName then
			return obj.__fullTypeName;
		end
		if type(obj) == "boolean" then
			if (obj == true) then
				return "True";
			end
			return "False";
		end
		return tostring(obj);
	end,
	Equals = function(obj, otherObj)
		return obj == otherObj or (type(obj) == "table" and type(otherObj) == "table" and obj.__obj == otherObj.__obj);
	end
}

for i,v in pairs(typeBasedMethods) do
	CsLuaMeta[i] = function(...) 
		local args = {...};
		return setmetatable({}, { 
			__mul = function(obj)
				if type(obj) == "table" and obj.__fullTypeName and type(obj[i]) == "function" then
					return obj[i](unpack(args));
				end
				return v(obj, unpack(args));
			end
		});
	 end;
end

CsLuaMeta.dot = DotMeta(
	function(obj, index)  -- useage:  a%CsLuaMeta.dot%b
		if type(obj) == "table" then
			local value = obj[index];

			if not(value == nil) then
				return value;
			end
		end
		if typeBasedMethods[index] then
			return function(...)
				return typeBasedMethods[index](obj, ...);
			end
		end
	end, 
	function(obj, index, value)
		if type(obj) == "table" then
			obj[index] = value;
		end
	end
);


int = {
	Parse = function(value)
		return tonumber(value);
	end,
}

CsLuaMeta.Cast = function(castType) return setmetatable({}, { __add = function(_, value)
	if not(type(value) == "table" and value.__fullTypeName) then
		return value;
	end

	if (CsLuaMeta.IsType(value, castType)) then
		return value;
	end

	if (value.__obj) then
		return CsLua.Wrapping.Wrapper.WrapObject[{{name = castType}}](value.__obj, true);
	end

	error("Could not cast table value to "..castType..". Got type "..tostring(value.__fullTypeName));
end}); end

CsLuaMeta.GetByFullName = function(s, doNotThrow)
	local n = {string.split(".",s)};
	local o = _G[n[1]];
	
	if not(o) then
		if doNotThrow then
			return;
		else
			error("Could not find global "..s);
		end
	end

	for i=2,#(n) do
		o = o[n[i]];
		
		if not(o) then
			if doNotThrow then
				return;
			else
				error("Could not find global "..s);
			end
		end
	end
	return o;
end

CsLuaMeta.GenericsMethod = function(func)
	local t = {}
	setmetatable(t, {
		__index = function(_, generics)
			return function(...) return func(generics, ...); end;
		end,
		__call = function(_, ...)
			return func(CsLuaMeta.GenericsList(CsLuaMeta.Generic('object')), ...);
		end
	});

	return t;
end

CsLuaMeta.EnumParse = function(typeName, value, doNotThrow)
	if type(typeName) == "table" and  typeName.__fullTypeName == "System.Type" then
		typeName = typeName.FullName;
	end

	local enumTable = CsLuaMeta.GetByFullName(typeName, doNotThrow);
	if type(enumTable) == "table" then
		for i,v in pairs(enumTable) do
			if string.lower(value) == string.lower(i) then
				return v;
			end
		end
	end

	if not(doNotThrow) then
		CsLuaMeta.Throw(CsLua.CsException().__Cstor("Could not parse enum value " .. tostring(value) .. " into " .. typeName));
	end
end

local defaultValues = {
	bool = false,
	int = 0,
	float = 0,
	long = 0,
	double = 0,
}

CsLuaMeta.GetDefaultValue = function(type, isNullable, generics)
	if isNullable then
		return nil;
	end

	for _,v in ipairs(generics or {}) do
		if v == type then
			return nil;
		end
	end

	if not(defaultValues[type] == nil) then
		return defaultValues[type];
	end

	--Handle eventual enum. Return nil if not an enum.
	return CsLuaMeta.EnumParse(type, "__default", true);
end

CsLuaMeta.IsType = function(obj, t)
	if t == "object" then
		return true;
	end

	if type(obj) == "table" then
		if obj.__IsType then
			return obj.__IsType(t);
		elseif type(obj.GetType) == "function" then
			return obj:GetType() == t;
		end
	end
	if type(obj) == "boolean" then
		return "bool" == t;
	elseif type(obj) == "function" then
		return "System.Action" == t;
	elseif type(obj) == "number" then
		return "double" == t or "int" == t;
	elseif type(obj) == "string" then
		return t == "string";
	elseif type(obj) == "nil" then
		return true;
	end
	return error("Unknown type handling "..type(obj));
end

CsLuaMeta.GetSignatures = function(...)
	local args = {...};
	local signatures = {};
	for i = 1,select('#', ...) do
		signatures[i] = {{"object"}};
		local obj = args[i]

		if type(obj) == "table" then
			if (obj.__GetSignature) then
				signatures[i] = obj.__GetSignature();
			elseif type(obj.GetObjectType) == "function" then
				table.insert(signatures[i], 1, {"Lua.NativeLuaTable"});
				table.insert(signatures[i], 1, {"BlizzardApi.WidgetInterfaces.INativeUIObject"});
			else
				table.insert(signatures[i], 1, {"Lua.NativeLuaTable"});
			end
		elseif type(obj) == "boolean" then
			table.insert(signatures[i], 1, {"bool"});
		elseif type(obj) == "function" then
			table.insert(signatures[i], 1, {"System.Action"});
			table.insert(signatures[i], 1, {"System.Func"});
		elseif type(obj) == "number" then
			table.insert(signatures[i], 1, {"double"});
			table.insert(signatures[i], 1, {"int"});
			table.insert(signatures[i], 1, {"long"});
		elseif type(obj) == "string" then
			table.insert(signatures[i], 1, {"string"});
		elseif type(obj) == "nil" then
			table.insert(signatures[i], 1, {"null"});
		end
	end

	return signatures;
end

CsLuaMeta.IsMatchingEnum = function(enumType, obj)
	if type(obj) == "string" then
		return not(CsLuaMeta.EnumParse(enumType, obj, true) == nil)
	end
	return false;
end

CsLuaMeta.IdentifyTypeInSignature = function(typeName, typeGenerics, signature, value)
	for j, argSignature in ipairs(signature or {}) do
		local argTypeName, argTypeGenerics = argSignature[1], argSignature[2];

		if typeName == argTypeName and (
				(not(typeGenerics) and not(argTypeGenerics))
				or (typeGenerics and (typeGenerics.Equals(argTypeGenerics) or not(argTypeGenerics)))
			)
			or argTypeName == "null" 
			or CsLuaMeta.IsMatchingEnum(typeName, value, true)  then

			return j - 1;
		end
	end
end

CsLuaMeta.ScoreFunction = function(types, signature, args, generic, hasParamKeyword)
	if not(#(types) == #(signature) or  hasParamKeyword) then
		return;
	end

	local functionScore = 0;
	for i, typeTable in ipairs(types) do
		local typeName, typeGenerics = typeTable[1], typeTable[2];
		typeName = (generic or {})[typeName] or typeName;

		local argScore;
		
		if (hasParamKeyword and i == #(types)) then
			if signature[i] == nil then
				argScore = 1;
			else
				if typeName == "System.Array" then
					for j = i,#(signature) do
						local score = CsLuaMeta.IdentifyTypeInSignature(typeGenerics[1].name, typeGenerics[1].innerGenerics, signature[j], args[j]);
						if not(score) then
							argScore = nil;
							break
						end

						argScore = (argScore == nil) and score or math.max(argScore, score);
					end
				end
			end
		else
			argScore = CsLuaMeta.IdentifyTypeInSignature(typeName, typeGenerics, signature[i], args[i]);
		end

		if argScore == nil then
			return;
		end
		functionScore = functionScore + argScore;
	end
	return functionScore;
end

CsLuaMeta.GetMatchingFunction = function(functions, generics, ...)
	local argSignature = CsLuaMeta.GetSignatures(...);
	local args = {...};

	local bestFunc, bestScore;
	for _, funcMeta in pairs(functions) do
		local score = CsLuaMeta.ScoreFunction(funcMeta.types, argSignature, args, generics, funcMeta.hasParamKeyword);
		if (score and (bestScore == nil or bestScore > score)) then
			bestScore = score;
			bestFunc = funcMeta.func;
		end
	end

	return bestFunc;
end

CsLuaMeta.Throw = function(exception)
	__CurrentException = exception;
	error(exception.ToString(), 2);
end

CsLuaMeta.GenericsList = function(...)
	local list = {...};

	for i,v in pairs(list) do
		if not(type(v) == "table" or v.__fullTypeName == "CsLuaMeta.Generic") then
			CsLuaMeta.Throw(CsLua.CsException().__Cstor("Invalid element in generics list at pos "..i));
		end
	end

	list.Equals = function(otherList)
		if not(type(otherList) == "table") then
			return false;
		end
		for i,v in ipairs(list) do
			if not(v.Equals(otherList[i])) then
				return false;
			end
		end
		return true;
	end

	list.ToString = function()
		local s = "<";
		for i,v in ipairs(list) do
			if i > 1 then
				s = s .. ",";
			end
			s = s .. v.ToString();
		end
		return s .. ">";
	end

	list.__fullTypeName = "CsLuaMeta.GenericsList";

	return list;
end

CsLuaMeta.Generic = function(name, innerGenerics)
	assert(type(name) == "string",  "Unexpected input to CsLuaMeta.Generic name. Expected string, got: "..tostring(name))
	if innerGenerics then assert(innerGenerics.__fullTypeName == "CsLuaMeta.GenericsList", "Unexpected input to CsLuaMeta.Generic inner generic. Got obj type "..tostring(innerGenerics.__fullTypeName)); end
	local class = {};
	class.innerGenerics = innerGenerics;
	class.name = name;

	class.Equals = function(otherGenerics)
		if type(otherGenerics) == "table" and otherGenerics.name == class.name then
			if (class.innerGenerics) then
				return class.innerGenerics.Equals(otherGenerics.innerGenerics)
			end
			return true;
		end
		return false;
	end

	class.ToString = function()
		local s = name;
		if class.innerGenerics then
			s = s..class.innerGenerics.ToString();
		end
		return s;
	end

	class.__fullTypeName = "CsLuaMeta.Generic";
	return class;
end

CsLuaMeta.Try = function(try, catch, finally)
	__CurrentException = nil;
	local success, err = pcall(try)
	local exception = __CurrentException;
	__CurrentException = nil;

	if not(success) then
		exception = exception or CsLua.CsException().__Cstor("Lua error:\n" .. (err or "nil"));
		
		local matchFound = false;
		for _, catchCase in ipairs(catch or {}) do
			if catchCase.type == nil or exception.__IsType(catchCase.type) then
				catchCase.func(exception)
				matchFound = true;
				break;
			end
		end

		if (matchFound == false) then -- rethrow
			if finally then
				finally();
			end

			__CurrentException = exception;
			error(err, 2);
		end
	end

	if finally then
		finally();
	end
end

CsLuaMeta.SignatureToString = function(signature)
	local args = {};
	for i, argSig in ipairs(signature) do
	    local s = nil;
		for j, sig in ipairs(argSig) do
			if s then s = s .. ":"; else s = ""; end
			s  = s .. tostring(sig[1]);
			if sig[2] then
				s  = s .. tostring(sig[2].ToString());
			end
		end
		args[i] = s;
	end
	return string.join(", ", unpack(args));
end

CsLuaMeta.CreateClass = function(info)
	local staticValues;
	local namespaceElement = {};
	local staticOverride;
	local loadStaticOverride = function()
		if info.inherits then
			staticOverride = CsLuaMeta.GetByFullName(info.inherits);
		end
	end

	local GenerateAmbigiousFunc = function(element, inheritiedClass, generics, methodGenerics)
		local value = element.value(methodGenerics);
		return function(...)
			local matchingFunc = CsLuaMeta.GetMatchingFunction(value, generics, ...);
			if matchingFunc then
				return matchingFunc(...);
			elseif inheritiedClass then
				local f = inheritiedClass[element.name];
				if f then
					return f(...);
				end
			end

			error("No method found for key '"..element.name.."' matching the signature: '"..CsLuaMeta.SignatureToString(CsLuaMeta.GetSignatures(...)).."'");
		end, value;
	end

	local initializeStaticElements = function()
		if staticValues then
			return;
		end

		staticValues = {};
		local elements = info.getElements(namespaceElement, {});

		for _, element in pairs(elements) do
			if (element.static) then
				if (element.type == "Method") then
					staticValues[element.name] = GenerateAmbigiousFunc(element, nil, nil);
				else
					staticValues[element.name] = element.value;
				end
			end
		end
	end

	local setStaticValue = function(key, value)
		local elements = info.getElements(staticValues);

		for _, element in pairs(elements) do
			if (element.type == "PropertySet") then
				element.value(value);
				return;
			elseif (element.static and element.name == key and not(element.type == "PropertyGet")) then
				staticValues[element.name] = value;
				return;
			end
		end

		loadStaticOverride();
		if (not(staticOverride == nil)) then
			 staticOverride[key] = value;
		else
			error("Could not set static value " + key);
		end
	end

	local getStaticValue = function(key)
		local elements = info.getElements(staticValues);

		for _, element in pairs(elements) do
			if (element.name == key and element.type == "PropertyGet") then
				return element.value();
			elseif (element.static and element.name == key and not(element.type == "PropertySet")) then
				return staticValues[element.name];
			end
		end

		loadStaticOverride();

		return not(staticOverride == nil) and staticOverride[key] or nil;
	end

	local initializeClass = function(generics, overridingClass)
		local class = {};
		local _, inheritiedClass, populateOverride;
		if info.inherits then
			_, inheritiedClass, populateOverride = CsLuaMeta.GetByFullName(info.inherits)(generics);
			if _ and not(inheritiedClass) then -- Adjust for objects returning only one variable. E.g. CsLuaList;
			   inheritiedClass = _;
			end
		end

		local indexerValues, dictionaryValues;
		if info.isIndexer then indexerValues = {}; end
		if info.isDictionary then dictionaryValues = {}; end
		local nonStaticValues = {};

		local elements = info.getElements(overridingClass or class, generics or {});
		
		local methods, nonStaticVariables, staticVariables, staticGetters, nonStaticGetters, staticSetters, nonStaticSetters = {}, {}, {}, {}, {}, {}, {};
		local staticMethods = {};
		local constructor, serialize, deserialize;
		local overrides = {};
		local interfaces = {};

		local appliedGenerics = {};
		if info.generics then
			for i, appliedGenericVar in ipairs(generics) do
				appliedGenerics[info.generics[i]] = appliedGenericVar;
			end
		end

		
		for _, element in pairs(elements) do
			if (element.type == "Interface") then
				table.insert(interfaces, element.value);
			elseif (element.type == "Method") then
				local func;

				if (element.generics) then
					func = CsLuaMeta.GenericsMethod(function(generics,...)
						local methodGenerics = {};
						for i,v in ipairs(generics) do
							methodGenerics[element.generics[i]] = { name = v.name };
						end
						local innerFunc = GenerateAmbigiousFunc(element, inheritiedClass, appliedGenerics, methodGenerics);
						return innerFunc(...);
					end);
				else
					func, _ = GenerateAmbigiousFunc(element, inheritiedClass, appliedGenerics);
				end

				methods[element.name] = func;

				if (element.static == true) then
					staticMethods[element.name] = func;
				end

				if (element.override == true) and inheritiedClass then
					overrides[element.name] = func;
				end
			elseif (element.type == "Field") then
				if (element.static) then
					staticVariables[element.name] = element;
				else
					nonStaticVariables[element.name] = element;
				end
			elseif (element.type == "PropertyGet") then
				if (element.static) then
					staticGetters[element.name] = element;
				else
					nonStaticGetters[element.name] = element;
				end
			elseif (element.type == "PropertySet") then
				if (element.static) then
					staticSetters[element.name] = element;
				else
					nonStaticSetters[element.name] = element;
				end
			elseif (element.type == "PropertyAuto") then
				if (element.static) then
					staticVariables[element.name] = element;
				else
					nonStaticVariables[element.name] = element;
				end
			elseif (element.type == "Serialization") then
				if (element.name == "serialize") then
					serialize = element;
				elseif (element.name == "deserialize") then
					deserialize = element;
				else
					error("Unknown Serialization element: "..tostring(element.name));
				end
			elseif (element.type == "Constructor") then
				constructor = function(...)
					local matchingFunc = CsLuaMeta.GetMatchingFunction(element.value(), appliedGenerics, ...);
					if matchingFunc then
						return matchingFunc(...);
					else
						error("No constructor matching the signature: '"..CsLuaMeta.SignatureToString(CsLuaMeta.GetSignatures(...)).."'");
					end
				end
			else
				error("Unhandled element type: "..tostring(element.type));
			end
		end

		if not(staticValues) then
			staticValues = {}
			for i, v in pairs(staticVariables) do
				staticValues[i] = v.value;
			end

			for i, v in pairs(staticMethods) do
				staticValues[i] = v;
			end
		end
		
		local meta = {};
		meta.__Initialize = function(t) for i, v in pairs(t) do class[i] = v; end  return class; end
		meta.__type = info.name;
		if (serialize) then
			meta.__Serialize = function(f)
				local t = {};
				if (inheritiedClass and inheritiedClass.__Serialize) then
					t = inheritiedClass.__Serialize(f);
				end
				serialize.value(f,t);
				return t;
			end
		end
		if (deserialize) then
			meta.__Deserialize = function(f, t)
				if (inheritiedClass and inheritiedClass.__Deserialize) then
					t = inheritiedClass.__Deserialize(f);
				end
				deserialize.value(f, t);
			end
		end
		meta.__fullTypeName = info.fullName;
		meta.__TableString = function()
			return tostring(class);
		end
		meta.__IsType = function(t) 
			local sig = meta.__GetSignature();
			for _, s in ipairs(sig) do
				if type(s) == "string" and s == t then
					return true;
				elseif type(s) == "table" and s[1] == t then
					return true;
				end
			end
			return false;
		end;
		meta.__GetSignature = function()
			local signature = {{"object"}};
			if (inheritiedClass) then
				signature = inheritiedClass.__GetSignature();
			end

			for _, interface in pairs(interfaces) do
				interface.__AddImplementedSignatures(signature);
			end

			table.insert(signature, 1, {info.fullName, generics});
			return signature;
		end
		meta.__GetOverrides = function()
			return overrides;
		end

		
		for i,v in pairs(nonStaticVariables) do
			nonStaticValues[i] = v.value;
		end
		for i,v in pairs(nonStaticGetters) do
			if not(type(v.value) == "function") then
				nonStaticValues[i] = v.value;
			end
		end

		meta.__Cstor = function(...)
			if constructor then
				constructor(...);
			elseif inheritiedClass then
				inheritiedClass.__Cstor(...);
			end

			return class;
		end

		setmetatable(class, {
			__index = function(_, key)
				if type(key) == "number" and info.isIndexer then
					return indexerValues[key];
				elseif meta[key] then
					return meta[key];
				elseif key == "__base" then
					return inheritiedClass;
				elseif methods[key] then 
					return methods[key];
				elseif nonStaticVariables[key] then 
					return nonStaticValues[key];
				elseif staticVariables[key] then 
					return staticValues[key];
				elseif staticGetters[key] then 
					if type(staticGetters[key].value) == "function" then
						return staticGetters[key].value();
					end
					return staticValues[key];
				elseif nonStaticGetters[key] then 
					if type(nonStaticGetters[key].value) == "function" then
						return nonStaticGetters[key].value();
					end
					return nonStaticValues[key];
				elseif not(inheritiedClass) and (nonStaticSetters[key] or staticSetters[key]) then
					error("Cannot get value for setter that is defined without a getter");
				elseif info.isDictionary then
					return dictionaryValues[key];
				elseif inheritiedClass then
					return inheritiedClass[key];
				end
			end,
			__newindex = function(_, key, value)
				if type(key) == "number" and info.isIndexer then
					indexerValues[key] = value;
				elseif meta[key] then
					error("Cannot assign value to "..tostring(key));
				elseif methods[key] then 
					error("Cannot assign value to method field "..tostring(key));
				elseif nonStaticVariables[key] then 
					nonStaticValues[key] = value;
				elseif staticVariables[key] then 
					staticValues[key] = value;
				elseif staticSetters[key] then 
					if type(staticSetters[key].value) == "function" then
						staticSetters[key].value(value);
					else
						staticValues[key] = value;
					end
				elseif nonStaticSetters[key] then 
					if type(nonStaticGetters[key].value) == "function" then
						nonStaticGetters[key].value(value);
					else
						nonStaticValues[key] = value;
					end
				elseif not(inheritiedClass) and (nonStaticGetters[key] or staticGetters[key]) then
					error("Cannot set value for getter that is defined without a setter");
				elseif info.isDictionary then
					dictionaryValues[key] = value;
				elseif inheritiedClass then
					inheritiedClass[key] = value;
				end
			end,
		});      

		return class, populateOverride;
	end

	local ClassWithOverride = function(generics)
		local overriddenClass = {};
		local class, populateParentOverrides = initializeClass(generics, overriddenClass);

		local overrides;

		local populateOverrides = function()
			if populateParentOverrides then
				overrides = populateParentOverrides();
			else
				overrides = {};
			end

			local ownOverrides = class.__GetOverrides();
			for i,v in pairs(ownOverrides) do
				overrides[i] = v;
			end

			return overrides;
		end

		setmetatable(overriddenClass, {
			__index = function(_, key)
				if overrides == null then
					populateOverrides();
				end
				if overrides[key] then
					return overrides[key];
				end
				return class[key];
			end,
			__newindex = function(_, key, value)
				class[key] = value;
			end,
		});

		return overriddenClass, class, populateOverrides;
	end

	if (info.isStatic) then
		local staticClass = nil;
		local Init = function()
			if staticClass == nil then
				staticClass = initializeClass().__Cstor();
			end
		end

		setmetatable(namespaceElement, {
			__index = function(_, key)
				Init();
				return getStaticValue(key);
			end,
			__newindex = function(_, key, value)
				Init();
				setStaticValue(key, value);
			end,
		});
	else
		setmetatable(namespaceElement, {
			__call = function(_, ...)
				return ClassWithOverride(...);
			end,
			__index = function(_, key)
				initializeStaticElements();
				return getStaticValue(key);
			end,
			__newindex = function(_, key, value)
				initializeStaticElements();
				setStaticValue(key, value);
			end,
		});
	end

	return namespaceElement;
end

System = System or {};
System.Action = function() 
	return {
		__Cstor = function(func) return func; end,
	};
end
System.Enum = {
	Parse = CsLuaMeta.EnumParse,
}
System.NotImplementedException = function(...) return CsLua.NotImplementedException(...) end;
System.Exception = function(...) return CsLua.CsException(...) end;


System.Array = function(generic)
	local class = {};

	local signature;

	local initializeSignature = function()
		if not(generic) then
			if type(class[0]) == "number" then
				local typeName = (math.floor(class[0]) == class[0]) and "int" or "double";
				generic = CsLuaMeta.GenericsList(CsLuaMeta.Generic(typeName))
			elseif class[0] then
				local sig = CsLuaMeta.GetSignatures(class[0])[1]; -- Choose the highest level
				generic = CsLuaMeta.GenericsList(CsLuaMeta.Generic(sig[1][1], sig[1][2]))
			else
				CsLuaMeta.Throw(CsLua.CsException().__Cstor("Could not get signature of implicit array because it is empty."));
			end
		end

		signature = {{"object"}, {"System.Array", generic} };
	end

	local cstor = function(arg)
		
		if type(arg) == "number" then
			class.Length = arg;
			return;
		end

		local zeroBasedArray = arg;
		class.Length = #(zeroBasedArray) > 0 and #(zeroBasedArray) + 1 or (zeroBasedArray[0] and 1 or 0);
		for i=0,class.Length-1 do
			class[i] = zeroBasedArray[i];
		end
		initializeSignature();
	end;

	class.GetEnumerator = function()
		return function(_, prevKey) 
			local key;
			if prevKey == nil then
				key = 0;
			else
				key = prevKey + 1;
			end

			if key < class.Length then
				return key, class[key];
			end
			return nil, nil;
		end;
	end

	local serialize = function(f)
		local t = {};

		for i=0, class.Length - 1 do
			t[i] = class[i];
		end

		t.__type = 'System.Array';
		t.__generic = generic;
		t.__size = class.Length;
		return t;
	end

	local deserialize = function(f, t)
		for i=0,class.Length-1 do
			class[i] = t[i];
		end
		initializeSignature();
	end

	CsLua.CreateSimpleClass(class, class, "Array", "System.Array", cstor, nil, serialize, deserialize);

	class.__GetSignature = function()
		return signature;
	end

	

	return class;
end

System.Collections = {
	Generic = {
		IDictionary = function()
			return {
				isInterface = true,
				name = "IDictionary",
				__AddImplementedSignatures = function() end,
			}
		end
	}
}


CsLuaAttributes = { 
	ICsLuaAddOn = function() return null; end
}
_M = { };
_M.MetaTypes = {
    TypeObject = "TypeObject",
    ClassObject = "ClassObject",
    NameSpace = "NameSpace",
    InteractionElement = "InteractionElement",
    StaticValues = "StaticValues",
    NameSpaceElement = "NameSpaceElement",
};
_M.__metaType = _M.MetaTypes.NameSpace

local function SetAddMeta(f)
    local mt = { __add = function(self, b) return f(self[1], b) end }
    return setmetatable({}, { __add = function(a, _) return setmetatable({ a }, mt) end })
end


_M.Add = SetAddMeta(function(a, b)
    assert(a, "Add called on a nil value (left).");
    assert(b, "Add called on a nil value (right).");
    if type(a) == "number" and type(b) == "number" then return a + b; end
    return tostring(a)..tostring(b);
end);
 
--============= Argument Matching =============

local ScoreArguments = function(expectedTypes, argTypes, args)
    local sum = 0;
    local str = "";

    for i,argType in ipairs(argTypes) do
        local expectedType = expectedTypes[i];
        if (expectedType == nil) then
            return nil, ", - Skipping candidate. Did not have enough expected types. Expected "..#(expectedTypes).." had: "..#(argTypes);
        end
        
        str = str .. "," .. expectedType.FullName;

        local score = argType.GetMatchScore(expectedType, args[i]);
        if (score == nil) then
            return nil, str .. ", - Skipping candidate. Arg did not match "..argType.FullName;
        end
        sum = sum + score;
    end
    
    return sum, str;
end

local SelectMatchingByTypes = function(list, args, methodGenerics)
    assert(type(list) == "table", "Expected a table as 1th argument to _M.AM, got "..type(list))
    assert(type(args) == "table", "Expected a table as 2th argument to _M.AM, got "..type(args))
    local argTypes = {};
    local argTypeStr = "";

    for i,arg in ipairs(args) do
        argTypes[i] = (arg%_M.DOT).GetType();
        argTypeStr = argTypeStr .. "," .. argTypes[i].FullName;
    end
    
    local bestMatch, bestScore;
    local candidatesStr = "Candidates:";

    for _, element in ipairs(list) do
        local types = element.types;

        if methodGenerics then
            types = {};

            for _, t in ipairs(element.types) do
                if type(t) == "string" then
                    table.insert(types, methodGenerics[element.generics[t]]);
                else
                    table.insert(types, t);
                end
            end
        end

        local score, scoreStr = ScoreArguments(types, argTypes, args);
        candidatesStr = candidatesStr .. "\n  " .. scoreStr;

        if not(score == nil) and (not(bestScore) or score > bestScore) then
            bestMatch = element;
            bestScore = score;
        end
    end
    
    if not(bestMatch) then
        error(string.format("No match found.\nArgs: %s.\n%s", argTypeStr, candidatesStr));
    end
    
    return bestMatch;
end

_M.AM = SelectMatchingByTypes;
local defaultValues;

local initializeDefaultValues = function()
    if defaultValues then 
        return 
    end

    defaultValues = {
        [System.Int32.__typeof] = 0,
        [System.String.__typeof] = "",
        [System.Boolean.__typeof] = false,
    };
end


local GetDefaultValue = function(type)
    initializeDefaultValues();
    return defaultValues[type];
end
_M.DV = GetDefaultValue;


local DotMeta = function(fIndex, fNewIndex, fCall) 
    return setmetatable({}, {
        __mod = function(obj, _) 
            return setmetatable({}, {
                __index = function(_, index)
                    return fIndex(obj,index);
                end,
                __newindex = function(_, index, value)
                    return fNewIndex(obj, index, value);
                end,
                __call = function(_,...)
                    return fCall(obj, ...);
                end
            })
        end
    });
end

local GetType = function(obj, index)
    if type(obj) == "table" and obj.type then
        return obj.type;
    elseif type(obj) == "string" then
        return System.String.__typeof;
    elseif type(obj) == "function" then
        return System.Action.__typeof;
    elseif type(obj) == "boolean" then
        return System.Boolean.__typeof;
    elseif type(obj) == "number" then
        if obj == math.floor(obj) then
            return System.Int.__typeof;
        end
        return System.Double.__typeof;
    else
        error("Could not get type of object "..type(obj)..". Attempting to address index "..tostring(index));
    end
end

_M.DOT_LVL = function(level)
    return DotMeta(
        function(obj, index)  -- useage:  a%_M.dot%b
            assert(not(obj == nil), "Attempted to read index "..tostring(index).." on a nil value.");
            --assert(not(type(obj) == "table") or not(obj.__metaType == nil), "Attempted to read index "..tostring(index).." on a obj value with no meta type");

            if (type(obj) == "table" and (obj.__metaType ~= _M.MetaTypes.ClassObject and obj.__metaType ~= _M.MetaTypes.StaticValues)) then
                return obj[index];
            end

            if (type(obj) == "table" and (obj.__metaType == _M.MetaTypes.NameSpaceElement)) then
                return obj.__index(obj, index, level); 
            end

            local typeObject = GetType(obj, index);
            if (index == "GetType") then
                return function() return typeObject; end
            end

            return typeObject.interactionElement.__index(obj, index, level); 
        end, 
        function(obj, index, value)
            assert(not(obj == nil), "Attempted to write index "..tostring(index).." to a nil value.");
            assert(not(type(obj) == "table") or not(obj.__metaType == nil), "Attempted to write index "..tostring(index).." on a obj value with no meta type");

            if (type(obj) == "table" and (obj.__metaType == _M.MetaTypes.NameSpaceElement)) then
                return obj.__newindex(obj, index, value, level);
            end

            local typeObject = GetType(obj, index);
            return typeObject.interactionElement.__newindex(obj, index, value, level); 
        end,
        function(obj, ...)
            assert(type(obj) == "function" or type(obj) == "table", "Attempted to invoke a "..type(obj).." value.");
            return obj(...);
        end
    );
end
_M.DOT = _M.DOT_LVL(nil);


local Enum = function(table, name, namespace)

    for _,v in pairs(table) do
        table.__default = v;
        break;
    end

    table.__typeof = System.Type(name, namespace, System.Object.__typeof, 0, nil, nil, table, 'Enum');

    return table;
end

_M.EN = Enum;


local GenericMethod = function(members, elementOrStaticValues)
    
    local t = {};

    setmetatable(t,{
        __index = function(_, generics)
            return function(...)
                local member = _M.AM(members, {...}, generics);
                return member.func(elementOrStaticValues,...);
            end
        end,
        __call = function(_, ...)
            local member = _M.AM(members, {...});
            return member.func(elementOrStaticValues,...);
        end,
    });

    return t;
end

_M.GM = GenericMethod;

--============= Interaction Element =============

local expectOneMember = function(members, key)
    assert(type(members) == "table", "A table of one member was expected for key '"..tostring(key).."'. Got no table.");
    assert(#(members) == 1, "A table of one member was expected for key '"..tostring(key).."'. Got "..#(members).." members");
end

local clone;
clone = function(t,dept)
    local t2 = {};
    for i,v in pairs(t) do
        if dept > 1 and type(v) == "table" then
            t2[i] = clone(v, dept-1);
        else
            t2[i] = v;
        end
    end
    return t2;
end

local InteractionElement = function(metaProvider, generics)
    local element = {};
    local staticValues = {__metaType = _M.MetaTypes.StaticValues};
    local extendedMethods = {};

    local catagory, typeObject, members, constructors, elementGenerator, implements, initialize = metaProvider(element, generics, staticValues);
    staticValues.type = typeObject;

    local where = function(list, evaluator, otherList)
        local t = {};
        for _, value in ipairs(list) do
            if evaluator(value) then
                table.insert(t, value);
            end
        end
        for _, value in ipairs(otherList) do
            if evaluator(value) then
                table.insert(t, value);
            end
        end
        return t;
    end

    local getMembers = function(key, level, staticOnly)
        return where(members[key] or {}, function(member)
            assert(member.memberType, "Member without member type in "..typeObject.FullName..". Key: "..key.." Level: "..tostring(member.level));
            local static = member.static;
            local public = member.scope == "Public";
            local protected = member.scope == "Protected";
            local memberLevel = member.level;
            local levelProvided = not(level == nil);
            local typeLevel = typeObject.level;

            return (not(staticOnly) or static) and
            (
                (levelProvided and memberLevel <= level) or
                (not(levelProvided) and (public or protected) and memberLevel <= typeLevel) or
                (not(levelProvided) and not(public or protected) and memberLevel == typeLevel)
            );

            --return (not(staticOnly) or member.static == true) and (member.scope == "Public" or (member.scope == "Protected" and level) or member.level == level or (not(level) and member.level == typeObject.level)) and (not(level) or level >= member.level);
        end, extendedMethods[key] or {});
    end

    local matchesAll = function(t1, t2)
        if not(#(t1) == #(t2)) then
            return false;
        end

        for i,_ in ipairs(t1) do
            if not(t1[i] == t2[i]) then
                return false;
            end
        end

        return true;
    end

    local orderByLevel = function(members)
        local t = {};

        for _,member in ipairs(members) do
            local inserted = false;

            for i,v in ipairs(t) do
                if member.level > v.level then
                    table.insert(t, i, member);
                    inserted = true;
                    break;
                end
            end

            if (inserted == false) then
                table.insert(t, member);
            end
        end

        return t;
    end

    local filterOverrides = function(fittingMembers, level)
        if #(fittingMembers) == 1 then
            return fittingMembers;
        end
        
        fittingMembers = orderByLevel(fittingMembers);

        local skippedMembers = {};
        local acceptedMembers = {};

        for i,member in ipairs(fittingMembers) do
            if not(acceptedMembers[i]) and not(skippedMembers[i]) then
                acceptedMembers[i] = true;
                if member.override then
                    for j,otherMember in ipairs(fittingMembers) do
                        if not(j == i) and matchesAll(member.types, otherMember.types) then
                            if member.level > otherMember.level then
                                acceptedMembers[i] = true;
                                skippedMembers[j] = true;
                                acceptedMembers[j] = false;
                            else
                                acceptedMembers[j] = true;
                                skippedMembers[i] = true;
                                acceptedMembers[i] = false;
                            end
                        end
                    end
                end
            end
        end

        local result = {};
        for i,v in pairs(acceptedMembers) do
            if v then
                table.insert(result, fittingMembers[i]);
            end
        end

        return result;
    end

    local index = function(self, key, level)
        if (key == "__metaType") then return _M.MetaTypes.InteractionElement; end

        if (key == "__Initialize") and initialize then
            return function(values) initialize(self, values); return self; end
        end

        local fittingMembers = filterOverrides(getMembers(key, level, false), level or typeObject.level);

        if #(fittingMembers) == 0 then
            fittingMembers = getMembers("#", level, false); -- Look up indexers
        end

        if #(fittingMembers) == 0 then
            error("Could not find member. Key: "..tostring(key)..". Object: "..typeObject.FullName.." Level: "..tostring(level));
        end

        for i=1,#(fittingMembers) do
            assert(type(fittingMembers[i].memberType) == "string", "Missing member type on member. Object: "..typeObject.FullName..". Key: "..tostring(key).." Level: "..tostring(level).." Member #: "..tostring(i))
        end

        if fittingMembers[1].memberType == "Field" or fittingMembers[1].memberType == "AutoProperty" then
            expectOneMember(fittingMembers, key);

            if fittingMembers[1].static then
                return staticValues[fittingMembers[1].level][key];
            end

            return self[fittingMembers[1].level][key];
        end

        if fittingMembers[1].memberType == "Indexer" then
            expectOneMember(fittingMembers, "#");
            return self[fittingMembers[1].level][key];
        end

        if fittingMembers[1].memberType == "Method" then
            return _M.GM(fittingMembers, self);
        end

        if fittingMembers[1].memberType == "Property" then
            return fittingMembers[1].get(self);
        end

        error("Could not handle member (get). Object: "..typeObject.FullName.." Type: "..tostring(fittingMembers[1].memberType)..". Key: "..tostring(key));
    end;

    local newIndex = function(self, key, value, level)
        local fittingMembers = getMembers(key, level, false);

        if #(fittingMembers) == 0 then
            fittingMembers = getMembers("#", level, false); -- Look up indexers
        end

        if #(fittingMembers) == 0 then
            error("Could not find member (set). Key: "..tostring(key)..". Object: "..typeObject.FullName.." Level: "..tostring(level));
        end

        if fittingMembers[1].memberType == "Field" or fittingMembers[1].memberType == "AutoProperty" then
            expectOneMember(fittingMembers, key);

            if (fittingMembers[1].static) then
                staticValues[fittingMembers[1].level][key] = value;
                return;
            end

            self[fittingMembers[1].level][key] = value;
            return
        end

        if fittingMembers[1].memberType == "Indexer" then
            expectOneMember(fittingMembers, "#");
            self[fittingMembers[1].level][key] = value;
            return
        end

        if fittingMembers[1].memberType == "Property" then
            expectOneMember(fittingMembers, key);
            fittingMembers[1].set(self, value);
            return 
        end

        error("Could not handle member (set). Object: "..typeObject.FullName.." Type: "..tostring(fittingMembers[1].memberType)..". Key: "..tostring(key)..". Num members: "..#(fittingMembers));
    end

    local meta = {
        __typeof = typeObject,
        __is = function(value) return typeObject.IsInstanceOfType(value); end,
        __meta = function(inheritingStaticValues) 
            for i = 1, typeObject.level do
                inheritingStaticValues[i] = staticValues[i];
            end

            return typeObject, clone(members, 2), clone(constructors or {},2), elementGenerator, clone(implements or {},1), initialize; 
        end,
        __index = index,
        __newindex = newIndex,
        __metaType = _M.MetaTypes.InteractionElement,
        __extend = function(extensions) 
            for _,v in ipairs(extensions) do
                if not(extendedMethods[v.name]) then
                    extendedMethods[v.name] = {};
                end
                v.level = typeObject.level;
                table.insert(extendedMethods[v.name], v);
            end
        end,
    };

    
    setmetatable(element, { 
        __index = function(_, key)
            if not(meta[key] == nil) then 
                return meta[key];
            end

            if (key == "type") then
                return nil;
            end

            if not(catagory == "Class") then
                if (key == "GetType") then
                    return nil;
                end

                error(string.format("Could not find key on a non class element. Category: %s. Key: %s.", tostring(catagory), tostring(key)));
            end

            local fittingMembers = getMembers(key, nil, true);

            if #(fittingMembers) == 0 then
                error("Could not find static member. Key: "..tostring(key)..". Object: "..typeObject.FullName);
            end

            if (fittingMembers[1].memberType == "Method") then
                return _M.GM(fittingMembers, staticValues);
            end

            expectOneMember(fittingMembers, key);
            local member = fittingMembers[1];

            if member.memberType == "Property" then
                return member.get(staticValues);
            end

            if member.memberType == "AutoProperty" then
                return staticValues[member.level][key];
            end

            assert(member.memberType == "Field", "Expected field member for key "..tostring(key)..". Got "..tostring(member.memberType)..". Object: "..typeObject.FullName..".");
            return staticValues[member.level][key];
        end,
        __newindex = function(_, key, value)
            if not(catagory == "Class") then
                error(string.format("Could not set key on a non class element. Category: %s. Key: %s.", tostring(catagory), tostring(key)));
            end

            local fittingMembers = getMembers(key, nil, true);
            expectOneMember(fittingMembers, key);
            local member = fittingMembers[1];

            if member.memberType == "Property" then
                member.set(staticValues, value);
                return 
            end

            if member.memberType == "AutoProperty" then
                staticValues[member.level][key] = value;
                return;
            end

            assert(member.memberType == "Field", "Expected field member for key "..tostring(key)..". Got "..tostring(member.memberType)..". Object: "..typeObject.FullName..".");
            staticValues[member.level][key] = value;
        end,
        __call = function(_, ...)
            assert(type(constructors)=="table" and #(constructors) > 0, "Class did not provide any constructors. Type: "..typeObject.FullName);
            -- Generate the base class element from constructor.GenerateBaseClass
            local classElement = elementGenerator();
            -- find the constructor fitting the arguments.
            local constructor = _M.AM(constructors, {...});
            -- Call the constructor
            constructor.func(classElement, ...);

            return classElement;
        end,
    });

    return element;
end

_M.IE = InteractionElement;

local InsertMember = function(members, key, member)
    if not(members[key]) then
        members[key] = {};
    end

    table.insert(members[key], member);
end
_M.IM = InsertMember;

local BaseCstor = function(classElement, baseConstructors, ...)
    local constructor = _M.AM(baseConstructors, {...});
    constructor.func(classElement, ...);
end
_M.BC = BaseCstor;

--============= Namespace Element =============

local getHashOfGenerics = function(generics)
    local i = 1;
    for _,v in ipairs(generics) do
        i = i*v.GetHashCode();
    end
    return i;
end

local isGenericsTable = function(t)
    if not(type(t) == "table") then
        return false;
    end
    
    for _,v in ipairs(t) do
        if not(type(v) == "table") or not(System.Type.__is(v)) then
            return false;
        end
    end
    return true;
end

local NamespaceElement = function(metaProviders)
    local interactionElements = {};
    
    local getInteractionElement = function(generics)
        generics = generics or {};
        local hash = getHashOfGenerics(generics);
        if not(interactionElements[hash]) then
            assert(metaProviders[#(generics)] or metaProviders["#"], string.format("Could not find meta provider fitting number of generics: %s or '#'", #(generics)));
            interactionElements[hash] = _M.IE(metaProviders[#(generics)] or metaProviders["#"], generics);
        end
        
        return interactionElements[hash];
    end

    local element = {};
    setmetatable(element, { 
        __index = function(_, key)
            if key == "__metaType" then
                return _M.MetaTypes.NameSpaceElement;
            end

            if not(isGenericsTable(key)) then
                return getInteractionElement()[key];
            end
            return getInteractionElement(key);
        end,
        __newindex = function(_, key, value)
            getInteractionElement()[key] = value;
        end,
        __call = function(_, ...)
            return getInteractionElement()(...);
        end,
    });

    return element;
end

_M.NE = NamespaceElement;

_M.Throw = function(exception)
    if not(System.Exception.__is(exception)) then
        error("Non exception thrown.");
    end

    _M._CurrentException = exception;
    error((exception % _M.DOT).ToString(), 2);
end

_M.Try = function(try, catch, finally)
    _M._CurrentException = nil;
    local success, err = pcall(try)
    local exception = _M._CurrentException;
    _M._CurrentException = nil;

    if not(success) then
        exception = exception or System.Exception("Lua error:\n" .. (err or "nil"));
        
        local matchFound = false;
        for _, catchCase in ipairs(catch or {}) do
            if catchCase.type == nil or catchCase.type.interactionElement.__is(exception) then
                catchCase.func(exception)
                matchFound = true;
                break;
            end
        end

        if (matchFound == false) then -- rethrow
            if finally then
                finally();
            end

            __CurrentException = exception;
            error(err, 2);
        end
    end

    if finally then
        finally();
    end
end
System = { __metaType = _M.MetaTypes.NameSpace };
local actionTypeObject;
System.Action = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    -- Note: System.Action is throwing away all generics, as it is not possible for lua to distingush between them.
    local typeObject = actionTypeObject or System.Type('Action','System',System.Object.__typeof,0,nil,nil,interactionElement);
    actionTypeObject = typeObject;
    local level = 2;
    local members = {
        
    };
    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
System.Array = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Array','System', baseTypeObject,#(generics),nil,nil,interactionElement);

    local len = function(element)
        return (element[typeObject.level][0] and 1 or 0) + #(element[typeObject.level]);
    end

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        func = function(element)
            return function(_, prevKey) 
                local key;
                if prevKey == nil then
                    key = 0;
                else
                    key = prevKey + 1;
                end

                if key < len(element) then
                    return key, element[typeObject.level][key];
                end
                return nil, nil;
            end;
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(self, values)
        for i,v in pairs(values) do
            self[typeObject.Level][i] = v;
        end
    end;
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator, nil, initialize;
end})
System.Boolean = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Boolean','System',baseTypeObject,0,nil,nil,interactionElement);

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})

System.Collections = System.Collections or { __metaType = _M.MetaTypes.NameSpace };
System.Collections.Generic = System.Collections.Generic or { __metaType = _M.MetaTypes.NameSpace };

System.Collections.Generic.Dictionary = _M.NE({[2] = function(interactionElement, generics, staticValues)
    local implements = {};
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Dictionary','System.Collections.Generic',baseTypeObject,2,generics,implements,interactionElement);
    
    _M.IM(members,'Keys',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        get = function(element)
            return System.Collections.Generic.KeyCollection[generics](element);
        end,
    });

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        func = function(element)
            return pairs(element[2]);
        end,
    });

    _M.IM(members,'#',{
        level = typeObject.Level,
        memberType = 'Indexer',
        scope = 'Public',
        types = {generics[1], generics[2]},
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(element, values)
        for i,v in pairs(values) do
            element[2][i] = v;
        end
    end

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator, implements, initialize;
end})

System.Collections.Generic.KeyCollection = _M.NE({[2] = function(interactionElement, generics, staticValues)
    local implements = {};
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('KeyCollection','System.Collections.Generic',baseTypeObject,1,generics,implements,interactionElement);
    
    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        func = function(element)
            return pairs(element[2]);
        end,
    });

    local constructors = {
        {
            types = {System.Collections.Generic.Dictionary[generics].__typeof},
            func = function(element, dictionary) 
                for key,_ in pairs(dictionary[2]) do
                    table.insert(element[2],key);
                end
            end,
        }
    };

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator;
end})

System.Collections.Generic.List = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {};
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('List','System.Collections.Generic',baseTypeObject,1,generics,implements,interactionElement);

    _M.IM(members,'ForEach',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {System.Action[{generics[1]}].__typeof},
        func = function(element,action)
            for i = 1,#(element[2]) do
                (action%_M.DOT)(element[2][i]);
            end
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(element, values)
        for i=1,#(values) do
            element[2][i] = values[i];
        end
    end

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator, implements, initialize;
end})
System.Double = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Double','System',baseTypeObject,0,nil,nil,interactionElement);

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
System.Enum = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Enum','System',baseTypeObject,0,nil,nil,interactionElement);

    _M.IM(members,'Parse',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Type.__typeof, System.String.__typeof},
        func = function(staticValues, typeObj, str)
            for _,v in pairs(typeObj.interactionElement) do
                if type(v) == "string" and string.lower(str) == string.lower(v) then
                    return v;
                end
            end
            return nil;
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})

System.Exception = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Exception','System',baseTypeObject,0,nil,nil,interactionElement);
    local level = 2;

    _M.IM(members,'Message',{
        level = typeObject.Level,
        memberType = 'AutoProperty',
        scope = 'Public',
    });

    _M.IM(members,'ToString',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        func = function(element)
            return (element % _M.DOT).Message;
        end,
    });

    local constructors = {
        {
            types = {System.String.__typeof},
            func = function(element, msg)
                (element %_M.DOT).Message = msg;
            end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
System.Int = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Double.__meta(staticValues);
    local typeObject = System.Type('Int','System',baseTypeObject,0,nil,nil,interactionElement);

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return 0; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})

System.Int32 = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta({});
    local typeObject = System.Type('Int32','System',baseTypeObject,0,nil,nil,interactionElement);
    local level = 2;
    members[level] = {};

    _M.IM(members,'Parse',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Object.__typeof},
        func = function(_, value)
            return math.floor(tonumber(value));
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return 0; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
System.Int = System.Int32;

local ext = {
};
System.Object = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Object','System',nil,0,nil,nil,interactionElement);
    local members = {
        {
            -- Note: GetType is implemented as a shortcut inside DOT, to avoid additional looks through AM.
        },
    };

    _M.IM(members,'Equals',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        func = function(element, obj)
            return element == obj;
        end,
    });

    _M.IM(members,'ToString',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        func = function(element)
            if type(element) == "table" then
                return ((element %_M.DOT).GetType() %_M).FullName;
            elseif type(element) == "boolean" then
                return element and "True" or "False";
            end

            return tostring(element);
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local elementGenerator = function() 
        return {
            [1] = {},
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, elementGenerator;
end})
System.String = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('String','System',baseTypeObject,0,nil,nil,interactionElement);

    _M.IM(members,'Split',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        func = function(element, delimiter)
            local t = {string.split(delimiter, element)};
            t[0] = t[1];
            table.remove(t,1);
            return (System.Array[{typeObject}]()%_M.DOT).__Initialize(t);
        end,
    });

    _M.IM(members,'IsNullOrEmpty',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {typeObject},
        func = function(_, str)
            return str == nil or str == "";
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return "";
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
local hashString = function(str, hash)
    hash = hash or 7;
    for i=1, string.len(str) do
        hash = math.mod(hash*31 + string.byte(str,i), 1000000);
    end
    return hash;
end

local typeType;
local objectType;

local GetMatchScore = function(self, otherType, otherValue)
    if otherType.GetHashCode() == self.hash then
        return self.level;
    end
    
    if self.implements then
        local bestScore;
        for _,interfaceType in ipairs(self.implements) do
            local score = interfaceType.GetMatchScore(otherType);
            if score then
                bestScore = bestScore and math.max(bestScore, score) or score;
            end
        end
        
        if bestScore then
            return math.min(bestScore, self.Level-1);
        end
    end
    
    if otherType.catagory == "Enum" and self.Equals(System.String.__typeof) then
        if (System.Enum % _M.DOT).Parse(otherType, otherValue) then
            return 0;
        end
    end

    if self.baseType then
        return self.baseType.GetMatchScore(otherType);
    end
    
    return nil;
end

local meta = {
    __index = function(self, index)
        if index == "__metaType" then
            return _M.MetaTypes.TypeObject;
        elseif index == "GetType" then
            return function()
                return typeType;
            end
        elseif index == "GetHashCode" then
            return function()
                return self.hash;
            end
        elseif index == "Equals" then
            return function(otherType)
                return self.hash == otherType.GetHashCode();
            end
        elseif index == "IsInstanceOfType" then
            return function(instance)
                local otherType = instance.type;
                if self.hash == otherType.hash then
                    return true;
                end

                if otherType.baseType and self.IsInstanceOfType({type = otherType.baseType}) then
                    return true;
                end

                for _,imp in ipairs(otherType.implements or {}) do
                    if self.IsInstanceOfType({type = imp}) then
                        return true;
                    end
                end
                return false;
            end
        elseif index == "Name" then
            return self.name;
        elseif index == "Namespace" then
            return self.namespace;
        elseif index == "BaseType" then
            return self.baseType;
        elseif index == "Level" then
            return self.level;
        elseif index == "GetMatchScore" then
            return function(otherType, otherValue) return GetMatchScore(self, otherType, otherValue); end;
        elseif index == "InteractionElement" then
            return self.interactionElement;
        elseif index == "FullName" then
            local generic = "";
            if self.numberOfGenerics > 1 then
                generic = "´" .. self.numberOfGenerics;
            end

            return self.namespace .. "." .. self.name .. generic;
        elseif index == "IsEnum" then
            return self.catagory == "Enum";
        end
    end,
};

local getHash = function(name, namespace, numberOfGenerics, generics)
    local genericsHash = 1;
    if generics then
        for i,v in pairs(generics) do
            genericsHash = genericsHash * v.GetHashCode();
        end
    end

    return hashString(namespace .. "." .. name .. "´".. numberOfGenerics, genericsHash);
end

local typeCache = {};


local typeCall = function(name, namespace, baseType, numberOfGenerics, generics, implements, interactionElement, catagory)
    assert(interactionElement, "Type cannot be created without an interactionElement.");

    catagory = catagory or "Class";
    numberOfGenerics = numberOfGenerics or 0;
    local hash = getHash(name, namespace, numberOfGenerics, generics);
    if typeCache[hash] then 
        error("The type object "..tostring(name).." was already created.");
    end

    local self = {
        catagory = catagory, 
        namespace = namespace,
        name = name, 
        numberOfGenerics = numberOfGenerics,
        hash = hash,
        generics = generics,
        baseType = baseType,
        level = (baseType and baseType.Level or 0) + 1,
        implements = implements,
        interactionElement = interactionElement,
    };
    
    setmetatable(self, meta);
    typeCache[hash] = self;
    return self;
end

--objectType = typeCall("Object", "System"); -- TODO: Initialize in a way that does not require the type cache
typeType = typeCall("Type", "System", nil, 0, nil, nil, {});

local meta = {
    __typeof = typeType,
    __is = function(value) return type(value) == "table" and type(value.GetType) == "function" and value.GetType() == typeType; end,
    __meta = function() return typeType; end,
};

local element = {};
setmetatable(element, { 
    __index = function(_, key)
        if meta[key] then 
            return meta[key];
        end
    end,
    __newindex = function(_, key, value)
    end,
    __call = function(_, ...)
        return typeCall(...);
    end,
});

System.Type = element;

CsLuaFramework = { };
CsLuaFramework.Serializer = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('CsLuaFramework','Serializer',baseTypeObject,0,nil,nil,interactionElement);

    local replaceTypeRefs;
    replaceTypeRefs = function(obj)
        local t = {};
        for i,v in ipairs(obj) do
            t[i] = {};
            for index, value in pairs(v) do
                if type(value) == "table" and value.__metaType == _M.MetaTypes.ClassObject then
                    t[i][index] = replaceTypeRefs(value);
                else
                    t[i][index] = value;
                end
            end
        end

        t.type = obj.type.hash;

        return t;
    end

    _M.IM(members,'Serialize',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Object.__typeof},
        func = function(_, obj)
            return replaceTypeRefs(obj);
        end,
    });


    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})

table.Foreach = foreach;
table.contains = tcontains;

_G.__isNamespace = true;
Lua = {
    __isNamespace = true;
    Core = _G,
    Strings = _G,
    LuaMath = _G,
	Table = table,

	NativeLuaTable = function() return {__Cstor = function() return {}; end}; end,    
};

Lua.Strings.format = function(str,...)
	-- TODO: replace {0} with %1$s
	str =  gsub(str,"{(%d)}", function(n) return "%"..(tonumber(n)+1).."$s" end);
	return string.format(str,...)
end

strsplittotable = function(d, str)
	return {strsplit(d, str)};
end
strjoinfromtable = function(d, t)
	return strjoin(d, unpack(t));
end
function strsubutf8(str, a, b) -- modified from http://wowprogramming.com/snippets/UTF-8_aware_stringsub_7
    assert(type(str) == "string" and type(a) == "number", "incorrect input strsubutf8");
    assert(not (b) or (type(b) == "number" and b <= strlenutf8(str)), "end pos larger than string lenght", b, strlenutf8(str));

    b = (b or strlenutf8(str));


    local start, _end = #str + 1, #str + 1;
    local currentIndex = 1
    local numChars = 0;
    if a <= 1 then
        start = a;
    end
    if b <= 1 then
        _end = b;
    end

    while currentIndex <= #str do
        local char = string.byte(str, currentIndex)
        if char > 240 then
            currentIndex = currentIndex + 4
        elseif char > 225 then
            currentIndex = currentIndex + 3
        elseif char > 192 then
            currentIndex = currentIndex + 2
        else
            currentIndex = currentIndex + 1
        end

        numChars = numChars + 1;

        if numChars == a - 1 then
            start = currentIndex;
        end
        if numChars == b then
            _end = currentIndex - 1;
        end
    end
    return str:sub(start, _end)
end

function IsStringNullOrEmpty(str)
	return str == nil or str == "";
end
