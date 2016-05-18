
System.Linq = {};
System.Linq.Iterator = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.IEnumerable.__typeof,
        System.Collections.Generic.IEnumerable[generics].__typeof,
    };
    
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Iterator','System.Linq', baseTypeObject,#(generics),generics,implements,interactionElement);

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        numMethodGenerics = 0,
        func = function(element)
            return element[typeObject.level]["Enumerator"];
        end,
    });

    _M.IM(members,'ToList',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        numMethodGenerics = 0,
        types = {},
        func = function(element)
            return ((System.Collections.Generic.List % _M.DOT)[generics] % _M.DOT)(element[typeObject.level]["Enumerator"]);
        end,
    });

    local constructors = {
        {
            types = {Lua.Function.__typeof},
            func = function(element, enumerator) element[typeObject.level]["Enumerator"] = enumerator; end,
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
    return "Class", typeObject, members, constructors, objectGenerator, implements, nil;
end});

_M.RE("System.Collections.Generic.IEnumerable", 1, function(generics)
    local methodGenericsMapping = {['T'] = 1};
    local methodGenerics = _M.MG(methodGenericsMapping);

    return {
        {
            name = "Any",
            types = {},
            func = function(element)
                for _,v in (element % _M.DOT).GetEnumerator() do
                    return true;
                end
                return false;
            end
        },
        {
            name = "Any",
            types = {System.Func[{System.Boolean.__typeof}].__typeof},
            func = function(element, predicate)
                for _,v in (element % _M.DOT).GetEnumerator() do
                    if (predicate(v)) then
                        return true;
                    end
                end
                return false;
            end
        },
        {
            name = "Count",
            types = {},
            func = function(element)
                local c = 0;
                for _,v in (element % _M.DOT).GetEnumerator() do
                    c = c + 1;
                end
                return c;
            end
        },
        {
            name = "Where",
            types = {System.Func[{generics[1], System.Boolean.__typeof}].__typeof},
            func = function(element, predicate)
                local enumerator = (element % _M.DOT).GetEnumerator();
                return System.Linq.Iterator[generics](function(_, prevKey)
                    while (true) do
                        local key, value = enumerator(_, prevKey);
                        if (key == nil) or (predicate % _M.DOT)(value) == true then
                            return key, value;
                        end
                        prevKey = key;
                    end
                end);
            end
        },
        {
            name = "Select",
            returnType = methodGenerics[methodGenericsMapping['T']],
            generics = methodGenericsMapping,
            --types = {System.Func[{generics[1], methodGenerics[methodGenericsMapping['T']]}].__typeof},
            types = {System.Func[{generics[1], System.Object.__typeof}].__typeof},
            func = function(element, predicate)
                local enumerator = (element % _M.DOT).GetEnumerator();
                return System.Linq.Iterator[generics](function(_, prevKey)
                    local key, value = enumerator(_, prevKey);
                    if (key == nil) then
                        return nil;
                    end
                    return key, predicate(value);
                end);
            end
        },
    };
end);