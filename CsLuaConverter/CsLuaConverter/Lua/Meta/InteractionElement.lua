
--============= Interaction Element =============

local expectOneMember = function(members, key)
    assert(type(members) == "table", "A table of one member was expected for key '"..tostring(key).."'. Got no table.");
    assert(#(members) == 1, "A table of one member was expected for key '"..tostring(key).."'. Got "..#(members).." members");
end

local InteractionElement = function(metaProvider, generics)
    local element = {};
    local staticValues = {};

    local catagory, typeObject, members, constructors, elementGenerator, implements, initialize = metaProvider(element, generics, staticValues);
    
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
        if (key == "__Initialize") and initialize then
            return function(values) initialize(self, values); return self; end
        end

        local fittingMembers = getMembers(key, level, false);

        if #(fittingMembers) == 0 then
            error("Could not find member. Key: "..tostring(key)..". Object: "..typeObject.FullName.." Level: "..tostring(level));
        end

        for i=1,#(fittingMembers) do
            assert(type(fittingMembers[i].memberType) == "string", "Missing member type on member. Object: "..typeObject.FullName..". Key: "..tostring(key).." Level: "..tostring(level).." Member #: "..tostring(i))
        end

        if fittingMembers[1].memberType == "Variable" or fittingMembers[1].memberType == "AutoProperty" then
            expectOneMember(fittingMembers, key);
            return self[fittingMembers[1].level][key];
        end

        if fittingMembers[1].memberType == "Method" then
            return function(...)
                local member = _M.AM(fittingMembers, {...});
                return member.func(self,...);
            end
        end

        if fittingMembers[1].memberType == "Property" then
            return fittingMembers[1].get(self);
        end

        error("Could not handle member (get). Type: "..tostring(fittingMembers[1].memberType)..". Key: "..tostring(key));
    end;

    local newIndex = function(self, key, value, level)
        local fittingMembers = getMembers(key, level, false);

        if #(fittingMembers) == 0 then
            error("Could not find member. Key: "..tostring(key)..". Object: "..typeObject.FullName);
        end

        if fittingMembers[1].memberType == "Variable" or fittingMembers[1].memberType == "AutoProperty" then
            expectOneMember(fittingMembers, key);
            self[fittingMembers[1].level][key] = value;
            return
        end

        error("Could not handle member (set). Type: "..tostring(fittingMembers[1].memberType)..". Key: "..tostring(key));
    end

    local meta = {
        __typeof = typeObject,
        __is = function(value) return typeObject.IsInstanceOfType(value); end,
        __meta = function() 
            -- TODO: Clone the members to allow multiple objects to inherit the same object.
            return typeObject, members, constructors, elementGenerator, implements, initialize; 
        end,
        __index = index,
        __newindex = newIndex,
        __isNamespace = false,
    };

    
    setmetatable(element, { 
        __index = function(_, key)
            if not(meta[key] == nil) then 
                return meta[key];
            end

            if (key == "type") then
                return nil;
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
            -- Call the constructor
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
