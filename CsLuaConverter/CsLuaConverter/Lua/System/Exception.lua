
System.Exception = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Exception','System',baseTypeObject,0,nil,nil,interactionElement,'Class',2530);
    local level = 2;

    _M.IM(members,'Message',{
        level = typeObject.Level,
        memberType = 'AutoProperty',
        scope = 'Public',
    });

    _M.IM(members,'ToString',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        numMethodGenerics = 0,
        signatureHash = 0,
        override = true,
        func = function(element)
            return (element % _M.DOT).Message;
        end,
    });

    local constructors = {
        {
            types = {},
            func = function(element)
                (element %_M.DOT).Message = "";
            end,
        },
        {
            types = {System.String.__typeof},
            func = function(element, msg)
                (element %_M.DOT).Message = msg;
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
