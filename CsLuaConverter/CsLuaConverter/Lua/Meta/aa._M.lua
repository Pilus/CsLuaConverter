_M = { };
_M.MetaTypes = {
    TypeObject = "TypeObject",
    ClassObject = "ClassObject",
    NameSpace = "NameSpace",
    InteractionElement = "InteractionElement",
    StaticValues = "StaticValues",
    NameSpaceElement = "NameSpaceElement",
    InteractionElement = "InteractionElement",
    GenericMethod = "GenericMethod",
};
_M.__metaType = _M.MetaTypes.NameSpace

_M.NOT = setmetatable({}, { __add = function(_, value) 
    return not(value); 
end}); 

_M.AddRange = function(t1, t2)
    for i,v in pairs(t2) do
        table.insert(t1, v);
    end
end

local recursive = {};
local RecursiveProtectionLock = function(value)
    if (recursive[value] == true) then
        error("Unexpected recursion detected");
    end

    recursive[value] = true;
end;
_M.RPL = RecursiveProtectionLock;

local RecursiveProtectionRelease = function(value)
    recursive[value] = nil;
end;
_M.RPR = RecursiveProtectionRelease;

local primes = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83};
local GetSignatureHash = function(...)
    local types = {...};

    local value = 0;
    for i, type in ipairs(types) do
        if (type[1]) then
            value = value + (primes[i] * type[1].signatureHash);
        end
    end

    return value;
end
_M.SH = GetSignatureHash;