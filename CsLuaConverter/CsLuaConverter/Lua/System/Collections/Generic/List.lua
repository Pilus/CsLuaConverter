
System.Collections.Generic.List = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.IList.__typeof
    };
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('List','System.Collections.Generic',baseTypeObject,1,generics,implements,interactionElement);

    local getCount = function(element)
        return not(element[2][0] == nil) and (#(element[2]) + 1) or 0;
    end

    _M.IM(members,'ForEach',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {System.Action[{generics[1]}].__typeof},
        func = function(element,action)
            for i = 0,getCount(element)-1 do
                (action%_M.DOT)(element[2][i]);
            end
        end,
    });

    _M.IM(members,'Count',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return getCount(element);
        end,
    });

    _M.IM(members,'#',{
        level = typeObject.Level,
        memberType = 'Indexer',
        scope = 'Public',
        types = {generics[1]},
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(element, values)
        for i=1,#(values) do
            element[2][i-1] = values[i];
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
