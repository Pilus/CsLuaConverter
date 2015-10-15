
--============= Interaction Element =============

local expectOneElement = function(elements, key)
	assert(type(elements) == "table", "A table of one element was expected for "..key..". Got no table.");
	assert(#(elements) == 1, "A table of one element was expected for "..key..". Got "..#(elements).." elements");
end

local InteractionElement = function(metaProvider, generics)
	
	local staticValues = {};

	local typeObject, statics, nonStatics, constructors, defaultValueProvider = metaProvider(generics, staticValues);
	
	local meta = {
		__typeof = typeObject,
		__is = function(value) return typeObject.Equals(value); end,
		__meta = function() return typeObject, statics, nonStatics, constructors, defaultValueProvider; end,
	};

	local element = {};
	setmetatable(element, { 
		__index = function(key)
			if meta[key] then 
				return meta[key];
			end
			local element = statics[key];
			expectOneElement(element, key);
			assert(element.type == "Variable", "Expected variable element. Got "..tostring(element.type)..".");
			return staticValues[key];
		end,
		__newindex = function(key, value)
			local element = statics[key];
			expectOneElement(element, key);
			assert(element.type == "Variable", "Expected variable element. Got "..tostring(element.type)..".");
			return staticValues[key];
		end,
		__call = function(...)
			-- find the constructor fitting the arguments.
			local constructor = _M.AM(constructors, {...});
			-- Generate the base class element from constructor.GenerateBaseClass
			local classElement = constructor.GenerateBaseClass();
			-- Override the type
			classElement.type = typeObject;
			-- Get the defaultValues from defaultValueProvider and add them to the base class element at the n+1 index.
			classElement[typeObject.Level] = defaultValueProvider();
			-- Call the constructor with (classElement,...)
			constructor.func(classElement, ...);
			
			return classElement;
		end,
	});

	return element;
end

_M = _M or {};
_M.IE = InteractionElement;
