System.Int = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Double.__meta(staticValues);
    local typeObject = System.Type('Int','System',baseTypeObject,0,nil,nil,interactionElement);

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    return "Class", typeObject, members, constructors, function() return {[1] = {},[2] = {}, ["type"] = typeObject}; end;
end})
