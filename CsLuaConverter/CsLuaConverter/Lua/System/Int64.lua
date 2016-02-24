
System.Int64 = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta({});
    local typeObject = System.Type('Int64','System',baseTypeObject,0,nil,nil,interactionElement);
    local level = 2;
    members[level] = {};

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
    local objectGenerator = function() 
        return 0; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
System.Long = System.Int64;
