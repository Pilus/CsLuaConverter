
--============= Namespace Element =============

local getHashOfGenerics = function(generics)
	local i = 1;
	for _,v in ipairs(generics) do
		i = i*GetHashCode;
	end
	return i;
end

local isGenericsTable = function(value)
	if not(type(value) == "table") then
		return false;
	end
	
	for _,v in ipairs(value) do
		if not(type(v) == "table") or not(System.Type.__is(value)) then
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
			interactionElements[hash] = _M.IE(metaProviders[#(generics)], generics);
		end
		
		return interactionElements[hash];
	end

	local element = {};
	setmetatable(element, { 
		__index = function(key)
			if not(isGenericsTable(key)) then
				return getInteractionElement()[key];
			end
			return getInteractionElement(key);
		end,
		__newindex = function(key, value)
			getInteractionElement()[key] = value;
		end,
		__call = function(...)
			return getInteractionElement()(...);
		end,
	});

	return element;
end

_M = _M or {};
_M.NE = NamespaceElement;
