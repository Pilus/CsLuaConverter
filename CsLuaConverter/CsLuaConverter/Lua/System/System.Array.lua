
System = System or {};

System.Array = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Array','System',System.Object.__typeof,#(generics),nil,nil,interactionElement);
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
