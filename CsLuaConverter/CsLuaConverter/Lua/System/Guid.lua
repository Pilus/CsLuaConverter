System.Guid = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members, baseConstructors = System.Object.__meta(staticValues);
    local typeObject = System.Type('Guid','System',baseTypeObject,0,nil,nil,interactionElement,'Class',1718);

    local toHex = function(value, num)
        local value = string.gsub(string.format("%"..num.."x", value)," ","0");
        return value;
    end

    local randomHexValue = function(size)
        return toHex(math.random(16^size), size);
    end

    _M.IM(members, 'NewGuid', {
        level = typeObject.Level,
        memberType = 'Method',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 0,
        scope = 'Public',
        func = function(element)
            local value = randomHexValue(4).. randomHexValue(4).."-".. randomHexValue(4).."-".. randomHexValue(4).."-".. randomHexValue(4).."-".. randomHexValue(6).. randomHexValue(6);

            return System.Guid._C_0_8736(value);
        end,
    });

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*4368,
        scope = 'Public',
        func = function(element, value)
            element[2].value = value;
        end,
    });

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
