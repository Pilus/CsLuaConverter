CsLuaFramework.Environment = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('CsLuaFramework','Environment',baseTypeObject,0,nil,nil,interactionElement);

    _M.IM(members,'IsExecutingAsLua',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        static = true,
        types = {System.Boolean.__typeof},
        get = function(_, obj)
            return true;
        end,
    });

    _M.IM(members,'ExecuteLuaCode',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {System.String.__typeof},
        numMethodGenerics = 0,
        signatureHash = 2678,
        func = function(_, lua)
            local func, err = loadstring(lua);
            return func();
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
