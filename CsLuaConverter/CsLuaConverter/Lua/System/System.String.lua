System.String = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('String','System',System.Object.__typeof,nil,nil,interactionElement);
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
