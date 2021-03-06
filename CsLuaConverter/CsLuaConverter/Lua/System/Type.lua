﻿local hashString = function(str, hash)
    hash = hash or 7;
    for i=1, string.len(str) do
        hash = (math.mod or mod)(hash*31 + string.byte(str,i), 1000000);
    end
    return hash;
end

local typeType;
local objectType;

local GetMatchScore;
GetMatchScore = function(self, otherType, otherValue)
    if otherType.GetHashCode() == self.hash then
        return self.level;
    end
    
    if self.implements then
        local bestScore;
        for _,interfaceType in ipairs(self.implements) do
            local score = GetMatchScore(interfaceType, otherType, otherValue);
            if score then
                bestScore = bestScore and math.max(bestScore, score) or score;
            end
        end
        
        if bestScore then
            return math.min(bestScore, self.Level-1);
        end
    end
    
    if otherType.catagory == "Enum" and self.Equals(System.String.__typeof) then
        if (System.Enum % _M.DOT).Parse(otherType, otherValue) then
            return 0;
        end
    end

    if self.baseType then
        return self.baseType.GetMatchScore(otherType);
    end
    
    return nil;
end

local GetFullNameWithGenerics;
GetFullNameWithGenerics = function(self)
    if (self.altTypeName) then
        return self.altTypeName;
    end

    local generic = "";
    if self.numberOfGenerics > 0 then
        generic = "`" .. self.numberOfGenerics .. "[";
        for i,v in ipairs(self.generics) do
            if i > 1 then generic = generic .. ","; end
            generic = generic .. GetFullNameWithGenerics(v);
        end

        generic = generic .. "]";
    end

    return self.namespace .. "." ..  self.name .. generic;
end

local meta = {
    __index = function(self, index)
        if index == "__metaType" then
            return _M.MetaTypes.TypeObject;
        elseif index == "GetType" then
            return function()
                return typeType;
            end
        elseif index == "GetHashCode" then
            return function()
                return self.hash;
            end
        elseif index == "ToString" then
            return function()
                local fullName = self.namespace .. "." .. self.name;

                if self.generics then
                    local genericsNames = {};
                    for i,v in pairs(self.generics) do
                        genericsNames[i] = v.ToString();
                    end
                    return fullName..(#(genericsNames) > 0 and "<"..string.join(",", unpack(genericsNames))..">" or "");
                end
                return fullName;
            end;
        elseif index == "Equals" then
            return function(otherType)
                return self.hash == otherType.GetHashCode();
            end
        elseif index == "IsInstanceOfType" then
            return function(instance)
                local otherType = (instance % _M.DOT).GetType();
                if self.hash == otherType.hash then
                    return true;
                end

                if otherType.baseType and self.IsInstanceOfType({type = otherType.baseType}) then
                    return true;
                end

                for _,imp in ipairs(otherType.implements or {}) do
                    if self.IsInstanceOfType({type = imp}) then
                        return true;
                    end
                end
                return false;
            end
        elseif index == "Name" then
            return self.name;
        elseif index == "Namespace" then
            return self.namespace;
        elseif index == "BaseType" then
            return self.baseType;
        elseif index == "Level" then
            return self.level;
        elseif index == "Generics" then
            return self.generics;
        elseif index == "GetMatchScore" then
            return function(otherType, otherValue) return GetMatchScore(self, otherType, otherValue); end;
        elseif index == "InteractionElement" then
            return self.interactionElement;
        elseif index == "FullName" then
            local generic = "";
            if self.numberOfGenerics > 0 then
                generic = "´" .. self.numberOfGenerics;
            end

            return self.namespace .. "." .. self.name .. generic;
        elseif index == "GetFullNameWithGenerics" then
            return function() return GetFullNameWithGenerics(self); end
        elseif index == "IsEnum" then
            return self.catagory == "Enum";
        elseif index == "IsArray" then
            return self.name == "Array" and self.namespace == "System";
        elseif index == "type" then
            return typeType;
        elseif index == "__is" then
            return function(value) 
                return type(value) == "table" and (value % _M.DOT).GetType() == self; 
            end
        elseif index == "GetGenericArguments" then
            return function()
                local t = {};

                for i,v in pairs(self.generics) do
                    t[i-1] = v;
                end

                return t;
            end
        end
    end,
};

local getHash = function(name, namespace, numberOfGenerics, generics)
    local genericsHash = 1;
    if generics then
        for i,v in pairs(generics) do
            genericsHash = genericsHash  + (primes[i]* v.GetHashCode());
        end
    end

    return hashString(namespace .. "." .. name .. "´".. numberOfGenerics, genericsHash);
end

local typeCache = {};

local typeCall = function(name, namespace, baseType, numberOfGenerics, generics, implements, interactionElement, catagory, signatureHash, altTypeName)
    assert(interactionElement, "Type cannot be created without an interactionElement.");

    catagory = catagory or "Class";
    numberOfGenerics = numberOfGenerics or 0;
    local hash = getHash(name, namespace, numberOfGenerics, generics);
    if typeCache[hash] then 
        error("The type object "..tostring(name).." was already created.");
    end

    local self = interactionElement.__typeof or {};
    self.GetType = nil;

    self.catagory = catagory; 
    self.namespace = namespace;
    self.generics = generics;
    self.name = name; 
    self.numberOfGenerics = numberOfGenerics;
    self.hash = hash;
    self.generics = generics;
    self.baseType = baseType;
    self.level = (baseType and baseType.Level or 0) + 1;
    self.implements = implements;
    self.interactionElement = interactionElement;
    self.interactionElement.__typeof = self;
    self.altTypeName = altTypeName;
    local genericHash = _M.SH(unpack(generics or {}));
    if genericHash == 0 then genericHash = 1; end;
    self.signatureHash = signatureHash*genericHash;
    
    
    setmetatable(self, meta);
    typeCache[hash] = self;
    typeCache[self.GetFullNameWithGenerics()] = self;
    return self;
end

GetTypeFromHash = function(hash)
    return typeCache[hash];
end

GetTypeFromFullName = function(name)
    return typeCache[name];
end

--objectType = typeCall("Object", "System"); -- TODO: Initialize in a way that does not require the type cache
typeType = typeCall("Type", "System", nil, 0, nil, nil, {}, 'Class', 1798);

local meta = {
    __typeof = typeType,
    __is = function(value) return type(value) == "table" and type(value.GetType) == "function" and value.GetType() == typeType; end,
    __meta = function() return typeType; end,
};

local element = {};
setmetatable(element, { 
    __index = function(_, key)
        if meta[key] then 
            return meta[key];
        end
    end,
    __newindex = function(_, key, value)
    end,
    __call = function(_, ...)
        return typeCall(...);
    end,
});

System.Type = element;

