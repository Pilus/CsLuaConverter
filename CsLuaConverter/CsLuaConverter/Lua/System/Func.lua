System.Func = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Func','System',System.Object.__typeof,#(generics),generics,nil,interactionElement,'Class',1734);
    local level = 2;
    local members = {
        
    };

    local inputGenerics = {};
    for i = 1, #(generics)-1 do
        table.insert(inputGenerics, generics[i]);
    end

    _M.IM(members,'Invoke',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = inputGenerics,
        numMethodGenerics = 0,
        signatureHash = 0, -- TODO: Fix hash
        func = function(element,...)
            return (element[typeObject.level].innerAction % _M.DOT)(...);
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
            element[typeObject.level].innerAction = innerAction;
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
            element[typeObject.level].innerAction = innerAction;
        end,
    });

    --[[
    local constructors = {
        {
            types = {typeObject},
            func = function(element, innerAction) 
                element[typeObject.level].innerAction = innerAction[typeObject.level].innerAction;
            end,
        },
        {
            types = {Lua.Function.__typeof},
            func = function(element, innerAction) 
                element[typeObject.level].innerAction = innerAction;
            end,
        }
    }; --]]

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
