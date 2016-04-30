System.Single = _M.NE({[0] = function(interactionElement, generics, staticValues) -- AKA float
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Single','System',baseTypeObject,0,nil,nil,interactionElement);

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