
CsLuaFramework.Wrapping.Wrapper = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local implements = { CsLuaFramework.Wrapping.IWrapper.__typeof };
    local typeObject = System.Type('Wrapper','CsLuaFramework.Wrapping',baseTypeObject,0,nil,implements,interactionElement, "Class", 6268);

    local methodGenericsMapping = {['T'] = 1};
    local methodGenerics = _M.MG(methodGenericsMapping);

    _M.IM(members,'Wrap',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = false,
        types = {System.String.__typeof},
        generics = methodGenericsMapping,
        numMethodGenerics = 1,
        signatureHash = 8736,
        func = function(element,methodGenericsMapping,methodGenerics,globalVarName)
            return CsLuaFramework.Wrapping.WrappedLuaTable[methodGenerics]._C_0_8686(_G[globalVarName]);
        end,
    });

    _M.IM(members,'Wrap',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = false,
        numMethodGenerics = 1,
        signatureHash = 318953760,
        types = {System.String.__typeof, System.Func[{Lua.NativeLuaTable.__typeof, System.Type.__typeof}].__typeof},
        generics = methodGenericsMapping,
        func = function(element,methodGenericsMapping,methodGenerics, globalVarName, typeTranslator)
            return CsLuaFramework.Wrapping.WrappedLuaTable[methodGenerics]._C_0_73252846(_G[globalVarName], typeTranslator);
        end,
    });

    _M.IM(members,'Wrap',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = false,
        types = {Lua.NativeLuaTable.__typeof},
        generics = methodGenericsMapping,
        numMethodGenerics = 1,
        signatureHash = 55918,
        func = function(element,methodGenericsMapping,methodGenerics,value)
            return CsLuaFramework.Wrapping.WrappedLuaTable[methodGenerics]._C_0_8686(value);
        end,
    });

    _M.IM(members,'Wrap',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = false,
        numMethodGenerics = 1,
        signatureHash = 319000942,
        types = {Lua.NativeLuaTable.__typeof, System.Func[{Lua.NativeLuaTable.__typeof, System.Type.__typeof}].__typeof},
        generics = methodGenericsMapping,
        func = function(element,methodGenericsMapping,methodGenerics,value, typeTranslator)
            return CsLuaFramework.Wrapping.WrappedLuaTable[methodGenerics]._C_0_73252846(value, typeTranslator);
        end,
    });

    _M.IM(members,'Unwrap',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = false,
        numMethodGenerics = 1,
        signatureHash = 2,
        generics = methodGenericsMapping,
        func = function(element,methodGenericsMapping,methodGenerics,value)
            return unwrap(value);
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
