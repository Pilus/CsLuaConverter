

local NoElements = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no elements"));
end

local NoMatch = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no matching element"));
end

_M.RE("System.Collections.Generic.IEnumerable", 1, function(generics)
    local genericsMapping = {['TSource'] = 1, ['TFirst'] = 1, ['TOuter'] = 1};

    return {
        { -- TSource FirstOrDefault(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "FirstOrDefault",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);
                return value;
            end,
        },
        { -- TSource FirstOrDefault(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "FirstOrDefault",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);
                while (key) do
                    if ((predicate % _M.DOT)(value) == true) then
                        return value;
                    end
                    key, value = enumerator(nil, key);
                end
                return nil;
            end,
        },
        { -- TSource LastOrDefault(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "LastOrDefault",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);
                local lastValue = nil;

                while (key) do
                    lastValue = value;
                    key, value = enumerator(_, key);
                end

                return lastValue;
            end,
        },
        { -- TSource LastOrDefault(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "LastOrDefault",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);
                local lastValue = nil;

                while (key) do
                    if ((predicate % _M.DOT)(value) == true) then
                        lastValue = value;
                    end

                    key, value = enumerator(_, key);
                end

                return lastValue;
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> Select(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TResult>)
            name = "Select",
            numMethodGenerics = 1,
            generics = _M.MG({['TResult'] = 1});
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, methodGenericsMapping, methodGenerics, selector)
                local enumerator = (source % _M.DOT).GetEnumerator();
                return System.Linq.Iterator[methodGenerics]._C_0_16704(function(_, prevKey)
                    local key, value = enumerator(_, prevKey);
                    if (key == nil) then
                        return nil;
                    end
                    return key, (selector %_M.DOT)(value);
                end);
            end,
        },
        { -- System.Linq.IOrderedEnumerable`1<TSource> OrderBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>)
            name = "OrderBy",
            numMethodGenerics = 1,
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local ordered;
                return System.Linq.Iterator[generics]._C_0_16704(function(_, prevKey)
                    if prevKey == nil then
                        ordered  = {};
                        local key, value = nil, nil;
                        while (true) do
                            key, value = enumerator(_, key);
                            if (key == nil) then
                                break;
                            else
                                table.insert(ordered, {
                                    sortValue = (keySelector %_M.DOT)(value),
                                    value = value
                                });
                            end
                        end
                        
                        table.sort(ordered, function(a,b) return a.sortValue < b.sortValue; end);
                    end

                    local key = (prevKey or -1) + 1;
                    if (ordered[key + 1] == nil) then
                        return nil, nil;
                    end

                    return key, ordered[key + 1].value;
                end);
            end,
        },
    };
end);


_M.RE("System.Collections.IEnumerable", 0, function()
    return {
        { -- T OfType<T>()
            name = "OfType",
            numMethodGenerics = 1,
            generics = _M.MG({['TResult'] = 1});
            signatureHash = 0,
            func = function(source, methodGenericsMapping, methodGenerics)
                local type = methodGenerics[1];

                local enumerator = (source % _M.DOT).GetEnumerator();
                return System.Linq.Iterator[{type}]._C_0_16704(function(_, prevKey)
                    while (true) do
                        local key, value = enumerator(_, prevKey);
                        if (key == nil) or (type %_M.DOT).IsInstanceOfType(value) == true then
                            return key, value;
                        end
                        prevKey = key;
                    end
                end);
            end,
        },
    }
end);