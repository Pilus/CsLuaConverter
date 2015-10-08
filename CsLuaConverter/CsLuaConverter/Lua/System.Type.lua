System = System or {};

local hashString = function(str, hash)
	hash = hash or 7;
	for i=1, string.len(str) do
		hash = hash*31 + string.byte(str,i);
	end
	return hash;
end

local typeType;
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
System.Type = function(name, namespace, numberOfGenerics, generics)
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
	};
	
	setmetatable(self, meta);
	typeCache[hash] = self;
	return self;
end
typeType = System.Type("Type", "System", 1);