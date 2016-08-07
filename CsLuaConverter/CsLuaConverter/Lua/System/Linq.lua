
System.Linq = {};
System.Linq.Iterator = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.IEnumerable.__typeof,
        System.Collections.Generic.IEnumerable[generics].__typeof,
    };
    
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('Iterator','System.Linq', baseTypeObject,#(generics),generics,implements,interactionElement,'Class',8425);

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        numMethodGenerics = 0,
        signatureHash = 0,
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
        signatureHash = 0,
        func = function(element)
            return System.Collections.Generic.List[generics]["_C_0_" .. (2*Lua.Function.__typeof.signatureHash)](element[typeObject.level]["Enumerator"]);
        end,
    });

    --[[
    local constructors = {
        {
            types = {Lua.Function.__typeof},
            func = function(element, enumerator) element[typeObject.level]["Enumerator"] = enumerator; end,
        }
    }; --]]

    _M.IM(members, '', {
        level = typeObject.Level,
        memberType = 'Cstor',
        static = true,
        numMethodGenerics = 0,
        signatureHash = 2*Lua.Function.__typeof.signatureHash,
        scope = 'Public',
        func = function(element, enumerator)
            element[typeObject.level]["Enumerator"] = enumerator; 
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
    return "Class", typeObject, members, constructors, objectGenerator, implements, nil;
end});

local NoElements = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no elements"));
end

local NoMatch = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no matching element"));
end

_M.RE("System.Collections.Generic.IEnumerable", 1, function(generics)
    local genericsMapping = {['TSource'] = 1, ['TFirst'] = 1, ['TOuter'] = 1};

    return {
        { -- TSource Max(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int32 Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int32>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 20423052+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Int32> Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int32>>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 322643375496+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int64 Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int64>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 20870424+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Int64> Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int64>>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 329710958352+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Single Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Single>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 44248212+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Single> Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Single>>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 699033253176+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Double Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Double>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 44123364+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Double> Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Double>>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 697060904472+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Decimal Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Decimal>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 62059860+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Decimal> Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Decimal>>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 980421668280+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TResult Max(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TResult>)
            name = "Max",
            numMethodGenerics = 0,
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Double Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int32>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 20423052+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Double> Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int32>>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 322643375496+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Double Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int64>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 20870424+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Double> Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int64>>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 329710958352+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Single Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Single>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 44248212+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Single> Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Single>>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 699033253176+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Double Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Double>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 44123364+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Double> Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Double>>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 697060904472+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Decimal Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Decimal>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 62059860+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Decimal> Average(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Decimal>>)
            name = "Average",
            numMethodGenerics = 0,
            signatureHash = 980421668280+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Boolean Any(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Any",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                for _,v in (source % _M.DOT).GetEnumerator() do
                    return true;
                end
                return false;
            end,
        },
        { -- System.Boolean Any(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "Any",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                for _,v in (source % _M.DOT).GetEnumerator() do
                    if ((predicate % _M.DOT)(v)) then
                        return true;
                    end
                end
                return false;
            end,
        },
        { -- System.Boolean All(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "All",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int32 Count(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Count",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                local c = 0;
                for _,v in (source % _M.DOT).GetEnumerator() do
                    c = c + 1;
                end
                return c;
            end,
        },
        { -- System.Int32 Count(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "Count",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int64 LongCount(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "LongCount",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int64 LongCount(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "LongCount",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Boolean Contains(System.Collections.Generic.IEnumerable`1<TSource>, TSource)
            name = "Contains",
            numMethodGenerics = 0,
            signatureHash = (2*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, value)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Boolean Contains(System.Collections.Generic.IEnumerable`1<TSource>, TSource, System.Collections.Generic.IEqualityComparer`1<TSource>)
            name = "Contains",
            numMethodGenerics = 0,
            signatureHash = (2*generics[genericsMapping['TSource']].signatureHash)+(279582*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, value, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TSource Aggregate(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`3<TSource,TSource,TSource>)
            name = "Aggregate",
            numMethodGenerics = 0,
            signatureHash = (6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash)+(17340*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, func)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TAccumulate Aggregate(System.Collections.Generic.IEnumerable`1<TSource>, TAccumulate, System.Func`3<TAccumulate,TSource,TAccumulate>)
            name = "Aggregate",
            numMethodGenerics = 0,
            signatureHash = 36416+(15606*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, seed, func)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TResult Aggregate(System.Collections.Generic.IEnumerable`1<TSource>, TAccumulate, System.Func`3<TAccumulate,TSource,TAccumulate>, System.Func`2<TAccumulate,TResult>)
            name = "Aggregate",
            numMethodGenerics = 0,
            signatureHash = 79766+(15606*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, seed, func, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int32 Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int32>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 20423052+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Int32> Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int32>>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 322643375496+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int64 Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int64>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 20870424+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Int64> Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int64>>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 329710958352+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Single Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Single>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 44248212+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Single> Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Single>>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 699033253176+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Double Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Double>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 44123364+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Double> Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Double>>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 697060904472+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Decimal Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Decimal>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 62059860+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Decimal> Sum(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Decimal>>)
            name = "Sum",
            numMethodGenerics = 0,
            signatureHash = 980421668280+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TSource Min(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int32 Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int32>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 20423052+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Int32> Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int32>>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 322643375496+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Int64 Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Int64>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 20870424+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Int64> Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Int64>>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 329710958352+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Single Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Single>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 44248212+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Single> Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Single>>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 699033253176+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Double Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Double>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 44123364+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Double> Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Double>>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 697060904472+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Decimal Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Decimal>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 62059860+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Nullable`1<System.Decimal> Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Nullable`1<System.Decimal>>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 980421668280+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TResult Min(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TResult>)
            name = "Min",
            numMethodGenerics = 0,
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> Zip(System.Collections.Generic.IEnumerable`1<TFirst>, System.Collections.Generic.IEnumerable`1<TSecond>, System.Func`3<TFirst,TSecond,TResult>)
            name = "Zip",
            numMethodGenerics = 0,
            signatureHash = 107744+(10404*generics[genericsMapping['TFirst']].signatureHash),
            func = function(first, second, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Distinct(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Distinct",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Distinct(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEqualityComparer`1<TSource>)
            name = "Distinct",
            numMethodGenerics = 0,
            signatureHash = (186388*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
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
        { -- System.Collections.Generic.IEnumerable`1<TSource> Union(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEqualityComparer`1<TSource>)
            name = "Union",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash)+(279582*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Intersect(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Intersect",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Intersect(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEqualityComparer`1<TSource>)
            name = "Intersect",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash)+(279582*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Except(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Except",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Except(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEqualityComparer`1<TSource>)
            name = "Except",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash)+(279582*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Reverse(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Reverse",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Boolean SequenceEqual(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>)
            name = "SequenceEqual",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Boolean SequenceEqual(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEqualityComparer`1<TSource>)
            name = "SequenceEqual",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash)+(279582*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> AsEnumerable(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "AsEnumerable",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
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
        { -- System.Collections.Generic.Dictionary`2<TKey,TSource> ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>)
            name = "ToDictionary",
            numMethodGenerics = 0,
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.Dictionary`2<TKey,TSource> ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "ToDictionary",
            numMethodGenerics = 0,
            signatureHash = 289986+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.Dictionary`2<TKey,TElement> ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>)
            name = "ToDictionary",
            numMethodGenerics = 0,
            signatureHash = 26010+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.Dictionary`2<TKey,TElement> ToDictionary(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "ToDictionary",
            numMethodGenerics = 0,
            signatureHash = 491980+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Linq.ILookup`2<TKey,TSource> ToLookup(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>)
            name = "ToLookup",
            numMethodGenerics = 0,
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Linq.ILookup`2<TKey,TSource> ToLookup(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "ToLookup",
            numMethodGenerics = 0,
            signatureHash = 289986+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Linq.ILookup`2<TKey,TElement> ToLookup(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>)
            name = "ToLookup",
            numMethodGenerics = 0,
            signatureHash = 26010+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Linq.ILookup`2<TKey,TElement> ToLookup(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "ToLookup",
            numMethodGenerics = 0,
            signatureHash = 491980+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> DefaultIfEmpty(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "DefaultIfEmpty",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> DefaultIfEmpty(System.Collections.Generic.IEnumerable`1<TSource>, TSource)
            name = "DefaultIfEmpty",
            numMethodGenerics = 0,
            signatureHash = (2*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, defaultValue)
                _M.Throw(System.NotImplementedException._C_0_0());
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
        { -- TSource Single(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Single",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TSource Single(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "Single",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TSource SingleOrDefault(System.Collections.Generic.IEnumerable`1<TSource>)
            name = "SingleOrDefault",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(source)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TSource SingleOrDefault(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "SingleOrDefault",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TSource ElementAt(System.Collections.Generic.IEnumerable`1<TSource>, System.Int32)
            name = "ElementAt",
            numMethodGenerics = 0,
            signatureHash = 3926,
            func = function(source, index)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- TSource ElementAtOrDefault(System.Collections.Generic.IEnumerable`1<TSource>, System.Int32)
            name = "ElementAtOrDefault",
            numMethodGenerics = 0,
            signatureHash = 3926,
            func = function(source, index)
                _M.Throw(System.NotImplementedException._C_0_0());
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
        { -- System.Collections.Generic.IEnumerable`1<TSource> Where(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`3<TSource,System.Int32,System.Boolean>)
            name = "Where",
            numMethodGenerics = 0,
            signatureHash = 124775172+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
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
        { -- System.Collections.Generic.IEnumerable`1<TResult> Select(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`3<TSource,System.Int32,TResult>)
            name = "Select",
            numMethodGenerics = 0,
            signatureHash = 20440392+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> SelectMany(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Collections.Generic.IEnumerable`1<TResult>>)
            name = "SelectMany",
            numMethodGenerics = 0,
            signatureHash = 343997856+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> SelectMany(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`3<TSource,System.Int32,System.Collections.Generic.IEnumerable`1<TResult>>)
            name = "SelectMany",
            numMethodGenerics = 0,
            signatureHash = 593752812+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, selector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> SelectMany(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`3<TSource,System.Int32,System.Collections.Generic.IEnumerable`1<TCollection>>, System.Func`3<TSource,TCollection,TResult>)
            name = "SelectMany",
            numMethodGenerics = 0,
            signatureHash = 593794428+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, collectionSelector, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> SelectMany(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Collections.Generic.IEnumerable`1<TCollection>>, System.Func`3<TSource,TCollection,TResult>)
            name = "SelectMany",
            numMethodGenerics = 0,
            signatureHash = 344039472+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, collectionSelector, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Take(System.Collections.Generic.IEnumerable`1<TSource>, System.Int32)
            name = "Take",
            numMethodGenerics = 0,
            signatureHash = 3926,
            func = function(source, count)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> TakeWhile(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "TakeWhile",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> TakeWhile(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`3<TSource,System.Int32,System.Boolean>)
            name = "TakeWhile",
            numMethodGenerics = 0,
            signatureHash = 124775172+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Skip(System.Collections.Generic.IEnumerable`1<TSource>, System.Int32)
            name = "Skip",
            numMethodGenerics = 0,
            signatureHash = 3926,
            func = function(source, count)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> SkipWhile(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,System.Boolean>)
            name = "SkipWhile",
            numMethodGenerics = 0,
            signatureHash = 62611272+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> SkipWhile(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`3<TSource,System.Int32,System.Boolean>)
            name = "SkipWhile",
            numMethodGenerics = 0,
            signatureHash = 124775172+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, predicate)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> Join(System.Collections.Generic.IEnumerable`1<TOuter>, System.Collections.Generic.IEnumerable`1<TInner>, System.Func`2<TOuter,TKey>, System.Func`2<TInner,TKey>, System.Func`3<TOuter,TInner,TResult>)
            name = "Join",
            numMethodGenerics = 0,
            signatureHash = 222188+(10404*generics[genericsMapping['TOuter']].signatureHash)+(24276*generics[genericsMapping['TOuter']].signatureHash),
            func = function(outer, inner, outerKeySelector, innerKeySelector, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> Join(System.Collections.Generic.IEnumerable`1<TOuter>, System.Collections.Generic.IEnumerable`1<TInner>, System.Func`2<TOuter,TKey>, System.Func`2<TInner,TKey>, System.Func`3<TOuter,TInner,TResult>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "Join",
            numMethodGenerics = 0,
            signatureHash = 1247322+(10404*generics[genericsMapping['TOuter']].signatureHash)+(24276*generics[genericsMapping['TOuter']].signatureHash),
            func = function(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> GroupJoin(System.Collections.Generic.IEnumerable`1<TOuter>, System.Collections.Generic.IEnumerable`1<TInner>, System.Func`2<TOuter,TKey>, System.Func`2<TInner,TKey>, System.Func`3<TOuter,System.Collections.Generic.IEnumerable`1<TInner>,TResult>)
            name = "GroupJoin",
            numMethodGenerics = 0,
            signatureHash = 1204178270+(10404*generics[genericsMapping['TOuter']].signatureHash)+(24276*generics[genericsMapping['TOuter']].signatureHash),
            func = function(outer, inner, outerKeySelector, innerKeySelector, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> GroupJoin(System.Collections.Generic.IEnumerable`1<TOuter>, System.Collections.Generic.IEnumerable`1<TInner>, System.Func`2<TOuter,TKey>, System.Func`2<TInner,TKey>, System.Func`3<TOuter,System.Collections.Generic.IEnumerable`1<TInner>,TResult>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "GroupJoin",
            numMethodGenerics = 0,
            signatureHash = 1205203404+(10404*generics[genericsMapping['TOuter']].signatureHash)+(24276*generics[genericsMapping['TOuter']].signatureHash),
            func = function(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
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
        { -- System.Linq.IOrderedEnumerable`1<TSource> OrderBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Collections.Generic.IComparer`1<TKey>)
            name = "OrderBy",
            numMethodGenerics = 1,
            signatureHash = 74226+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Linq.IOrderedEnumerable`1<TSource> OrderByDescending(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>)
            name = "OrderByDescending",
            numMethodGenerics = 1,
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Linq.IOrderedEnumerable`1<TSource> OrderByDescending(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Collections.Generic.IComparer`1<TKey>)
            name = "OrderByDescending",
            numMethodGenerics = 1,
            signatureHash = 74226+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<System.Linq.IGrouping`2<TKey,TSource>> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 10404+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<System.Linq.IGrouping`2<TKey,TSource>> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 289986+(6936*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<System.Linq.IGrouping`2<TKey,TElement>> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 26010+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<System.Linq.IGrouping`2<TKey,TElement>> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 491980+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`3<TKey,System.Collections.Generic.IEnumerable`1<TSource>,TResult>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 46818+(6936*generics[genericsMapping['TSource']].signatureHash)+(515996784*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>, System.Func`3<TKey,System.Collections.Generic.IEnumerable`1<TElement>,TResult>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 860081340+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector, resultSelector)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`3<TKey,System.Collections.Generic.IEnumerable`1<TSource>,TResult>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 512788+(6936*generics[genericsMapping['TSource']].signatureHash)+(515996784*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, resultSelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TResult> GroupBy(System.Collections.Generic.IEnumerable`1<TSource>, System.Func`2<TSource,TKey>, System.Func`2<TSource,TElement>, System.Func`3<TKey,System.Collections.Generic.IEnumerable`1<TElement>,TResult>, System.Collections.Generic.IEqualityComparer`1<TKey>)
            name = "GroupBy",
            numMethodGenerics = 0,
            signatureHash = 860733698+(6936*generics[genericsMapping['TSource']].signatureHash)+(10404*generics[genericsMapping['TSource']].signatureHash),
            func = function(source, keySelector, elementSelector, resultSelector, comparer)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
        { -- System.Collections.Generic.IEnumerable`1<TSource> Concat(System.Collections.Generic.IEnumerable`1<TSource>, System.Collections.Generic.IEnumerable`1<TSource>)
            name = "Concat",
            numMethodGenerics = 0,
            signatureHash = (66128*generics[genericsMapping['TSource']].signatureHash),
            func = function(first, second)
                _M.Throw(System.NotImplementedException._C_0_0());
            end,
        },
    };
end);