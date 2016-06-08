Lua.Function = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Function','Lua',baseTypeObject,0,nil,nil,interactionElement,"Class", 8352);

    local constructors = {
        {
            types = {},
            func = function() 
                error("Lua.Function can not be constructed");
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
