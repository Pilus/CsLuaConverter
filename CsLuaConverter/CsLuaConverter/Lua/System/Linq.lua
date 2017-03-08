

local NoElements = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no elements"));
end

local NoMatch = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no matching element"));
end

_M.RE("System.Collections.Generic.IEnumerable", 1, function(generics)
    local genericsMapping = {['TSource'] = 1, ['TFirst'] = 1, ['TOuter'] = 1};

    return {
        { -- System.Collections.Generic.IEnumerable`1<TSource> Union(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Union",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second)
                local firstSource = (first % _M.DOT).GetEnumerator();
                local secondSource = (second % _M.DOT).GetEnumerator();
                local currentSource, returned;
                return System.Linq.Iterator[generics]._C_0_16704(function(_, prevKey)
                    if prevKey == nil then
                        currentSource = firstSource;
                        returned = {};
                    end
                    
                    while (true) do
                        local key, value = currentSource(_, prevKey);
                        if (key == nil) then
                            if currentSource == firstSource then
                                currentSource = secondSource;
                            else
                                return nil, nil;
                            end
                        else
                            if (currentSource == firstSource) then
                                table.insert(returned, value);
                                return key, value;
                            else
                                if not(tContains(returned, value)) then
                                    return key, value;
                                end
                                prevKey = key;
                            end
                        end

                        prevKey = key;
                    end
                end);
            end,
        },
        { -- System.Linq.TSource[] ToArray(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "ToArray",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                local array = System.Array[generics]._C_0_0();
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);
                local c = 0;
                while not(key == nil) do
                    (array %_M.DOT)[c] = value;
                    c = c + 1;
                    key, value = enumerator(nil, key);
                end

                return array;
            end,
        },
        { -- System.Collections.Generic.List`1<TSource> ToList(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "ToList",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                local list = System.Collections.Generic.List[generics]._C_0_0();
                (list % _M.DOT)["AddRange_M_0_"..(2*System.Collections.Generic.IEnumerable[generics].__typeof.signatureHash)](source);
                return list;
            end,
        },
        { -- TSource First(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "First",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);

                if (key == nil) then
                    NoElements();
                end

                return value;
            end,
        },
        { -- TSource First(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "First",
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

                NoMatch();
            end,
        },
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
        { -- TSource Last(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Last",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);
                local lastKey, lastValue = nil, nil;

                while (key) do
                    lastKey = key;
                    lastValue = value;
                    key, value = enumerator(_, key);
                end

                if (lastKey == nil) then
                    NoElements();
                end

                return lastValue;
            end,
        },
        { -- TSource Last(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "Last",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                local enumerator = (source % _M.DOT).GetEnumerator();
                local key, value = enumerator(nil, nil);
                local lastKey, lastValue = nil, nil;

                while (key) do
                    if ((predicate % _M.DOT)(value) == true) then
                        lastKey = key;
                        lastValue = value;
                    end

                    key, value = enumerator(_, key);
                end

                if (lastKey == nil) then
                    NoElements();
                end

                return lastValue;
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
        { -- System.Collections.Generic.IEnumerable`1<TSource> Where(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "Where",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                local enumerator = (source % _M.DOT).GetEnumerator();
                return System.Linq.Iterator[generics]._C_0_16704(function(_, prevKey)
                    while (true) do
                        local key, value = enumerator(_, prevKey);
                        if (key == nil) or (predicate % _M.DOT)(value) == true then
                            return key, value;
                        end
                        prevKey = key;
                    end
                end);
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