
namespace CsLuaConverterTests
{
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CsLuaConverter;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.MethodSignature;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.PartialElement;
    using CsLuaConverter.Providers.TypeKnowledgeRegistry;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

    [TestClass]
    public class GeneratedLuaMatchingTest
    {
        private readonly string testOutputFolder = Directory.GetCurrentDirectory();

        [TestMethod]
        public void CsLuaMatchesEarlierVersionsOutput()
        {
            var csLuaTestProjectPath = this.testOutputFolder + "\\..\\..\\..\\..\\CsLuaProjects\\CsLuaTest.sln";
            var fileInfo = new FileInfo(csLuaTestProjectPath);
            var workspace = MSBuildWorkspace.Create();
            var solutionTask = workspace.OpenSolutionAsync(fileInfo.FullName);
            solutionTask.Wait();
            var solution = solutionTask.Result;
            var project = solution.Projects.Single(p => p.Name.Equals("CsLuaTest"));

            var providers = new EmptyProviders();
            providers.PartialElementState = new PartialElementState();
            providers.SemanticAdaptor = new TypeSymbolSemanticAdaptor();
            providers.TypeReferenceWriter = new TypeReferenceWriter<ITypeSymbol>(providers.SemanticAdaptor);
            providers.SignatureWriter = new SignatureWriter<ITypeSymbol>(new SignatureComposer<ITypeSymbol>(providers.SemanticAdaptor), providers.TypeReferenceWriter);
            var analyzer = new Analyzer(new CodeTreeVisitor(providers));

            var namespaces = analyzer.GetNamespaces(project).ToArray();
            var comparingTextWriter = new ComparingTextWriter(CsLuaCompiled);
            namespaces.First().WritingAction(new IndentedTextWriterWrapper(comparingTextWriter));
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
            ContinueOnError = true,
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
                                    ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName  +_M.Add+  "" Success"");
                                end,
                                {
                                    {
                                        type = CsLuaTest.TestIgnoredException.__typeof,
                                        func = function(ex)
                                            (element % _M.DOT_LVL(typeObject.Level)).IgnoreCount = (element % _M.DOT_LVL(typeObject.Level)).IgnoreCount + 1;
                                            ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName  +_M.Add+  "" Ignored"");
                                        end,
                                    },
                                    {
                                        type = System.Exception.__typeof,
                                        func = function(ex)
                                            (element % _M.DOT_LVL(typeObject.Level)).FailCount = (element % _M.DOT_LVL(typeObject.Level)).FailCount + 1;
                                            ((lineWriter % _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName  +_M.Add+  "" Failed"");
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
                signatureHash = 21430,
                func = function(element, expectedValueObj, actualValueObj)
                    local expectedValue = ((Lua.Strings % _M.DOT).tostring_M_0_8572 % _M.DOT)(expectedValueObj);
                    local actualValue = ((Lua.Strings % _M.DOT).tostring_M_0_8572 % _M.DOT)(actualValueObj);
                    if (expectedValue ~= actualValue) then
                        _M.Throw(System.Exception._C_0_8736(((Lua.Strings % _M.DOT).format_M_0_47310 % _M.DOT)(""Incorrect value. Expected: '{0}' got: '{1}'."", expectedValue, actualValue)));
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
                        CsLuaTest.ActionsFunctions.ActionsFunctionsTests._C_0_0()
                    });
                    ((tests % _M.DOT).ForEach_M_0_239752368 % _M.DOT)(System.Action[{CsLuaTest.ITestSuite.__typeof}]._C_0_16704(function(test) return ((test % _M.DOT).PerformTests_M_0_104846 % _M.DOT)(CsLuaTest.IndentedLineWriter._C_0_0()) end));
                    ((Lua.Core % _M.DOT).print_M_0_25716 % _M.DOT)(""CsLua test completed."");
                    ((Lua.Core % _M.DOT).print_M_0_25716 % _M.DOT)((CsLuaTest.BaseTest % _M.DOT).TestCount, ""tests run."", (CsLuaTest.BaseTest % _M.DOT).FailCount, ""failed."", (CsLuaTest.BaseTest % _M.DOT).TestCount - (CsLuaTest.BaseTest % _M.DOT).FailCount - (CsLuaTest.BaseTest % _M.DOT).IgnoreCount, ""succeded. "", (CsLuaTest.BaseTest % _M.DOT).IgnoreCount, ""ignored."");
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
                    ((Lua.Core % _M.DOT).print_M_0_25716 % _M.DOT)(((Lua.Strings % _M.DOT).strrep_M_0_14625 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).indentChar, (element % _M.DOT_LVL(typeObject.Level)).indent)  +_M.Add+  line);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestMethodsCastToActionAndFunction""] = (element % _M.DOT_LVL(typeObject.Level)).TestMethodsCastToActionAndFunction;
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Action[{System.Int32.__typeof}].__is(action));
                    ((action % _M.DOT).Invoke % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(43, invokedValue);
                    (action % _M.DOT)(10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(10, invokedValue);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""int"", r1);
                    local r2 = ((CsLuaTest.ActionsFunctions.StaticClass % _M.DOT).ExpectFunc_M_0_75172368 % _M.DOT)(System.Func[{System.Object.__typeof, System.String.__typeof}]._C_0_16704(function(v) return ((v % _M.DOT).ToString_M_0_0 % _M.DOT)() end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""obj"", r2);
                    local r3 = ((CsLuaTest.ActionsFunctions.StaticClass % _M.DOT).ExpectFunc_M_0_74961534 % _M.DOT)(System.Func[{System.Single.__typeof, System.String.__typeof}]._C_0_16704(function(v) return ((v % _M.DOT).ToString_M_0_0 % _M.DOT)() end), true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""float"", r3);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(43, value);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(System.Action[{System.Int32.__typeof}].__typeof, action1Type);
                    local func1 = (mc % _M.DOT).MethodWithReturn;
                    local func1Type = ((func1 % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(System.Func[{System.Int32.__typeof, System.String.__typeof}].__typeof, func1Type);
                    local value1 = (func1 % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""43"", value1);
                    local genericMethod = (mc % _M.DOT).MethodWithReturnAndGeneric;
                    local genericFuncType = ((genericMethod % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(System.Func[{System.Double.__typeof, System.String.__typeof}].__typeof, genericFuncType);
                    local valueGeneric = (genericMethod % _M.DOT)(43.03);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""43.03"", valueGeneric);
                    local action2 = (CsLuaTest.ActionsFunctions.ClassWithMethods % _M.DOT).StaticMethodWithNoReturn;
                    local action2Type = ((action2 % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(System.Action[{System.Int32.__typeof}].__typeof, action2Type);
                    local func2 = (CsLuaTest.ActionsFunctions.ClassWithMethods % _M.DOT).StaticMethodWithReturn;
                    local func2Type = ((func2 % _M.DOT).GetType_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(System.Func[{System.Int32.__typeof, System.String.__typeof}].__typeof, func2Type);
                    local value2 = (func2 % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""43"", value2);
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
                returnType = System.String.__typeof,
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
                returnType = System.String.__typeof,
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
                returnType = System.String.__typeof,
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
                returnType = System.String.__typeof,
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
                returnType = System.String.__typeof,
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
                returnType = System.String.__typeof,
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
                returnType = methodGenerics[methodGenericsMapping['T']],
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, CsLuaTest.ActivatorImplementation.Class1.__is(value1));
                    local value2 = ((System.Activator % _M.DOT).CreateInstance_M_1_0[{CsLuaTest.ActivatorImplementation.Class1.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, CsLuaTest.ActivatorImplementation.Class1.__is(value2));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArg_Int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArg_M_0_8736 % _M.DOT)(""Test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArg_String"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArgWithObj_Int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArgWithObj_M_0_8572 % _M.DOT)(""Test"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArgWithObj_Object"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArgWithClass_Int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArgWithClass_M_0_7716 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassA._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArgWithClass_ClassA"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArgWithInterface_InterfaceB"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).OneArgWithInterface_M_0_9442 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassB2._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArgWithInterface_ClassB2"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""OneArgWithInterface_InterfaceBClassB2"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Method_string"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).Method_M_0_3926 % _M.DOT)(10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Method_int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).Method_M_0_12036 % _M.DOT)(true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Method_bool"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""NullPicking1_InterfaceB"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""MethodEnumA"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).EnumMethod_M_0_5084 % _M.DOT)((CsLuaTest.AmbigousMethods.EnumB % _M.DOT).Something);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""MethodEnumB"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""GenericPicking_bool"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).GenericPicking_M_0_359582340 % _M.DOT)(CsLuaTest.AmbigousMethods.ClassWithGenerics[{System.Int32.__typeof}]._C_0_0());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""GenericPicking_int"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""GenericPickingNumber_double"", (element % _M.DOT_LVL(typeObject.Level)).Output);
                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput_M_0_0 % _M.DOT)();
                    ((theClass % _M.DOT).GenericPickingNumber_M_0_8482 % _M.DOT)(4.5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""GenericPickingNumber_double"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""Method_int"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""Method_string"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""Method_bool"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArg_Int"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArg_String"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithObj_Int"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithObj_Object"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithClass_Int"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithClass_ClassA"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithClass_Object"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithInterface_InterfaceB"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithInterface_ClassB2"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithInterface_InterfaceBClassB2"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""OneArgWithInterface_InterfaceBInterfaceB"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""NullPicking1_InterfaceB"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""NullPicking1_object"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""GenericPicking_int"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""GenericPicking_bool"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""GenericPickingNumber_double"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""MethodEnumA"";
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
                    (CsLuaTest.AmbigousMethods.AmbigousMethodsTests % _M.DOT).Output = ""MethodEnumB"";
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Array[{System.String.__typeof}].__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Array.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.IList.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IList[{System.String.__typeof}].__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.ICollection.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.ICollection[{System.String.__typeof}].__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.IEnumerable.__is(a));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IEnumerable[{System.String.__typeof}].__is(a));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""string"", ((arrayClass % _M.DOT).TypeDependent_M_0_26208 % _M.DOT)(a1));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(0, (a1 % _M.DOT).Length);
                    local a2 = (System.Array[{System.String.__typeof, }]._C_0_0()%_M.DOT).__Initialize({[0] = ""abc"", ""def""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""string"", ((arrayClass % _M.DOT).TypeDependent_M_0_26208 % _M.DOT)(a2));
                    local a3 = (System.Array[{System.Int32.__typeof, }]._C_0_0()%_M.DOT).__Initialize({[0] = 1, 3});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""int"", ((arrayClass % _M.DOT).TypeDependent_M_0_21232 % _M.DOT)(a3));
                    local a3b = (System.Array[{System.Double.__typeof, }]._C_0_0()%_M.DOT).__Initialize({[0] = 1.1, 3.2});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""double"", ((arrayClass % _M.DOT).TypeDependent_M_0_40556 % _M.DOT)(a3b));
                    local a4 = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = true, 1, ""ok""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""object"", ((arrayClass % _M.DOT).TypeDependent_M_0_40916 % _M.DOT)(a4));
                    local a5 = (System.Array[{CsLuaTest.Arrays.AClass[{System.Int32.__typeof, }].__typeof, }]._C_0_0() %_M.DOT).__Initialize({[0] = (CsLuaTest.Arrays.AClass[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({Value = 4}), (CsLuaTest.Arrays.AClass[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({Value = 6})});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Aint"", ((arrayClass % _M.DOT).TypeDependent_M_0_101534980 % _M.DOT)(a5));
                    local a6 = (System.Array[{CsLuaTest.Arrays.AClass[{System.String.__typeof, }].__typeof, }]._C_0_0() %_M.DOT).__Initialize({[0] = CsLuaTest.Arrays.AClass[{System.String.__typeof}]._C_0_0()});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Astring"", ((arrayClass % _M.DOT).TypeDependent_M_0_225921580 % _M.DOT)(a6));
                    local a7 = (System.Array[{CsLuaTest.Arrays.AClass[{System.String.__typeof}].__typeof}]._C_0_0() % _M.DOT).__Initialize({});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Astring"", ((arrayClass % _M.DOT).TypeDependent_M_0_225921580 % _M.DOT)(a7));
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
                    local array = (System.Array[{System.String.__typeof, }]._C_0_0()%_M.DOT).__Initialize({[0] = ""abc"", ""def""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, ((arrayClass % _M.DOT).GetLengthOfStringArray_M_0_26208 % _M.DOT)(array));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(4, (array % _M.DOT).Length);
                    (array % _M.DOT)[2] = ""ok"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""ok"", (array % _M.DOT)[2]);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, ((c % _M.DOT).Array1 % _M.DOT).Length);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, ((c % _M.DOT).Array2 % _M.DOT).Length);
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
                returnType = System.Int32.__typeof,
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
                signatureHash = 40916,
                returnType = System.String.__typeof,
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
                returnType = System.String.__typeof,
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
                signatureHash = 21232,
                returnType = System.String.__typeof,
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
                signatureHash = 21576,
                returnType = System.String.__typeof,
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
                signatureHash = 40556,
                returnType = System.String.__typeof,
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
                signatureHash = 101534980,
                returnType = System.String.__typeof,
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
                signatureHash = 225921580,
                returnType = System.String.__typeof,
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
_M.ATN('CsLuaTest.Arrays','AClass', _M.NE({";


    }
}
