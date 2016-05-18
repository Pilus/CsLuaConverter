
System.Int64 = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Int32.__meta({});
    local typeObject = System.Type('Int64','System',baseTypeObject,0,nil,nil,interactionElement);
    members[typeObject.level] = {};

    _M.IM(members,'Parse',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Object.__typeof},
        numMethodGenerics = 0,
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
