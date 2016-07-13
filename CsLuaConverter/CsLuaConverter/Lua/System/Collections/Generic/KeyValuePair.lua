System.Collections.Generic.KeyValuePair = _M.NE({[2] = function(interactionElement, generics, staticValues)
    local implements = {
    };
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('KeyValuePair','System.Collections.Generic',baseTypeObject,2,generics,implements,interactionElement,'Class', 20165);
    
    _M.IM(members,'Key',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {generics[1]},
        get = function(element)
            return element[typeObject.level].key;
        end,
    });

    _M.IM(members,'Value',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {generics[2]},
        get = function(element)
            return element[typeObject.level].value;
        end,
    });

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*generics[1].signatureHash + 3*generics[2].signatureHash,
        scope = 'Public',
        func = function(element, key, value)
            element[typeObject.level].key = key;
            element[typeObject.level].value = value;
        end,
    });

    --[[
    local constructors = {
        {
            types = {generics[1], generics[2]},
            func = function(element, key, value)
                element[typeObject.level].key = key;
                element[typeObject.level].value = value;
            end,
        }
    }; --]]

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