System.Collections.Generic.KeyValuePair = _M.NE({[2] = function(interactionElement, generics, staticValues)
    local implements = {
    };
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('KeyValuePair','System.Collections.Generic',baseTypeObject,2,generics,implements,interactionElement);
    
    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(element, values)
        for i,v in pairs(values) do
            element[2][i] = v;
        end
    end

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator, implements, initialize;
end})