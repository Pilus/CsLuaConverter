
local wrap = function(typeObj, typeTranslator, value, ...)
    if (typeObj == nil) then -- void
        return;
    end

    if (typeObj.FullName == "CsLuaFramework.Wrapping.IMultipleValues") then
        return CsLuaFramework.Wrapping.MultipleValues[typeObj.Generics](value, ...);
    end
    
    if not(type(value) == "table") then
        return value;
    end

    if (type(value.type) == "table" and value.type.type == System.Type.__typeof) then
        return value;
    end
    
    if (typeTranslator) then
        typeObj = (typeTranslator %_M.DOT)(value) or typeObj;
    end
    
    return CsLuaFramework.Wrapping.WrappedLuaTable[{typeObj}](value, typeTranslator);
end

local unwrap = function(value)
    if type(value) == "table" and type(value[2]) == "table" and type(value[2].luaTable) == "table" then
        return value[2].luaTable;
    end

    return value;
end

local select = function(t, e)
    local t2 = {};
    for i,v in pairs(t) do
        t2[i] = e(v);
    end
    return t2;
end

local insert = function(t, i, v)
    local t2 = {[i] = v};

    for index, v in pairs(t) do
        if type(index) == "number" and index >= i then
            t2[index + 1] = v;
        else
            t2[index] = v;
        end
    end

    return t2;
end

local hasProvideSelfAttribute = function(attributes)
    local selfAttribute = CsLuaFramework.Attributes.ProvideSelfAttribute.__typeof;

    for _,v in pairs(attributes or {}) do
        if v == selfAttribute then
            return true;
        end
    end

    return false;
end

CsLuaFramework.Wrapping.WrappedLuaTable = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local interfaceType = generics[1];

    local implements = {
        interfaceType
    };

    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('WrappedLuaTable_'..interfaceType.name,'CsLuaFramework.Wrapping',baseTypeObject,#(generics),generics,implements,interactionElement);

    local _, interfaceMembers, _, _, _, _, attributes = interfaceType.interactionElement.__meta({});

    local constructors = {
        {
            types = {Lua.NativeLuaTable.__typeof},
            func = function(element, luaTable) 
                element[typeObject.level].luaTable = luaTable
            end,
        },
        {
            types = {Lua.NativeLuaTable.__typeof, System.Func[{Lua.NativeLuaTable.__typeof, System.Type.__typeof}].__typeof},
            func = function(element, luaTable, typeTranslator) 
                element[typeObject.level].luaTable = luaTable;
                element[typeObject.level].typeTranslator = typeTranslator;
            end,
        }
    };

    for name,memberSet in pairs(interfaceMembers) do
        for _, member in pairs(memberSet) do
            local m = {
                level = typeObject.Level,
                memberType = member.memberType,
                scope = 'Public',
                static = false,
            };

            if member.memberType == "Property" or member.memberType == "AutoProperty" then
                m.memberType = "Property";
                m.get = function(element)
                    return wrap(member.returnType, element[typeObject.level].typeTranslator, element[typeObject.level].luaTable[name]);
                end;
                m.set = function(element, value)
                    element[typeObject.level].luaTable[name] = unwrap(value);
                end;
            elseif member.memberType == "Method" then
                m.types = member.types;
                m.func = function(element,...)
                    local args = select({...}, unwrap);
                    if hasProvideSelfAttribute(attributes) then
                        args = insert(args, 1, element[typeObject.level].luaTable);
                    end
                    return wrap(member.returnType, element[typeObject.level].typeTranslator, element[typeObject.level].luaTable[name](unpack(args)));
                end;
            end

            _M.IM(members, name, m);
        end
    end

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator, implements;
end})
