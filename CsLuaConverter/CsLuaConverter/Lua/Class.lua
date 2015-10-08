

BasicClass = function(call, get, set, name, namespace, baseType, numberOfGenerics)
	local namespaceElement = {};

	setmetatable(namespaceElement, {
		__call = function(_, ...)
			return ClassWithOverride(...);
		end,
		__index = function(_, key)
			if key == "typeof" then
				return function(generics)
					return System.Type(name, namespace, baseType, numberOfGenerics, generics);
				end
			end
			if get then 
				return get(key);
			end
		end,
		__newindex = function(_, key, value)
			if set then
				set(key, value);
			end
		end,
	});

	return namespaceElement;
end
