CsLuaFramework.Wrapping.MultipleValues = _M.NE({['#'] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local implements = {
        CsLuaFramework.Wrapping.IMultipleValues[generics].__typeof,
    };
    local typeObject = System.Type('MultipleValues','CsLuaFramework.Wrapping',baseTypeObject,#(generics),generics,implements,interactionElement);
    
    for i=1,#(generics) do
        _M.IM(members,'Value'..i,{
            level = typeObject.Level,
            memberType = 'Property',
            scope = 'Public',
            types = {generics[i]},
            get = function(element)
                return element[typeObject.level]["Value"..i];
            end,
        });
    end
    
    local constructors = {
        {
            types = generics,
            func = function(element, ...)
                for i = 1, #(generics) do
                    element[typeObject.level]["Value"..i] = select(i, ...);
                end
            end,
        },
    };
    
    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    
    return "Class", typeObject, members, constructors, objectGenerator, implements, nil;
end})