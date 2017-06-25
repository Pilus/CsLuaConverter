

local NoElements = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no elements"));
end

local NoMatch = function()
    _M.Throw(System.InvalidOperationException._C_0_8736("Sequence contains no matching element"));
end


_M.RE("System.Collections.IEnumerable", 0, function()
    return {
        { -- T OfType<T>()
            name = "OfType",
            numMethodGenerics = 1,
            generics = _M.MG({['TResult'] = 1});
            signatureHash = 0,
            func = function(source, methodGenericsMapping, methodGenerics)
                
            end,
        },
    }
end);