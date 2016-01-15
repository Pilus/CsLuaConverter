
System.Int32 = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Int32','System',System.Object.__typeof,0,nil,nil,interactionElement);
    local level = 2;
    local members = {
        
    };

    _M.IM(members,'Parse',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Object.__typeof},
        func = function(_, value)
            return math.floor(tonumber(value));
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    return "Class", typeObject, members, constructors, function() return {[1] = {},[2] = {}, ["type"] = typeObject}; end;
end})
System.Int = System.Int32;
