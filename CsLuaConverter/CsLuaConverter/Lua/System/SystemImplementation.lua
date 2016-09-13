-- This file have generated from a C# namespace.
_M.ATN('System','ArgumentException', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ArgumentException','System', nil, 0, generics, nil, interactionElement, 'Class', 47153);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.SystemException.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Message = _M.DV(System.String.__typeof),
                ParamName = _M.DV(System.String.__typeof),
                m_paramName = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Message == nil) then element[typeObject.Level].Message = values.Message; end
            if not(values.ParamName == nil) then element[typeObject.Level].ParamName = values.ParamName; end
            if not(values.m_paramName == nil) then element[typeObject.Level].m_paramName = values.m_paramName; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                scope = 'Public',
                func = function(element)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736("Value does not fall within the expected range.");
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, message)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736(message);
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 41328,
                scope = 'Public',
                func = function(element, message, innerException)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_41328(message, innerException);
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 76160,
                scope = 'Public',
                func = function(element, message, paramName, innerException)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_41328(message, innerException);
                    (element % _M.DOT_LVL(typeObject.Level)).m_paramName = paramName;
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 21840,
                scope = 'Public',
                func = function(element, message, paramName)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736(message);
                    (element % _M.DOT_LVL(typeObject.Level)).m_paramName = paramName;
                end,
            });
            _M.IM(members, 'm_paramName', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Private',
                static = false,
            });
            _M.IM(members, 'Message',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
                get = function(element)
                    local s = (element% _M.DOT_LVL(typeObject.Level - 1, true)).Message;
                    if (not(((System.String% _M.DOT).IsNullOrEmpty_M_0_8736 % _M.DOT)((element% _M.DOT_LVL(typeObject.Level)).m_paramName))) then
                        local resourcestring = "Parameter name: "  +_M.Add+  (element% _M.DOT_LVL(typeObject.Level)).m_paramName;
                        return s  +_M.Add+  "\n"  +_M.Add+  resourcestring;
                    else
                    return s;
                    end
                end,
            });
            _M.IM(members, 'ParamName',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
                get = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).m_paramName;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('System','ArgumentNullException', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ArgumentNullException','System', nil, 0, generics, nil, interactionElement, 'Class', 75548);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.ArgumentException.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                scope = 'Public',
                func = function(element)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736("Value cannot be null.");
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, paramName)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_21840("Value cannot be null.", paramName);
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 41328,
                scope = 'Public',
                func = function(element, paramName, innerException)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_41328(paramName, innerException);
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 21840,
                scope = 'Public',
                func = function(element, paramName, message)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_21840(paramName, message);
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('System','Environment', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Environment','System', nil, 0, generics, nil, interactionElement, 'Class', 17540);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
            };
            return element;
        end
        staticValues[typeObject.Level] = {
            NewLine = _M.DV(System.String.__typeof),
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.NewLine == nil) then element[typeObject.Level].NewLine = values.NewLine; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                scope = 'Public',
                func = function(element)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                end,
            });
            _M.IM(members, 'NewLine',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = true,
                returnType = System.String.__typeof;
                get = function(element)
                    return "\n";
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('System','SystemException', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('SystemException','System', nil, 0, generics, nil, interactionElement, 'Class', 35115);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Exception.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                scope = 'Public',
                func = function(element)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736("System error.");
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, message)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736(message);
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 41328,
                scope = 'Public',
                func = function(element, message, innerException)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_41328(message, innerException);
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
