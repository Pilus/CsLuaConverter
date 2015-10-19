
System = System or {};
System.Collections = System.Collections or {};
System.Collections.Generic = System.Collections.Generic or {};

System.Collections.Generic.Dictionary = _M.NE({[2] = function()
    local typeObject = System.Type('Dictionary','System.Collections.Generic',System.Object.__typeof,2);
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
