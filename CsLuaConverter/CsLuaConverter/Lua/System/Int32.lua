
System.Int32 = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Double.__meta({});
    local typeObject = System.Type('Int32','System',baseTypeObject,0,nil,nil,interactionElement,'Class', 550);
    members[typeObject.level] = {};

    _M.IM(members,'Parse',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Object.__typeof},
        numMethodGenerics = 0,
        signatureHash = 3016,
        func = function(_, value)
            return math.floor(tonumber(value));
        end,
    });

    _M.IM(members,'Equals',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        numMethodGenerics = 0,
        signatureHash = 1100,
        func = function(element, obj)
            return element == obj;
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
System.Int = System.Int32;
