
System.Exception = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Exception','System',System.Object.__typeof,0,nil,nil,interactionElement);
    local level = 2;
    local members = {
        Message = {
            {
                level = 2,
                memberType = 'AutoProperty',
                scope = 'Public',
            },
        },
    };
    local constructors = {
        {
            types = {System.String.__typeof},
            func = function(element, msg)
                (element %_M.DOT).Message = msg;
            end,
        }
    };
    return "Class", typeObject, members, constructors, function() return {[1] = {},[2] = { msg = "";}, ["type"] = typeObject}; end;
end})
