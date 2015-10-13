
--============= Interaction Element =============

local expectOneElement = function(elements, key)
	assert(type(elements) == "table", "A table of one element was expected for "..key..". Got no table.");
	assert(#(elements) == 1, "A table of one element was expected for "..key..". Got "..#(elements).." elements");
end

local InteractionElement = function(metaProvider, generics)
	
	local staticValues = {};

	local typeObject, statics, nonStatics, defaultValuePopulator, constructor, initializer = metaProvider(generics, staticValues);
	
	local meta = {
		__typeof = typeObject,
		__is = function(value) return typeObject.Equals(value); end,
	};

	local element = {};
	setmetatable(element, { 
		__index = function(key)
			if meta[key] then 
				return meta[key];
			end
			local element = statics[key];
			expectOneElement(element);
			assert(element.type == "Variable", "Expected variable element. Got "..tostring(element.type)..".");
			return staticValues[key];
		end,
		__newindex = function(key, value)
			local element = statics[key];
			expectOneElement(element);
			assert(element.type == "Variable", "Expected variable element. Got "..tostring(element.type)..".");
			return staticValues[key];
		end,
		__call = function(...)
			local defaultValueProvider
		end,
	});

	return element;
end

_M = _M or {};
_M.IE = InteractionElement;
