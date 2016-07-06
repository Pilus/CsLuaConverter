
System.Action = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Action','System',baseTypeObject,#(generics),generics,nil,interactionElement,'Class', 4393);
    
    local members = {
        
    };

    _M.IM(members,'Invoke',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = generics,
        numMethodGenerics = 0,
        signatureHash = _M.SH(unpack(generics)),
        func = function(element,...)
            (element[typeObject.level].innerAction % _M.DOT)(...);
        end,
    });

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*Lua.Function.__typeof.signatureHash,
        scope = 'Public',
        func = function(element, innerAction)
        end,
    });

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*typeObject.signatureHash,
        scope = 'Public',
        func = function(element, innerAction)
        end,
    });

    --[[
    local constructors = {
        {
            types = {typeObject},
            func = function(element, innerAction) 
                element[typeObject.level].innerAction = innerAction;
            end,
        },
        {
            types = {Lua.Function.__typeof},
            func = function(element, innerAction) 
                element[typeObject.level].innerAction = innerAction;
            end,
        }
    };--]]
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
