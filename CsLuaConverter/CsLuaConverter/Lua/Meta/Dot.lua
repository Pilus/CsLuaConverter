

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
    if type(obj) == "table" then
        if type(obj.type) == "table" and obj.type.__metaType == _M.MetaTypes.TypeObject then
            return obj.type;
        end
        return Lua.NativeLuaTable.__typeof;
    elseif type(obj) == "string" then
        return System.String.__typeof;
    elseif type(obj) == "function" then
        return Lua.Function.__typeof;
    elseif type(obj) == "boolean" then
        return System.Boolean.__typeof;
    elseif type(obj) == "number" then
        if obj == math.floor(obj) then
            return System.Int.__typeof;
        end
        return System.Double.__typeof;
    else
        error("Could not get type of object "..type(obj)..". Attempting to address index "..tostring(index));
    end
end

_M.DOT_LVL = function(level, explicitLevel)
    return DotMeta(
        function(obj, index)  -- useage:  a%_M.dot%b
            assert(not(obj == nil), "Attempted to read index "..tostring(index).." on a nil value.");
            --assert(not(type(obj) == "table") or not(obj.__metaType == nil), "Attempted to read index "..tostring(index).." on a obj value with no meta type");

            if (type(obj) == "table" and (obj.__metaType ~= _M.MetaTypes.ClassObject and obj.__metaType ~= _M.MetaTypes.StaticValues and obj.__metaType ~= _M.MetaTypes.NameSpaceElement) and not(index == "GetType")) then
                if type(index) == "string" then
                    local newIndex, indexType, numGenerics, hash = string.split("_", index);

                    if (indexType == "M") then
                        index = newIndex;
                    end
                end

                return obj[index];
            end

            if (type(obj) == "table" and (obj.__metaType == _M.MetaTypes.NameSpaceElement)) then
                local indexer = obj.__index;
                if type(indexer) == "function" then
                    return indexer(obj, index, level); 
                end
                return obj[index];
            end

            local typeObject = GetType(obj, index);
            if (index == "GetType") then
                return function() return typeObject; end
            end

            return typeObject.interactionElement.__index(obj, index, level, explicitLevel); 
        end, 
        function(obj, index, value)
            assert(not(obj == nil), "Attempted to write index "..tostring(index).." to a nil value.");
            --assert(not(type(obj) == "table") or not(obj.__metaType == nil), "Attempted to write index "..tostring(index).." on a obj value with no meta type");

            if (type(obj) == "table" and (obj.__metaType == _M.MetaTypes.NameSpaceElement)) then
                return obj.__newindex(obj, index, value, level);
            end

            if (type(obj) == "table" and ((obj.__metaType == _M.MetaTypes.InteractionElement) or obj.__metaType == nil)) then
                obj[index] = value;
                return;
            end

            local typeObject = GetType(obj, index);
            return typeObject.interactionElement.__newindex(obj, index, value, level, explicitLevel); 
        end,
        function(obj, ...)
            if (type(obj) == "table" and (obj.__metaType == _M.MetaTypes.ClassObject)) then
                local typeObject = GetType(obj, "Invoke");
                return typeObject.interactionElement.__index(obj, "Invoke", level)(...); 
            end 

            assert(type(obj) == "function" or type(obj) == "table", "Attempted to invoke a "..type(obj).." value.");
            return obj(...);
        end
    );
end
_M.DOT = _M.DOT_LVL(nil);
