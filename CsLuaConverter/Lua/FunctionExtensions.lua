_M.RE("System.Action", "#", function(generics)
    return {
        {
            name = "ToLuaFunction",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(element)
                return element[2].innerAction
            end
        },
    };
end);

_M.RE("System.Func", "#", function(generics)
    return {
        {
            name = "ToLuaFunction",
            numMethodGenerics = 0,
            signatureHash = 0,
            func = function(element)
                return element[2].innerAction
            end
        },
    };
end);