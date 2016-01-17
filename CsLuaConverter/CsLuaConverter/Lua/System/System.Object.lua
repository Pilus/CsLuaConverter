System.Object = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Object','System',nil,0,nil,nil,interactionElement);
    local members = {
        {
            -- Note: GetType is implemented as a shortcut inside DOT, to avoid additional looks through AM.
        },
    };
    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local elementGenerator = function() 
        return {
            [1] = {},
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, elementGenerator;
end})
