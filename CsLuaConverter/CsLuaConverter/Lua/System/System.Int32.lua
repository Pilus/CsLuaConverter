
System = System or {};

System.Int32 = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Int32','System',System.Object.__typeof,0,nil,nil,interactionElement);
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
System.Int = System.Int32;
