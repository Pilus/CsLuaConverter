System.Array = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.IList.__typeof,
        System.Collections.ICollection.__typeof,
        System.Collections.IEnumerable.__typeof,
    };

    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Array','System', baseTypeObject,#(generics),generics,implements,interactionElement);

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(self, values)
    end;
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator, nil, initialize;
end,
[1] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.Generic.IList[generics].__typeof,
        System.Collections.Generic.ICollection[generics].__typeof,
        System.Collections.Generic.IEnumerable[generics].__typeof,
        System.Collections.Generic.IReadOnlyList[generics].__typeof,
        System.Collections.Generic.IReadOnlyCollection[generics].__typeof,
    };
    
    local baseTypeObject, members = System.Array.__meta(staticValues);
    local typeObject = System.Type('Array','System', baseTypeObject,#(generics),generics,implements,interactionElement);

    local len = function(element)
        return (element[typeObject.level][0] and 1 or 0) + #(element[typeObject.level]);
    end

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        numMethodGenerics = 0,
        func = function(element)
            return function(_, prevKey) 
                local key;
                if prevKey == nil then
                    key = 0;
                else
                    key = prevKey + 1;
                end

                if key < len(element) then
                    return key, element[typeObject.level][key];
                end
                return nil, nil;
            end;
        end,
    });

    _M.IM(members,'Length',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return len(element);
        end,
    });

    _M.IM(members,'#',{
        level = typeObject.Level,
        memberType = 'Indexer',
        scope = 'Public',
        types = {generics[1]},
        get = function(element, key)
            assert(type(key) == "number", "Attempted to address array with a non number index: "..tostring(key));
            return element[typeObject.Level][key];
        end,
        set = function(element, key, value)
            element[typeObject.Level][key] = value;
        end
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(self, values)
        for i,v in pairs(values) do
            self[typeObject.Level][i] = v;
        end
    end;
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            [3] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator, implements, initialize;
end})

local areAllOfType = function(objs, type)
    for _,v in pairs(objs) do
        if not(type.IsInstanceOfType(v)) then
            return false;
        end
    end
    return true;
end

local getHighestCommonType = function(objs)
    local type;
    for i,v in pairs(objs) do
        type = (v %_M.DOT).GetType();
        break;
    end

    while (type) do
        if areAllOfType(objs, type) then
            return type;
        end
        type = type.BaseType;
    end

    error("No common type found");
end

ImplicitArray = function(t)
    local type = getHighestCommonType(t);
    return (System.Array[{type}]() % _M.DOT).__Initialize(t);
end
