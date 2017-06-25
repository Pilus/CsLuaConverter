
namespace CsLuaConverterTests
{
    using System.IO;
    using System.Linq;

    using CsLuaSyntaxTranslator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GeneratedLuaMatchingTest
    {
        private readonly string testOutputFolder = Directory.GetCurrentDirectory();

        [TestMethod]
        public void CsLuaMatchesEarlierVersionsOutput()
        {
            var root = this.testOutputFolder.Substring(0, testOutputFolder.IndexOf(@"\CsLuaConverter\CsLuaConverter"));
            var csLuaTestProjectPath = root + "\\CsLuaConverter\\CsLuaProjects\\CsLuaTest\\CsLuaTest.csproj";
            var fileInfo = new FileInfo(csLuaTestProjectPath);

            var namespaceConstructor = new NamespaceConstructor();
            var namespaces = namespaceConstructor.GetNamespacesFromProject(fileInfo.FullName);

            var comparingTextWriter = new ComparingTextWriter(CsLuaCompiled);
            namespaces.First().WritingAction(new IndentedTextWriterWrapper(comparingTextWriter));
            Assert.IsTrue(comparingTextWriter.EndOfExpectedReached(), "The whole expected output have not been written.");
        }

        // Starting expected compiled string @ line 100 to make it easier to see its internal line number





































































    private const string CsLuaCompiled = @"_M.ATN('CsLuaTest','BaseTest', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('BaseTest','CsLuaTest', nil, 0, generics, nil, interactionElement, 'Class', 8101);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.ITestSuite.__typeof);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Tests = _M.DV(System.Collections.Generic.Dictionary[{System.String.__typeof, System.Action.__typeof}].__typeof),
                Name = ""Unnamed"",
            };
            return element;
        end
        staticValues[typeObject.Level] = {
            Output = """",
            ContinueOnError = false,
            TestCount = _M.DV(System.Int32.__typeof),
            FailCount = _M.DV(System.Int32.__typeof),
            IgnoreCount = _M.DV(System.Int32.__typeof),
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Tests == nil) then element[typeObject.Level].Tests = values.Tests; end
            if not(values.Name == nil) then element[typeObject.Level].Name = values.Name; end
            if not(values.Output == nil) then element[typeObject.Level].Output = values.Output; end
            if not(values.ContinueOnError == nil) then element[typeObject.Level].ContinueOnError = values.ContinueOnError; end
            if not(values.TestCount == nil) then element[typeObject.Level].TestCount = values.TestCount; end
            if not(values.FailCount == nil) then element[typeObject.Level].FailCount = values.FailCount; end
            if not(values.IgnoreCount == nil) then element[typeObject.Level].IgnoreCount = values.IgnoreCount; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'Name', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Output', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'ContinueOnError', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'TestCount', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'FailCount', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'IgnoreCount', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'Tests',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.Collections.Generic.Dictionary[{System.String.__typeof, System.Action.__typeof}].__typeof;
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                scope = 'Public',
                func = function(element)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Tests = System.Collections.Generic.Dictionary[{System.String.__typeof, System.Action.__typeof}]._C_0_0();
                end,
            });
            _M.IM(members, 'PerformTests', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 104846,
                func = function(element, lineWriter)
                    ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).Name);
                    (lineWriter % _M.DOT).indent = (lineWriter % _M.DOT).indent + 1;
                    for _,testName in (((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT).Keys % _M.DOT).GetEnumerator() do
                        local test = ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[testName];
                        if ((element % _M.DOT_LVL(typeObject.Level)).ContinueOnError) then
                            _M.Try(
                                function()
                                    (element % _M.DOT_LVL(typeObject.Level)).TestCount = (element % _M.DOT_LVL(typeObject.Level)).TestCount + 1;
                                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                                    (test % _M.DOT)();
                                    ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName +_M.Add+ "" Success"");
                                end,
                                {
                                    {
                                        type = CsLuaTest.TestIgnoredException.__typeof,
                                        func = function(ex)
                                            (element % _M.DOT_LVL(typeObject.Level)).IgnoreCount = (element % _M.DOT_LVL(typeObject.Level)).IgnoreCount + 1;
                                            ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName +_M.Add+ "" Ignored"");
                                        end,
                                    },
                                    {
                                        type = System.Exception.__typeof,
                                        func = function(ex)
                                            (element % _M.DOT_LVL(typeObject.Level)).FailCount = (element % _M.DOT_LVL(typeObject.Level)).FailCount + 1;
                                            ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName +_M.Add+ "" Failed"");
                                            (lineWriter % _M.DOT).indent = (lineWriter % _M.DOT).indent + 1;
                                            for _,errorLine in ((((ex % _M.DOT).Message % _M.DOT).Split_M_0_10374 % _M.DOT)('\n') % _M.DOT).GetEnumerator() do
                                                ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(errorLine);
                                            end
                                            (lineWriter % _M.DOT).indent = (lineWriter % _M.DOT).indent - 1;
                                        end,
                                    },
                                },
                                nil
                            );
                        else
                            (element % _M.DOT_LVL(typeObject.Level)).TestCount = (element % _M.DOT_LVL(typeObject.Level)).TestCount + 1;
                            ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName);
                            ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                            (test % _M.DOT)();
                        end
                    end
                    (lineWriter % _M.DOT).indent = (lineWriter % _M.DOT).indent - 1;
                end
            });
            _M.IM(members, 'ResetOutput', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Protected',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                end
            });
            _M.IM(members, 'Assert', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Protected',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 43270,
                func = function(element, expectedValueObj, actualValueObj, additionalString)
                    local expectedValue = ((Lua.Strings % _M.DOT).tostring % _M.DOT)(expectedValueObj);
                    local actualValue = ((Lua.Strings % _M.DOT).tostring % _M.DOT)(actualValueObj);
                    if (expectedValue ~= actualValue) then
                        _M.Throw(System.Exception._C_0_8736(((Lua.Strings % _M.DOT).format % _M.DOT)(""Incorrect value. Expected: '{0}' got: '{1}'. {2}"", expectedValue, actualValue, additionalString or (System.String % _M.DOT).Empty)));
                    end
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest','CsLuaTest', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('CsLuaTest','CsLuaTest', nil, 0, generics, nil, interactionElement, 'Class', 10407);
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
            _M.IM(members, 'Execute', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local tests = (System.Collections.Generic.List[{CsLuaTest.ITestSuite.__typeof}]._C_0_0() % _M.DOT).__Initialize({
                        CsLuaTest.General.GeneralTests._C_0_0(),
                        CsLuaTest.Namespaces.NamespacesTests._C_0_0(),
                        CsLuaTest.Type.TypeTests._C_0_0(),
                        CsLuaTest.TryCatchFinally.TryCatchFinallyTests._C_0_0(),
                        CsLuaTest.AmbigousMethods.AmbigousMethodsTests._C_0_0(),
                        CsLuaTest.Constructors.ConstructorsTests._C_0_0(),
                        CsLuaTest.Inheritance.InheritanceTests._C_0_0(),
                        CsLuaTest.Override.OverrideTest._C_0_0(),
                        CsLuaTest.Static.StaticTests._C_0_0(),
                        CsLuaTest.Expressions.ExpressionsTests._C_0_0(),
                        CsLuaTest.TypeMethods.TypeMethodsTests._C_0_0(),
                        CsLuaTest.Generics.GenericsTests._C_0_0(),
                        CsLuaTest.DefaultValues.DefaultValuesTests._C_0_0(),
                        CsLuaTest.Arrays.ArraysTests._C_0_0(),
                        CsLuaTest.Serialization.SerializationTests._C_0_0(),
                        CsLuaTest.Interfaces.InterfacesTests._C_0_0(),
                        CsLuaTest.Params.ParamsTests._C_0_0(),
                        CsLuaTest.StringExtensions.StringExtensionTests._C_0_0(),
                        CsLuaTest.Wrap.WrapTests._C_0_0(),
                        CsLuaTest.Collections.CollectionsTests._C_0_0(),
                        CsLuaTest.Statements.StatementsTests._C_0_0(),
                        CsLuaTest.ActivatorImplementation.ActivatorTests._C_0_0(),
                        CsLuaTest.ActionsFunctions.ActionsFunctionsTests._C_0_0(),
                        CsLuaTest.Linq.LinqTests._C_0_0()
                    });
                    ((tests % _M.DOT).ForEach_M_0_239752368 % _M.DOT)(System.Action[{CsLuaTest.ITestSuite.__typeof}]._C_0_16704(function(test) return ((test % _M.DOT).PerformTests_M_0_104846 % _M.DOT)(CsLuaTest.IndentedLineWriter._C_0_0()) end));
                    ((Lua.Core % _M.DOT).print % _M.DOT)(""CsLua test completed."");
                    ((Lua.Core % _M.DOT).print % _M.DOT)((CsLuaTest.BaseTest % _M.DOT).TestCount, ""tests run."", (CsLuaTest.BaseTest % _M.DOT).FailCount, ""failed."", (CsLuaTest.BaseTest % _M.DOT).TestCount - (CsLuaTest.BaseTest % _M.DOT).FailCount - (CsLuaTest.BaseTest % _M.DOT).IgnoreCount, ""succeded. "", (CsLuaTest.BaseTest % _M.DOT).IgnoreCount, ""ignored."");
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest','IndentedLineWriter', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IndentedLineWriter','CsLuaTest', nil, 0, generics, nil, interactionElement, 'Class', 52423);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                indent = _M.DV(System.Int32.__typeof),
                indentChar = ""   "",
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.indent == nil) then element[typeObject.Level].indent = values.indent; end
            if not(values.indentChar == nil) then element[typeObject.Level].indentChar = values.indentChar; end
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
            _M.IM(members, 'indent', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'indentChar', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'WriteLine', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                func = function(element, line)
                    ((Lua.Core % _M.DOT).print % _M.DOT)(((Lua.Strings % _M.DOT).strrep % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).indentChar, (element % _M.DOT_LVL(typeObject.Level)).indent) +_M.Add+ line);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest','ITestSuite', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ITestSuite','CsLuaTest', nil, 0, generics, nil, interactionElement, 'Interface',13644);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'PerformTests', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 104846,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest','TestIgnoredException', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('TestIgnoredException','CsLuaTest', nil, 0, generics, nil, interactionElement, 'Class', 67539);
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736(""Test ignored."");
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.ActionsFunctions','ActionsFunctionsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ActionsFunctionsTests','CsLuaTest.ActionsFunctions', nil, 0, generics, nil, interactionElement, 'Class', 76716);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Actions and Functions"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestActionGenericsFromConstructed""] = (element % _M.DOT_LVL(typeObject.Level)).TestActionGenericsFromConstructed;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""OtherTests""] = (element % _M.DOT_LVL(typeObject.Level)).OtherTests;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestActionWithAssignment""] = (element % _M.DOT_LVL(typeObject.Level)).TestActionWithAssignment;
                end,
            });
            _M.IM(members, 'TestActionGenericsFromConstructed', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local invokedValue = 0;
                    local action = System.Action[{System.Int32.__typeof}]._C_0_34493836(System.Action[{System.Int32.__typeof}]._C_0_16704(function(i)
                        invokedValue = i;
                    end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Action[{System.Int32.__typeof}].__is(action));
                    ((action % _M.DOT).Invoke % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, invokedValue);
                    (action % _M.DOT)(10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(10, invokedValue);
                end
            });
            _M.IM(members, 'OtherTests', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local r1 = ((CsLuaTest.ActionsFunctions.StaticClass % _M.DOT).ExpectFunc_M_0_59060040 % _M.DOT)(System.Func[{System.Int32.__typeof, System.String.__typeof}]._C_0_16704(function(v) return ((v % _M.DOT).ToString_M_0_0 % _M.DOT)() end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""int"", r1);
                    local r2 = ((CsLuaTest.ActionsFunctions.StaticClass % _M.DOT).ExpectFunc_M_0_75172368 % _M.DOT)(System.Func[{System.Object.__typeof, System.String.__typeof}]._C_0_16704(function(v) return ((v % _M.DOT).ToString_M_0_0 % _M.DOT)() end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""obj"", r2);
                    local r3 = ((CsLuaTest.ActionsFunctions.StaticClass % _M.DOT).ExpectFunc_M_0_74961534 % _M.DOT)(System.Func[{System.Single.__typeof, System.String.__typeof}]._C_0_16704(function(v) return ((v % _M.DOT).ToString_M_0_0 % _M.DOT)() end), true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""float"", r3);
                end
            });
            _M.IM(members, 'TestActionWithAssignment', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local value = 0;
                    ((CsLuaTest.ActionsFunctions.StaticClass % _M.DOT).ExpectAction_M_0_34493836 % _M.DOT)(System.Action[{System.Int32.__typeof}]._C_0_16704(function(v)value = v end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, value);
                end
            });
            _M.IM(members, 'TestMethodsCastToActionAndFunction', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    _M.Throw(CsLuaTest.TestIgnoredException._C_0_0());
                    local mc = CsLuaTest.ActionsFunctions.ClassWithMethods._C_0_0();
                    local action1 = (mc % _M.DOT).MethodWithNoReturn;
                    local action1Type = ((action1 % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(System.Action[{System.Int32.__typeof}].__typeof, action1Type);
                    local func1 = (mc % _M.DOT).MethodWithReturn;
                    local func1Type = ((func1 % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(System.Func[{System.Int32.__typeof, System.String.__typeof}].__typeof, func1Type);
                    local value1 = (func1 % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""43"", value1);
                    local genericMethod = (mc % _M.DOT).MethodWithReturnAndGeneric;
                    local genericFuncType = ((genericMethod % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(System.Func[{System.Double.__typeof, System.String.__typeof}].__typeof, genericFuncType);
                    local valueGeneric = (genericMethod % _M.DOT)(43.03);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""43.03"", valueGeneric);
                    local action2 = (CsLuaTest.ActionsFunctions.ClassWithMethods % _M.DOT).StaticMethodWithNoReturn;
                    local action2Type = ((action2 % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(System.Action[{System.Int32.__typeof}].__typeof, action2Type);
                    local func2 = (CsLuaTest.ActionsFunctions.ClassWithMethods % _M.DOT).StaticMethodWithReturn;
                    local func2Type = ((func2 % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(System.Func[{System.Int32.__typeof, System.String.__typeof}].__typeof, func2Type);
                    local value2 = (func2 % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""43"", value2);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.ActionsFunctions','ClassWithMethods', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithMethods','CsLuaTest.ActionsFunctions', nil, 0, generics, nil, interactionElement, 'Class', 40013);
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
            _M.IM(members, 'MethodWithNoReturn', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, value)
                end
            });
            _M.IM(members, 'MethodWithReturn', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                returnType = function() return System.String.__typeof end,
                func = function(element, value)
                    return ((value % _M.DOT).ToString_M_0_0 % _M.DOT)();
                end
            });
            local methodGenericsMapping = {['T'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'MethodWithReturnAndGeneric', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 2,
                returnType = function() return System.String.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, value)
                    return ((value % _M.DOT).ToString_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'StaticMethodWithNoReturn', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, value)
                end
            });
            _M.IM(members, 'StaticMethodWithReturn', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                returnType = function() return System.String.__typeof end,
                func = function(element, value)
                    return ((value % _M.DOT).ToString_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.ActionsFunctions','StaticClass', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('StaticClass','CsLuaTest.ActionsFunctions', nil, 0, generics, nil, interactionElement, 'Class', 16575);
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
            _M.IM(members, 'ExpectFunc', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 75172368,
                returnType = function() return System.String.__typeof end,
                func = function(element, f)
                    return ""obj"";
                end
            });
            _M.IM(members, 'ExpectFunc', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 59060040,
                returnType = function() return System.String.__typeof end,
                func = function(element, f)
                    return ""int"";
                end
            });
            _M.IM(members, 'ExpectFunc', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 74961534,
                returnType = function() return System.String.__typeof end,
                func = function(element, f, extra)
                    return ""float"";
                end
            });
            local methodGenericsMapping = {['T'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ReturnInput', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 2,
                returnType = function() return methodGenerics[methodGenericsMapping['T']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, input)
                    return input;
                end
            });
            _M.IM(members, 'ExpectAction', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 34493836,
                func = function(element, a)
                    (a % _M.DOT)(43);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.ActivatorImplementation','ActivatorTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ActivatorTests','CsLuaTest.ActivatorImplementation', nil, 0, generics, nil, interactionElement, 'Class', 30527);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Activator"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestCreateInstance""] = (element % _M.DOT_LVL(typeObject.Level)).TestCreateInstance;
                end,
            });
            _M.IM(members, 'TestCreateInstance', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local type = CsLuaTest.ActivatorImplementation.Class1.__typeof;
                    local value1 = ((System.Activator % _M.DOT).CreateInstance_M_0_3596 % _M.DOT)(type);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.ActivatorImplementation.Class1.__is(value1));
                    local value2 = ((System.Activator % _M.DOT).CreateInstance_M_1_0[{CsLuaTest.ActivatorImplementation.Class1.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.ActivatorImplementation.Class1.__is(value2));
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.ActivatorImplementation','Class1', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Class1','CsLuaTest.ActivatorImplementation', nil, 0, generics, nil, interactionElement, 'Class', 3650);
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','AmbigousMethodsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('AmbigousMethodsTests','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 68195);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""AmbigousMethods"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbiguousMethodWith1Arg""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbiguousMethodWith1Arg;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbiguousMethodWith1ArgAndObject""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbiguousMethodWith1ArgAndObject;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbiguousMethodWith1ArgAndClass""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbiguousMethodWith1ArgAndClass;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbiguousMethodWithInterface""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbiguousMethodWithInterface;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbiguousMethodWithInterfaceAndTwoArgs""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbiguousMethodWithInterfaceAndTwoArgs;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbiguousMethodWithInheritance""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbiguousMethodWithInheritance;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestNullPickingCorrectMethods""] = (element % _M.DOT_LVL(typeObject.Level)).TestNullPickingCorrectMethods;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbigousMethodsWithEnum""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbigousMethodsWithEnum;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAmbigousMethodsWithGenerics""] = (element % _M.DOT_LVL(typeObject.Level)).TestAmbigousMethodsWithGenerics;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestNumberMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestNumberMethod;
                end,
            });
            _M.IM(members, 'TestAmbiguousMethodWith1Arg', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).OneArg_M_0_3926 % _M.DOT)(0);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArg_Int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArg_M_0_8736 % _M.DOT)(""Test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArg_String"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestAmbiguousMethodWith1ArgAndObject', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).OneArgWithObj_M_0_3926 % _M.DOT)(0);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArgWithObj_Int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArgWithObj_M_0_8572 % _M.DOT)(""Test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArgWithObj_Object"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestAmbiguousMethodWith1ArgAndClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).OneArgWithClass_M_0_3926 % _M.DOT)(0);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArgWithClass_Int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArgWithClass_M_0_7716 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassA._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArgWithClass_ClassA"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestAmbiguousMethodWithInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).OneArgWithInterface_M_0_24220 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassB1._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArgWithInterface_InterfaceB"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArgWithInterface_M_0_9442 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassB2._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArgWithInterface_ClassB2"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestAmbiguousMethodWithInterfaceAndTwoArgs', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).TwoArgsWithInterface_M_0_38383 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassB1._C_0_0(), CsLuaTest.AmbigousMethods.ClassB2._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OneArgWithInterface_InterfaceBClassB2"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestAmbiguousMethodWithInheritance', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassC2._C_0_0();
                    ((theClass % _M.DOT).Method_M_0_8736 % _M.DOT)(""x"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Method_string"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).Method_M_0_3926 % _M.DOT)(10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Method_int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).Method_M_0_12036 % _M.DOT)(true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Method_bool"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestNullPickingCorrectMethods', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).NullPicking1_M_0_24220 % _M.DOT)(nil);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""NullPicking1_InterfaceB"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestAmbigousMethodsWithEnum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithEnumMethod._C_0_0();
                    ((theClass % _M.DOT).EnumMethod_M_0_5062 % _M.DOT)((CsLuaTest.AmbigousMethods.EnumA % _M.DOT).Value1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""MethodEnumA"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).EnumMethod_M_0_5084 % _M.DOT)((CsLuaTest.AmbigousMethods.EnumB % _M.DOT).Something);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""MethodEnumB"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestAmbigousMethodsWithGenerics', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).GenericPicking_M_0_1102377240 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassWithGenerics[{System.Boolean.__typeof}]._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericPicking_bool"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).GenericPicking_M_0_359582340 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassWithGenerics[{System.Int32.__typeof}]._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericPicking_int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestNumberMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.AmbigousMethods.ClassWithAmbigousMethods._C_0_0();
                    ((theClass % _M.DOT).GenericPickingNumber_M_0_8482 % _M.DOT)(4);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericPickingNumber_double"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).GenericPickingNumber_M_0_8482 % _M.DOT)(4.5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericPickingNumber_double"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassA', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassA','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 3858);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Ok = 0,
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Ok == nil) then element[typeObject.Level].Ok = values.Ok; end
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
            _M.IM(members, 'Ok', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassB1', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassB1','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 4704);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.AmbigousMethods.InterfaceB.__typeof);
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassB2', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassB2','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 4721);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.AmbigousMethods.InterfaceB.__typeof);
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassC1', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassC1','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 4717);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.AmbigousMethods.ClassCBase.__meta(staticValues);
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
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""Method_int"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassC2', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassC2','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 4734);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.AmbigousMethods.ClassC1.__meta(staticValues);
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
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""Method_string"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassCBase', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassCBase','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 12423);
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
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 12036,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""Method_bool"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassWithAmbigousMethods', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithAmbigousMethods','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 100948);
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
            _M.IM(members, 'OneArg', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArg_Int"";
                end
            });
            _M.IM(members, 'OneArg', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArg_String"";
                end
            });
            _M.IM(members, 'OneArgWithObj', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithObj_Int"";
                end
            });
            _M.IM(members, 'OneArgWithObj', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8572,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithObj_Object"";
                end
            });
            _M.IM(members, 'OneArgWithClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithClass_Int"";
                end
            });
            _M.IM(members, 'OneArgWithClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 7716,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithClass_ClassA"";
                end
            });
            _M.IM(members, 'OneArgWithClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8572,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithClass_Object"";
                end
            });
            _M.IM(members, 'OneArgWithInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 24220,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithInterface_InterfaceB"";
                end
            });
            _M.IM(members, 'OneArgWithInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 9442,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithInterface_ClassB2"";
                end
            });
            _M.IM(members, 'TwoArgsWithInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 38383,
                func = function(element, x, y)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithInterface_InterfaceBClassB2"";
                end
            });
            _M.IM(members, 'TwoArgsWithInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 60550,
                func = function(element, x, y)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OneArgWithInterface_InterfaceBInterfaceB"";
                end
            });
            _M.IM(members, 'NullPicking1', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 24220,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""NullPicking1_InterfaceB"";
                end
            });
            _M.IM(members, 'NullPicking1', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8572,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""NullPicking1_object"";
                end
            });
            _M.IM(members, 'GenericPicking', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 359582340,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""GenericPicking_int"";
                end
            });
            _M.IM(members, 'GenericPicking', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 1102377240,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""GenericPicking_bool"";
                end
            });
            _M.IM(members, 'GenericPickingNumber', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8482,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""GenericPickingNumber_double"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassWithEnumMethod', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithEnumMethod','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Class', 58547);
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
            _M.IM(members, 'EnumMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 5062,
                func = function(element, val)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""MethodEnumA"";
                end
            });
            _M.IM(members, 'EnumMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 5084,
                func = function(element, val)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""MethodEnumB"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','ClassWithGenerics', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('ClassWithGenerics','CsLuaTest.AmbigousMethods', nil, 1, generics, nil, interactionElement, 'Class', (91590*generics[genericsMapping['T']].signatureHash));
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.AmbigousMethods','EnumA', _M.NE({
    [0] = _M.EN({
        __default = ""Value1"",
        [""Value1""] = ""Value1"",
        [""Value2""] = ""Value2""
    },'EnumA','CsLuaTest.AmbigousMethods',2531),
}));
_M.ATN('CsLuaTest.AmbigousMethods','EnumB', _M.NE({
    [0] = _M.EN({
        __default = ""Something"",
        [""Something""] = ""Something"",
        [""SomethingElse""] = ""SomethingElse""
    },'EnumB','CsLuaTest.AmbigousMethods',2542),
}));
_M.ATN('CsLuaTest.AmbigousMethods','InterfaceB', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('InterfaceB','CsLuaTest.AmbigousMethods', nil, 0, generics, nil, interactionElement, 'Interface',12110);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Arrays','AClass', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('AClass','CsLuaTest.Arrays', nil, 1, generics, nil, interactionElement, 'Class', (8620*generics[genericsMapping['T']].signatureHash));
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(generics[genericsMapping['T']]),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
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
            _M.IM(members, 'Value', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Arrays','ArraysTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ArraysTests','CsLuaTest.Arrays', nil, 0, generics, nil, interactionElement, 'Class', 17468);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Arrays"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ArrayImplementsInterfaces""] = (element % _M.DOT_LVL(typeObject.Level)).ArrayImplementsInterfaces;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ArrayInitializationAndAmbigurity""] = (element % _M.DOT_LVL(typeObject.Level)).ArrayInitializationAndAmbigurity;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ArraysAsMethodArgument""] = (element % _M.DOT_LVL(typeObject.Level)).ArraysAsMethodArgument;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ArrayImplementedWithSpecificLength""] = (element % _M.DOT_LVL(typeObject.Level)).ArrayImplementedWithSpecificLength;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ArrayImplementedWithSpecificLengthInClass""] = (element % _M.DOT_LVL(typeObject.Level)).ArrayImplementedWithSpecificLengthInClass;
                end,
            });
            _M.IM(members, 'ArrayImplementsInterfaces', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.String.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = ""a""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Array[{System.String.__typeof}].__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Array.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.IList.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IList[{System.String.__typeof}].__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.ICollection.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.ICollection[{System.String.__typeof}].__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.IEnumerable.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IEnumerable[{System.String.__typeof}].__is(a));
                end
            });
            _M.IM(members, 'ArrayInitializationAndAmbigurity', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local arrayClass = CsLuaTest.Arrays.ClassWithArrays._C_0_0();
                    local a1 = (System.Array[{System.String.__typeof}]._C_0_0() % _M.DOT).__Initialize({});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""string"", ((arrayClass % _M.DOT).TypeDependent_M_0_26208 % _M.DOT)(a1));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (a1 % _M.DOT).Length);
                    local a2 = (System.Array[{System.String.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = ""abc"", ""def""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""string"", ((arrayClass % _M.DOT).TypeDependent_M_0_26208 % _M.DOT)(a2));
                    local a3 = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 1, 3});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""int"", ((arrayClass % _M.DOT).TypeDependent_M_0_11778 % _M.DOT)(a3));
                    local a3b = (System.Array[{System.Double.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 1.1, 3.2});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""double"", ((arrayClass % _M.DOT).TypeDependent_M_0_25446 % _M.DOT)(a3b));
                    local a4 = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = true, 1, ""ok""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""object"", ((arrayClass % _M.DOT).TypeDependent_M_0_25716 % _M.DOT)(a4));
                    local a5 = (System.Array[{CsLuaTest.Arrays.AClass[{System.Int32.__typeof}].__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = (CsLuaTest.Arrays.AClass[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({Value = 4}), (CsLuaTest.Arrays.AClass[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({Value = 6})});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Aint"", ((arrayClass % _M.DOT).TypeDependent_M_0_101526360 % _M.DOT)(a5));
                    local a6 = (System.Array[{CsLuaTest.Arrays.AClass[{System.String.__typeof}].__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = CsLuaTest.Arrays.AClass[{System.String.__typeof}]._C_0_0()});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Astring"", ((arrayClass % _M.DOT).TypeDependent_M_0_225912960 % _M.DOT)(a6));
                    local a7 = (System.Array[{CsLuaTest.Arrays.AClass[{System.String.__typeof}].__typeof}]._C_0_0() % _M.DOT).__Initialize({});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Astring"", ((arrayClass % _M.DOT).TypeDependent_M_0_225912960 % _M.DOT)(a7));
                end
            });
            _M.IM(members, 'ArraysAsMethodArgument', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local arrayClass = CsLuaTest.Arrays.ClassWithArrays._C_0_0();
                    local array = (System.Array[{System.String.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = ""abc"", ""def""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((arrayClass % _M.DOT).GetLengthOfStringArray_M_0_26208 % _M.DOT)(array));
                end
            });
            _M.IM(members, 'ArrayImplementedWithSpecificLength', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local array = (System.Array[{System.String.__typeof}]._C_0_2112(4) % _M.DOT);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(4, (array % _M.DOT).Length);
                    (array % _M.DOT)[2] = ""ok"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""ok"", (array % _M.DOT)[2]);
                end
            });
            _M.IM(members, 'ArrayImplementedWithSpecificLengthInClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Arrays.ClassWithPredefinedArray._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, ((c % _M.DOT).Array1 % _M.DOT).Length);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, ((c % _M.DOT).Array2 % _M.DOT).Length);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Arrays','ClassWithArrays', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithArrays','CsLuaTest.Arrays', nil, 0, generics, nil, interactionElement, 'Class', 34747);
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
            _M.IM(members, 'GetLengthOfStringArray', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 26208,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, args)
                    return (args % _M.DOT).Length;
                end
            });
            _M.IM(members, 'TypeDependent', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 25716,
                returnType = function() return System.String.__typeof end,
                func = function(element, args)
                    return ""object"";
                end
            });
            _M.IM(members, 'TypeDependent', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 26208,
                returnType = function() return System.String.__typeof end,
                func = function(element, args)
                    return ""string"";
                end
            });
            _M.IM(members, 'TypeDependent', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 11778,
                returnType = function() return System.String.__typeof end,
                func = function(element, args)
                    return ""int"";
                end
            });
            _M.IM(members, 'TypeDependent', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 12036,
                returnType = function() return System.String.__typeof end,
                func = function(element, args)
                    return ""long"";
                end
            });
            _M.IM(members, 'TypeDependent', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 25446,
                returnType = function() return System.String.__typeof end,
                func = function(element, args)
                    return ""double"";
                end
            });
            _M.IM(members, 'TypeDependent', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 101526360,
                returnType = function() return System.String.__typeof end,
                func = function(element, args)
                    return ""Aint"";
                end
            });
            _M.IM(members, 'TypeDependent', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 225912960,
                returnType = function() return System.String.__typeof end,
                func = function(element, args)
                    return ""Astring"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Arrays','ClassWithPredefinedArray', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithPredefinedArray','CsLuaTest.Arrays', nil, 0, generics, nil, interactionElement, 'Class', 99032);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Array1 = (System.Array[{System.String.__typeof}]._C_0_2112(3) % _M.DOT),
                Array2 = (System.Array[{System.String.__typeof}]._C_0_2112((element % _M.DOT_LVL(typeObject.Level)).Array2Size) % _M.DOT),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
            Array2Size = 5,
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Array2Size == nil) then element[typeObject.Level].Array2Size = values.Array2Size; end
            if not(values.Array1 == nil) then element[typeObject.Level].Array1 = values.Array1; end
            if not(values.Array2 == nil) then element[typeObject.Level].Array2 = values.Array2; end
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
            _M.IM(members, 'Array2Size', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'Array1', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Array2', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Collections','CollectionsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('CollectionsTests','CsLuaTest.Collections', nil, 0, generics, nil, interactionElement, 'Class', 41177);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Collections"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestListInterfaces""] = (element % _M.DOT_LVL(typeObject.Level)).TestListInterfaces;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestListImplementation""] = (element % _M.DOT_LVL(typeObject.Level)).TestListImplementation;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestDictionaryInterfaces""] = (element % _M.DOT_LVL(typeObject.Level)).TestDictionaryInterfaces;
                end,
            });
            _M.IM(members, 'TestListInterfaces', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local list = System.Collections.Generic.List[{System.Int32.__typeof}]._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.IList.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IList[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.ICollection.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.ICollection[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.IEnumerable.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IEnumerable[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IReadOnlyList[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IReadOnlyCollection[{System.Int32.__typeof}].__is(list));
                end
            });
            _M.IM(members, 'TestListImplementation', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local list = System.Collections.Generic.List[{System.Int32.__typeof}]._C_0_0();
                    local iList = list;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (list % _M.DOT).Capacity);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (list % _M.DOT).Count);
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(4, (list % _M.DOT).Capacity);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, (iList % _M.DOT).IsFixedSize);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, (iList % _M.DOT).IsReadOnly);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, (iList % _M.DOT).IsSynchronized);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, (iList % _M.DOT).SyncRoot == nil);
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((iList % _M.DOT).Add_M_0_8572 % _M.DOT)(50));
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(75);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(4, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, (list % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, (list % _M.DOT)[1]);
                    (list % _M.DOT)[1] = 6;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (list % _M.DOT)[1]);
                    _M.Try(
                        function()
                            local x = (list % _M.DOT)[-1];
                            _M.Throw(System.Exception._C_0_8736(""Expected IndexOutOfRangeException""));
                        end,
                        {
                            {
                                type = System.ArgumentOutOfRangeException.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                    _M.Try(
                        function()
                            (list % _M.DOT)[4] = 10;
                            _M.Throw(System.Exception._C_0_8736(""Expected IndexOutOfRangeException""));
                        end,
                        {
                            {
                                type = System.ArgumentOutOfRangeException.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                    local verificationList = System.Collections.Generic.List[{System.Int32.__typeof}]._C_0_0();
                    for _,item in (list % _M.DOT).GetEnumerator() do
                        ((verificationList % _M.DOT).Add_M_0_3926 % _M.DOT)(item);
                    end
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((list % _M.DOT).Count, (verificationList % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((list % _M.DOT)[0], (verificationList % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((list % _M.DOT)[1], (verificationList % _M.DOT)[1]);
                    local list2 = System.Collections.Generic.List[{System.Int32.__typeof}]._C_0_129809264((System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 7, 9, 13}));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, (list2 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(7, (list2 % _M.DOT)[0]);
                    ((list2 % _M.DOT).AddRange_M_0_129809264 % _M.DOT)((System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 21, 28}));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, (list2 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(21, (list2 % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(28, (list2 % _M.DOT)[4]);
                    ((list2 % _M.DOT).Clear_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (list2 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((list % _M.DOT).Contains_M_0_3926 % _M.DOT)(6));
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(6);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, ((list % _M.DOT).Find_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, ((list % _M.DOT).FindIndex_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, ((list % _M.DOT).FindLast_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(4, ((list % _M.DOT).FindLastIndex_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    local all = ((list % _M.DOT).FindAll_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, (all % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (all % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (all % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, ((list % _M.DOT).IndexOf_M_0_3926 % _M.DOT)(6));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(-1, ((list % _M.DOT).IndexOf_M_0_3926 % _M.DOT)(500));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(4, ((list % _M.DOT).LastIndexOf_M_0_3926 % _M.DOT)(6));
                    ((list % _M.DOT).Insert_M_0_9815 % _M.DOT)(1, 24);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(24, (list % _M.DOT)[1]);
                    local res = ((list % _M.DOT).GetRange_M_0_9815 % _M.DOT)(1, 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, (res % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(24, (res % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (res % _M.DOT)[1]);
                    ((list % _M.DOT).InsertRange_M_0_194717822 % _M.DOT)(1, (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 110, 120}));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(8, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(110, (list % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(120, (list % _M.DOT)[2]);
                    ((list % _M.DOT).RemoveRange_M_0_9815 % _M.DOT)(2, 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(110, (list % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (list % _M.DOT)[2]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(50, (list % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((list % _M.DOT).Remove_M_0_3926 % _M.DOT)(50));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, ((list % _M.DOT).Remove_M_0_3926 % _M.DOT)(50));
                end
            });
            _M.IM(members, 'TestDictionaryInterfaces', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local list = System.Collections.Generic.Dictionary[{System.Int32.__typeof, System.String.__typeof}]._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.IDictionary.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IDictionary[{System.Int32.__typeof, System.String.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.ICollection.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.ICollection[{System.Collections.Generic.KeyValuePair[{System.Int32.__typeof, System.String.__typeof}].__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.IEnumerable.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IEnumerable[{System.Collections.Generic.KeyValuePair[{System.Int32.__typeof, System.String.__typeof}].__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IReadOnlyDictionary[{System.Int32.__typeof, System.String.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.IReadOnlyCollection[{System.Collections.Generic.KeyValuePair[{System.Int32.__typeof, System.String.__typeof}].__typeof}].__is(list));
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Constructors','Class1', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Class1','CsLuaTest.Constructors', nil, 0, generics, nil, interactionElement, 'Class', 3650);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'Value', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Value = ""null"";
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, val)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Value = ""str"" +_M.Add+ val;
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                scope = 'Public',
                func = function(element, val)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Value = ""int"" +_M.Add+ val;
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8572,
                scope = 'Public',
                func = function(element, val)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Value = ""object"" +_M.Add+ val;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Constructors','Class2', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Class2','CsLuaTest.Constructors', nil, 0, generics, nil, interactionElement, 'Class', 3663);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Result = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Result == nil) then element[typeObject.Level].Result = values.Result; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'Result', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, s1)
                    (element % _M.DOT_LVL(typeObject.Level))._C_0_21840(""this1"", ""this2"");
                    (element % _M.DOT_LVL(typeObject.Level)).Result = (element % _M.DOT_LVL(typeObject.Level)).Result +_M.Add+ s1;
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 21840,
                scope = 'Public',
                func = function(element, s1, s2)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Result = (element % _M.DOT_LVL(typeObject.Level)).Result +_M.Add+ s1 +_M.Add+ s2;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Constructors','Class3', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('Class3','CsLuaTest.Constructors', nil, 1, generics, nil, interactionElement, 'Class', (7352*generics[genericsMapping['T']].signatureHash));
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Str = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Str == nil) then element[typeObject.Level].Str = values.Str; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'Str', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T']].signatureHash),
                scope = 'Public',
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level))['_C_0_'..(13104+(2*generics[genericsMapping['T']].signatureHash))](value, ""abc"");
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 13104+(2*generics[genericsMapping['T']].signatureHash),
                scope = 'Public',
                func = function(element, value, additional)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Str = additional;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Constructors','ConstructorsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ConstructorsTests','CsLuaTest.Constructors', nil, 0, generics, nil, interactionElement, 'Class', 48251);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Constructors"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestConstructorWithNoArgs""] = (element % _M.DOT_LVL(typeObject.Level)).TestConstructorWithNoArgs;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestConstructorWithAmbArgs""] = (element % _M.DOT_LVL(typeObject.Level)).TestConstructorWithAmbArgs;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestConstructorCallingOtherConstructor""] = (element % _M.DOT_LVL(typeObject.Level)).TestConstructorCallingOtherConstructor;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestConstructorCallingOtherConstructorWithGenericInSignatureHash""] = (element % _M.DOT_LVL(typeObject.Level)).TestConstructorCallingOtherConstructorWithGenericInSignatureHash;
                end,
            });
            _M.IM(members, 'TestConstructorWithNoArgs', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Constructors.Class1._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""null"", (c % _M.DOT).Value);
                end
            });
            _M.IM(members, 'TestConstructorWithAmbArgs', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c1 = CsLuaTest.Constructors.Class1._C_0_8736(""Test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""strTest"", (c1 % _M.DOT).Value);
                    local c2 = CsLuaTest.Constructors.Class1._C_0_3926(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""int43"", (c2 % _M.DOT).Value);
                    local c3 = CsLuaTest.Constructors.Class1._C_0_8572(43.7);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""object43.7"", (c3 % _M.DOT).Value);
                end
            });
            _M.IM(members, 'TestConstructorCallingOtherConstructor', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Constructors.Class2._C_0_8736(""abc"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""this1this2abc"", (c % _M.DOT).Result);
                end
            });
            _M.IM(members, 'TestConstructorCallingOtherConstructorWithGenericInSignatureHash', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Constructors.Class3[{System.Int32.__typeof}]._C_0_3926(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abc"", (c % _M.DOT).Str);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.DefaultValues','AnEnum', _M.NE({
    [0] = _M.EN({
        __default = ""Something"",
        [""Something""] = ""Something"",
        [""SomethingElse""] = ""SomethingElse"",
        [""AValueWithA""] = ""AValueWithA""
    },'AnEnum','CsLuaTest.DefaultValues',4279),
}));
_M.ATN('CsLuaTest.DefaultValues','DefaultValuesClass', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('DefaultValuesClass','CsLuaTest.DefaultValues', nil, 0, generics, nil, interactionElement, 'Class', 52265);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Int = _M.DV(System.Int32.__typeof),
                String = _M.DV(System.String.__typeof),
                AnAction = _M.DV(System.Action.__typeof),
                AFunc = _M.DV(System.Func[{System.Int32.__typeof}].__typeof),
                AClass = _M.DV(CsLuaTest.DefaultValues.ReferencedClass.__typeof),
                Enum = _M.DV(CsLuaTest.DefaultValues.AnEnum.__typeof),
                Bool = _M.DV(System.Boolean.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
            StaticInt = _M.DV(System.Int32.__typeof),
            StaticString = _M.DV(System.String.__typeof),
            StaticAction = _M.DV(System.Action.__typeof),
            StaticFunc = _M.DV(System.Func[{System.Int32.__typeof}].__typeof),
            StaticClass = _M.DV(CsLuaTest.DefaultValues.ReferencedClass.__typeof),
            StaticEnum = _M.DV(CsLuaTest.DefaultValues.AnEnum.__typeof),
            StaticBool = _M.DV(System.Boolean.__typeof),
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.StaticInt == nil) then element[typeObject.Level].StaticInt = values.StaticInt; end
            if not(values.StaticString == nil) then element[typeObject.Level].StaticString = values.StaticString; end
            if not(values.StaticAction == nil) then element[typeObject.Level].StaticAction = values.StaticAction; end
            if not(values.StaticFunc == nil) then element[typeObject.Level].StaticFunc = values.StaticFunc; end
            if not(values.StaticClass == nil) then element[typeObject.Level].StaticClass = values.StaticClass; end
            if not(values.StaticEnum == nil) then element[typeObject.Level].StaticEnum = values.StaticEnum; end
            if not(values.StaticBool == nil) then element[typeObject.Level].StaticBool = values.StaticBool; end
            if not(values.Int == nil) then element[typeObject.Level].Int = values.Int; end
            if not(values.String == nil) then element[typeObject.Level].String = values.String; end
            if not(values.AnAction == nil) then element[typeObject.Level].AnAction = values.AnAction; end
            if not(values.AFunc == nil) then element[typeObject.Level].AFunc = values.AFunc; end
            if not(values.AClass == nil) then element[typeObject.Level].AClass = values.AClass; end
            if not(values.Enum == nil) then element[typeObject.Level].Enum = values.Enum; end
            if not(values.Bool == nil) then element[typeObject.Level].Bool = values.Bool; end
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
            _M.IM(members, 'StaticInt', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'StaticString', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'StaticAction', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'StaticFunc', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'StaticClass', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'StaticEnum', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'StaticBool', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'Int', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'String', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'AnAction', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'AFunc', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'AClass', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Enum', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Bool', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.DefaultValues','DefaultValuesTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('DefaultValuesTests','CsLuaTest.DefaultValues', nil, 0, generics, nil, interactionElement, 'Class', 53680);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""DefaultValues"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""DefaultInt""] = (element % _M.DOT_LVL(typeObject.Level)).DefaultInt;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""DefaultBool""] = (element % _M.DOT_LVL(typeObject.Level)).DefaultBool;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""DefaultString""] = (element % _M.DOT_LVL(typeObject.Level)).DefaultString;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""DefaultEnum""] = (element % _M.DOT_LVL(typeObject.Level)).DefaultEnum;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""DefaultAction""] = (element % _M.DOT_LVL(typeObject.Level)).DefaultAction;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""DefaultFunc""] = (element % _M.DOT_LVL(typeObject.Level)).DefaultFunc;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""DefaultClass""] = (element % _M.DOT_LVL(typeObject.Level)).DefaultClass;
                end,
            });
            _M.IM(members, 'DefaultInt', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticInt);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).Int);
                end
            });
            _M.IM(members, 'DefaultBool', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticBool);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).Bool);
                end
            });
            _M.IM(members, 'DefaultString', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticString);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).String);
                end
            });
            _M.IM(members, 'DefaultEnum', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((CsLuaTest.DefaultValues.AnEnum % _M.DOT).Something, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticEnum);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((CsLuaTest.DefaultValues.AnEnum % _M.DOT).Something, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).Enum);
                end
            });
            _M.IM(members, 'DefaultAction', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticAction);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).AnAction);
                end
            });
            _M.IM(members, 'DefaultFunc', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticFunc);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).AFunc);
                end
            });
            _M.IM(members, 'DefaultClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticClass);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).AClass);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.DefaultValues','ReferencedClass', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ReferencedClass','CsLuaTest.DefaultValues', nil, 0, generics, nil, interactionElement, 'Class', 33625);
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Expressions','ExpressionsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ExpressionsTests','CsLuaTest.Expressions', nil, 0, generics, nil, interactionElement, 'Class', 41461);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Expressions"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""BinaryExpressionsTest""] = (element % _M.DOT_LVL(typeObject.Level)).BinaryExpressionsTest;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""AssignmentExpressionTest""] = (element % _M.DOT_LVL(typeObject.Level)).AssignmentExpressionTest;
                end,
            });
            _M.IM(members, 'BinaryExpressionsTest', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, 2 +_M.Add+ 3);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(-1, 2 - 3);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, 2 * 3);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, 10 / 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(7, math.mod(17, 10));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(144, bit.lshift(36, 2));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(9, bit.rshift(36, 2));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(8, bit.band(137, 26));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(155, bit.bor(137, 26));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(147, bit.bxor(137, 26));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, true and true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, true and false);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, true or false);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, false or false);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 5 == 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, 10 == 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, 5 ~= 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 10 ~= 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 5 < 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, 10 < 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 5 <= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 10 <= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, 15 <= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, 10 > 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 15 > 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, 5 >= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 10 >= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, 15 >= 10);
                    local value = nil;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Default"", value or ""Default"");
                end
            });
            _M.IM(members, 'AssignmentExpressionTest', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local v1 = 3;
                    v1 = v1 +_M.Add+ 2;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, v1);
                    v1 = v1 - 3;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, v1);
                    v1 = v1 * 3;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, v1);
                    v1 = v1 / 2;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, v1);
                    local v2 = 32;
                    v2 = math.mod(v2, 20);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(12, v2);
                    local v3 = 56;
                    v3 = bit.lshift(v3, 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(224, v3);
                    v3 = bit.rshift(v3, 1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(112, v3);
                    local v4 = 50;
                    v4 = bit.bor(v4, 30);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(62, v4);
                    v4 = bit.band(v4, 124);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(60, v4);
                    v4 = bit.bxor(v4, 34);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(30, v4);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','ACommonName', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ACommonName','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 16166);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'Value', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Value = value;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','Base', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Base','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 1705);
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
            ConstValue = 50,
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.ConstValue == nil) then element[typeObject.Level].ConstValue = values.ConstValue; end
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
            _M.IM(members, 'ConstValue', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','ClassAmb', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassAmb','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 7573);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                ambValue = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.ambValue == nil) then element[typeObject.Level].ambValue = values.ambValue; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'ambValue', {
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
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, ambValue)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).ambValue = ambValue;
                end,
            });
            _M.IM(members, 'GetAmbValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).ambValue;
                end
            });
            _M.IM(members, 'SetAmbValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                func = function(element, ambValue)
                    (element % _M.DOT_LVL(typeObject.Level)).ambValue = ambValue;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','ClassInitializerAndCstor', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassInitializerAndCstor','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 99803);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'Value',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Value = value;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','ClassWithEquals', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithEquals','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 34384);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(System.Int32.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
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
            _M.IM(members, 'Value', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Equals', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8572,
                override = true,
                returnType = function() return System.Boolean.__typeof end,
                func = function(element, obj)
                    if (CsLuaTest.General.ClassWithEquals.__is(obj)) then
                        return ((obj) % _M.DOT).Value == (element % _M.DOT_LVL(typeObject.Level)).Value;
                    end
                    return ((element % _M.DOT_LVL(typeObject.Level - 1, true)).Equals_M_0_8572 % _M.DOT)(obj);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','ClassWithIndexer', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithIndexer','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 39842);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Set = """",
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Set == nil) then element[typeObject.Level].Set = values.Set; end
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
            _M.IM(members, 'Set', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members,'#',{
                level = typeObject.Level,
                memberType = 'Indexer',
                scope = 'Public',
                get = function(element, index)
                    return ""GetAtIndex"" +_M.Add+ index;
                end,
                set = function(element, index, value)
                    (element % _M.DOT_LVL(typeObject.Level)).Set = ""SetAtIndex"" +_M.Add+ index +_M.Add+ ""Is"" +_M.Add+ value;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','ClassWithProperty', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithProperty','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 48066);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                AutoProperty = _M.DV(System.String.__typeof),
                PropertyWithGet = _M.DV(System.String.__typeof),
                PropertyWithSet = _M.DV(System.String.__typeof),
                PropertyWithGetAndSet = _M.DV(System.String.__typeof),
                ACommonName = _M.DV(System.String.__typeof),
                IntProperty = _M.DV(System.Int32.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.AutoProperty == nil) then element[typeObject.Level].AutoProperty = values.AutoProperty; end
            if not(values.PropertyWithGet == nil) then element[typeObject.Level].PropertyWithGet = values.PropertyWithGet; end
            if not(values.PropertyWithSet == nil) then element[typeObject.Level].PropertyWithSet = values.PropertyWithSet; end
            if not(values.PropertyWithGetAndSet == nil) then element[typeObject.Level].PropertyWithGetAndSet = values.PropertyWithGetAndSet; end
            if not(values.ACommonName == nil) then element[typeObject.Level].ACommonName = values.ACommonName; end
            if not(values.IntProperty == nil) then element[typeObject.Level].IntProperty = values.IntProperty; end
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
            _M.IM(members, 'AutoProperty',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
            });
            _M.IM(members, 'PropertyWithGet',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
                get = function(element)
                    return ""GetValue"";
                end,
            });
            _M.IM(members, 'PropertyWithSet',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
                set = function(element, value)
                    (CsLuaTest.BaseTest % _M.DOT).Output = value;
                end,
            });
            _M.IM(members, 'PropertyWithGetAndSet',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
                set = function(element, value)
                    (CsLuaTest.BaseTest % _M.DOT).Output = value;
                end,
                get = function(element)
                    return (CsLuaTest.BaseTest % _M.DOT).Output;
                end,
            });
            _M.IM(members, 'ACommonName',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
            });
            _M.IM(members, 'IntProperty',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.Int32.__typeof;
            });
            _M.IM(members, 'Run', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 21840,
                returnType = function() return System.String.__typeof end,
                func = function(element, a, b)
                    (element % _M.DOT_LVL(typeObject.Level)).ACommonName = a;
                    local obj = CsLuaTest.General.ACommonName._C_0_8736(b);
                    return (element % _M.DOT_LVL(typeObject.Level)).ACommonName +_M.Add+ (obj % _M.DOT).Value;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','ClassWithTypeAndVariableNaming', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithTypeAndVariableNaming','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 160576);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Action = _M.DV(System.Action.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Action == nil) then element[typeObject.Level].Action = values.Action; end
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
            _M.IM(members, 'Action', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8786,
                func = function(element, action)
                    (element % _M.DOT_LVL(typeObject.Level)).Action = action;
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""Action"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','GeneralTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('GeneralTests','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 21158);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""General"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestVariableTypeVsVariableName""] = (element % _M.DOT_LVL(typeObject.Level)).TestVariableTypeVsVariableName;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""GetConstValueFromBase""] = (element % _M.DOT_LVL(typeObject.Level)).GetConstValueFromBase;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ConstructorShouldUseArgumentsOverClassElements""] = (element % _M.DOT_LVL(typeObject.Level)).ConstructorShouldUseArgumentsOverClassElements;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""MethodShouldUseArgumentsOverClassElements""] = (element % _M.DOT_LVL(typeObject.Level)).MethodShouldUseArgumentsOverClassElements;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ClassesShouldBeAbleToUseCustomEqualsImplementation""] = (element % _M.DOT_LVL(typeObject.Level)).ClassesShouldBeAbleToUseCustomEqualsImplementation;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""MinusEqualsShouldBeHandled""] = (element % _M.DOT_LVL(typeObject.Level)).MinusEqualsShouldBeHandled;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""CommonStringExtensionsShouldWork""] = (element % _M.DOT_LVL(typeObject.Level)).CommonStringExtensionsShouldWork;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""HandleAmbigurityBetweenPropertyNameAndType""] = (element % _M.DOT_LVL(typeObject.Level)).HandleAmbigurityBetweenPropertyNameAndType;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestClassWithInitializerAndConstructor""] = (element % _M.DOT_LVL(typeObject.Level)).TestClassWithInitializerAndConstructor;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestClassWithProperties""] = (element % _M.DOT_LVL(typeObject.Level)).TestClassWithProperties;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""NonStaticClassWithStaticMethod""] = (element % _M.DOT_LVL(typeObject.Level)).NonStaticClassWithStaticMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestClassReferencingSelfInMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestClassReferencingSelfInMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestPartialClasses""] = (element % _M.DOT_LVL(typeObject.Level)).TestPartialClasses;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestIndexerInClass""] = (element % _M.DOT_LVL(typeObject.Level)).TestIndexerInClass;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestIndexerInClass""] = (element % _M.DOT_LVL(typeObject.Level)).TestIndexerInClass;
                end,
            });
            _M.IM(members, 'NonStaticClassWithStaticMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((CsLuaTest.General.NonStaticClass % _M.DOT).StaticMethod_M_0_3926 % _M.DOT)(1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""StaticMethodInt"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestVariableTypeVsVariableName', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.General.ClassWithTypeAndVariableNaming._C_0_0();
                    ((theClass % _M.DOT).Method_M_0_8786 % _M.DOT)(System.Action._C_0_16704(function()
                    end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Action"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'GetConstValueFromBase', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(50, ((CsLuaTest.General.Inheriter % _M.DOT).GetConstValue_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'ConstructorShouldUseArgumentsOverClassElements', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local classA = CsLuaTest.General.ClassAmb._C_0_8736(""X"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X"", ((classA % _M.DOT).GetAmbValue_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'MethodShouldUseArgumentsOverClassElements', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local classA = CsLuaTest.General.ClassAmb._C_0_8736(""X"");
                    ((classA % _M.DOT).SetAmbValue_M_0_8736 % _M.DOT)(""Y"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Y"", ((classA % _M.DOT).GetAmbValue_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'ClassesShouldBeAbleToUseCustomEqualsImplementation', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c1 = (CsLuaTest.General.ClassWithEquals._C_0_0() % _M.DOT).__Initialize({Value = 1});
                    local c2 = (CsLuaTest.General.ClassWithEquals._C_0_0() % _M.DOT).__Initialize({Value = 2});
                    local c3 = (CsLuaTest.General.ClassWithEquals._C_0_0() % _M.DOT).__Initialize({Value = 1});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, ((c1 % _M.DOT).Equals_M_0_8572 % _M.DOT)(c2));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((c1 % _M.DOT).Equals_M_0_8572 % _M.DOT)(c3));
                end
            });
            _M.IM(members, 'MinusEqualsShouldBeHandled', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local i = 10;
                    i = i - 3;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(7, i);
                end
            });
            _M.IM(members, 'CommonStringExtensionsShouldWork', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""s1"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, ((System.String % _M.DOT).IsNullOrEmpty_M_0_8736 % _M.DOT)(s));
                    local s2 = """";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((System.String % _M.DOT).IsNullOrEmpty_M_0_8736 % _M.DOT)(s2));
                    local i1 = ((System.Int32 % _M.DOT).Parse_M_0_8736 % _M.DOT)(""4"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(4, i1);
                end
            });
            _M.IM(members, 'HandleAmbigurityBetweenPropertyNameAndType', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.General.ClassWithProperty._C_0_0();
                    local s = ((c % _M.DOT).Run_M_0_21840 % _M.DOT)(""A"", ""B"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""AB"", s);
                end
            });
            _M.IM(members, 'TestClassWithInitializerAndConstructor', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = (CsLuaTest.General.ClassInitializerAndCstor._C_0_8736(""A"") % _M.DOT).__Initialize({Value = ""B""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""B"", (c % _M.DOT).Value);
                end
            });
            _M.IM(members, 'TestClassWithProperties', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.General.ClassWithProperty._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (c % _M.DOT).IntProperty);
                    (c % _M.DOT).AutoProperty = ""A"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""A"", (c % _M.DOT).AutoProperty);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GetValue"", (c % _M.DOT).PropertyWithGet);
                    (c % _M.DOT).PropertyWithGetAndSet = ""B"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).Output, ""B"");
                    (element % _M.DOT_LVL(typeObject.Level)).Output = ""C"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""C"", (c % _M.DOT).PropertyWithGetAndSet);
                    (c % _M.DOT).PropertyWithSet = ""D"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).Output, ""D"");
                end
            });
            _M.IM(members, 'TestClassReferencingSelfInMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (CsLuaTest.General.NonStaticClass._C_0_0() % _M.DOT).__Initialize({Value = 3});
                    local b = (CsLuaTest.General.NonStaticClass._C_0_0() % _M.DOT).__Initialize({Value = 7});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(10, ((a % _M.DOT).CallWithSameClass_M_0_57882 % _M.DOT)(b));
                end
            });
            _M.IM(members, 'TestPartialClasses', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local pClass = CsLuaTest.General.Partial._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""CstorA"", (pClass % _M.DOT).InnerValue);
                    pClass = CsLuaTest.General.Partial._C_0_3926(1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""CstorB"", (pClass % _M.DOT).InnerValue);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, ((pClass % _M.DOT).MethodA_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((pClass % _M.DOT).MethodB_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, ((pClass % _M.DOT).MethodC_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'TestIndexerInClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c1 = CsLuaTest.General.ClassWithIndexer._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GetAtIndexTest1"", (c1 % _M.DOT)[""Test1""]);
                    (c1 % _M.DOT)[""Test2""] = ""TheValue"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""SetAtIndexTest2IsTheValue"", (c1 % _M.DOT).Set);
                end
            });
            _M.IM(members, 'TestConditionalIndexers', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local value = nil;
                    local len = _M.CA(value,function(obj) return (obj % _M.DOT).Length; end);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(nil, len);
                    local value2 = ""abc"";
                    local len2 = _M.CA(value2,function(obj) return (obj % _M.DOT).Length; end);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, len2);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','Inheriter', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Inheriter','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 10835);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.General.Base.__meta(staticValues);
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
            _M.IM(members, 'GetConstValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).ConstValue;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','NonStaticClass', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('NonStaticClass','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 28941);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(System.Int32.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
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
            _M.IM(members, 'Value', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'CallWithSameClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 57882,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, other)
                    return (other % _M.DOT).Value +_M.Add+ (element % _M.DOT_LVL(typeObject.Level)).Value;
                end
            });
            _M.IM(members, 'StaticMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""StaticMethodInt"";
                end
            });
            _M.IM(members, 'StaticMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""StaticMethodString"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General','Partial', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Partial','CsLuaTest.General', nil, 0, generics, nil, interactionElement, 'Class', 6085);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                InnerValue = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.InnerValue == nil) then element[typeObject.Level].InnerValue = values.InnerValue; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'InnerValue', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).InnerValue = ""CstorA"";
                end,
            });
            _M.IM(members, 'MethodA', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return 1;
                end
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                scope = 'Public',
                func = function(element, x)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).InnerValue = ""CstorB"";
                end,
            });
            _M.IM(members, 'MethodB', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return 2;
                end
            });
            _M.IM(members, 'MethodC', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return 3;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.General.SubNamespace.SubSubNamespace','ClassInSubSubNamespace', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassInSubSubNamespace','CsLuaTest.General.SubNamespace.SubSubNamespace', nil, 0, generics, nil, interactionElement, 'Class', 80123);
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
            _M.IM(members, 'GetStatic', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return ""static"";
                end
            });
            _M.IM(members, 'GetNormal', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return ""normal"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Generics','ClassA', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassA','CsLuaTest.Generics', nil, 0, generics, nil, interactionElement, 'Class', 3858);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                value = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.value == nil) then element[typeObject.Level].value = values.value; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'value', {
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
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).value = value;
                end,
            });
            _M.IM(members, 'ToString', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).value;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Generics','ClassUsingGenericsInMethods', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('ClassUsingGenericsInMethods','CsLuaTest.Generics', nil, 1, generics, nil, interactionElement, 'Class', (260104*generics[genericsMapping['T']].signatureHash));
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                inner = _M.DV(CsLuaTest.Generics.MethodsWithGeneric[{generics[genericsMapping['T']], System.Int32.__typeof}].__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.inner == nil) then element[typeObject.Level].inner = values.inner; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'inner', {
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).inner = CsLuaTest.Generics.MethodsWithGeneric[{generics[genericsMapping['T']], System.Int32.__typeof}]._C_0_0();
                end,
            });
            _M.IM(members, 'UseClassGenericAsMethodGeneric', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Boolean.__typeof end,
                func = function(element)
                    local list = System.Collections.Generic.List[{generics[genericsMapping['T']]}]._C_0_0();
                    local returnValue = (((element % _M.DOT_LVL(typeObject.Level)).inner % _M.DOT).GenericReturnType_M_1_8572[{System.Collections.Generic.List[{generics[genericsMapping['T']]}].__typeof}] % _M.DOT)(list);
                    return list == returnValue;
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'UseMethodGenericAsMethodGeneric', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 2,
                returnType = function() return System.Boolean.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, value)
                    local list = (System.Collections.Generic.List[{methodGenerics[methodGenericsMapping['T3']]}]._C_0_0() % _M.DOT).__Initialize({
                        value
                    });
                    local returnValue = (((element % _M.DOT_LVL(typeObject.Level)).inner % _M.DOT).GenericReturnType_M_1_8572[{System.Collections.Generic.List[{methodGenerics[methodGenericsMapping['T3']]}].__typeof}] % _M.DOT)(list);
                    return list == returnValue;
                end
            });
            _M.IM(members, 'UseClassGenericInLambda', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T']].signatureHash)+(3*generics[genericsMapping['T']].signatureHash),
                returnType = function() return System.Boolean.__typeof end,
                func = function(element, correctValue, falseValue)
                    local value = ((element % _M.DOT_LVL(typeObject.Level)).MethodWithGenericAndLambda_M_1_13625772[{generics[genericsMapping['T']]}] % _M.DOT)(System.Func[{System.Int32.__typeof, generics[genericsMapping['T']]}]._C_0_16704(function(i) return i == 43 and correctValue or falseValue end));
                    return ((value % _M.DOT).Equals_M_0_8572 % _M.DOT)(correctValue);
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'MethodWithGenericAndLambda', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 13625772,
                returnType = function() return methodGenerics[methodGenericsMapping['T3']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, selector)
                    return (selector % _M.DOT)(43);
                end
            });
            _M.IM(members, 'InvokingAmbMethodDependingOnClassGeneric', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T']].signatureHash),
                func = function(element, obj)
                    (((element % _M.DOT_LVL(typeObject.Level)).inner % _M.DOT)['AmbGenericMethod_M_0_'..((2*generics[genericsMapping['T']].signatureHash))] % _M.DOT)(obj);
                    (((element % _M.DOT_LVL(typeObject.Level)).inner % _M.DOT).AmbGenericMethod_M_0_3926 % _M.DOT)(43);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Generics','ClassWithGenericConstructor', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('ClassWithGenericConstructor','CsLuaTest.Generics', nil, 1, generics, nil, interactionElement, 'Class', (270940*generics[genericsMapping['T']].signatureHash));
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
                signatureHash = (2*generics[genericsMapping['T']].signatureHash),
                scope = 'Public',
                func = function(element, x)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""GenericConstructorT"";
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                scope = 'Public',
                func = function(element, x)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""GenericConstructorint"";
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Generics','ClassWithGenericElements', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithGenericElements','CsLuaTest.Generics', nil, 0, generics, nil, interactionElement, 'Class', 100101);
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
            StaticString = _M.DV(System.String.__typeof),
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.StaticString == nil) then element[typeObject.Level].StaticString = values.StaticString; end
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
            _M.IM(members, 'StaticString', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('ClassWithGenericElements','CsLuaTest.Generics', nil, 1, generics, nil, interactionElement, 'Class', (200202*generics[genericsMapping['T']].signatureHash));
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Property = _M.DV(generics[genericsMapping['T']]),
                Variable = _M.DV(generics[genericsMapping['T']]),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
            StaticString = _M.DV(System.String.__typeof),
            StaticT = _M.DV(generics[genericsMapping['T']]),
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Property == nil) then element[typeObject.Level].Property = values.Property; end
            if not(values.StaticString == nil) then element[typeObject.Level].StaticString = values.StaticString; end
            if not(values.StaticT == nil) then element[typeObject.Level].StaticT = values.StaticT; end
            if not(values.Variable == nil) then element[typeObject.Level].Variable = values.Variable; end
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
            _M.IM(members, 'StaticString', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'StaticT', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'Variable', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Property',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = generics[genericsMapping['T']];
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Generics','ClassWithSelfInGenericInterface', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithSelfInGenericInterface','CsLuaTest.Generics', nil, 0, generics, nil, interactionElement, 'Class', 173874);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.Generics.IInterfaceWithGenerics[{CsLuaTest.Generics.ClassWithSelfInGenericInterface.__typeof}].__typeof);
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
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 347748,
                returnType = function() return System.Boolean.__typeof end,
                func = function(element, self)
                    return element == self;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Generics','GenericsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('GenericsTests','CsLuaTest.Generics', nil, 0, generics, nil, interactionElement, 'Class', 25622);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Generics"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericConstructor""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericConstructor;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericVariable""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericVariable;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericProperty""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericProperty;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericReturnArg""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericReturnArg;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericReturnSpecificForMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericReturnSpecificForMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericStatic""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericStatic;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericsInAmbMethods""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericsInAmbMethods;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGenericsInStaticMethods""] = (element % _M.DOT_LVL(typeObject.Level)).TestGenericsInStaticMethods;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestSelfRefInInterface""] = (element % _M.DOT_LVL(typeObject.Level)).TestSelfRefInInterface;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestMethodGenericAsGenericInInputObject""] = (element % _M.DOT_LVL(typeObject.Level)).TestMethodGenericAsGenericInInputObject;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestPassOfGenericsToGenericMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestPassOfGenericsToGenericMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestPassofGenericsToGenericMethodThroughLambda""] = (element % _M.DOT_LVL(typeObject.Level)).TestPassofGenericsToGenericMethodThroughLambda;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestInvokingAmbMethodDependingOnClassGeneric""] = (element % _M.DOT_LVL(typeObject.Level)).TestInvokingAmbMethodDependingOnClassGeneric;
                end,
            });
            _M.IM(members, 'TestGenericMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.String.__typeof}]._C_0_0();
                    ((theClass % _M.DOT).AmbGenericMethod_M_0_3926 % _M.DOT)(1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericMethodT1"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).AmbGenericMethod_M_0_8736 % _M.DOT)(""x"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericMethodT2"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestGenericConstructor', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.ClassWithGenericConstructor[{System.String.__typeof}]._C_0_8736(""ok"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericConstructorT"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestGenericVariable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.ClassWithGenericElements[{System.Int32.__typeof}]._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (theClass % _M.DOT).Variable);
                    (theClass % _M.DOT).Variable = 43;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, (theClass % _M.DOT).Variable);
                end
            });
            _M.IM(members, 'TestGenericProperty', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.ClassWithGenericElements[{System.Int32.__typeof}]._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (theClass % _M.DOT).Property);
                    (theClass % _M.DOT).Property = 43;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, (theClass % _M.DOT).Property);
                end
            });
            _M.IM(members, 'TestGenericReturnArg', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.Int32.__typeof}]._C_0_0();
                    local value1 = ((theClass % _M.DOT).GenericReturnType_M_1_8572[{System.Boolean.__typeof}] % _M.DOT)(true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, value1);
                    local value2 = ((theClass % _M.DOT).GenericReturnType_M_1_8572[{System.String.__typeof}] % _M.DOT)(""test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""test"", value2);
                end
            });
            _M.IM(members, 'TestGenericReturnSpecificForMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.Int32.__typeof}]._C_0_0();
                    local value1 = ((theClass % _M.DOT).GenericAtMethod_M_1_2[{System.String.__typeof}] % _M.DOT)(""test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""test"", value1);
                    local value2 = ((theClass % _M.DOT).GenericAtMethod_M_1_2[{System.String.__typeof}] % _M.DOT)(""test2"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""test2"", value2);
                    local value3 = ((theClass % _M.DOT).GenericAtMethod_M_1_2[{System.Boolean.__typeof}] % _M.DOT)(true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""True"", value3);
                    local value4 = ((theClass % _M.DOT).GenericAtMethod_M_1_2[{CsLuaTest.Generics.ClassA.__typeof}] % _M.DOT)(CsLuaTest.Generics.ClassA._C_0_8736(""test4""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""test4"", value4);
                    local obj = CsLuaTest.Generics.ClassA._C_0_8736(""test5"");
                    local value5 = ((theClass % _M.DOT).GenericAtMethod_M_1_2[{CsLuaTest.Generics.ClassA.__typeof}] % _M.DOT)(obj);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""test5"", value5);
                end
            });
            _M.IM(members, 'TestGenericsInAmbMethods', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.Int32.__typeof}]._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""XYA"", ((theClass % _M.DOT).GenericAtAmbMethod_M_2_5[{System.String.__typeof, System.String.__typeof}] % _M.DOT)(""X"", ""Y""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""XYB"", ((theClass % _M.DOT).GenericAtAmbMethod_M_1_13106[{System.String.__typeof}] % _M.DOT)(""X"", ""Y""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""XYB"", ((theClass % _M.DOT).GenericAtAmbMethod_M_1_13106[{System.String.__typeof}] % _M.DOT)(""X"", ""Y""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X1A"", ((theClass % _M.DOT).GenericAtAmbMethod_M_2_5[{System.String.__typeof, System.Int32.__typeof}] % _M.DOT)(""X"", 1));
                end
            });
            _M.IM(members, 'TestGenericsInStaticMethods', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""String"", ((CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.Int32.__typeof}] % _M.DOT).StaticMethodWithGenerics_M_1_2[{System.String.__typeof}] % _M.DOT)(""X""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""String"", ((CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.Int32.__typeof}] % _M.DOT).StaticMethodWithGenerics_M_1_2[{System.String.__typeof}] % _M.DOT)(""X""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Int32"", ((CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.Int32.__typeof}] % _M.DOT).StaticMethodWithGenerics_M_1_2[{System.Int32.__typeof}] % _M.DOT)(43));
                end
            });
            _M.IM(members, 'TestGenericStatic', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.Generics.ClassWithGenericElements[{System.Int32.__typeof}] % _M.DOT).StaticT = 2;
                    (CsLuaTest.Generics.ClassWithGenericElements[{System.Int32.__typeof}] % _M.DOT).StaticString = ""X"";
                    (CsLuaTest.Generics.ClassWithGenericElements[{System.Boolean.__typeof}] % _M.DOT).StaticT = true;
                    (CsLuaTest.Generics.ClassWithGenericElements[{System.Boolean.__typeof}] % _M.DOT).StaticString = ""Y"";
                    (CsLuaTest.Generics.ClassWithGenericElements % _M.DOT).StaticString = ""Z"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, (CsLuaTest.Generics.ClassWithGenericElements[{System.Int32.__typeof}] % _M.DOT).StaticT);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X"", (CsLuaTest.Generics.ClassWithGenericElements[{System.Int32.__typeof}] % _M.DOT).StaticString);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, (CsLuaTest.Generics.ClassWithGenericElements[{System.Boolean.__typeof}] % _M.DOT).StaticT);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Y"", (CsLuaTest.Generics.ClassWithGenericElements[{System.Boolean.__typeof}] % _M.DOT).StaticString);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Z"", (CsLuaTest.Generics.ClassWithGenericElements % _M.DOT).StaticString);
                end
            });
            _M.IM(members, 'TestSelfRefInInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c1 = CsLuaTest.Generics.ClassWithSelfInGenericInterface._C_0_0();
                    local c2 = CsLuaTest.Generics.ClassWithSelfInGenericInterface._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((c1 % _M.DOT).Method_M_0_347748 % _M.DOT)(c1));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, ((c1 % _M.DOT).Method_M_0_347748 % _M.DOT)(c2));
                end
            });
            _M.IM(members, 'TestMethodGenericAsGenericInInputObject', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.MethodsWithGeneric[{System.Int32.__typeof, System.Int32.__typeof}]._C_0_0();
                    local a = ((theClass % _M.DOT).MethodWithGenericAsObjectGenericInArg_M_1_6936[{System.Int32.__typeof}] % _M.DOT)((System.Func[{System.Int32.__typeof}]._C_0_16704(function() return 3 end)));
                    local b = ((theClass % _M.DOT).MethodWithGenericAsObjectGenericInArg_M_1_6936[{System.String.__typeof}] % _M.DOT)((System.Func[{System.String.__typeof}]._C_0_16704(function() return ""x"" end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, a);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""x"", b);
                end
            });
            _M.IM(members, 'TestPassOfGenericsToGenericMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.ClassUsingGenericsInMethods[{System.Int32.__typeof}]._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((theClass % _M.DOT).UseClassGenericAsMethodGeneric_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((theClass % _M.DOT).UseMethodGenericAsMethodGeneric_M_1_2[{System.Int32.__typeof}] % _M.DOT)(3));
                end
            });
            _M.IM(members, 'TestPassofGenericsToGenericMethodThroughLambda', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.ClassUsingGenericsInMethods[{System.String.__typeof}]._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((theClass % _M.DOT).UseClassGenericInLambda_M_0_21840 % _M.DOT)(""correct"", ""incorrect""));
                end
            });
            _M.IM(members, 'TestInvokingAmbMethodDependingOnClassGeneric', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Generics.ClassUsingGenericsInMethods[{System.Boolean.__typeof}]._C_0_0();
                    ((theClass % _M.DOT).InvokingAmbMethodDependingOnClassGeneric_M_0_12036 % _M.DOT)(true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""GenericMethodT1GenericMethodT2"", (CsLuaTest.BaseTest % _M.DOT).Output);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Generics','IInterfaceWithGenerics', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('IInterfaceWithGenerics','CsLuaTest.Generics', nil, 1, generics, nil, interactionElement, 'Interface',(163318*generics[genericsMapping['T']].signatureHash));
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T']].signatureHash),
                returnType = function() return System.Boolean.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Generics','MethodsWithGeneric', _M.NE({
    [2] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T1'] = 1,['T2'] = 2};
        local typeObject = System.Type('MethodsWithGeneric','CsLuaTest.Generics', nil, 2, generics, nil, interactionElement, 'Class', (102836*generics[genericsMapping['T1']].signatureHash)+(154254*generics[genericsMapping['T2']].signatureHash));
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
            _M.IM(members, 'AmbGenericMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T1']].signatureHash),
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""GenericMethodT1"";
                end
            });
            _M.IM(members, 'AmbGenericMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T2']].signatureHash),
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""GenericMethodT2"";
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GenericReturnType', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 8572,
                returnType = function() return methodGenerics[methodGenericsMapping['T3']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, value)
                    return value;
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GenericAtMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 2,
                returnType = function() return System.String.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, obj)
                    return ((obj % _M.DOT).ToString_M_0_0 % _M.DOT)();
                end
            });
            local methodGenericsMapping = {['T3'] = 1,['T4'] = 2};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GenericAtAmbMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 2,
                signatureHash = 5,
                returnType = function() return System.String.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, obj, obj2)
                    return ((obj % _M.DOT).ToString_M_0_0 % _M.DOT)() +_M.Add+ ((obj2 % _M.DOT).ToString_M_0_0 % _M.DOT)() +_M.Add+ ""A"";
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GenericAtAmbMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 13106,
                returnType = function() return System.String.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, obj, obj2)
                    return ((obj % _M.DOT).ToString_M_0_0 % _M.DOT)() +_M.Add+ ((obj2 % _M.DOT).ToString_M_0_0 % _M.DOT)() +_M.Add+ ""B"";
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'StaticMethodWithGenerics', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 2,
                returnType = function() return System.String.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, value)
                    return (((value % _M.DOT).GetType_M_0_0 % _M.DOT)() % _M.DOT).Name;
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'MethodWithGenericAsObjectGenericInArg', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 6936,
                returnType = function() return methodGenerics[methodGenericsMapping['T3']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, f)
                    return (f % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Inheritance','Base', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Base','CsLuaTest.Inheritance', nil, 0, generics, nil, interactionElement, 'Class', 1705);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                privateInt = 43,
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.privateInt == nil) then element[typeObject.Level].privateInt = values.privateInt; end
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
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                scope = 'Public',
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).privateInt = value;
                end,
            });
            _M.IM(members, 'privateInt', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Private',
                static = false,
            });
            _M.IM(members, 'GetFromPrivateInBase', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).privateInt;
                end
            });
            _M.IM(members, 'SetToPrivateInBase', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level)).privateInt = value;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Inheritance','InheritanceTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('InheritanceTests','CsLuaTest.Inheritance', nil, 0, generics, nil, interactionElement, 'Class', 40410);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Inheritance"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestPrivateVariableInBaseCstor""] = (element % _M.DOT_LVL(typeObject.Level)).TestPrivateVariableInBaseCstor;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestPrivateVariableInBase""] = (element % _M.DOT_LVL(typeObject.Level)).TestPrivateVariableInBase;
                end,
            });
            _M.IM(members, 'TestPrivateVariableInBase', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Inheritance.Inheriter._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, ((c % _M.DOT).GetFromPrivateInBase_M_0_0 % _M.DOT)());
                    ((c % _M.DOT).SetToPrivateInBase_M_0_3926 % _M.DOT)(5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, ((c % _M.DOT).GetFromPrivateInBase_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'TestPrivateVariableInBaseCstor', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Inheritance.Inheriter._C_0_3926(50);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Inheritance','Inheriter', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Inheriter','CsLuaTest.Inheritance', nil, 0, generics, nil, interactionElement, 'Class', 10835);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Inheritance.Base.__meta(staticValues);
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
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                scope = 'Public',
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_3926(value);
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','ClassA', _M.NE({
    [2] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T1'] = 1,['T2'] = 2};
        local typeObject = System.Type('ClassA','CsLuaTest.Interfaces', nil, 2, generics, nil, interactionElement, 'Class', (7716*generics[genericsMapping['T1']].signatureHash)+(11574*generics[genericsMapping['T2']].signatureHash));
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.Interfaces.InterfaceWithGenerics[{generics[genericsMapping['T2']]}].__typeof);
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
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T2']].signatureHash),
                func = function(element, arg)
                end
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'MethodWithGenericInReturn', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 8572,
                returnType = function() return methodGenerics[methodGenericsMapping['T3']] end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, arg)
                    return arg;
                end
            });
            local methodGenericsMapping = {['T4'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'MethodWithGenericInArg', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 2,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, arg)
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','ClassWithMethod', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithMethod','CsLuaTest.Interfaces', nil, 0, generics, nil, interactionElement, 'Class', 33918);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.Interfaces.InterfaceWithMethod.__typeof);
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
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.String.__typeof end,
                func = function(element, input)
                    return input +_M.Add+ ""X"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','IBaseInterface', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IBaseInterface','CsLuaTest.Interfaces', nil, 0, generics, nil, interactionElement, 'Interface',28550);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','IInheritingInterface', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IInheritingInterface','CsLuaTest.Interfaces', nil, 0, generics, nil, interactionElement, 'Interface',65750);
        local implements = {
            CsLuaTest.Interfaces.IBaseInterface.__typeof,
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','InheritingInterfaceImplementation', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('InheritingInterfaceImplementation','CsLuaTest.Interfaces', nil, 0, generics, nil, interactionElement, 'Class', 209615);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.Interfaces.IInheritingInterface.__typeof);
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
            _M.IM(members, 'AMethodTakingBaseInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 57100,
                func = function(element, arg)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""OK"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','InterfacesTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('InterfacesTests','CsLuaTest.Interfaces', nil, 0, generics, nil, interactionElement, 'Class', 34980);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Interfaces"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""InheritiedInterfaceShouldBeloadedInSignature""] = (element % _M.DOT_LVL(typeObject.Level)).InheritiedInterfaceShouldBeloadedInSignature;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ImplementedInterfaceWithGenerics""] = (element % _M.DOT_LVL(typeObject.Level)).ImplementedInterfaceWithGenerics;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestInterfaceWithMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestInterfaceWithMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestInterfaceWithBuildInMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestInterfaceWithBuildInMethod;
                end,
            });
            _M.IM(members, 'InheritiedInterfaceShouldBeloadedInSignature', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Interfaces.InheritingInterfaceImplementation._C_0_0();
                    ((CsLuaTest.Interfaces.InheritingInterfaceImplementation % _M.DOT).AMethodTakingBaseInterface_M_0_57100 % _M.DOT)(theClass);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OK"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'ImplementedInterfaceWithGenerics', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Interfaces.ClassA[{System.Int32.__typeof, System.String.__typeof}]._C_0_0();
                    ((theClass % _M.DOT).Method_M_0_8736 % _M.DOT)(""test"");
                    local value = ((theClass % _M.DOT).MethodWithGenericInReturn_M_1_8572[{System.String.__typeof}] % _M.DOT)(""test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""test"", value);
                end
            });
            _M.IM(members, 'TestInterfaceWithMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Interfaces.ClassWithMethod._C_0_0();
                    local castClass = theClass;
                    local value = ((castClass % _M.DOT).Method_M_0_8736 % _M.DOT)(""str"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""strX"", value);
                end
            });
            _M.IM(members, 'TestInterfaceWithBuildInMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Interfaces.ClassWithMethod._C_0_0();
                    local castClass = theClass;
                    local value = ((castClass % _M.DOT).Equals_M_0_8572 % _M.DOT)(castClass);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, value);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','InterfaceWithGenerics', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('InterfaceWithGenerics','CsLuaTest.Interfaces', nil, 1, generics, nil, interactionElement, 'Interface',(147064*generics[genericsMapping['T']].signatureHash));
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T']].signatureHash),
            });
            local methodGenericsMapping = {['T3'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'MethodWithGenericInArg', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 2,
                generics = methodGenericsMapping,
            });
            local methodGenericsMapping = {['T2'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'MethodWithGenericInReturn', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 8572,
                returnType = function() return methodGenerics[methodGenericsMapping['T2']] end,
                generics = methodGenericsMapping,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Interfaces','InterfaceWithMethod', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('InterfaceWithMethod','CsLuaTest.Interfaces', nil, 0, generics, nil, interactionElement, 'Interface',58343);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.String.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Linq','ClassWithProperties', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithProperties','CsLuaTest.Linq', nil, 0, generics, nil, interactionElement, 'Class', 60988);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Number = _M.DV(System.Int32.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Number == nil) then element[typeObject.Level].Number = values.Number; end
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
            _M.IM(members, 'Number', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Linq','LinqTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('LinqTests','CsLuaTest.Linq', nil, 0, generics, nil, interactionElement, 'Class', 10849);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Linq"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WhereWithNoSourceThrows""] = (element % _M.DOT_LVL(typeObject.Level)).WhereWithNoSourceThrows;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WhereWithNoPredicateThrows""] = (element % _M.DOT_LVL(typeObject.Level)).WhereWithNoPredicateThrows;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WhereReturnsExpectedCollection""] = (element % _M.DOT_LVL(typeObject.Level)).WhereReturnsExpectedCollection;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WhereWithIndexWithNoSourceThrows""] = (element % _M.DOT_LVL(typeObject.Level)).WhereWithIndexWithNoSourceThrows;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WhereWithIndexWithNoPredicateThrows""] = (element % _M.DOT_LVL(typeObject.Level)).WhereWithIndexWithNoPredicateThrows;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WhereWithIndexReturnsExpectedCollection""] = (element % _M.DOT_LVL(typeObject.Level)).WhereWithIndexReturnsExpectedCollection;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestCountAndAny""] = (element % _M.DOT_LVL(typeObject.Level)).TestCountAndAny;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestAnyWithPredicate""] = (element % _M.DOT_LVL(typeObject.Level)).TestAnyWithPredicate;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestSelect""] = (element % _M.DOT_LVL(typeObject.Level)).TestSelect;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestSelectWithIndex""] = (element % _M.DOT_LVL(typeObject.Level)).TestSelectWithIndex;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestUnion""] = (element % _M.DOT_LVL(typeObject.Level)).TestUnion;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestFirst""] = (element % _M.DOT_LVL(typeObject.Level)).TestFirst;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestFirstWithPredicate""] = (element % _M.DOT_LVL(typeObject.Level)).TestFirstWithPredicate;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestLast""] = (element % _M.DOT_LVL(typeObject.Level)).TestLast;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestLastWithPredicate""] = (element % _M.DOT_LVL(typeObject.Level)).TestLastWithPredicate;
                end,
            });
            local methodGenericsMapping = {['T'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'ExpectException', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 1,
                signatureHash = 21890,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics, action, expectedText)
                    local x = methodGenerics[methodGenericsMapping['T']].name;
                    _M.Try(
                        function()
                            (action % _M.DOT)();
                            _M.Throw(System.Exception._C_0_8736(""Expected to throw exception. No exception thrown.""));
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, methodGenerics[methodGenericsMapping['T']].__is(ex), ""Expected "" +_M.Add+ methodGenerics[methodGenericsMapping['T']].name +_M.Add+ "", got "" +_M.Add+ (((ex % _M.DOT).GetType_M_0_0 % _M.DOT)() % _M.DOT).Name);
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(expectedText, (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                end
            });
            _M.IM(members, 'WhereWithNoSourceThrows', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    _M.Try(
                        function()
                            ((System.Linq.Enumerable % _M.DOT).Where_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(nil, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return false end));
                            _M.Throw(System.Exception._C_0_8736(""Expected to throw exception. No exception thrown.""));
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.ArgumentNullException.__is(ex), ""Expected ArgumentNullException, got "" +_M.Add+ (((ex % _M.DOT).GetType_M_0_0 % _M.DOT)() % _M.DOT).Name);
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Value cannot be null."" +_M.Add+ (System.Environment % _M.DOT).NewLine +_M.Add+ ""Parameter name: source"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                end
            });
            _M.IM(members, 'WhereWithNoPredicateThrows', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    _M.Try(
                        function()
                            local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                            local predicate = nil;
                            ((System.Linq.Enumerable % _M.DOT).Where_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, predicate);
                            _M.Throw(System.Exception._C_0_8736(""Expected to throw exception. No exception thrown.""));
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.ArgumentNullException.__is(ex), ""Expected ArgumentNullException, got "" +_M.Add+ (((ex % _M.DOT).GetType_M_0_0 % _M.DOT)() % _M.DOT).Name);
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Value cannot be null."" +_M.Add+ (System.Environment % _M.DOT).NewLine +_M.Add+ ""Parameter name: predicate"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                end
            });
            _M.IM(members, 'WhereReturnsExpectedCollection', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    local res = ((System.Linq.Enumerable % _M.DOT).ToArray_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).Where_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return v > 10 and v < 40 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, (res % _M.DOT).Length);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(16, (res % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(32, (res % _M.DOT)[1]);
                end
            });
            _M.IM(members, 'WhereWithIndexWithNoSourceThrows', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    _M.Try(
                        function()
                            ((System.Linq.Enumerable % _M.DOT).Where_M_1_187239290[{System.Int32.__typeof}] % _M.DOT)(nil, System.Func[{System.Int32.__typeof, System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v, i) return false end));
                            _M.Throw(System.Exception._C_0_8736(""Expected to throw exception. No exception thrown.""));
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.ArgumentNullException.__is(ex), ""Expected ArgumentNullException, got "" +_M.Add+ (((ex % _M.DOT).GetType_M_0_0 % _M.DOT)() % _M.DOT).Name);
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Value cannot be null."" +_M.Add+ (System.Environment % _M.DOT).NewLine +_M.Add+ ""Parameter name: source"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                end
            });
            _M.IM(members, 'WhereWithIndexWithNoPredicateThrows', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    _M.Try(
                        function()
                            local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                            local predicate = nil;
                            ((System.Linq.Enumerable % _M.DOT).Where_M_1_187239290[{System.Int32.__typeof}] % _M.DOT)(a, predicate);
                            _M.Throw(System.Exception._C_0_8736(""Expected to throw exception. No exception thrown.""));
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.ArgumentNullException.__is(ex), ""Expected ArgumentNullException, got "" +_M.Add+ (((ex % _M.DOT).GetType_M_0_0 % _M.DOT)() % _M.DOT).Name);
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Value cannot be null.\nParameter name: predicate"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                end
            });
            _M.IM(members, 'WhereWithIndexReturnsExpectedCollection', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    local res = ((System.Linq.Enumerable % _M.DOT).ToArray_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).Where_M_1_187239290[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v, i) return v > 4 and i < 4 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, (res % _M.DOT).Length);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(8, (res % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(16, (res % _M.DOT)[1]);
                end
            });
            _M.IM(members, 'TestCountAndAny', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((System.Linq.Enumerable % _M.DOT).Any_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, ((System.Linq.Enumerable % _M.DOT).Count_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(a));
                    local list = System.Collections.Generic.List[{System.String.__typeof}]._C_0_0();
                    ((list % _M.DOT).Add_M_0_8736 % _M.DOT)(""a"");
                    ((list % _M.DOT).Add_M_0_8736 % _M.DOT)(""b"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((System.Linq.Enumerable % _M.DOT).Any_M_1_66128[{System.String.__typeof}] % _M.DOT)(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((System.Linq.Enumerable % _M.DOT).Count_M_1_66128[{System.String.__typeof}] % _M.DOT)(list));
                    local enumerable = ((System.Linq.Enumerable % _M.DOT).Where_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(e) return e > 10 and e < 50 end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((System.Linq.Enumerable % _M.DOT).Count_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(enumerable));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((System.Linq.Enumerable % _M.DOT).Count_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(enumerable));
                    local enumerable2 = ((System.Linq.Enumerable % _M.DOT).Where_M_1_93993440[{System.String.__typeof}] % _M.DOT)(list, System.Func[{System.String.__typeof, System.Boolean.__typeof}]._C_0_16704(function(e) return (e % _M.DOT).Length == 1 end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((System.Linq.Enumerable % _M.DOT).Count_M_1_66128[{System.String.__typeof}] % _M.DOT)(enumerable2));
                    ((list % _M.DOT).Add_M_0_8736 % _M.DOT)(""c"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, ((System.Linq.Enumerable % _M.DOT).Count_M_1_66128[{System.String.__typeof}] % _M.DOT)(enumerable2));
                end
            });
            _M.IM(members, 'TestAnyWithPredicate', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((System.Linq.Enumerable % _M.DOT).Any_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return v < 8 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, ((System.Linq.Enumerable % _M.DOT).Any_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return v > 80 end)));
                end
            });
            _M.IM(members, 'TestSelect', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    local l1 = ((System.Linq.Enumerable % _M.DOT).ToList_M_1_66128[{System.String.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).Select_M_2_92138[{System.Int32.__typeof, System.String.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.String.__typeof}]._C_0_16704(function(v) return ((v % _M.DOT).ToString_M_0_0 % _M.DOT)() end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.List[{System.String.__typeof}].__is(l1));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (l1 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""2"", (l1 % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""4"", (l1 % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""8"", (l1 % _M.DOT)[2]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""16"", (l1 % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""32"", (l1 % _M.DOT)[4]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""64"", (l1 % _M.DOT)[5]);
                    local l2 = ((System.Linq.Enumerable % _M.DOT).ToList_M_1_66128[{System.Single.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).Select_M_2_92138[{System.Int32.__typeof, System.Single.__typeof}] % _M.DOT)(a, (element % _M.DOT_LVL(typeObject.Level)).ToFloat));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.List[{System.Single.__typeof}].__is(l2));
                end
            });
            _M.IM(members, 'TestSelectWithIndex', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    local l1 = ((System.Linq.Enumerable % _M.DOT).ToList_M_1_66128[{System.String.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).Select_M_2_30737120[{System.Int32.__typeof, System.String.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Int32.__typeof, System.String.__typeof}]._C_0_16704(function(v, i) return (((v +_M.Add+ i) % _M.DOT).ToString_M_0_0 % _M.DOT)() end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, System.Collections.Generic.List[{System.String.__typeof}].__is(l1));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (l1 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""2"", (l1 % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""5"", (l1 % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""10"", (l1 % _M.DOT)[2]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""19"", (l1 % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""36"", (l1 % _M.DOT)[4]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""69"", (l1 % _M.DOT)[5]);
                end
            });
            _M.IM(members, 'TestUnion', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 1, 3, 5, 7});
                    local b = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 3, 9, 11, 7});
                    local result = ((System.Linq.Enumerable % _M.DOT).ToArray_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).Union_M_1_165320[{System.Int32.__typeof}] % _M.DOT)(a, b));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, (result % _M.DOT).Length);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, (result % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, (result % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, (result % _M.DOT)[2]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(7, (result % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(9, (result % _M.DOT)[4]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(11, (result % _M.DOT)[5]);
                end
            });
            _M.IM(members, 'TestFirst', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((System.Linq.Enumerable % _M.DOT).First_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(a));
                    local empty = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({});
                    ((CsLuaTest.Linq.LinqTests % _M.DOT).ExpectException_M_1_21890[{System.InvalidOperationException.__typeof}] % _M.DOT)(System.Action._C_0_16704(function()
                        ((System.Linq.Enumerable % _M.DOT).First_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(empty);
                    end), ""Sequence contains no elements"");
                end
            });
            _M.IM(members, 'TestFirstWithPredicate', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(16, ((System.Linq.Enumerable % _M.DOT).First_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return v >= 10 end)));
                    ((CsLuaTest.Linq.LinqTests % _M.DOT).ExpectException_M_1_21890[{System.InvalidOperationException.__typeof}] % _M.DOT)(System.Action._C_0_16704(function()
                        ((System.Linq.Enumerable % _M.DOT).First_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return v > 100 end));
                    end), ""Sequence contains no elements"");
                end
            });
            _M.IM(members, 'TestLast', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(64, ((System.Linq.Enumerable % _M.DOT).Last_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(a));
                    local empty = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({});
                    ((CsLuaTest.Linq.LinqTests % _M.DOT).ExpectException_M_1_21890[{System.InvalidOperationException.__typeof}] % _M.DOT)(System.Action._C_0_16704(function()
                        ((System.Linq.Enumerable % _M.DOT).Last_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(empty);
                    end), ""Sequence contains no elements"");
                end
            });
            _M.IM(members, 'TestLastWithPredicate', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local a = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 2, 4, 8, 16, 32, 64});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(8, ((System.Linq.Enumerable % _M.DOT).Last_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return v < 10 end)));
                    ((CsLuaTest.Linq.LinqTests % _M.DOT).ExpectException_M_1_21890[{System.InvalidOperationException.__typeof}] % _M.DOT)(System.Action._C_0_16704(function()
                        ((System.Linq.Enumerable % _M.DOT).Last_M_1_93993440[{System.Int32.__typeof}] % _M.DOT)(a, System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(v) return v > 100 end));
                    end), ""Sequence contains no elements"");
                end
            });
            _M.IM(members, 'TestOrderBy', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local input = (System.Array[{CsLuaTest.Linq.ClassWithProperties.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = (CsLuaTest.Linq.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 13}), (CsLuaTest.Linq.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 7}), (CsLuaTest.Linq.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 9}), (CsLuaTest.Linq.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 5})});
                    local ordered = ((System.Linq.Enumerable % _M.DOT).ToArray_M_1_66128[{CsLuaTest.Linq.ClassWithProperties.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).OrderBy_M_2_92138[{CsLuaTest.Linq.ClassWithProperties.__typeof, System.Int32.__typeof}] % _M.DOT)(input, System.Func[{CsLuaTest.Linq.ClassWithProperties.__typeof, System.Int32.__typeof}]._C_0_16704(function(v) return (v % _M.DOT).Number end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, ((ordered % _M.DOT)[0] % _M.DOT).Number);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(7, ((ordered % _M.DOT)[1] % _M.DOT).Number);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(9, ((ordered % _M.DOT)[2] % _M.DOT).Number);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(13, ((ordered % _M.DOT)[3] % _M.DOT).Number);
                end
            });
            _M.IM(members, 'TestOfLinqOfType', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local mixedCollection = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 1, 2, ""c"", true, ""e"", 6});
                    local ints = ((System.Linq.Enumerable % _M.DOT).ToArray_M_1_66128[{System.Int32.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).OfType_M_1_33064[{System.Int32.__typeof}] % _M.DOT)(mixedCollection));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, (ints % _M.DOT).Length);
                    local strings = ((System.Linq.Enumerable % _M.DOT).ToArray_M_1_66128[{System.String.__typeof}] % _M.DOT)(((System.Linq.Enumerable % _M.DOT).OfType_M_1_33064[{System.String.__typeof}] % _M.DOT)(mixedCollection));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, (strings % _M.DOT).Length);
                end
            });
            _M.IM(members, 'ToFloat', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                returnType = function() return System.Single.__typeof end,
                func = function(element, value)
                    return value;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Namespaces','NamespacesTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('NamespacesTests','CsLuaTest.Namespaces', nil, 0, generics, nil, interactionElement, 'Class', 35057);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""NameSpaces"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestNamespaceAsDirectReference""] = (element % _M.DOT_LVL(typeObject.Level)).TestNamespaceAsDirectReference;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestNamespaceAsFullReference""] = (element % _M.DOT_LVL(typeObject.Level)).TestNamespaceAsFullReference;
                end,
            });
            _M.IM(members, 'TestNamespaceAsDirectReference', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((NamespaceA.NamespaceB.NamespaceC.Class1 % _M.DOT).Value, ""OK"");
                end
            });
            _M.IM(members, 'TestNamespaceAsFullReference', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((NamespaceA.NamespaceB.NamespaceC.Class1 % _M.DOT).Value, ""OK"");
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','Base', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Base','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 1705);
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
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""BaseBlank,"";
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, str)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ((Lua.Strings % _M.DOT).format % _M.DOT)(""BaseString{0},"", str);
                end,
            });
            _M.IM(members, 'M1', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""BaseM1,"";
                end
            });
            _M.IM(members, 'M2', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).M1_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','Inheriter', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Inheriter','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 10835);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Override.Base.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736(""test"");
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""InheriterBlank,"";
                end,
            });
            _M.IM(members, '', {
                level = typeObject.Level,
                memberType = 'Cstor',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                scope = 'Public',
                func = function(element, x)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ((Lua.Strings % _M.DOT).format % _M.DOT)(""InheriterInt{0},"", x);
                end,
            });
            _M.IM(members, 'M1', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""InheriterM1,"";
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).M1_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','Level1', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Level1','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 3577);
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
            _M.IM(members, 'M', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""Level1M,"";
                end
            });
            _M.IM(members, 'Level1Self', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).M_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','Level2', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Level2','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 3590);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Override.Level1.__meta(staticValues);
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
            _M.IM(members, 'M', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""Level2M,"";
                end
            });
            _M.IM(members, 'Level2Base', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).M_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'Level2Self', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).M_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','Level3', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Level3','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 3603);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Override.Level2.__meta(staticValues);
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
            _M.IM(members, 'M', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""Level3M,"";
                end
            });
            _M.IM(members, 'Level3Base', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).M_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'Level3Self', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).M_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','Level4', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Level4','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 3616);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Override.Level3.__meta(staticValues);
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
            _M.IM(members, 'M', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""Level4M,"";
                end
            });
            _M.IM(members, 'Level4Base', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).M_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'Level4Self', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).M_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','OverrideTest', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('OverrideTest','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 20771);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Override"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestConstructors""] = (element % _M.DOT_LVL(typeObject.Level)).TestConstructors;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestMethods""] = (element % _M.DOT_LVL(typeObject.Level)).TestMethods;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestMultiLayerMethods""] = (element % _M.DOT_LVL(typeObject.Level)).TestMultiLayerMethods;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestMultiLayerJumps""] = (element % _M.DOT_LVL(typeObject.Level)).TestMultiLayerJumps;
                end,
            });
            _M.IM(members, 'TestConstructors', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    CsLuaTest.Override.Inheriter._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""BaseStringtest,InheriterBlank,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    CsLuaTest.Override.Inheriter._C_0_3926(1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""BaseBlank,InheriterInt1,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestMethods', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local obj = CsLuaTest.Override.Inheriter._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).M1_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""InheriterM1,BaseM1,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).M2_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""InheriterM1,BaseM1,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestMultiLayerMethods', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local obj = CsLuaTest.Override.Level4._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).M_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level4M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).Level4Self_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level4M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).Level4Base_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level3M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).Level3Self_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level4M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).Level3Base_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level2M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).Level2Self_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level4M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).Level2Base_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level1M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((obj % _M.DOT).Level1Self_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Level4M,"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestMultiLayerJumps', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local x1 = CsLuaTest.Override.X1._C_0_0();
                    local res = ((x1 % _M.DOT).Test_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""testx"", res);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((x1 % _M.DOT).DoTest2_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X2Test2"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((x1 % _M.DOT).Test3_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X1Test3X2Test3X3Test3"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((x1 % _M.DOT).DoTest4_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X1Test4"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((x1 % _M.DOT).DoTest5_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X1Test5"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    (element % _M.DOT_LVL(typeObject.Level)).Output = """";
                    ((x1 % _M.DOT).DoTest6_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""X2Test6"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','X1', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('X1','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 323);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Override.X2.__meta(staticValues);
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
            _M.IM(members, 'Test', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return ((element % _M.DOT_LVL(typeObject.Level - 1, true)).Test_M_0_0 % _M.DOT)() +_M.Add+ ""x"";
                end
            });
            _M.IM(members, 'DoTest2', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).Test2_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'Test3', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X1Test3"";
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).Test3_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'Test4', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X1Test4"";
                end
            });
            _M.IM(members, 'Test5', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X1Test5"";
                end
            });
            _M.IM(members, 'Test6', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X1Test6"";
                end
            });
            _M.IM(members, 'DoTest6', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).Test6_M_0_0 % _M.DOT)();
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','X2', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('X2','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 326);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Override.X3.__meta(staticValues);
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
            _M.IM(members, 'Test2', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X2Test2"";
                end
            });
            _M.IM(members, 'Test3', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X2Test3"";
                    ((element % _M.DOT_LVL(typeObject.Level - 1, true)).Test3_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'DoTest4', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Test4_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'Test5', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X2Test5"";
                end
            });
            _M.IM(members, 'DoTest5', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Test5_M_0_0 % _M.DOT)();
                end
            });
            _M.IM(members, 'Test6', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X2Test6"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Override','X3', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('X3','CsLuaTest.Override', nil, 0, generics, nil, interactionElement, 'Class', 329);
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
            _M.IM(members, 'Test', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return ""test"";
                end
            });
            _M.IM(members, 'Test2', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X3Test2"";
                end
            });
            _M.IM(members, 'Test3', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X3Test3"";
                end
            });
            _M.IM(members, 'Test4', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X3Test4"";
                end
            });
            _M.IM(members, 'Test5', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X3Test5"";
                end
            });
            _M.IM(members, 'Test6', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    (CsLuaTest.BaseTest % _M.DOT).Output = (CsLuaTest.BaseTest % _M.DOT).Output +_M.Add+ ""X3Test5"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Params','ClassWithParams', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithParams','CsLuaTest.Params', nil, 0, generics, nil, interactionElement, 'Class', 34139);
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
            _M.IM(members, 'Method1', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 50610,
                isParams = true,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, b, firstParam, ...)
                    local args = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});
                    return (args % _M.DOT).Length;
                end
            });
            _M.IM(members, 'Method2', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 25716,
                isParams = true,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, firstParam, ...)
                    local args = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});
                    return ((element % _M.DOT_LVL(typeObject.Level)).Method1_M_0_50610 % _M.DOT)(true, args);
                end
            });
            _M.IM(members, 'Method3', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 25716,
                isParams = true,
                returnType = function() return System.String.__typeof end,
                func = function(element, firstParam, ...)
                    local args = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});
                    return ""Method3_object"" +_M.Add+ (args % _M.DOT).Length;
                end
            });
            _M.IM(members, 'Method3', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 11778,
                isParams = true,
                returnType = function() return System.String.__typeof end,
                func = function(element, firstParam, ...)
                    local args = (System.Array[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});
                    return ""Method3_int"" +_M.Add+ (args % _M.DOT).Length;
                end
            });
            _M.IM(members, 'Method3', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 26208,
                isParams = true,
                returnType = function() return System.String.__typeof end,
                func = function(element, firstParam, ...)
                    local args = (System.Array[{System.String.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});
                    return ""Method3_string"" +_M.Add+ (args % _M.DOT).Length;
                end
            });
            _M.IM(members, 'MethodExpectingAction', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 225940776,
                func = function(element, a)
                    (a % _M.DOT)((System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = true, ""b"", 3}));
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Params','ParamsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ParamsTests','CsLuaTest.Params', nil, 0, generics, nil, interactionElement, 'Class', 17315);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Params"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""BasicParamsScenario""] = (element % _M.DOT_LVL(typeObject.Level)).BasicParamsScenario;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ParamsWithAmbigiousMethod""] = (element % _M.DOT_LVL(typeObject.Level)).ParamsWithAmbigiousMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestActionWithParams""] = (element % _M.DOT_LVL(typeObject.Level)).TestActionWithParams;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""AdvancedParamsScenario""] = (element % _M.DOT_LVL(typeObject.Level)).AdvancedParamsScenario;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestParamsCalledWithArray""] = (element % _M.DOT_LVL(typeObject.Level)).TestParamsCalledWithArray;
                end,
            });
            _M.IM(members, 'BasicParamsScenario', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Params.ClassWithParams._C_0_0();
                    local i1 = ((c % _M.DOT).Method1_M_0_50610 % _M.DOT)(true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, i1);
                    local i2 = ((c % _M.DOT).Method1_M_0_50610 % _M.DOT)(true, ""abc"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, i2);
                    local i3 = ((c % _M.DOT).Method1_M_0_50610 % _M.DOT)(true, ""abc"", ""def"", ""ghi"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, i3);
                end
            });
            _M.IM(members, 'AdvancedParamsScenario', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Params.ClassWithParams._C_0_0();
                    local i1 = ((c % _M.DOT).Method2_M_0_25716 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, i1);
                    local i2 = ((c % _M.DOT).Method2_M_0_25716 % _M.DOT)(""abc"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, i2);
                    local i3 = ((c % _M.DOT).Method2_M_0_25716 % _M.DOT)(""abc"", ""def"", ""ghi"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, i3);
                end
            });
            _M.IM(members, 'ParamsWithAmbigiousMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Params.ClassWithParams._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Method3_string2"", ((c % _M.DOT).Method3_M_0_26208 % _M.DOT)(""abc"", ""def""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Method3_int3"", ((c % _M.DOT).Method3_M_0_11778 % _M.DOT)(1, 2, 7));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Method3_object4"", ((c % _M.DOT).Method3_M_0_25716 % _M.DOT)(1, 2, 7, ""abc""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Method3_object5"", ((c % _M.DOT).Method3_M_0_25716 % _M.DOT)(1, nil, 2, 7, ""abc""));
                end
            });
            _M.IM(members, 'TestActionWithParams', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    local c = CsLuaTest.Params.ClassWithParams._C_0_0();
                    ((c % _M.DOT).MethodExpectingAction_M_0_225940776 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).ActionWithParams);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Trueb3"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestParamsCalledWithArray', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).ActionWithParams_M_0_25716 % _M.DOT)((System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = ""a"", 1}));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""a1"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'ActionWithParams', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 25716,
                isParams = true,
                func = function(element, firstParam, ...)
                    local args = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});
                    for _,t in (args % _M.DOT).GetEnumerator() do
                        (element % _M.DOT_LVL(typeObject.Level)).Output = (element % _M.DOT_LVL(typeObject.Level)).Output +_M.Add+ ((t % _M.DOT).ToString_M_0_0 % _M.DOT)();
                    end
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Serialization','ClassWithNativeObjects', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithNativeObjects','CsLuaTest.Serialization', nil, 0, generics, nil, interactionElement, 'Class', 81996);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                AString = _M.DV(System.String.__typeof),
                ANumber = _M.DV(System.Int32.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.AString == nil) then element[typeObject.Level].AString = values.AString; end
            if not(values.ANumber == nil) then element[typeObject.Level].ANumber = values.ANumber; end
        end
        local getMembers = function()
            local members = _M.RTEF(getBaseMembers);
            _M.IM(members, 'AString', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'ANumber', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
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
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_0();
                    (element % _M.DOT_LVL(typeObject.Level)).AString = ""TheString"";
                    (element % _M.DOT_LVL(typeObject.Level)).ANumber = 10;
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Serialization','ClassWithSubObject', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithSubObject','CsLuaTest.Serialization', nil, 0, generics, nil, interactionElement, 'Class', 50890);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                AnArray = (System.Array[{System.String.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = ""1"", ""2""}),
                AClass = CsLuaTest.Serialization.ClassWithNativeObjects._C_0_0(),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.AnArray == nil) then element[typeObject.Level].AnArray = values.AnArray; end
            if not(values.AClass == nil) then element[typeObject.Level].AClass = values.AClass; end
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
            _M.IM(members, 'AnArray', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'AClass', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Serialization','SerializationTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('SerializationTests','CsLuaTest.Serialization', nil, 0, generics, nil, interactionElement, 'Class', 54128);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Serialization"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestBasicSerializableClass""] = (element % _M.DOT_LVL(typeObject.Level)).TestBasicSerializableClass;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestClassWithSubObject""] = (element % _M.DOT_LVL(typeObject.Level)).TestClassWithSubObject;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestClassInList""] = (element % _M.DOT_LVL(typeObject.Level)).TestClassInList;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestSerializeDictionary""] = (element % _M.DOT_LVL(typeObject.Level)).TestSerializeDictionary;
                end,
            });
            _M.IM(members, 'TestBasicSerializableClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local serializer = CsLuaFramework.Serializer._C_0_0();
                    local theClass = CsLuaTest.Serialization.ClassWithNativeObjects._C_0_0();
                    local res = ((serializer % _M.DOT).Serialize_M_1_2[{CsLuaTest.Serialization.ClassWithNativeObjects.__typeof}] % _M.DOT)(theClass);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).AString, (res % _M.DOT)[""2_AString""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).ANumber, (res % _M.DOT)[""2_ANumber""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""CsLuaTest.Serialization.ClassWithNativeObjects"", (res % _M.DOT)[""type""]);
                    local processedClass = ((serializer % _M.DOT).Deserialize_M_1_55918[{CsLuaTest.Serialization.ClassWithNativeObjects.__typeof}] % _M.DOT)(res);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).AString, (processedClass % _M.DOT).AString);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).ANumber, (processedClass % _M.DOT).ANumber);
                end
            });
            _M.IM(members, 'TestClassWithSubObject', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local serializer = CsLuaFramework.Serializer._C_0_0();
                    local theClass = CsLuaTest.Serialization.ClassWithSubObject._C_0_0();
                    local res = ((serializer % _M.DOT).Serialize_M_1_2[{CsLuaTest.Serialization.ClassWithSubObject.__typeof}] % _M.DOT)(theClass);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""CsLuaTest.Serialization.ClassWithSubObject"", (res % _M.DOT)[""type""]);
                    local arrayRes = (res % _M.DOT)[""2_AnArray""];
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""System.String[]"", (arrayRes % _M.DOT)[""type""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AnArray % _M.DOT)[0], (arrayRes % _M.DOT)[""3#_0""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AnArray % _M.DOT)[1], (arrayRes % _M.DOT)[""3#_1""]);
                    local subRes = (res % _M.DOT)[""2_AClass""];
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""CsLuaTest.Serialization.ClassWithNativeObjects"", (subRes % _M.DOT)[""type""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AClass % _M.DOT).AString, (subRes % _M.DOT)[""2_AString""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AClass % _M.DOT).ANumber, (subRes % _M.DOT)[""2_ANumber""]);
                    local processedClass = ((serializer % _M.DOT).Deserialize_M_1_55918[{CsLuaTest.Serialization.ClassWithSubObject.__typeof}] % _M.DOT)(res);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AnArray % _M.DOT)[0], ((processedClass % _M.DOT).AnArray % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AnArray % _M.DOT)[1], ((processedClass % _M.DOT).AnArray % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AClass % _M.DOT).AString, ((processedClass % _M.DOT).AClass % _M.DOT).AString);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(((theClass % _M.DOT).AClass % _M.DOT).ANumber, ((processedClass % _M.DOT).AClass % _M.DOT).ANumber);
                end
            });
            _M.IM(members, 'TestClassInList', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local theClass = CsLuaTest.Serialization.ClassWithNativeObjects._C_0_0();
                    local list = (System.Collections.Generic.List[{CsLuaTest.Serialization.ClassWithNativeObjects.__typeof}]._C_0_0() % _M.DOT).__Initialize({
                        theClass
                    });
                    local serializer = CsLuaFramework.Serializer._C_0_0();
                    local res = ((serializer % _M.DOT).Serialize_M_1_2[{System.Collections.Generic.List[{CsLuaTest.Serialization.ClassWithNativeObjects.__typeof}].__typeof}] % _M.DOT)(list);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""System.Collections.Generic.List`1[CsLuaTest.Serialization.ClassWithNativeObjects]"", (res % _M.DOT)[""type""]);
                    local subRes = (res % _M.DOT)[""2#_0""];
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).AString, (subRes % _M.DOT)[""2_AString""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).ANumber, (subRes % _M.DOT)[""2_ANumber""]);
                    local processedClass = ((serializer % _M.DOT).Deserialize_M_1_55918[{System.Collections.Generic.List[{CsLuaTest.Serialization.ClassWithNativeObjects.__typeof}].__typeof}] % _M.DOT)(res);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, (processedClass % _M.DOT).Count);
                    local res1 = (processedClass % _M.DOT)[0];
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).AString, (res1 % _M.DOT).AString);
                    local res2 = ((processedClass % _M.DOT)[0] % _M.DOT).AString;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).AString, res2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((theClass % _M.DOT).ANumber, ((processedClass % _M.DOT)[0] % _M.DOT).ANumber);
                end
            });
            _M.IM(members, 'TestSerializeDictionary', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local serializer = CsLuaFramework.Serializer._C_0_0();
                    local dict = (System.Collections.Generic.Dictionary[{System.Object.__typeof, System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({
                        [43] = ""something"",
                        [""an index""] = ""Someting else""
                    });
                    local res = ((serializer % _M.DOT).Serialize_M_1_2[{System.Collections.Generic.Dictionary[{System.Object.__typeof, System.Object.__typeof}].__typeof}] % _M.DOT)(dict);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((dict % _M.DOT)[43], (res % _M.DOT)[""2#_43""]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((dict % _M.DOT)[""an index""], (res % _M.DOT)[""2_an index""]);
                    local processedDict = ((serializer % _M.DOT).Deserialize_M_1_55918[{System.Collections.Generic.Dictionary[{System.Object.__typeof, System.Object.__typeof}].__typeof}] % _M.DOT)(res);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((dict % _M.DOT)[43], (processedDict % _M.DOT)[43]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)((dict % _M.DOT)[""an index""], (processedDict % _M.DOT)[""an index""]);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Statements','ClassWithSwitch', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithSwitch','CsLuaTest.Statements', nil, 0, generics, nil, interactionElement, 'Class', 34407);
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
            _M.IM(members, 'Switch', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, input)
                    local y = -1;
                    local switchValue = input;
                    if (switchValue == ""a"") then
                        return 1;
                    elseif (switchValue == ""b"") then
                        return 2;
                    elseif (switchValue == ""c"") then
                        local x = 6;
                        x = x / 2;
                        return x;
                    elseif (switchValue == ""d"") then
                        y = 4;
                    elseif (switchValue == ""e"" or switchValue == ""f"") then
                        return 5;
                    elseif (switchValue == ""g"" or true) then
                        return 6;
                    end
                    return y;
                end
            });
            _M.IM(members, 'Switch2', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.Int32.__typeof end,
                func = function(element, input)
                    local switchValue = input;
                    if (true) then
                        return 0;
                    end
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Statements','StatementsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('StatementsTests','CsLuaTest.Statements', nil, 0, generics, nil, interactionElement, 'Class', 35598);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Statement"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""ForStatementTest""] = (element % _M.DOT_LVL(typeObject.Level)).ForStatementTest;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestSwitch""] = (element % _M.DOT_LVL(typeObject.Level)).TestSwitch;
                end,
            });
            _M.IM(members, 'ForStatementTest', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c1 = 0;
                    local i = -3;
                    while (i < 5) do
                        c1 = c1 +_M.Add+ 1;
                    i = i + 1;
                    end
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(8, c1);
                    local c2 = 0;
                    local i = 10;
                    while (i > 0) do
                        c2 = c2 +_M.Add+ 1;
                    i = i - 2;
                    end
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, c2);
                    local c3 = 0;
                    local i = (System.Collections.Generic.List[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({
                        7,
                        9,
                        13
                    });
                    while ((i % _M.DOT).Count > 0) do
                        c3 = c3 +_M.Add+ 1;
                    ((i % _M.DOT).RemoveAt_M_0_3926 % _M.DOT)(0);
                    end
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, c3);
                end
            });
            _M.IM(members, 'TestSwitch', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(1, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""a""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(2, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""b""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""c""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(4, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""d""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""e""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(5, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""f""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""g""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(6, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch_M_0_8736 % _M.DOT)(""XX""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, ((CsLuaTest.Statements.ClassWithSwitch % _M.DOT).Switch2_M_0_8736 % _M.DOT)(""XX""));
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Static','ClassInheritingNonStaticClass', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassInheritingNonStaticClass','CsLuaTest.Static', nil, 0, generics, nil, interactionElement, 'Class', 152059);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Static.NonStaticClass.__meta(staticValues);
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Static','NonStaticClass', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('NonStaticClass','CsLuaTest.Static', nil, 0, generics, nil, interactionElement, 'Class', 28941);
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
            Const = 50,
            Field = 43,
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Const == nil) then element[typeObject.Level].Const = values.Const; end
            if not(values.Field == nil) then element[typeObject.Level].Field = values.Field; end
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
            _M.IM(members, 'Const', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Private',
                static = true,
            });
            _M.IM(members, 'Field', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Private',
                static = true,
            });
            _M.IM(members, 'GetPrivateStaticFieldValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).Field;
                end
            });
            _M.IM(members, 'SetPrivateStaticFieldValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, v)
                    (element % _M.DOT_LVL(typeObject.Level)).Field = v;
                end
            });
            _M.IM(members, 'GetPrivateConstFieldValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).Const;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Static','StaticClass', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('StaticClass','CsLuaTest.Static', nil, 0, generics, nil, interactionElement, 'Class', 16575);
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
            AutoProperty = _M.DV(System.Int32.__typeof),
            PropertyWithGetSet = _M.DV(System.Int32.__typeof),
            Variable = 40,
            VariableWithDefault = _M.DV(System.Int32.__typeof),
            backingField = _M.DV(System.Int32.__typeof),
            Field = 43,
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.AutoProperty == nil) then element[typeObject.Level].AutoProperty = values.AutoProperty; end
            if not(values.PropertyWithGetSet == nil) then element[typeObject.Level].PropertyWithGetSet = values.PropertyWithGetSet; end
            if not(values.Variable == nil) then element[typeObject.Level].Variable = values.Variable; end
            if not(values.VariableWithDefault == nil) then element[typeObject.Level].VariableWithDefault = values.VariableWithDefault; end
            if not(values.backingField == nil) then element[typeObject.Level].backingField = values.backingField; end
            if not(values.Field == nil) then element[typeObject.Level].Field = values.Field; end
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
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""StaticMethodInt"";
                end
            });
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 8736,
                func = function(element, x)
                    (CsLuaTest.BaseTest % _M.DOT).Output = ""StaticMethodString"";
                end
            });
            _M.IM(members, 'Variable', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'GetFromInternal_Variable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).Variable;
                end
            });
            _M.IM(members, 'SetFromInternal_Variable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                func = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level)).Variable = value;
                end
            });
            _M.IM(members, 'VariableWithDefault', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            _M.IM(members, 'GetFromInternal_VariableWithDefault', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Int32.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).VariableWithDefault;
                end
            });
            _M.IM(members, 'AutoProperty',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = true,
                returnType = System.Int32.__typeof;
            });
            _M.IM(members, 'backingField', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Private',
                static = true,
            });
            _M.IM(members, 'PropertyWithGetSet',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = true,
                returnType = System.Int32.__typeof;
                get = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).backingField;
                end,
                set = function(element, value)
                    (element % _M.DOT_LVL(typeObject.Level)).backingField = value * 2;
                end,
            });
            _M.IM(members, 'Field', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = true,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Static','StaticTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('StaticTests','CsLuaTest.Static', nil, 0, generics, nil, interactionElement, 'Class', 17174);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Static"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestStaticClassWithMethod""] = (element % _M.DOT_LVL(typeObject.Level)).TestStaticClassWithMethod;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestStaticClassWithVariable""] = (element % _M.DOT_LVL(typeObject.Level)).TestStaticClassWithVariable;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestStaticClassWithAutoProperty""] = (element % _M.DOT_LVL(typeObject.Level)).TestStaticClassWithAutoProperty;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestStaticClassWithCustomProperty""] = (element % _M.DOT_LVL(typeObject.Level)).TestStaticClassWithCustomProperty;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestStaticField""] = (element % _M.DOT_LVL(typeObject.Level)).TestStaticField;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestStaticFieldInNonStaticClass""] = (element % _M.DOT_LVL(typeObject.Level)).TestStaticFieldInNonStaticClass;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestConstFieldInNonStaticClass""] = (element % _M.DOT_LVL(typeObject.Level)).TestConstFieldInNonStaticClass;
                end,
            });
            _M.IM(members, 'TestStaticClassWithMethod', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((CsLuaTest.Static.StaticClass % _M.DOT).Method_M_0_3926 % _M.DOT)(1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""StaticMethodInt"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                end
            });
            _M.IM(members, 'TestStaticClassWithVariable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(40, (CsLuaTest.Static.StaticClass % _M.DOT).Variable);
                    (CsLuaTest.Static.StaticClass % _M.DOT).Variable = 50;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(50, (CsLuaTest.Static.StaticClass % _M.DOT).Variable);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(50, ((CsLuaTest.Static.StaticClass % _M.DOT).GetFromInternal_Variable_M_0_0 % _M.DOT)());
                    ((CsLuaTest.Static.StaticClass % _M.DOT).SetFromInternal_Variable_M_0_3926 % _M.DOT)(60);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(60, (CsLuaTest.Static.StaticClass % _M.DOT).Variable);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(60, ((CsLuaTest.Static.StaticClass % _M.DOT).GetFromInternal_Variable_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (CsLuaTest.Static.StaticClass % _M.DOT).VariableWithDefault);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, ((CsLuaTest.Static.StaticClass % _M.DOT).GetFromInternal_VariableWithDefault_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'TestStaticClassWithAutoProperty', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (CsLuaTest.Static.StaticClass % _M.DOT).AutoProperty);
                    (CsLuaTest.Static.StaticClass % _M.DOT).AutoProperty = 20;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(20, (CsLuaTest.Static.StaticClass % _M.DOT).AutoProperty);
                end
            });
            _M.IM(members, 'TestStaticClassWithCustomProperty', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(0, (CsLuaTest.Static.StaticClass % _M.DOT).PropertyWithGetSet);
                    (CsLuaTest.Static.StaticClass % _M.DOT).PropertyWithGetSet = 25;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(50, (CsLuaTest.Static.StaticClass % _M.DOT).PropertyWithGetSet);
                end
            });
            _M.IM(members, 'TestStaticField', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, (CsLuaTest.Static.StaticClass % _M.DOT).Field);
                    (CsLuaTest.Static.StaticClass % _M.DOT).Field = 55;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(55, (CsLuaTest.Static.StaticClass % _M.DOT).Field);
                end
            });
            _M.IM(members, 'TestStaticFieldInNonStaticClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Static.NonStaticClass._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, ((c % _M.DOT).GetPrivateStaticFieldValue_M_0_0 % _M.DOT)());
                    ((c % _M.DOT).SetPrivateStaticFieldValue_M_0_3926 % _M.DOT)(55);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(55, ((c % _M.DOT).GetPrivateStaticFieldValue_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'TestConstFieldInNonStaticClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local c = CsLuaTest.Static.ClassInheritingNonStaticClass._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(50, ((c % _M.DOT).GetPrivateConstFieldValue_M_0_0 % _M.DOT)());
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.StringExtensions','StringExtensionTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('StringExtensionTests','CsLuaTest.StringExtensions', nil, 0, generics, nil, interactionElement, 'Class', 68694);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""StringExtension"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestLength""] = (element % _M.DOT_LVL(typeObject.Level)).TestLength;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestContains""] = (element % _M.DOT_LVL(typeObject.Level)).TestContains;
                end,
            });
            _M.IM(members, 'TestLength', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local str = ""abc"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(3, (str % _M.DOT).Length);
                end
            });
            _M.IM(members, 'TestContains', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local str = ""abc"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((str % _M.DOT).Contains_M_0_8736 % _M.DOT)(""bc""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, ((str % _M.DOT).Contains_M_0_8736 % _M.DOT)(""bcd""));
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.TryCatchFinally','CustomException', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('CustomException','CsLuaTest.TryCatchFinally', nil, 0, generics, nil, interactionElement, 'Class', 35181);
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
                signatureHash = 8736,
                scope = 'Public',
                func = function(element, msg)
                    (element % _M.DOT_LVL(typeObject.Level - 1))._C_0_8736(msg);
                end,
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.TryCatchFinally','TryCatchFinallyTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('TryCatchFinallyTests','CsLuaTest.TryCatchFinally', nil, 0, generics, nil, interactionElement, 'Class', 67594);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""TryCatchFinally"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestSimpleThrow""] = (element % _M.DOT_LVL(typeObject.Level)).TestSimpleThrow;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestFinally""] = (element % _M.DOT_LVL(typeObject.Level)).TestFinally;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestFinallyWithCatch""] = (element % _M.DOT_LVL(typeObject.Level)).TestFinallyWithCatch;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestFinallyWithCatchAndExceptionType""] = (element % _M.DOT_LVL(typeObject.Level)).TestFinallyWithCatchAndExceptionType;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestFinallyWithCatchAndThrow""] = (element % _M.DOT_LVL(typeObject.Level)).TestFinallyWithCatchAndThrow;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestCustomExceptionCatching""] = (element % _M.DOT_LVL(typeObject.Level)).TestCustomExceptionCatching;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestExceptionRethrowing""] = (element % _M.DOT_LVL(typeObject.Level)).TestExceptionRethrowing;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestFinallyWithCatchAndRethrow""] = (element % _M.DOT_LVL(typeObject.Level)).TestFinallyWithCatchAndRethrow;
                end,
            });
            _M.IM(members, 'TestSimpleThrow', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    _M.Try(
                        function()
                            _M.Throw(System.Exception._C_0_8736(""Ok""));
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Ok"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                end
            });
            _M.IM(members, 'TestFinally', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""a"";
                    _M.Try(
                        function()
                            s = s +_M.Add+ ""b"";
                        end,
                        {
                        },
                        function()
                            s = s +_M.Add+ ""c"";
                        end
                    );
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abc"", s);
                end
            });
            _M.IM(members, 'TestFinallyWithCatch', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""a"";
                    _M.Try(
                        function()
                            s = s +_M.Add+ ""b"";
                        end,
                        {
                            {
                                func = function()
                                    s = s +_M.Add+ ""x"";
                                end,
                            },
                        },
                        function()
                            s = s +_M.Add+ ""c"";
                        end
                    );
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abc"", s);
                end
            });
            _M.IM(members, 'TestFinallyWithCatchAndExceptionType', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""a"";
                    _M.Try(
                        function()
                            s = s +_M.Add+ ""b"";
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function()
                                    s = s +_M.Add+ ""x"";
                                end,
                            },
                        },
                        function()
                            s = s +_M.Add+ ""c"";
                        end
                    );
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abc"", s);
                end
            });
            _M.IM(members, 'TestFinallyWithCatchAndThrow', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""a"";
                    _M.Try(
                        function()
                            s = s +_M.Add+ ""b"";
                            _M.Throw(System.Exception._C_0_8736(""Error""));
                            s = s +_M.Add+ ""x"";
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function()
                                    s = s +_M.Add+ ""c"";
                                end,
                            },
                        },
                        function()
                            s = s +_M.Add+ ""d"";
                        end
                    );
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abcd"", s);
                end
            });
            _M.IM(members, 'TestCustomExceptionCatching', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""a"";
                    _M.Try(
                        function()
                            s = s +_M.Add+ ""b"";
                            _M.Throw(CsLuaTest.TryCatchFinally.CustomException._C_0_8736(""Error""));
                        end,
                        {
                            {
                                type = CsLuaTest.TryCatchFinally.CustomException.__typeof,
                                func = function()
                                    s = s +_M.Add+ ""c"";
                                end,
                            },
                            {
                                type = System.Exception.__typeof,
                                func = function()
                                    s = s +_M.Add+ ""x"";
                                end,
                            },
                        },
                        function()
                            s = s +_M.Add+ ""d"";
                        end
                    );
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abcd"", s);
                end
            });
            _M.IM(members, 'TestExceptionRethrowing', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""a"";
                    _M.Try(
                        function()
                            _M.Try(
                                function()
                                    s = s +_M.Add+ ""b"";
                                    _M.Throw(System.Exception._C_0_8736(""Error""));
                                end,
                                {
                                    {
                                        type = CsLuaTest.TryCatchFinally.CustomException.__typeof,
                                        func = function()
                                            s = s +_M.Add+ ""x"";
                                        end,
                                    },
                                },
                                nil
                            );
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function()
                                    s = s +_M.Add+ ""c"";
                                end,
                            },
                        },
                        nil
                    );
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abc"", s);
                end
            });
            _M.IM(members, 'TestFinallyWithCatchAndRethrow', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local s = ""a"";
                    _M.Try(
                        function()
                            _M.Try(
                                function()
                                    s = s +_M.Add+ ""b"";
                                    _M.Throw(System.Exception._C_0_8736(""Error""));
                                end,
                                {
                                    {
                                        type = CsLuaTest.TryCatchFinally.CustomException.__typeof,
                                        func = function()
                                            s = s +_M.Add+ ""x"";
                                        end,
                                    },
                                },
                                function()
                                    s = s +_M.Add+ ""c"";
                                end
                            );
                        end,
                        {
                            {
                                type = System.Exception.__typeof,
                                func = function()
                                    s = s +_M.Add+ ""d"";
                                end,
                            },
                        },
                        nil
                    );
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""abcd"", s);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Type','ClassA', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassA','CsLuaTest.Type', nil, 0, generics, nil, interactionElement, 'Class', 3858);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        table.insert(implements, CsLuaTest.Type.InterfaceA.__typeof);
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Type','ClassB', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassB','CsLuaTest.Type', nil, 0, generics, nil, interactionElement, 'Class', 3871);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.Type.ClassA.__meta(staticValues);
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
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Type','GenericTypeTestClass', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T1'] = 1};
        local typeObject = System.Type('GenericTypeTestClass','CsLuaTest.Type', nil, 1, generics, nil, interactionElement, 'Class', (132236*generics[genericsMapping['T1']].signatureHash));
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
            _M.IM(members, 'GetClassGenericsName', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return (generics[genericsMapping['T1']] % _M.DOT).Name;
                end
            });
            local methodGenericsMapping = {['T2'] = 1};
            local methodGenerics = _M.MG(methodGenericsMapping);
            _M.IM(members, 'GetMethodGenericsName', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 1,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
                generics = methodGenericsMapping,
                func = function(element, methodGenericsMapping, methodGenerics)
                    return (methodGenerics[methodGenericsMapping['T2']] % _M.DOT).Name;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Type','InterfaceA', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('InterfaceA','CsLuaTest.Type', nil, 0, generics, nil, interactionElement, 'Interface',12081);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Type','TypeTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('TypeTests','CsLuaTest.Type', nil, 0, generics, nil, interactionElement, 'Class', 10839);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Type"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGetTypeOfNumber""] = (element % _M.DOT_LVL(typeObject.Level)).TestGetTypeOfNumber;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestGetTypeOfClass""] = (element % _M.DOT_LVL(typeObject.Level)).TestGetTypeOfClass;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestIsType""] = (element % _M.DOT_LVL(typeObject.Level)).TestIsType;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestIsInstanceOf""] = (element % _M.DOT_LVL(typeObject.Level)).TestIsInstanceOf;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestTypeof""] = (element % _M.DOT_LVL(typeObject.Level)).TestTypeof;
                end,
            });
            _M.IM(members, 'TestGetTypeOfNumber', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local num = 123;
                    local type = ((num % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""Int32"", (type % _M.DOT).Name);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""System"", (type % _M.DOT).Namespace);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""System.Int32"", (type % _M.DOT).FullName);
                end
            });
            _M.IM(members, 'TestGetTypeOfClass', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local obj = CsLuaTest.Type.ClassA._C_0_0();
                    local type = ((obj % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""ClassA"", (type % _M.DOT).Name);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""CsLuaTest.Type"", (type % _M.DOT).Namespace);
                end
            });
            _M.IM(members, 'TestIsType', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.Type.ClassA.__is(CsLuaTest.Type.ClassA._C_0_0()));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.Type.ClassA.__is(CsLuaTest.Type.ClassB._C_0_0()));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, CsLuaTest.Type.ClassB.__is(CsLuaTest.Type.ClassA._C_0_0()));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.Type.ClassB.__is(CsLuaTest.Type.ClassB._C_0_0()));
                end
            });
            _M.IM(members, 'TestIsInstanceOf', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local obj = CsLuaTest.Type.ClassA._C_0_0();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.Type.ClassA.__is(obj));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.Type.InterfaceA.__is(obj));
                    local typeClass = CsLuaTest.Type.ClassA.__typeof;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((typeClass % _M.DOT).IsInstanceOfType_M_0_8572 % _M.DOT)(obj));
                    local typeInterface = CsLuaTest.Type.InterfaceA.__typeof;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((typeInterface % _M.DOT).IsInstanceOfType_M_0_8572 % _M.DOT)(obj));
                end
            });
            _M.IM(members, 'TestTypeof', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""ClassA"", (CsLuaTest.Type.ClassA.__typeof % _M.DOT).Name);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""ClassA"", ((CsLuaTest.Type.GenericTypeTestClass[{CsLuaTest.Type.ClassA.__typeof}]._C_0_0() % _M.DOT).GetClassGenericsName_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""String"", ((CsLuaTest.Type.GenericTypeTestClass[{CsLuaTest.Type.ClassA.__typeof}]._C_0_0() % _M.DOT).GetMethodGenericsName_M_1_0[{System.String.__typeof}] % _M.DOT)());
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.TypeMethods','Class1', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('Class1','CsLuaTest.TypeMethods', nil, 0, generics, nil, interactionElement, 'Class', 3650);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
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
            _M.IM(members, 'Value', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Public',
                static = false,
            });
            _M.IM(members, 'Equals', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8572,
                override = true,
                returnType = function() return System.Boolean.__typeof end,
                func = function(element, obj)
                    if (CsLuaTest.TypeMethods.Class1.__is(obj)) then
                        local otherClass = (obj);
                        return (((otherClass % _M.DOT).Value % _M.DOT).Equals_M_0_8736 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).Value);
                    end
                    return false;
                end
            });
            _M.IM(members, 'ToString', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                override = true,
                returnType = function() return System.String.__typeof end,
                func = function(element)
                    return (element % _M.DOT_LVL(typeObject.Level)).Value;
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.TypeMethods','TypeMethodsTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('TypeMethodsTests','CsLuaTest.TypeMethods', nil, 0, generics, nil, interactionElement, 'Class', 40665);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""TypeMethods"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestEquals""] = (element % _M.DOT_LVL(typeObject.Level)).TestEquals;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestToString""] = (element % _M.DOT_LVL(typeObject.Level)).TestToString;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestIntParse""] = (element % _M.DOT_LVL(typeObject.Level)).TestIntParse;
                end,
            });
            _M.IM(members, 'TestEquals', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, (((""test"") % _M.DOT).Equals_M_0_8736 % _M.DOT)(""test""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, (((""test"") % _M.DOT).Equals_M_0_8736 % _M.DOT)(""test2""));
                    local i = 43;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((i % _M.DOT).Equals_M_0_3926 % _M.DOT)(43));
                    local c1a = (CsLuaTest.TypeMethods.Class1._C_0_0() % _M.DOT).__Initialize({Value = ""A""});
                    local c1b = (CsLuaTest.TypeMethods.Class1._C_0_0() % _M.DOT).__Initialize({Value = ""A""});
                    local c1c = (CsLuaTest.TypeMethods.Class1._C_0_0() % _M.DOT).__Initialize({Value = ""C""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((c1a % _M.DOT).Equals_M_0_8572 % _M.DOT)(c1b));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(false, ((c1a % _M.DOT).Equals_M_0_8572 % _M.DOT)(c1c));
                end
            });
            _M.IM(members, 'TestToString', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    local value = ((43 % _M.DOT).ToString_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""43"", value);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""43"", (((43) % _M.DOT).ToString_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""43"", ((43 % _M.DOT).ToString_M_0_0 % _M.DOT)());
                    local x = true;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""True"", ((x % _M.DOT).ToString_M_0_0 % _M.DOT)());
                    local c1 = (CsLuaTest.TypeMethods.Class1._C_0_0() % _M.DOT).__Initialize({Value = ""c1""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""c1"", ((c1 % _M.DOT).ToString_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'TestIntParse', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, ((System.Int32 % _M.DOT).Parse_M_0_8736 % _M.DOT)(""43""));
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Wrap','ClassA', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassA','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Class', 3858);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = System.Object.__meta(staticValues);
        typeObject.baseType = baseTypeObject;
        typeObject.level = baseTypeObject.level + 1;
        typeObject.implements = implements;
        local elementGenerator = function()
            local element = baseElementGenerator();
            element.type = typeObject;
            element[typeObject.Level] = {
                Value = _M.DV(System.String.__typeof),
            };
            return element;
        end
        staticValues[typeObject.Level] = {
        };
        local initialize = function(element, values)
            if baseInitialize then baseInitialize(element, values); end
            if not(values.Value == nil) then element[typeObject.Level].Value = values.Value; end
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
            _M.IM(members, 'Value',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IA', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IA','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',341);
        local implements = {
            CsLuaTest.Wrap.IBase.__typeof,
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'IsA', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Boolean.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IB', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IB','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',344);
        local implements = {
            CsLuaTest.Wrap.IBase.__typeof,
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'IsB', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Boolean.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IBase', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IBase','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',2745);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'IsBase', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Boolean.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IEmptyInterface', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IEmptyInterface','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',33748);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IInheritingInterface', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IInheritingInterface','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',65750);
        local implements = {
            CsLuaTest.Wrap.ISimpleInterface.__typeof,
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IInheritingInterfaceWithGenerics', _M.NE({
    [2] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T1'] = 1,['T2'] = 2};
        local typeObject = System.Type('IInheritingInterfaceWithGenerics','CsLuaTest.Wrap', nil, 2, generics, nil, interactionElement, 'Interface',(380868*generics[genericsMapping['T1']].signatureHash)+(571302*generics[genericsMapping['T2']].signatureHash));
        local implements = {
            CsLuaTest.Wrap.IInterfaceWithGenerics[{generics[genericsMapping['T2']]}].__typeof,
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IInterfaceWithGenerics', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('IInterfaceWithGenerics','CsLuaTest.Wrap', nil, 1, generics, nil, interactionElement, 'Interface',(163318*generics[genericsMapping['T']].signatureHash));
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = (2*generics[genericsMapping['T']].signatureHash),
                returnType = function() return System.String.__typeof end,
            });
            _M.IM(members, 'Property',{
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
_M.ATN('CsLuaTest.Wrap','IInterfaceWithIndexer', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IInterfaceWithIndexer','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',73768);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members,'#',{
                level = typeObject.Level,
                memberType = 'Indexer',
                scope = 'Public',
                returnType = System.String.__typeof,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IInterfaceWithMethod', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IInterfaceWithMethod','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',65610);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 234849084,
                returnType = function() return System.Boolean.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IInterfaceWithMultipleReturnValues', _M.NE({
    [1] = function(interactionElement, generics, staticValues)
        local genericsMapping = {['T'] = 1};
        local typeObject = System.Type('IInterfaceWithMultipleReturnValues','CsLuaTest.Wrap', nil, 1, generics, nil, interactionElement, 'Interface',(449214*generics[genericsMapping['T']].signatureHash));
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return CsLuaFramework.Wrapping.IMultipleValues[{System.String.__typeof, System.Int32.__typeof, generics[genericsMapping['T']]}].__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IInterfaceWithWrappedValues', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IInterfaceWithWrappedValues','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',131426);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'GetInner', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return CsLuaTest.Wrap.IInterfaceWithWrappedValues.__typeof end,
            });
            _M.IM(members, 'Inner',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = CsLuaTest.Wrap.IInterfaceWithWrappedValues.__typeof;
            });
            _M.IM(members, 'GetValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
            });
            _M.IM(members, 'Property',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.Boolean.__typeof;
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','INonWrappedProperty', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('INonWrappedProperty','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',61459);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Property',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = CsLuaTest.Wrap.ClassA.__typeof;
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IPartial', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IPartial','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',8011);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'MethodA', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
            });
            _M.IM(members, 'MethodB', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.String.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IProducer', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IProducer','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',10578);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Produce', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return CsLuaTest.Wrap.IBase.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','IReturningNativeTypes', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('IReturningNativeTypes','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',75444);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'ReturnObject', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return System.Object.__typeof end,
            });
            _M.IM(members, 'ReturnLuaTable', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = function() return Lua.NativeLuaTable.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','ISetSelfInterface', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ISetSelfInterface','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',44739);
        local implements = {
        };
        typeObject.implements = implements;
        local attributes = {
            CsLuaFramework.Attributes.ProvideSelfAttribute.__typeof};
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.String.__typeof end,
            });
            _M.IM(members, 'Method2', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 43680,
                returnType = function() return System.String.__typeof end,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','ISimpleInterface', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ISimpleInterface','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Interface',39025);
        local implements = {
        };
        typeObject.implements = implements;
        local getMembers = function()
            local members = {};
            _M.GAM(members, implements);
            _M.IM(members, 'Method', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 8736,
                returnType = function() return System.String.__typeof end,
            });
            _M.IM(members, 'Value',{
                level = typeObject.Level,
                memberType = 'AutoProperty',
                scope = 'Public',
                static = false,
                returnType = System.Int32.__typeof;
            });
            _M.IM(members, 'MethodVoid', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 12036,
            });
            return members;
        end
        return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;
    end,
}));
_M.ATN('CsLuaTest.Wrap','WrapTests', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('WrapTests','CsLuaTest.Wrap', nil, 0, generics, nil, interactionElement, 'Class', 10826);
        local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = CsLuaTest.BaseTest.__meta(staticValues);
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
                    (element % _M.DOT_LVL(typeObject.Level)).Name = ""Wrap"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapSimpleInterface""] = (element % _M.DOT_LVL(typeObject.Level)).WrapSimpleInterface;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestWrapAndUnwrap""] = (element % _M.DOT_LVL(typeObject.Level)).TestWrapAndUnwrap;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapInheritingInterface""] = (element % _M.DOT_LVL(typeObject.Level)).WrapInheritingInterface;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapGenericInterface""] = (element % _M.DOT_LVL(typeObject.Level)).WrapGenericInterface;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapInheritingInterfaceWithGenericInterface""] = (element % _M.DOT_LVL(typeObject.Level)).WrapInheritingInterfaceWithGenericInterface;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapInheritingInterfaceWithProvideSelf""] = (element % _M.DOT_LVL(typeObject.Level)).WrapInheritingInterfaceWithProvideSelf;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapHandleMultipleValues""] = (element % _M.DOT_LVL(typeObject.Level)).WrapHandleMultipleValues;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapGenericWithProperty""] = (element % _M.DOT_LVL(typeObject.Level)).WrapGenericWithProperty;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapHandleMultipleValues""] = (element % _M.DOT_LVL(typeObject.Level)).WrapHandleMultipleValues;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapHandleRecursiveWrapping""] = (element % _M.DOT_LVL(typeObject.Level)).WrapHandleRecursiveWrapping;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapWithTargetTypeTranslation""] = (element % _M.DOT_LVL(typeObject.Level)).WrapWithTargetTypeTranslation;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""NonWrappedAsPropertyInWrappedObject""] = (element % _M.DOT_LVL(typeObject.Level)).NonWrappedAsPropertyInWrappedObject;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrappedObjectWithPartialInterface""] = (element % _M.DOT_LVL(typeObject.Level)).WrappedObjectWithPartialInterface;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrappedObjectWithInterfaceWithIndexer""] = (element % _M.DOT_LVL(typeObject.Level)).WrappedObjectWithInterfaceWithIndexer;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapperReplacesActionAndFuncWithLuaFunctions""] = (element % _M.DOT_LVL(typeObject.Level)).WrapperReplacesActionAndFuncWithLuaFunctions;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""WrapperDoesNotWrapAReturedLuaTableIfExpectingItOrObject""] = (element % _M.DOT_LVL(typeObject.Level)).WrapperDoesNotWrapAReturedLuaTableIfExpectingItOrObject;
                end,
            });
            _M.IM(members, 'WrapSimpleInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""interfaceImplementation = { Method = function(str) return 'OK' .. str; end, Value = 10, MethodVoid = function() end, };"");
                    local interfaceImplementation = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.ISimpleInterface.__typeof}] % _M.DOT)(""interfaceImplementation"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OKInput"", ((interfaceImplementation % _M.DOT).Method_M_0_8736 % _M.DOT)(""Input""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(10, (interfaceImplementation % _M.DOT).Value);
                    (interfaceImplementation % _M.DOT).Value = 20;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(20, (interfaceImplementation % _M.DOT).Value);
                    ((interfaceImplementation % _M.DOT).MethodVoid_M_0_12036 % _M.DOT)(true);
                end
            });
            _M.IM(members, 'WrapGenericInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""interfaceImplementation = { Method = function(n) return 'OK' .. n; end, };"");
                    local interfaceImplementation = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInterfaceWithGenerics[{System.Int32.__typeof}].__typeof}] % _M.DOT)(""interfaceImplementation"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OK10"", ((interfaceImplementation % _M.DOT).Method_M_0_3926 % _M.DOT)(10));
                end
            });
            _M.IM(members, 'WrapGenericWithProperty', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""interfaceImplementation = { Method = function() end, Property = { Value = 43, Method = function() end}, };"");
                    local interfaceImplementation = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInterfaceWithGenerics[{CsLuaTest.Wrap.ISimpleInterface.__typeof}].__typeof}] % _M.DOT)(""interfaceImplementation"");
                    local inner = (interfaceImplementation % _M.DOT).Property;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, (inner % _M.DOT).Value);
                end
            });
            _M.IM(members, 'WrapInheritingInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""interfaceImplementation = { Method = function(str) return 'OK' .. str; end, Value = 10, };"");
                    local interfaceImplementation = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInheritingInterface.__typeof}] % _M.DOT)(""interfaceImplementation"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OKInput"", ((interfaceImplementation % _M.DOT).Method_M_0_8736 % _M.DOT)(""Input""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(10, (interfaceImplementation % _M.DOT).Value);
                    (interfaceImplementation % _M.DOT).Value = 20;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(20, (interfaceImplementation % _M.DOT).Value);
                end
            });
            _M.IM(members, 'WrapInheritingInterfaceWithGenericInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""interfaceImplementation = { Method = function(n) return 'OK' .. n; end, };"");
                    local interfaceImplementation = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInheritingInterfaceWithGenerics[{System.String.__typeof, System.Int32.__typeof}].__typeof}] % _M.DOT)(""interfaceImplementation"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OK10"", ((interfaceImplementation % _M.DOT).Method_M_0_3926 % _M.DOT)(10));
                end
            });
            _M.IM(members, 'WrapInheritingInterfaceWithProvideSelf', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""interfaceImplementation = { Method = function(self, str) return 'OK' .. str; end, Method2 = function(self,a,b,c) return tostring(a)..tostring(b)..tostring(c); end  };"");
                    local interfaceImplementation = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.ISetSelfInterface.__typeof}] % _M.DOT)(""interfaceImplementation"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OKmore"", ((interfaceImplementation % _M.DOT).Method_M_0_8736 % _M.DOT)(""more""));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""nilbc"", ((interfaceImplementation % _M.DOT).Method2_M_0_43680 % _M.DOT)(nil, ""b"", ""c""));
                end
            });
            _M.IM(members, 'WrapHandleMultipleValues', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""interfaceImplementation = { Method = function() return 'OK', 43, true; end, };"");
                    local interfaceImplementation = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInterfaceWithMultipleReturnValues[{System.Boolean.__typeof}].__typeof}] % _M.DOT)(""interfaceImplementation"");
                    local multiple = ((interfaceImplementation % _M.DOT).Method_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""OK"", (multiple % _M.DOT).Value1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, (multiple % _M.DOT).Value2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, (multiple % _M.DOT).Value3);
                end
            });
            _M.IM(members, 'WrapHandleRecursiveWrapping', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)([[
                local recursiveInterfaceGenerator = function(inner, value)
                    return {
                        GetInner = function() return inner; end,
                        Inner = inner,
                        GetValue = function() return value; end,
                        Property = true,
                    };
                end

                A = recursiveInterfaceGenerator(null, 'a');
                B = recursiveInterfaceGenerator(A, 'b');
                C = recursiveInterfaceGenerator(B, 'c');
            ]]);
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    local C = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInterfaceWithWrappedValues.__typeof}] % _M.DOT)(""C"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)('c', ((C % _M.DOT).GetValue_M_0_0 % _M.DOT)());
                    local B = (C % _M.DOT).Inner;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)('b', ((B % _M.DOT).GetValue_M_0_0 % _M.DOT)());
                    local A = ((B % _M.DOT).GetInner_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)('a', ((A % _M.DOT).GetValue_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'WrapWithTargetTypeTranslation', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)([[
                retTrue = function() return true; end;

                A = {
                    IsBase = retTrue,
                    IsA = retTrue,
                }
                B = {
                    IsBase = retTrue,
                    IsB = retTrue,
                }

                P = {
                    Produce = function(s)
                        return _G[s];
                    end
                };
            ]]);
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    local producer = ((wrapper % _M.DOT).Wrap_M_1_318953760[{CsLuaTest.Wrap.IProducer.__typeof}] % _M.DOT)(""P"", System.Func[{Lua.NativeLuaTable.__typeof, System.Type.__typeof}]._C_0_16704(function(table) return (((table % _M.DOT)[""IsA""] ~= nil) and CsLuaTest.Wrap.IA.__typeof or CsLuaTest.Wrap.IB.__typeof) end));
                    local a = ((producer % _M.DOT).Produce_M_0_8736 % _M.DOT)(""A"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, CsLuaTest.Wrap.IA.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, (((a) % _M.DOT).IsA_M_0_0 % _M.DOT)());
                    local b = ((producer % _M.DOT).Produce_M_0_8736 % _M.DOT)(""B"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((b % _M.DOT).IsB_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'NonWrappedAsPropertyInWrappedObject', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""A = {};"");
                    local obj = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.INonWrappedProperty.__typeof}] % _M.DOT)(""A"");
                    local cA = (CsLuaTest.Wrap.ClassA._C_0_0() % _M.DOT).__Initialize({Value = ""ok""});
                    (obj % _M.DOT).Property = cA;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, (obj % _M.DOT).Property == cA);
                    (cA % _M.DOT).Value = ""2"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""2"", ((obj % _M.DOT).Property % _M.DOT).Value);
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)(""A2 = A"");
                    local objRef2 = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.INonWrappedProperty.__typeof}] % _M.DOT)(""A2"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, (objRef2 % _M.DOT).Property == cA);
                end
            });
            _M.IM(members, 'WrappedObjectWithPartialInterface', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)([[
                P = {
                    MethodA = function()
                        return 'MA';
                    end,
                    MethodB = function()
                        return 'MB';
                    end
                };
            ]]);
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    local obj = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IPartial.__typeof}] % _M.DOT)(""P"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""MA"", ((obj % _M.DOT).MethodA_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""MB"", ((obj % _M.DOT).MethodB_M_0_0 % _M.DOT)());
                end
            });
            _M.IM(members, 'WrappedObjectWithInterfaceWithIndexer', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)([[
                P = { Value1 = 'V1' };
            ]]);
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    local obj = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInterfaceWithIndexer.__typeof}] % _M.DOT)(""P"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""V1"", (obj % _M.DOT)[""Value1""]);
                    (obj % _M.DOT)[""Value2""] = ""V2"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(""V2"", (obj % _M.DOT)[""Value2""]);
                end
            });
            _M.IM(members, 'WrapperReplacesActionAndFuncWithLuaFunctions', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)([[
                P = { 
                    Method = function(f, a)
                        return type(f) == 'function' and type(a) == 'function';
                    end
                };
            ]]);
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    local obj = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IInterfaceWithMethod.__typeof}] % _M.DOT)(""P"");
                    local inputValue;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((obj % _M.DOT).Method_M_0_234849084 % _M.DOT)(System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(x) return x < 10 end), System.Action[{System.Boolean.__typeof}]._C_0_16704(function(input)inputValue = input end)));
                end
            });
            _M.IM(members, 'WrapperDoesNotWrapAReturedLuaTableIfExpectingItOrObject', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)([[
                P = { 
                    ReturnObject = function()
                        return { X = true };
                    end,
                    ReturnLuaTable = function()
                        return { Y = true };
                    end
                };
            ]]);
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    local obj = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IReturningNativeTypes.__typeof}] % _M.DOT)(""P"");
                    local t1 = ((obj % _M.DOT).ReturnLuaTable_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, Lua.NativeLuaTable.__is(t1));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, (t1 % _M.DOT)[""Y""]);
                    local t2 = ((obj % _M.DOT).ReturnObject_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, Lua.NativeLuaTable.__is(t2));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(true, ((t2) % _M.DOT)[""X""]);
                end
            });
            _M.IM(members, 'TestWrapAndUnwrap', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 0,
                func = function(element)
                    if (not((CsLuaFramework.Environment % _M.DOT).IsExecutingAsLua)) then
                        return;
                    end
                    ((CsLuaFramework.Environment % _M.DOT).ExecuteLuaCode_M_0_8736 % _M.DOT)([[
                O = { 
                    Value = 43;
                };
            ]]);
                    local wrapper = CsLuaFramework.Wrapping.Wrapper._C_0_0();
                    local obj = ((wrapper % _M.DOT).Wrap_M_1_8736[{CsLuaTest.Wrap.IReturningNativeTypes.__typeof}] % _M.DOT)(""O"");
                    local table = ((wrapper % _M.DOT).Unwrap_M_1_2[{CsLuaTest.Wrap.IReturningNativeTypes.__typeof}] % _M.DOT)(obj);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_43270 % _M.DOT)(43, (table % _M.DOT)[""Value""]);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
(CsLuaTest.CsLuaTest._C_0_0() % _M.DOT).Execute();";

    }
}
