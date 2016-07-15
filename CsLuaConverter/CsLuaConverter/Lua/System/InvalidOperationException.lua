System.InvalidOperationException = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members, baseConstructors = System.Exception.__meta(staticValues);
    local typeObject = System.Type('InvalidOperationException','System',baseTypeObject,0,nil,nil,interactionElement,'Class',0); -- TODO: Fix type hash

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 8736,
        scope = 'Public',
        func = function(element, msg)
            (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736(msg);
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
            (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736("Operation is not valid due to the current state of the object.");
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
