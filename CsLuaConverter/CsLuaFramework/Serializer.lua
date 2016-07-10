CsLuaFramework.Serializer = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Serializer','CsLuaFramework',baseTypeObject,0,nil,nil,interactionElement, "Class", 13977);

    local replaceTypeRefs;
    replaceTypeRefs = function(obj)
        local t = {};
        for i,v in ipairs(obj) do
            for index, value in pairs(v) do
                local strPrefix = type(index) == "string" and i.."_" or i.."#_";

                if type(value) == "table" and value.__metaType == _M.MetaTypes.ClassObject then
                    t[strPrefix..index] = replaceTypeRefs(value);
                else
                    t[strPrefix..index] = value;
                end
            end
        end

        t.type = obj.type.hash;

        return t;
    end

    local replaceHashsWithTypes;
    replaceHashsWithTypes = function(obj)
        if type(obj) == "table" then
            local t = {[1] = {}};
            for i,v in pairs(obj) do
                if i == "type" and type(v) == "number" then
                    t[i] = GetTypeFromHash(v);
                    t.__metaType = _M.MetaTypes.ClassObject;
                else
                    local level, isNum, index = string.match(i,"^(%d*)(#?)_(.*)");
                    level = tonumber(level);
                    index = not(isNum == nil) and tonumber(index) or index;
                    t[level] = t[level] or {};
                    t[level][index] = replaceHashsWithTypes(v);
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
        static = false,
        numMethodGenerics = 1,
        signatureHash = 2,
        types = {System.Object.__typeof},
        func = function(_, obj)
            return replaceTypeRefs(obj);
        end,
    });

    _M.IM(members,'Deserialize',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = false,
        numMethodGenerics = 1,
        signatureHash = 55918,
        types = {System.Object.__typeof},
        func = function(_, obj)
            return replaceHashsWithTypes(obj);
        end,
    });

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 0,
        scope = 'Public',
        func = function(element)
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
