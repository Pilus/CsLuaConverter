
--============= Interaction Element =============

local expectOneElement = function(elements, key)
	assert(type(elements) == "table", "A table of one element was expected for "..key..". Got no table.");
	assert(#(elements) == 1, "A table of one element was expected for "..key..". Got "..#(elements).." elements");
end

local InteractionElement = function(metaProvider, generics)
	local element = {};
	local staticValues = {};

	local typeObject, statics, nonStatics, constructors, defaultValueProvider = metaProvider(element, generics, staticValues);
	

    local index = function(self, key)
        if statics[key] then
            local element = statics[key][1];
            if element.elementType == "Variable" or element.elementType == "Property" then
                return staticValues[element.level][key];
            end
        elseif nonStatics[key] then
            local firstElement = nonStatics[key][1];
            if firstElement.elementType == "Variable" or firstElement.elementType == "Property" then
                return self[element.level][key];
            end
        end
        error("Unknown key on element "..key);
    end,

	local meta = {
		__typeof = typeObject,
		__is = function(value) return typeObject.Equals(value); end,
		__meta = function() return typeObject, statics, nonStatics, constructors, defaultValueProvider; end,
        
        __newindex = function(key, value) 
        end,
	};

	
	setmetatable(element, { 
		__index = function(key)
            if key == "__index" then
                return index;
            end
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
