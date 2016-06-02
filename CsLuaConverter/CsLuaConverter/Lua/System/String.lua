System.String = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('String','System',baseTypeObject,0,nil,nil,interactionElement,'Class',4368);

    -- String compare
    local compare = function(strA, indexA, strB, indexB, length)
        for (i=1,length) do
            local ca = string.char(strA, indexA+i);
            local cb = string.char(strB, indexB+i);
            
            if ca > cb then
                return 1;
            elseif cb > ba then
                return -1;
            end
        end
        return 0;
    end
    
    _M.IM(members,'Compare',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 21840, -- String, String
        func = function(element, strA, strB)
            return compare(strA, 0, strB, 0, math.max(string.len(strA), string.len(strB));
        end,
    });

    _M.IM(members,'Split',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        numMethodGenerics = 0,
        signatureHash = 8736,
        func = function(element, delimiter)
            local t = {string.split(delimiter, element)};
            t[0] = t[1];
            table.remove(t,1);
            return (System.Array[{typeObject}]()%_M.DOT).__Initialize(t);
        end,
    });

    _M.IM(members,'Contains',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        numMethodGenerics = 0,
        signatureHash = 8736,
        func = function(element, str)
            return not(string.find(element, str) == nil)
        end,
    });

    _M.IM(members,'Length',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        numMethodGenerics = 0,
        get = function(element)
            return string.len(element);
        end,
    });

    _M.IM(members,'IsNullOrEmpty',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        static = true,
        types = {typeObject},
        numMethodGenerics = 0,
        signatureHash = 8736,
        func = function(_, str)
            return str == nil or str == "";
        end,
    });

    _M.IM(members,'Equals',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {typeObject},
        numMethodGenerics = 0,
        signatureHash = 8736,
        func = function(element, obj)
            return element == obj;
        end,
    });
    
    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };
    local objectGenerator = function() 
        return "";
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})
