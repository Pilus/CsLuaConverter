
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

local where = function(list, evaluator)
    local t = {};
    for _, value in ipairs(list) do
        if evaluator(value) then
            table.insert(t, value);
        end
    end
    return t;
end

local joinTablesDistinct = function(tables)
    local res = {};

    for _,t in pairs(tables) do
        for _,v in pairs(t) do
            if not(tContains(res, v)) then
                table.insert(res, v);
            end
        end
    end

    return res;
end

local join = function(t1, t2)
    local t3 = {};
    for _,v in pairs(t1) do table.insert(t3, v); end
    for _,v in pairs(t2) do table.insert(t3, v); end
    return t3;
end

local memberTypeTranslation = {
    ["M"] = "Method",
    ["C"] = "Cstor",
};

local InteractionElement = function(metaProvider, generics, selfObj)
    if (type(metaProvider)=="table" and type(metaProvider.__typeof) == "table" and metaProvider.__typeof.IsEnum) then
        for i,v in pairs(metaProvider) do
            selfObj[i] = v;
        end

        return metaProvider;
    end

    local element = selfObj or { __metaType = _M.MetaTypes.InteractionElement };
    local staticValues = {__metaType = _M.MetaTypes.StaticValues};
    local extensions = {};

    _M.RPL(tostring(metaProvider));

    local catagory, typeObject, memberProvider, constructors, elementGenerator, implements, initialize, attributes = metaProvider(element, generics, staticValues);
    staticValues.type = typeObject;

    local cachedMembers = nil;

    local filterMethodSignature = function(key)
        local methodMetaIndex = type(key) == "string" and (string.find(key, "_M_") or string.find(key, "_C_")) or nil;
        if (methodMetaIndex) then
            local newKey = string.sub(key, 0, methodMetaIndex-1);
            local indexType, numGenerics, hash = string.split("_", string.sub(key, methodMetaIndex+1));
            return newKey, indexType, tonumber(numGenerics), tonumber(hash);
        end

        return key;
    end

    local getMembers = function(key, level, staticOnly, extensions)
        if not(cachedMembers) then
            cachedMembers = _M.RTEF(memberProvider);
        end

        local indexType, numGenerics, hash;
        key, indexType, numGenerics, hash = filterMethodSignature(key);

        return where(cachedMembers[key] or {}, function(member)
            assert(member.memberType, "Member without member type in "..typeObject.FullName..". Key: "..key.." Level: "..tostring(member.level));
            local static = member.static;
            local public = member.scope == "Public";
            local protected = member.scope == "Protected";
            local memberLevel = member.level;
            local levelProvided = not(level == nil);
            local typeLevel = typeObject.level;
            local memberType = member.memberType;

            return (not(staticOnly) or static) and
                (indexType == nil or memberType == memberTypeTranslation[indexType]) and
                (numGenerics == nil or numGenerics == member.numMethodGenerics) and
                (hash == nil or hash == member.signatureHash) and
                (
                    (levelProvided and not(indexType == "C") and memberLevel <= level) or
                    (levelProvided and (indexType == "C") and memberLevel == level) or
                    (not(levelProvided) and (public or protected) and memberLevel <= typeLevel) or
                    (not(levelProvided) and not(public or protected) and memberLevel == typeLevel)
                );
        end);
    end

    local getExtensions = function()
        local ext = {_M.GE(typeObject.FullName, generics)};

        if not(typeObject.BaseType == nil) then
            table.insert(ext, typeObject.BaseType.InteractionElement.__getExtensions());
        end

        for _, imp in pairs(implements or {}) do
            table.insert(ext, imp.InteractionElement.__getExtensions());
        end
        return joinTablesDistinct(ext);
    end

    local extensions = nil;
    local getFittingExtensions = function(key)
        if (extensions == nil) then
            extensions = getExtensions();
        end

        local indexType, numGenerics, hash;
        key, indexType, numGenerics, hash = filterMethodSignature(key);

        return where(extensions, function(ext) 
            return ext.name == key and
                (numGenerics == nil or numGenerics == ext.numMethodGenerics) and
                (hash == nil or hash == ext.signatureHash); 
            end);
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

        local result = {};

        for i,member in ipairs(fittingMembers) do
            local accepted = true;
            for j,otherMember in ipairs(fittingMembers) do
                if not(i == j) and
                    member.signatureHash == otherMember.signatureHash and
                    member.numMethodGenerics == otherMember.numMethodGenerics and
                    member.level < otherMember.level
                then
                    accepted = false;
                end
            end

            if accepted then
                table.insert(result, member);
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
            fittingMembers = getFittingExtensions(key);
        end

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

        if (#(fittingMembers) > 1) then
            local nonMethodMembers = where(fittingMembers, function(m) return not(m.memberType == "Method"); end)
            if #(nonMethodMembers) > 0 then
                fittingMembers = nonMethodMembers;
            end
        end

        expectOneMember(fittingMembers, key);
        local member = fittingMembers[1];

        if member.memberType == "Field" or member.memberType == "AutoProperty" then
            if member.static then
                return staticValues[member.level][key];
            end

            return self[member.level][key];
        end

        if member.memberType == "Indexer" then
            if (member.get) then
                return member.get(self, key);
            end

            return self[member.level][key];
        end

        if member.memberType == "Method" then
            return _M.GM(member, self, key);
        end

        if member.memberType == "Property" then
            return member.get(self);
        end

        if member.memberType == "Cstor" then
            return function(...)
                member.func(self, ...);
            end
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
        __as = function(value) return typeObject.IsInstanceOfType(value) and value or nil; end,
        __meta = function(inheritingStaticValues) 
            for i = 1, typeObject.level do
                inheritingStaticValues[i] = staticValues[i];
            end

            if not(cachedMembers) then
                cachedMembers = _M.RTEF(memberProvider);
            end

            return typeObject, clone(cachedMembers, 2), clone(constructors or {},2), elementGenerator, clone(implements or {},1), initialize, attributes; 
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
        __getExtensions = getExtensions,
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

            local fittingMembers = getMembers(key, typeObject.level, true);

            if #(fittingMembers) == 0 then
                error("Could not find static member. Key: "..tostring(key)..". Object: "..typeObject.FullName);
            end

            expectOneMember(fittingMembers, key);

            local member = fittingMembers[1];

            if (member.memberType == "Method") then
                return _M.GM(member, staticValues, key);
            end

            if member.memberType == "Property" then
                return member.get(staticValues);
            end

            if member.memberType == "AutoProperty" then
                return staticValues[member.level][key];
            end

            if member.memberType == "Cstor" then
                local classElement = elementGenerator();
                return function(...)
                    member.func(classElement, ...);
                    return classElement;
                end;
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
        end, --[[
        __call = function(_, ...)
            
            
            assert(type(constructors)=="table" and #(constructors) > 0, "Class did not provide any constructors. Type: "..typeObject.FullName);
            -- Generate the base class element from constructor.GenerateBaseClass
            local classElement = elementGenerator();
            -- find the constructor fitting the arguments.
            local constructor, foldOutLast = _M.AM(constructors, {...}, "Constructor");
            -- Call the constructor
            constructor.func(classElement, ...);

            return classElement; 
        end, --]]
    });

    _M.RPR(tostring(metaProvider));

    return element;
end

_M.IE = InteractionElement;

local InsertMember = function(members, key, member)
    if not(members[key]) then
        members[key] = {};
    end

    if member.memberType == 'Method' and member.signatureHash == nil then 
        error("No signature hash for member ".. key); 
    end

    table.insert(members[key], member);
end
_M.IM = InsertMember;

local BaseCstor = function(classElement, baseConstructors, ...)
    local constructor = _M.AM(baseConstructors, {...}, "Base constructor");
    constructor.func(classElement, ...);
end
_M.BC = BaseCstor;

local ReturnTableOrExecuteFunction = function(t)
    return type(t) == "table" and t or t();
end
_M.RTEF = ReturnTableOrExecuteFunction;
