
System = System or {};
System.Object = _M.NE({[0] = function(element)
    local typeObject = System.Type('Object','System');
    local members = {
        
    };
    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    return "Class", typeObject, members, constructors, function() return {[1] = {}, ["type"] = typeObject}; end;
end})
