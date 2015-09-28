System = System or {};

local typeType;
local meta = {
	__index = function(self, index)
		if index == "GetType" then
			return function()
				return typeType;
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

System.Type = function(name, namespace, numberOfGenrics, ...)
	local self = { 
		namespace = namespace,
		name = name, 
		numberOfGenrics = numberOfGenrics,
	};
	
	return class;
end
typeType = System.Type("Type", "System", 1);