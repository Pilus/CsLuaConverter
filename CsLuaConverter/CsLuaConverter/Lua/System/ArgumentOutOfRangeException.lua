System.ArgumentOutOfRangeException = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members, baseConstructors = System.Exception.__meta(staticValues);
    local typeObject = System.Type('ArgumentOutOfRangeException','System',baseTypeObject,0,nil,nil,interactionElement,'Class',131151);

    local constructors = {
        {
            types = {},
            func = function(element) 
                _M.BC(element, baseConstructors, "Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index");
            end,
        }
    };
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
