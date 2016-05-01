
System.Action = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Action','System',baseTypeObject,#(generics),generics,nil,interactionElement);
    
    local members = {
        
    };

    _M.IM(members,'Invoke',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = generics,
        func = function(element,...)
            (element[typeObject.level].innerAction % _M.DOT)(...);
        end,
    });

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
