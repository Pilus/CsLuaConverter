System = System or {};

local hashString = function(str, hash)
	hash = hash or 7;
	for i=1, string.len(str) do
		hash = hash*31 + string.byte(str,i);
	end
	return hash;
end

local typeType;
local objectType;

local GetMatchScore = function(self, otherType)
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
	
	if self.baseType then
		return self.baseType.GetMatchScore(otherType);
	end
	
	return nil;
end

local meta = {
	__index = function(self, index)
		if index == "GetType" then
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
		elseif index == "Name" then
			return self.name;
		elseif index == "Namespace" then
			return self.namespace;
		elseif index == "BaseType" then
			return self.baseType;
		elseif index == "Level" then
			return self.level;
		elseif index == "GetMatchScore" then
			return function(otherType) return GetMatchScore(self, otherType); end;
        elseif index == "InteractionElement" then
            return self.interactionElement;
		elseif index == "FullName" then
			local generic = "";
			if self.numberOfGenerics > 1 then
				generic = "´" .. self.numberOfGenerics;
			end

			return self.namespace .. "." .. self.name .. generic;
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


local typeCall = function(name, namespace, baseType, numberOfGenerics, generics, implements, interactionElement)
	numberOfGenerics = numberOfGenerics or 0;
	local hash = getHash(name, namespace, numberOfGenerics, generics);
	if typeCache[hash] then
		return typeCache[hash];
	end

	local self = { 
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

objectType = typeCall("Object", "System");
typeType = typeCall("Type", "System", objectType);

System.Type = typeCall;

