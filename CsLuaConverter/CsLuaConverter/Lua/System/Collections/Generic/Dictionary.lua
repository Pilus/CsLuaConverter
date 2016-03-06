System.Collections.Generic.Dictionary = _M.NE({[2] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.Generic.IDictionary[generics].__typeof,
        System.Collections.IDictionary.__typeof,
        System.Collections.ICollection.__typeof,
        System.Collections.Generic.ICollection[{System.Collections.Generic.KeyValuePair[generics].__typeof}].__typeof,
        System.Collections.IEnumerable.__typeof,
        System.Collections.Generic.IEnumerable[{System.Collections.Generic.KeyValuePair[generics].__typeof}].__typeof,
        System.Collections.Generic.IReadOnlyDictionary[generics].__typeof,
        System.Collections.Generic.IReadOnlyCollection[{System.Collections.Generic.KeyValuePair[generics].__typeof}].__typeof,
    };
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Dictionary','System.Collections.Generic',baseTypeObject,2,generics,implements,interactionElement);
    
    _M.IM(members,'Keys',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        get = function(element)
            return System.Collections.Generic.KeyCollection[generics](element);
        end,
    });

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        func = function(element)
            return pairs(element[2]);
        end,
    });

    _M.IM(members,'Add',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {generics[1], generics[2]},
        func = function(element, key, value)
            element[2][key] = value;
        end,
    });

    _M.IM(members,'#',{
        level = typeObject.Level,
        memberType = 'Indexer',
        scope = 'Public',
        types = {generics[1], generics[2]},
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(element, values)
        for i,v in pairs(values) do
            element[2][i] = v;
        end
    end

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator, implements, initialize;
end})

System.Collections.Generic.KeyCollection = _M.NE({[2] = function(interactionElement, generics, staticValues)
    local implements = {};
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('KeyCollection','System.Collections.Generic',baseTypeObject,1,generics,implements,interactionElement);
    
    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        func = function(element)
            return pairs(element[2]);
        end,
    });

    local constructors = {
        {
            types = {System.Collections.Generic.Dictionary[generics].__typeof},
            func = function(element, dictionary) 
                for key,_ in pairs(dictionary[2]) do
                    table.insert(element[2],key);
                end
            end,
        }
    };

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator;
end})
