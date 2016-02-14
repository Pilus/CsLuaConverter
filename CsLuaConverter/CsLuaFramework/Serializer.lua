CsLuaFramework.Serializer = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('CsLuaFramework','Serializer',baseTypeObject,0,nil,nil,interactionElement);

    local replaceTypeRefs;
    replaceTypeRefs = function(obj)
        local t = {};
        for i,v in ipairs(obj) do
            t[i] = {};
            for index, value in pairs(v) do
                if type(value) == "table" and value.__metaType == _M.MetaTypes.ClassObject then
                    t[i][index] = replaceTypeRefs(value);
                else
                    t[i][index] = value;
                end
            end
        end

        t.type = obj.type.hash;

        return t;
    end

    local replaceHashsWithTypes;
    replaceHashsWithTypes = function(obj)
        if type(obj) == "table" then
            local t = {};
            for i,v in pairs(obj) do
                if i == "type" and type(v) == "number" then
                    t[i] = GetTypeFromHash(v);
                    t.__metaType = _M.MetaTypes.ClassObject;
                else
                    t[i] = replaceHashsWithTypes(v);
                end
            end
            return t;
        else
            return obj;
        end
    end

    _M.IM(members,'Serialize',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Object.__typeof},
        func = function(_, obj)
            return replaceTypeRefs(obj);
        end,
    });

    _M.IM(members,'Deserialize',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.Object.__typeof},
        func = function(_, obj)
            return replaceHashsWithTypes(obj);
        end,
    });


    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
