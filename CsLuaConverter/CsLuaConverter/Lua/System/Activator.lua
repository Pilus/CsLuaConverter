System.Activator = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members, baseConstructors = System.Object.__meta(staticValues);
    local typeObject = System.Type('Activator','System',baseTypeObject,0,nil,nil,interactionElement,'Class',0);

    _M.IM(members, 'CreateInstance', {
        level = typeObject.Level,
        memberType = 'Method',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*1798,
        scope = 'Public',
        func = function(element, typeObject)
            return typeObject.interactionElement._C_0_0();
        end,
    });

    _M.IM(members, 'CreateInstance', {
        level = typeObject.Level,
        memberType = 'Method',
        static = true,
        numMethodGenerics = 1,
        signatureHash = 0,
        scope = 'Public',
        generics = {['T'] = 1};
        func = function(element, methodGenericsMapping, methodGenerics)
            return methodGenerics[1].interactionElement._C_0_0();
        end,
    });

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            [3] = {},
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        };  
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})