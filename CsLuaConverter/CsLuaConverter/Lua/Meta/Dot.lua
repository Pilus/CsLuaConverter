

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
        return System.Bool.__typeof;
    elseif type(obj) == "number" then
        if obj == math.floor(obj) then
            return System.Int.__typeof;
        end
        return System.Double.__typeof;
    else
        error("Could not get type of object "..type(obj)..". Attempting to read index "..tostring(index));
    end
end

_M.DOT_LVL = function(level)
    return DotMeta(
        function(obj, index)  -- useage:  a%_M.dot%b
            assert(not(obj == nil), "Attempted to read index "..tostring(index).." on a nil value.");

            if (type(obj) == "table" and (obj.__isNamespace == true or obj.__isNamespaceElement)) then
                return obj[index];
            end

            local typeObject = GetType(obj, index);
            if (index == "GetType") then
                return function() return typeObject; end
            end

            return typeObject.interactionElement.__index(obj, index, level); 
        end, 
        function(obj, index, value)
            assert(not(obj == nil), "Attempted to write index "..tostring(index).." to a nil value.");

            local typeObject = GetType(obj);
            return typeObject.interactionElement.__newindex(obj, index, value, level); 
        end,
        function(obj, ...)
            assert(type(obj) == "function", "Attempted to invoke a "..type(obj).." value.");
            return obj(...);
        end
    );
end
_M.DOT = _M.DOT_LVL(nil);
