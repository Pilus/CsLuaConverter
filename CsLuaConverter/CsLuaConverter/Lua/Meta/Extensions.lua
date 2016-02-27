
local Extensions = {};
local RegisterExtension = function(name, numGenerics, provider)
    Extensions[name] = Extensions[name] or {};
    Extensions[name][numGenerics] = Extensions[name][numGenerics] or {};
    table.insert(Extensions[name][numGenerics], provider);
end;
_M.RE = RegisterExtension;

local addRange = function(t, range)
    for _,v in pairs(range) do
        table.insert(t, v);
    end
end

local addMethodType = function(t)
    for _,v in pairs(t) do
        v.memberType = 'Method';
    end
    return t;
end

local GetExtensions = function(name, generics)
    local numGenerics = #(generics);
    local t = {};
    if type(Extensions[name]) == "table" and type(Extensions[name][numGenerics]) == "table" then
        for _, provider in pairs(Extensions[name][numGenerics]) do
            addRange(t, addMethodType(provider(generics)));
        end
    end
    return t;
end
_M.GE = GetExtensions;
