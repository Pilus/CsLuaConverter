

local GenericMethod = function(members, elementOrStaticValues)
    
    local t = {};

    setmetatable(t,{
        __index = function(_, generics)
            return function(...)
                local member = _M.AM(members, {...}, generics);
                return member.func(elementOrStaticValues,...);
            end
        end,
        __call = function(_, ...)
            local member = _M.AM(members, {...});
            return member.func(elementOrStaticValues,...);
        end,
    });

    return t;
end

_M.GM = GenericMethod;
