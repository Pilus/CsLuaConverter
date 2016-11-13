
local InvokeMethod = function(member, element, generics, args)
    if member.isParams then
        local i = #(args);

        local value = args[i];

        if not(value == nil) and ((value % _M.DOT).GetType() % _M.DOT).IsArray then
            args[i] = nil;
            for j = 0, (value % _M.DOT).Length - 1 do
                args[i+j] = (value % _M.DOT)[j];
            end
        end
    end 

    if member.generics then
        return member.func(element, member.generics, generics, unpack(args));
    else
        return member.func(element, unpack(args));
    end
end

local meta = {};
setmetatable(meta,{
    __index = function(_, index)
        if index == "__metaType" then
            return _M.MetaTypes.GenericMethod;
        end
    end
});

local GenericMethod = function(member, elementOrStaticValues, name)
    
    local t = {};

    setmetatable(t,{
        __index = function(_, generics)
            if meta[generics] then
                return meta[generics];
            end

            if generics == "type" then
                local actionGenerics = {};
                for _,v in pairs(member.types) do
                    table.insert(actionGenerics, v)
                end

                if (member.isParams == true) then
                    local i = #(actionGenerics)
                    actionGenerics[i] = System.Array[{actionGenerics[i]}].__typeof;
                end

                if not(member.returnType == nil) then
                    table.insert(actionGenerics, 1, member.returnType())
                    return System.Func[actionGenerics].__typeof;
                end

                return System.Action[actionGenerics].__typeof;
            end

            return function(...)
                return InvokeMethod(member, elementOrStaticValues, generics, {...});
            end
        end,
        __call = function(_, ...)
            return InvokeMethod(member, elementOrStaticValues, {}, {...});
        end,
    });

    return t;
end

_M.GM = GenericMethod;

local MethodGenerics = function(generics)
    local t = {};
    setmetatable(t, {
        __index = function(self, key)
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