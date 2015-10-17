

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

local GetType = function(obj)
    if type(obj) == "table" and obj.type then
        return obj.type;
    elseif type(obj) == "string" then
        return System.String.__typeof;
    else
        error("Could not get type of object "..type(obj));
    end
end

_M = _M or {};
_M.DOT = DotMeta(
    function(obj, index)  -- useage:  a%_M.dot%b
        local typeObject = GetType(obj);
        return typeObject.InteractionElement.__index(obj, index); 
    end, 
    function(obj, index, value)
        local typeObject = GetType(obj);
        return typeObject.InteractionElement.__newindex(obj, index, value); 
    end
);
