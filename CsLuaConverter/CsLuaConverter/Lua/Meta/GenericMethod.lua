

local GenericMethod = function(members, elementOrStaticValues)
    
    local t = {};

    setmetatable(t,{
        __index = function(_, generics)
            return function(...)
                local member = _M.AM(members, {...}, generics);

                if member.generics then
                    return member.func(elementOrStaticValues, member.generics, generics,...);
                else
                    return member.func(elementOrStaticValues,...);
                end
            end
        end,
        __call = function(_, ...)
            local member = _M.AM(members, {...});

            if member.generics then
                return member.func(elementOrStaticValues, member.generics, {},...);
            else
                return member.func(elementOrStaticValues,...);
            end
        end,
    });

    return t;
end

_M.GM = GenericMethod;

local MethodGenerics = function(generics)
    local t = {};
    setmetatable(t,{
        __index = function(_, key)
            for i,v in pairs(generics) do
                if v == key then
                    return i;
                end
            end
        end
    });
    return t;
end

_M.MG = MethodGenerics;