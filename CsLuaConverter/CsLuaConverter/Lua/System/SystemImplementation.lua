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
            _M.IM(members, 'm_paramName', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Private',
                static = false,
            });
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
            _M.IM(members, 'Message',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
                get = function(element)
                    local s = (element % _M.DOT_LVL(typeObject.Level - 1, true)).Message;
                    if (not(((System.String % _M.DOT).IsNullOrEmpty_M_0_8736 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).m_paramName))) then
                        local resourcestring = "Parameter name: " +_M.Add+ (element % _M.DOT_LVL(typeObject.Level)).m_paramName;
                        return s +_M.Add+ "\n" +_M.Add+ resourcestring;
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
_M.ATN('System','IDisposable', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IDisposable','System', nil, 0, generics, nil, interactionElement, 'Interface',16670);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Dispose', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
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
_M.ATN('System.Collections','IEnumerable', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IEnumerable','System.Collections', nil, 0, generics, nil, interactionElement, 'Interface',16532);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'GetEnumerator', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Collections.IEnumerator.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('System.Collections','IEnumerator', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IEnumerator','System.Collections', nil, 0, generics, nil, interactionElement, 'Interface',17436);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Current',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.Object.__typeof;
            });
            _M.IM(members, 'MoveNext', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Boolean.__typeof end,
            });
            _M.IM(members, 'Reset', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('System.Collections.Generic','IEnumerable', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('IEnumerable','System.Collections.Generic', nil, 1, generics, nil, interactionElement, 'Interface',(33064*generics[genericsMapping['T']].signatureHash));
        local implements = {
            System.Collections.IEnumerable.__typeof,
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'GetEnumerator', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Collections.Generic.IEnumerator[{generics[genericsMapping['T']]}].__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('System.Collections.Generic','IEnumerator', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('IEnumerator','System.Collections.Generic', nil, 1, generics, nil, interactionElement, 'Interface',(34872*generics[genericsMapping['T']].signatureHash));
        local implements = {
            System.IDisposable.__typeof,
            System.Collections.IEnumerator.__typeof,
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Current',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = generics[genericsMapping['T']];
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('System.Linq','Enumerable', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Enumerable','System.Linq', nil, 0, generics, nil, interactionElement, 'Class', 13333);
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                end,
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Aggregate', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 118148,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, func)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TAccumulate'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Aggregate', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 152831,
                returnType = function() return methodGenerics[methodGenericsMapping['TAccumulate']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, seed, func)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TAccumulate'] = 2,['TResult'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Aggregate', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 213521,
                returnType = function() return methodGenerics[methodGenericsMapping['TResult']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, seed, func, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'All', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Any', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    for _,value in (source % _M.DOT).GetEnumerator() do
                        return true;
                    end
                    return false;
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Any', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    if (predicate == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("predicate"));
                    end
                    
            for _,v in (source % _M.DOT).GetEnumerator() do
                if ((predicate % _M.DOT)(v)) then
                    return true;
                end
            end
            return false; 
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'AsEnumerable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1470632578952,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93166322,
                returnType = function() return System.Decimal.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 129809264,
                returnType = function() return System.Double.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2050726752672,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 132652768,
                returnType = function() return System.Double.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2095648428864,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 281242384,
                returnType = function() return System.Single.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 280448848,
                returnType = function() return System.Double.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4430530900704,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 394453520,
                returnType = function() return System.Decimal.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 6231576708960,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 30711110,
                returnType = function() return System.Double.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 483965139776,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 31382168,
                returnType = function() return System.Double.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 494566514060,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66448850,
                returnType = function() return System.Single.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1048549956296,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66261578,
                returnType = function() return System.Double.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1045591433240,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Average', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4443067182432,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TResult'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Cast', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 33064,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Concat', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 165320,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Contains', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66131,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, value)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Contains', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 532101,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, value, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Count', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return System.Int32.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Count', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Int32.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    local count = 0;
                    for _,value in (source % _M.DOT).GetEnumerator() do
                        count = count + 1;
                    end
                    return count;
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'DefaultIfEmpty', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'DefaultIfEmpty', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66131,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, defaultValue)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Distinct', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 345710,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Distinct', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ElementAt', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 72017,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, index)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ElementAtOrDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 72017,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, index)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TResult'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Empty', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 0,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Except', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 165320,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Except', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 631290,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'First', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'First', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'FirstOrDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'FirstOrDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return System.Collections.Generic.IEnumerable[{System.Linq.IGrouping[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TSource']]}].__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 558108,
                returnType = function() return System.Collections.Generic.IEnumerable[{System.Linq.IGrouping[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TSource']]}].__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TResult'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 860147468,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3,['TResult'] = 4};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 4,
                signatureHash = 1204212950,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TResult'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 860799826,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, resultSelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3,['TResult'] = 4};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 4,
                signatureHash = 1205238084,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector, resultSelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 787846,
                returnType = function() return System.Collections.Generic.IEnumerable[{System.Linq.IGrouping[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TElement']]}].__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 135488,
                returnType = function() return System.Collections.Generic.IEnumerable[{System.Linq.IGrouping[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TElement']]}].__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TOuter'] = 1,['TInner'] = 2,['TKey'] = 3,['TResult'] = 4};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupJoin', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 4,
                signatureHash = 1892391086,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, outer, inner, outerKeySelector, innerKeySelector, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TOuter'] = 1,['TInner'] = 2,['TKey'] = 3,['TResult'] = 4};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GroupJoin', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 4,
                signatureHash = 1893602608,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Intersect', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 165320,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Intersect', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 631290,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TOuter'] = 1,['TInner'] = 2,['TKey'] = 3,['TResult'] = 4};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Join', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 4,
                signatureHash = 460100,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, outer, inner, outerKeySelector, innerKeySelector, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TOuter'] = 1,['TInner'] = 2,['TKey'] = 3,['TResult'] = 4};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Join', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 4,
                signatureHash = 1671622,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Last', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Last', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'LastOrDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'LastOrDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'LongCount', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return System.Int64.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'LongCount', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Int64.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 280448848,
                returnType = function() return System.Double.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 394453520,
                returnType = function() return System.Decimal.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 6231576708960,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93166322,
                returnType = function() return System.Decimal.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1045591433240,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 129809264,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66261578,
                returnType = function() return System.Double.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1048549956296,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2050726752672,
                returnType = function() return System.Nullable[{System.Int32.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 132652768,
                returnType = function() return System.Int64.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66448850,
                returnType = function() return System.Single.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 494566514060,
                returnType = function() return System.Nullable[{System.Int64.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 31382168,
                returnType = function() return System.Int64.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 483965139776,
                returnType = function() return System.Nullable[{System.Int32.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 30711110,
                returnType = function() return System.Int32.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4443067182432,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4430530900704,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2095648428864,
                returnType = function() return System.Nullable[{System.Int64.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 281242384,
                returnType = function() return System.Single.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TResult'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return methodGenerics[methodGenericsMapping['TResult']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Max', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1470632578952,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2050726752672,
                returnType = function() return System.Nullable[{System.Int32.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 132652768,
                returnType = function() return System.Int64.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2095648428864,
                returnType = function() return System.Nullable[{System.Int64.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 281242384,
                returnType = function() return System.Single.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4443067182432,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 280448848,
                returnType = function() return System.Double.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4430530900704,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 394453520,
                returnType = function() return System.Decimal.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 6231576708960,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 30711110,
                returnType = function() return System.Int32.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 483965139776,
                returnType = function() return System.Nullable[{System.Int32.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 31382168,
                returnType = function() return System.Int64.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 494566514060,
                returnType = function() return System.Nullable[{System.Int64.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66448850,
                returnType = function() return System.Single.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1048549956296,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66261578,
                returnType = function() return System.Double.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1045591433240,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93166322,
                returnType = function() return System.Decimal.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 129809264,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1470632578952,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TResult'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Min', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return methodGenerics[methodGenericsMapping['TResult']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TResult'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'OfType', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 33064,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'OrderBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 198508,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'OrderBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'OrderByDescending', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 198508,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'OrderByDescending', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Range', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 9815,
                returnType = function() return System.Collections.Generic.IEnumerable[{System.Int32.__typeof}].__typeof end,
                func = function(element, start, count)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TResult'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Repeat', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 5891,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, element, count)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Reverse', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TResult'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Select', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    if (selector == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("selector"));
                    end
                    
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_16704(function(_, prevKey)
                local key, value = enumerator(_, prevKey);
                if (key == nil) then
                    return nil;
                end
                return key, (selector %_M.DOT)(value);
            end); 
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TResult'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Select', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 30737120,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    if (selector == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("selector"));
                    end
                    
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_16704(function(_, prevKey)
                local key, value = enumerator(_, prevKey);
                if (key == nil) then
                    return nil;
                end
                return key, (selector %_M.DOT)(value, key);
            end); 
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TResult'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SelectMany', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 516073316,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TCollection'] = 2,['TResult'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SelectMany', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 516160016,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, collectionSelector, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TCollection'] = 2,['TResult'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SelectMany', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 890792450,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, collectionSelector, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TResult'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SelectMany', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 890705750,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SequenceEqual', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 631290,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SequenceEqual', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 165320,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Single', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Single', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SingleOrDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SingleOrDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return methodGenerics[methodGenericsMapping['TSource']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Skip', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 72017,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, count)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SkipWhile', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'SkipWhile', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 187239290,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 483965139776,
                returnType = function() return System.Nullable[{System.Int32.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93166322,
                returnType = function() return System.Decimal.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1470632578952,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 129809264,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2050726752672,
                returnType = function() return System.Nullable[{System.Int32.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 132652768,
                returnType = function() return System.Int64.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 2095648428864,
                returnType = function() return System.Nullable[{System.Int64.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 281242384,
                returnType = function() return System.Single.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4443067182432,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 4430530900704,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 280448848,
                returnType = function() return System.Double.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 6231576708960,
                returnType = function() return System.Nullable[{System.Decimal.__typeof}].__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 30711110,
                returnType = function() return System.Int32.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 31382168,
                returnType = function() return System.Int64.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 494566514060,
                returnType = function() return System.Nullable[{System.Int64.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66448850,
                returnType = function() return System.Single.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1048549956296,
                returnType = function() return System.Nullable[{System.Single.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66261578,
                returnType = function() return System.Double.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 1045591433240,
                returnType = function() return System.Nullable[{System.Double.__typeof}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, selector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            _M.IM(members, 'Sum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 394453520,
                returnType = function() return System.Decimal.__typeof end,
                func = function(element, source)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Take', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 72017,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, count)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'TakeWhile', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'TakeWhile', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 187239290,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ThenBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 232526,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ThenBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 338896,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ThenByDescending', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 232526,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ThenByDescending', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 338896,
                returnType = function() return System.Linq.IOrderedEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToArray', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Array[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    local array = (System.Array[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_0() % _M.DOT).__Initialize({});
                    local c = 0;
                    for _,element in (source % _M.DOT).GetEnumerator() do
                        (array % _M.DOT)[c] = element;
                        c = c + 1;
                    end
                    return array;
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToDictionary', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 787846,
                returnType = function() return System.Collections.Generic.Dictionary[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TElement']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToDictionary', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 135488,
                returnType = function() return System.Collections.Generic.Dictionary[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TElement']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToDictionary', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 558108,
                returnType = function() return System.Collections.Generic.Dictionary[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToDictionary', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return System.Collections.Generic.Dictionary[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToList', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 66128,
                returnType = function() return System.Collections.Generic.List[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    return System.Collections.Generic.List[{methodGenerics[methodGenericsMapping['TSource']]}]['_C_0_'..(66128*methodGenerics[methodGenericsMapping['TSource']].signatureHash)](source);
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToLookup', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 92138,
                returnType = function() return System.Linq.ILookup[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToLookup', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 135488,
                returnType = function() return System.Linq.ILookup[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TElement']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2,['TElement'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToLookup', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 787846,
                returnType = function() return System.Linq.ILookup[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TElement']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, elementSelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1,['TKey'] = 2};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ToLookup', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 2,
                signatureHash = 558108,
                returnType = function() return System.Linq.ILookup[{methodGenerics[methodGenericsMapping['TKey']], methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, keySelector, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Union', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 631290,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second, comparer)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Union', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 165320,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Where', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 187239290,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    if (predicate == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("predicate"));
                    end
                    
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_16704(function(_, prevKey)
                while (true) do
                    local key, value = enumerator(_, prevKey);
                    if (key == nil) or (predicate % _M.DOT)(value, key) == true then
                        return key, value;
                    end
                    prevKey = key;
                end
            end); 
                end
            });
            methodGenericsMapping = {['TSource'] = 1};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Where', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 93993440,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TSource']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, source, predicate)
                    if (source == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("source"));
                    end
                    if (predicate == nil) then
                    _M.Throw(((System.Linq.Error % _M.DOT).ArgumentNull_M_0_8736 % _M.DOT)("predicate"));
                    end
                    
            local enumerator = (source % _M.DOT).GetEnumerator();
            return System.Linq.Iterator[{methodGenerics[methodGenericsMapping['TSource']]}]._C_0_16704(function(_, prevKey)
                while (true) do
                    local key, value = enumerator(_, prevKey);
                    if (key == nil) or (predicate % _M.DOT)(value) == true then
                        return key, value;
                    end
                    prevKey = key;
                end
            end); 
                end
            });
            methodGenericsMapping = {['TFirst'] = 1,['TSecond'] = 2,['TResult'] = 3};
            methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'Zip', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 3,
                signatureHash = 252020,
                returnType = function() return System.Collections.Generic.IEnumerable[{methodGenerics[methodGenericsMapping['TResult']]}].__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, first, second, resultSelector)
                    _M.Throw(System.NotImplementedException._C_0_0());
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('System.Linq','Error', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Error','System.Linq', nil, 0, generics, nil, interactionElement, 'Class', 3081);
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                end,
            });
            _M.IM(members, 'ArgumentArrayHasTooManyElements', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0)
                    return System.ArgumentException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("Parameter {0} has too many elements", p0));
                end
            });
            _M.IM(members, 'ArgumentNotIEnumerableGeneric', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0)
                    return System.ArgumentException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("{0} is not IEnumerable<>;", p0));
                end
            });
            _M.IM(members, 'ArgumentNotSequence', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0)
                    return System.ArgumentException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("{0} is not a sequence", p0));
                end
            });
            _M.IM(members, 'ArgumentNotValid', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0)
                    return System.ArgumentException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("Argument {0} is not valid", p0));
                end
            });
            _M.IM(members, 'IncompatibleElementTypes', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Exception.__typeof end,
                func = function(element)
                    return System.ArgumentException._C_0_8736("The two sequences have incompatible element types");
                end
            });
            _M.IM(members, 'ArgumentNotLambda', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0)
                    return System.ArgumentException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("Argument {0} is not a LambdaExpression", p0));
                end
            });
            _M.IM(members, 'MoreThanOneElement', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Exception.__typeof end,
                func = function(element)
                    return System.InvalidOperationException._C_0_8736("Sequence contains more than one element");
                end
            });
            _M.IM(members, 'MoreThanOneMatch', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Exception.__typeof end,
                func = function(element)
                    return System.InvalidOperationException._C_0_8736("Sequence contains more than one matching element");
                end
            });
            _M.IM(members, 'NoArgumentMatchingMethodsInQueryable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0)
                    return System.InvalidOperationException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("There is no method '{0}' on class System.Linq.Enumerable that matches the specified arguments", p0));
                end
            });
            _M.IM(members, 'NoElements', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Exception.__typeof end,
                func = function(element)
                    return System.InvalidOperationException._C_0_8736("Sequence contains no elements");
                end
            });
            _M.IM(members, 'NoMatch', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Exception.__typeof end,
                func = function(element)
                    return System.InvalidOperationException._C_0_8736("Sequence contains no matching element");
                end
            });
            _M.IM(members, 'NoMethodOnType', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 21430,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0, p1)
                    return System.InvalidOperationException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("There is no method '{0}' on type '{1}' that matches the specified arguments", p0, p1));
                end
            });
            _M.IM(members, 'NoMethodOnTypeMatchingArguments', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 21430,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0, p1)
                    return System.InvalidOperationException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("There is no method '{0}' on type '{1}' that matches the specified arguments", p0, p1));
                end
            });
            _M.IM(members, 'NoNameMatchingMethodsInQueryable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, p0)
                    return System.InvalidOperationException._C_0_8736(((element % _M.DOT_LVL(typeObject.Level)).F_M_0_47310 % _M.DOT)("There is no method '{0}' on class System.Linq.Queryable that matches the specified arguments", p0));
                end
            });
            _M.IM(members, 'ArgumentNull', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, paramName)
                    return System.ArgumentNullException._C_0_8736(paramName);
                end
            });
            _M.IM(members, 'ArgumentOutOfRange', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.Exception.__typeof end,
                func = function(element, paramName)
                    return System.ArgumentOutOfRangeException._C_0_8736(paramName);
                end
            });
            _M.IM(members, 'NotImplemented', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Exception.__typeof end,
                func = function(element)
                    return System.NotImplementedException._C_0_0();
                end
            });
            _M.IM(members, 'NotSupported', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Internal',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Exception.__typeof end,
                func = function(element)
                    return System.NotSupportedException._C_0_0();
                end
            });
            _M.IM(members, 'F', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 47310,
                isParams = true,
                returnType = function() return System.String.__typeof end,
                func = function(element, str, firstParam, ...)
                    local args = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});
                    return ((System.String % _M.DOT).Format_M_0_47310 % _M.DOT)(str, args);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
