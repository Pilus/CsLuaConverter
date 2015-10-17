
System = System or {};
System.Collections = System.Collections or {};
System.Collections.Generic = System.Collections.Generic or {};

System.Collections.Generic.List = _M.NE({[0] = function()
    local typeObject = System.Type('List','System.Collections.Generic',System.Object.__typeof,1);
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
