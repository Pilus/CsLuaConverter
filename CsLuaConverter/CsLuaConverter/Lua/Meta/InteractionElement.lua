
--============= Interaction Element =============

local expectOneMember = function(members, key)
    assert(type(members) == "table", "A table of one member was expected for key '"..tostring(key).."'. Got no table.");
    assert(#(members) == 1, "A table of one member was expected for key '"..tostring(key).."'. Got "..#(members).." members");
end

local clone;
clone = function(t,dept)
    local t2 = {};
    for i,v in pairs(t) do
        if dept > 1 and type(v) == "table" then
            t2[i] = clone(v, dept-1);
        else
            t2[i] = v;
        end
    end
    return t2;
end

local InteractionElement = function(metaProvider, generics)
    local element = { __metaType = _M.MetaTypes.InteractionElement };
    local staticValues = {__metaType = _M.MetaTypes.StaticValues};
    local extendedMethods = {};

    local catagory, typeObject, members, constructors, elementGenerator, implements, initialize = metaProvider(element, generics, staticValues);
    staticValues.type = typeObject;

    local where = function(list, evaluator, otherList)
        local t = {};
        for _, value in ipairs(list) do
            if evaluator(value) then
                table.insert(t, value);
            end
        end
        for _, value in ipairs(otherList) do
            if evaluator(value) then
                table.insert(t, value);
            end
        end
        return t;
    end

    local getMembers = function(key, level, staticOnly)
        return where(members[key] or {}, function(member)
            assert(member.memberType, "Member without member type in "..typeObject.FullName..". Key: "..key.." Level: "..tostring(member.level));
            local static = member.static;
            local public = member.scope == "Public";
            local protected = member.scope == "Protected";
            local memberLevel = member.level;
            local levelProvided = not(level == nil);
            local typeLevel = typeObject.level;

            return (not(staticOnly) or static) and
            (
                (levelProvided and memberLevel <= level) or
                (not(levelProvided) and (public or protected) and memberLevel <= typeLevel) or
                (not(levelProvided) and not(public or protected) and memberLevel == typeLevel)
            );

            --return (not(staticOnly) or member.static == true) and (member.scope == "Public" or (member.scope == "Protected" and level) or member.level == level or (not(level) and member.level == typeObject.level)) and (not(level) or level >= member.level);
        end, extendedMethods[key] or {});
    end

    local matchesAll = function(t1, t2)
        if not(#(t1) == #(t2)) then
            return false;
        end

        for i,_ in ipairs(t1) do
            if not(t1[i] == t2[i]) then
                return false;
            end
        end

        return true;
    end

    local orderByLevel = function(members)
        local t = {};

        for _,member in ipairs(members) do
            local inserted = false;

            for i,v in ipairs(t) do
                if member.level > v.level then
                    table.insert(t, i, member);
                    inserted = true;
                    break;
                end
            end

            if (inserted == false) then
                table.insert(t, member);
            end
        end

        return t;
    end

    local filterOverrides = function(fittingMembers, level)
        if #(fittingMembers) == 1 then
            return fittingMembers;
        end
        
        fittingMembers = orderByLevel(fittingMembers);

        local skippedMembers = {};
        local acceptedMembers = {};

        for i,member in ipairs(fittingMembers) do
            if not(acceptedMembers[i]) and not(skippedMembers[i]) then
                acceptedMembers[i] = true;
                if member.override then
                    for j,otherMember in ipairs(fittingMembers) do
                        if not(j == i) and matchesAll(member.types, otherMember.types) then
                            if member.level > otherMember.level then
                                acceptedMembers[i] = true;
                                skippedMembers[j] = true;
                                acceptedMembers[j] = false;
                            else
                                acceptedMembers[j] = true;
                                skippedMembers[i] = true;
                                acceptedMembers[i] = false;
                            end
                        end
                    end
                end
            end
        end

        local result = {};
        for i,v in pairs(acceptedMembers) do
            if v then
                table.insert(result, fittingMembers[i]);
            end
        end

        return result;
    end

    local index = function(self, key, level)
        if (key == "__metaType") then return _M.MetaTypes.InteractionElement; end

        if (key == "__Initialize") and initialize then
            return function(values) initialize(self, values); return self; end
        end

        local fittingMembers = filterOverrides(getMembers(key, level, false), level or typeObject.level);

        if #(fittingMembers) == 0 then
            fittingMembers = getMembers("#", level, false); -- Look up indexers
        end

        if #(fittingMembers) == 0 and type(key) == "table" then
            return self[key];
        end

        if #(fittingMembers) == 0 then
            error("Could not find member. Key: "..tostring(key)..". Object: "..typeObject.FullName.." Level: "..tostring(level));
        end

        for i=1,#(fittingMembers) do
            assert(type(fittingMembers[i].memberType) == "string", "Missing member type on member. Object: "..typeObject.FullName..". Key: "..tostring(key).." Level: "..tostring(level).." Member #: "..tostring(i))
        end

        if fittingMembers[1].memberType == "Field" or fittingMembers[1].memberType == "AutoProperty" then
            expectOneMember(fittingMembers, key);

            if fittingMembers[1].static then
                return staticValues[fittingMembers[1].level][key];
            end

            return self[fittingMembers[1].level][key];
        end

        if fittingMembers[1].memberType == "Indexer" then
            expectOneMember(fittingMembers, "#");

            if (fittingMembers[1].get) then
                return fittingMembers[1].get(self, key);
            end

            return self[fittingMembers[1].level][key];
        end

        if fittingMembers[1].memberType == "Method" then
            return _M.GM(fittingMembers, self);
        end

        if fittingMembers[1].memberType == "Property" then
            return fittingMembers[1].get(self);
        end

        error("Could not handle member (get). Object: "..typeObject.FullName.." Type: "..tostring(fittingMembers[1].memberType)..". Key: "..tostring(key));
    end;

    local newIndex = function(self, key, value, level)
        local fittingMembers = getMembers(key, level, false);

        if #(fittingMembers) == 0 then
            fittingMembers = getMembers("#", level, false); -- Look up indexers
        end

        if #(fittingMembers) == 0 then
            error("Could not find member (set). Key: "..tostring(key)..". Object: "..typeObject.FullName.." Level: "..tostring(level));
        end

        if fittingMembers[1].memberType == "Field" or fittingMembers[1].memberType == "AutoProperty" then
            expectOneMember(fittingMembers, key);

            if (fittingMembers[1].static) then
                staticValues[fittingMembers[1].level][key] = value;
                return;
            end

            self[fittingMembers[1].level][key] = value;
            return
        end

        if fittingMembers[1].memberType == "Indexer" then
            expectOneMember(fittingMembers, "#");

            if (fittingMembers[1].set) then
                fittingMembers[1].set(self, key, value);
                return
            end

            self[fittingMembers[1].level][key] = value;
            return
        end

        if fittingMembers[1].memberType == "Property" then
            expectOneMember(fittingMembers, key);
            fittingMembers[1].set(self, value);
            return 
        end

        error("Could not handle member (set). Object: "..typeObject.FullName.." Type: "..tostring(fittingMembers[1].memberType)..". Key: "..tostring(key)..". Num members: "..#(fittingMembers));
    end

    local meta = {
        __typeof = typeObject,
        __is = function(value) return typeObject.IsInstanceOfType(value); end,
        __meta = function(inheritingStaticValues) 
            for i = 1, typeObject.level do
                inheritingStaticValues[i] = staticValues[i];
            end

            return typeObject, clone(members, 2), clone(constructors or {},2), elementGenerator, clone(implements or {},1), initialize; 
        end,
        __index = index,
        __newindex = newIndex,
        __metaType = _M.MetaTypes.InteractionElement,
        __extend = function(extensions) 
            for _,v in ipairs(extensions) do
                if not(extendedMethods[v.name]) then
                    extendedMethods[v.name] = {};
                end
                v.level = typeObject.level;
                table.insert(extendedMethods[v.name], v);
            end
        end,
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
                if (key == "GetType") then
                    return nil;
                end

                error(string.format("Could not find key on a non class element. Category: %s. Key: %s.", tostring(catagory), tostring(key)));
            end

            local fittingMembers = getMembers(key, nil, true);

            if #(fittingMembers) == 0 then
                error("Could not find static member. Key: "..tostring(key)..". Object: "..typeObject.FullName);
            end

            if (fittingMembers[1].memberType == "Method") then
                return _M.GM(fittingMembers, staticValues);
            end

            expectOneMember(fittingMembers, key);
            local member = fittingMembers[1];

            if member.memberType == "Property" then
                return member.get(staticValues);
            end

            if member.memberType == "AutoProperty" then
                return staticValues[member.level][key];
            end

            assert(member.memberType == "Field", "Expected field member for key "..tostring(key)..". Got "..tostring(member.memberType)..". Object: "..typeObject.FullName..".");
            return staticValues[member.level][key];
        end,
        __newindex = function(_, key, value)
            if not(catagory == "Class") then
                error(string.format("Could not set key on a non class element. Category: %s. Key: %s.", tostring(catagory), tostring(key)));
            end

            local fittingMembers = getMembers(key, nil, true);
            expectOneMember(fittingMembers, key);
            local member = fittingMembers[1];

            if member.memberType == "Property" then
                member.set(staticValues, value);
                return 
            end

            if member.memberType == "AutoProperty" then
                staticValues[member.level][key] = value;
                return;
            end

            assert(member.memberType == "Field", "Expected field member for key "..tostring(key)..". Got "..tostring(member.memberType)..". Object: "..typeObject.FullName..".");
            staticValues[member.level][key] = value;
        end,
        __call = function(_, ...)
            assert(type(constructors)=="table" and #(constructors) > 0, "Class did not provide any constructors. Type: "..typeObject.FullName);
            -- Generate the base class element from constructor.GenerateBaseClass
            local classElement = elementGenerator();
            -- find the constructor fitting the arguments.
            local constructor, foldOutLast = _M.AM(constructors, {...});
            -- Call the constructor
            constructor.func(classElement, ...);

            return classElement;
        end,
    });

    return element;
end

_M.IE = InteractionElement;

local InsertMember = function(members, key, member)
    if not(members[key]) then
        members[key] = {};
    end

    table.insert(members[key], member);
end
_M.IM = InsertMember;

local BaseCstor = function(classElement, baseConstructors, ...)
    local constructor = _M.AM(baseConstructors, {...});
    constructor.func(classElement, ...);
end
_M.BC = BaseCstor;
