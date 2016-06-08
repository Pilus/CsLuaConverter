System.Enum = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Enum','System',baseTypeObject,0,nil,nil,interactionElement,'Class',1816);

    _M.IM(members,'Parse',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 16700,
        types = {System.Type.__typeof, System.String.__typeof},
        func = function(staticValues, typeObj, str)
            for _,v in pairs(typeObj.interactionElement) do
                if type(v) == "string" and string.lower(str) == string.lower(v) then
                    return v;
                end
            end
            return nil;
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
