

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
    function(objRef, index)  -- useage:  a%_M.dot%b
        assert(not(objRef == nil), "Attempted to read index "..tostring(index).." on a nil value.");
        local typeObject = GetType(obj);

        local obj,level;
        if type(objRef) == "table" and #(objRef) == 2 then
            level = objRef[2];
            obj = objRef[1]
        else
            level = nil;
            obj = objRef;
        end

        local typeObject = GetType(obj);
        return typeObject.InteractionElement.__index(obj, index, level); 
    end, 
    function(objRef, index, value)
        assert(not(objRef == nil), "Attempted to write index "..tostring(index).." to a nil value.");

        local obj,level;
        if type(objRef) == "table" and #(objRef) == 2 then
            level = objRef[2];
            obj = objRef[1]
        else
            level = nil;
            obj = objRef;
        end

        local typeObject = GetType(obj);
        return typeObject.InteractionElement.__newindex(obj, index, value, level); 
    end
);
