
--============= Namespace Element =============

local getHashOfGenerics = function(generics)
    local hash = 0;
    for i,v in ipairs(generics) do
        hash = hash + (primes[i]*v.GetHashCode());
    end
    return hash;
end

local isGenericsTable = function(t)
    if not(type(t) == "table") then
        return false;
    end
    
    for i,v in ipairs(t) do
        if (not(type(v) == "table") or not(System.Type.__is(v))) then
            return false;
        end
    end
    return true;
end

local NamespaceElement = function(metaProviders)
    local interactionElements = {};
    
    local getInteractionElement = function(generics)
        generics = generics or {};
        local hash = getHashOfGenerics(generics);
        if not(interactionElements[hash]) then
            assert(metaProviders[#(generics)] or metaProviders["#"], string.format("Could not find meta provider fitting number of generics: %s or '#'", #(generics)));

            local selfObj = { __metaType = _M.MetaTypes.InteractionElement };
            interactionElements[hash] = selfObj;
            _M.IE(metaProviders[#(generics)] or metaProviders["#"], generics, selfObj);
        end
        
        return interactionElements[hash];
    end

    local element = {};
    setmetatable(element, { 
        __index = function(_, key)
            if key == "__metaType" then
                return _M.MetaTypes.NameSpaceElement;
            end

            if not(isGenericsTable(key)) then
                return getInteractionElement()[key];
            end
            return getInteractionElement(key);
        end,
        __newindex = function(_, key, value)
            getInteractionElement()[key] = value;
        end,
    });

    return element;
end

_M.NE = NamespaceElement;
