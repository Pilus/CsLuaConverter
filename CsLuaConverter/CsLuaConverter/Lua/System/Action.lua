local actionTypeObject;
System.Action = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    -- Note: System.Action is throwing away all generics, as it is not possible for lua to distingush between them.
    local typeObject = actionTypeObject or System.Type('Action','System',System.Func.__typeof,0,nil,nil,interactionElement);
    actionTypeObject = typeObject;
    local level = 2;
    local members = {
        
    };
    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            [3] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
