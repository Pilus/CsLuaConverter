System.Func = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Func','System',System.Object.__typeof,#(generics),generics,nil,interactionElement,'Class',693);
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
        func = function(element,...)
            return (element[typeObject.level].innerAction % _M.DOT)(...);
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
