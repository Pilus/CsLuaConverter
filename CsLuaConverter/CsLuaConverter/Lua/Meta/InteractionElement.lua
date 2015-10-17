
--============= Interaction Element =============

local expectOneMember = function(members, key)
    assert(type(members) == "table", "A table of one member was expected for "..key..". Got no table.");
    assert(#(members) == 1, "A table of one member was expected for "..key..". Got "..#(members).." members");
end

local InteractionElement = function(metaProvider, generics)
    local element = {};
    local staticValues = {};

    local catagory, typeObject, members, constructors, elementGenerator, implements = metaProvider(element, generics, staticValues);
    
    local where = function(list, evaluator)
        local t = {};
        for _, value in ipairs(list) do
            if evaluator(value) then
                table.insert(t, value);
            end
        end
        return t;
    end

    local getMembers = function(key, level, staticOnly)
        return where(members[key] or {}, function(member)
            return (not(staticOnly) or member.static == true) and (member.scope == "Public" or member.level == level);
        end);
    end

    local index = function(self, key, level)
        local fittingMembers = getMembers(key, level, false);

        if #(fittingMembers) == 0 then
            error("Incorrect key for member. Key: "..key);
        end

        if fittingMembers[1].type == "Variable" or fittingMembers[1].type == "AutoProperty" then
            expectOneMember(fittingMembers, key);
            return self[fittingMembers[1].level][key];
        end

        if fittingMembers[1].type == "Method" then
            return function(...)
                local member = _M.AM(fittingMembers, {...});
                return member.func(...);
            end
        end

        error("Could not handle member. Type: "..tostring(fittingMembers[1].type)..". Key: "..tostring(key));
    end;

    local newIndex = function(self, key, value, level)
        local fittingMembers = getMembers(key, level, false);

        if #(fittingMembers) == 0 then
            error("Incorrect key for member. Key: "..key);
        end

        if fittingMembers[1].type == "Variable" or fittingMembers[1].type == "AutoProperty" then
            expectOneMember(fittingMembers, key);
            self[fittingMembers[1].level][key] = value;
        end
    end

    local meta = {
        __typeof = typeObject,
        __is = function(value) return typeObject.IsInstanceOfType(value); end,
        __meta = function() return typeObject, members, constructors, elementGenerator, implements; end,
        __index = index,
        __newindex = newIndex,
    };

    
    setmetatable(element, { 
        __index = function(_, key)
            if meta[key] then 
                return meta[key];
            end

            if not(catagory == "Class") then
                error(string.format("Could not find key on a non class element. Category: %s. Key: %s.", tostring(catagory), tostring(key)));
            end

            local fittingMembers = getMembers(key, nil, true);
            expectOneMember(fittingMembers, key);
            assert(fittingMembers[1].type == "Variable", "Expected variable member. Got "..tostring(element.type)..".");
            return staticValues[key];
        end,
        __newindex = function(_, key, value)
            if not(catagory == "Class") then
                error(string.format("Could not set key on a non class element. Category: %s. Key: %s.", tostring(catagory), tostring(key)));
            end

            local fittingMembers = getMembers(key, nil, true);
            expectOneMember(fittingMembers, key);
            assert(fittingMembers[1].type == "Variable", "Expected variable member. Got "..tostring(element.type)..".");
            return staticValues[key];
        end,
        __call = function(_, ...)
            assert(type(constructors)=="table" and #(constructors) > 0, "Class did not provide any constructors. Type: "..typeObject.FullName);
            -- Generate the base class element from constructor.GenerateBaseClass
            local classElement = elementGenerator();
            -- find the constructor fitting the arguments.
            local constructor = _M.AM(constructors, {...});
            -- Call the constructor with (classElement,...)
            constructor.func(classElement, ...);
            
            return classElement;
        end,
    });

    return element;
end

_M = _M or {};
_M.IE = InteractionElement;

local InsertMember = function(members, key, member)
    if not(members[key]) then
        members[key] = {};
    end

    table.insert(members[key], member);
end
_M.IM = InsertMember;
