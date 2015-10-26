System.Action = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    -- Note: System.Action is throwing away all generics, as it is not possible for lua to distingush between them.
    local typeObject = System.Type('Action','System',System.Object.__typeof,0,nil,nil,interactionElement);
    local level = 2;
    local members = {
        
    };
    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    return "Class", typeObject, members, constructors, function() return {[1] = {},[2] = {}, ["type"] = typeObject}; end;
end})
