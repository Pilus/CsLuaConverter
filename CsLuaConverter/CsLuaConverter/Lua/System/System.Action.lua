
System = System or {};

System.Action = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Action','System',System.Object.__typeof,#(generics));
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
