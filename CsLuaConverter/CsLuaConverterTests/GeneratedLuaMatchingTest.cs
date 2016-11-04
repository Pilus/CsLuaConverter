
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
                    local a2 = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = ""abc"", ""def""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""string"", ((arrayClass % _M.DOT).TypeDependent_M_0_26208 % _M.DOT)(a2));
                    local a3 = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 1, 3});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""int"", ((arrayClass % _M.DOT).TypeDependent_M_0_11778 % _M.DOT)(a3));
                    local a3b = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 1.1, 3.2});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""double"", ((arrayClass % _M.DOT).TypeDependent_M_0_25446 % _M.DOT)(a3b));
                    local a4 = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = true, 1, ""ok""});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""object"", ((arrayClass % _M.DOT).TypeDependent_M_0_25716 % _M.DOT)(a4));
                    local a5 = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = (CsLuaTest.Arrays.AClass[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({Value = 4}), (CsLuaTest.Arrays.AClass[{System.Int32.__typeof}]._C_0_0() % _M.DOT).__Initialize({Value = 6})});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Aint"", ((arrayClass % _M.DOT).TypeDependent_M_0_101526360 % _M.DOT)(a5));
                    local a6 = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = CsLuaTest.Arrays.AClass[{System.String.__typeof}]._C_0_0()});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Astring"", ((arrayClass % _M.DOT).TypeDependent_M_0_225912960 % _M.DOT)(a6));
                    local a7 = (System.Array[{CsLuaTest.Arrays.AClass[{System.String.__typeof}].__typeof}]._C_0_0() % _M.DOT).__Initialize({});
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Astring"", ((arrayClass % _M.DOT).TypeDependent_M_0_225912960 % _M.DOT)(a7));
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
                    local array = (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = ""abc"", ""def""});
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
                signatureHash = 25716,
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
                signatureHash = 11778,
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
                signatureHash = 12036,
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
                signatureHash = 25446,
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
                signatureHash = 101526360,
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
                signatureHash = 225912960,
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
_M.ATN('CsLuaTest.Collections','ClassWithProperties', _M.NE({
    [0] = function(interactionElement, generics, staticValues)
        local genericsMapping = {};
        local typeObject = System.Type('ClassWithProperties','CsLuaTest.Collections', nil, 0, generics, nil, interactionElement, 'Class', 60988);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestCountAndAny""] = (element % _M.DOT_LVL(typeObject.Level)).TestCountAndAny;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestSelect""] = (element % _M.DOT_LVL(typeObject.Level)).TestSelect;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestUnion""] = (element % _M.DOT_LVL(typeObject.Level)).TestUnion;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestOrderBy""] = (element % _M.DOT_LVL(typeObject.Level)).TestOrderBy;
                    ((element % _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[""TestOfLinqOfType""] = (element % _M.DOT_LVL(typeObject.Level)).TestOfLinqOfType;
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.IList.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IList[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.ICollection.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.ICollection[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.IEnumerable.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IEnumerable[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IReadOnlyList[{System.Int32.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IReadOnlyCollection[{System.Int32.__typeof}].__is(list));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(0, (list % _M.DOT).Capacity);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(0, (list % _M.DOT).Count);
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(4, (list % _M.DOT).Capacity);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(1, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, (iList % _M.DOT).IsFixedSize);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, (iList % _M.DOT).IsReadOnly);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, (iList % _M.DOT).IsSynchronized);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, (iList % _M.DOT).SyncRoot == nil);
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, ((iList % _M.DOT).Add_M_0_8572 % _M.DOT)(50));
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(75);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(4, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(43, (list % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, (list % _M.DOT)[1]);
                    (list % _M.DOT)[1] = 6;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (list % _M.DOT)[1]);
                    _M.Try(
                        function()
                            local x = (list % _M.DOT)[-1];
                            _M.Throw(System.Exception._C_0_8736(""Expected IndexOutOfRangeException""));
                        end,
                        {
                            {
                                type = System.ArgumentOutOfRangeException.__typeof,
                                func = function(ex)
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index"", (ex % _M.DOT).Message);
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
                                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Index was out of range. Must be non-negative and less than the size of the collection.\r\nParameter name: index"", (ex % _M.DOT).Message);
                                end,
                            },
                        },
                        nil
                    );
                    local verificationList = System.Collections.Generic.List[{System.Int32.__typeof}]._C_0_0();
                    for _,item in (list % _M.DOT).GetEnumerator() do
                        ((verificationList % _M.DOT).Add_M_0_3926 % _M.DOT)(item);
                    end
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)((list % _M.DOT).Count, (verificationList % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)((list % _M.DOT)[0], (verificationList % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)((list % _M.DOT)[1], (verificationList % _M.DOT)[1]);
                    local list2 = System.Collections.Generic.List[{System.Int32.__typeof}]._C_0_129809264((System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 7, 9, 13}));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, (list2 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(7, (list2 % _M.DOT)[0]);
                    ((list2 % _M.DOT).AddRange_M_0_129809264 % _M.DOT)((System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 21, 28}));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, (list2 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(21, (list2 % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(28, (list2 % _M.DOT)[4]);
                    ((list2 % _M.DOT).Clear_M_0_0 % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(0, (list2 % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, ((list % _M.DOT).Contains_M_0_3926 % _M.DOT)(6));
                    ((list % _M.DOT).Add_M_0_3926 % _M.DOT)(6);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, ((list % _M.DOT).Find_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(1, ((list % _M.DOT).FindIndex_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, ((list % _M.DOT).FindLast_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(4, ((list % _M.DOT).FindLastIndex_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end)));
                    local all = ((list % _M.DOT).FindAll_M_0_81071900 % _M.DOT)(System.Predicate[{System.Int32.__typeof}]._C_0_16704(function(i) return i == 6 end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, (all % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (all % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (all % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(1, ((list % _M.DOT).IndexOf_M_0_3926 % _M.DOT)(6));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(-1, ((list % _M.DOT).IndexOf_M_0_3926 % _M.DOT)(500));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(4, ((list % _M.DOT).LastIndexOf_M_0_3926 % _M.DOT)(6));
                    ((list % _M.DOT).Insert_M_0_9815 % _M.DOT)(1, 24);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(24, (list % _M.DOT)[1]);
                    local res = ((list % _M.DOT).GetRange_M_0_9815 % _M.DOT)(1, 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, (res % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(24, (res % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (res % _M.DOT)[1]);
                    ((list % _M.DOT).InsertRange_M_0_194717822 % _M.DOT)(1, (System.Array[{System.Object.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = 110, 120}));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(8, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(110, (list % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(120, (list % _M.DOT)[2]);
                    ((list % _M.DOT).RemoveRange_M_0_9815 % _M.DOT)(2, 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (list % _M.DOT).Count);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(110, (list % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (list % _M.DOT)[2]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(50, (list % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, ((list % _M.DOT).Remove_M_0_3926 % _M.DOT)(50));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, ((list % _M.DOT).Remove_M_0_3926 % _M.DOT)(50));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.IDictionary.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IDictionary[{System.Int32.__typeof, System.String.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.ICollection.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.ICollection[{System.Collections.Generic.KeyValuePair[{System.Int32.__typeof, System.String.__typeof}].__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.IEnumerable.__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IEnumerable[{System.Collections.Generic.KeyValuePair[{System.Int32.__typeof, System.String.__typeof}].__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IReadOnlyDictionary[{System.Int32.__typeof, System.String.__typeof}].__is(list));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.IReadOnlyCollection[{System.Collections.Generic.KeyValuePair[{System.Int32.__typeof, System.String.__typeof}].__typeof}].__is(list));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, ((a % _M.DOT).Any_M_1_0[{System.Int32.__typeof}] % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, ((a % _M.DOT).Count_M_1_0[{System.Int32.__typeof}] % _M.DOT)());
                    local list = System.Collections.Generic.List[{System.String.__typeof}]._C_0_0();
                    ((list % _M.DOT).Add_M_0_8736 % _M.DOT)(""a"");
                    ((list % _M.DOT).Add_M_0_8736 % _M.DOT)(""b"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, ((list % _M.DOT).Any_M_1_0[{System.String.__typeof}] % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, ((list % _M.DOT).Count_M_1_0[{System.String.__typeof}] % _M.DOT)());
                    local enumerable = ((a % _M.DOT).Where_M_1_76226640[{System.Int32.__typeof}] % _M.DOT)(System.Func[{System.Int32.__typeof, System.Boolean.__typeof}]._C_0_16704(function(e) return e > 10 and e < 50 end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, ((enumerable % _M.DOT).Count_M_1_0[{System.Int32.__typeof}] % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, ((enumerable % _M.DOT).Count_M_1_0[{System.Int32.__typeof}] % _M.DOT)());
                    local enumerable2 = ((list % _M.DOT).Where_M_1_92907720[{System.String.__typeof}] % _M.DOT)(System.Func[{System.String.__typeof, System.Boolean.__typeof}]._C_0_16704(function(e) return (e % _M.DOT).Length == 1 end));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, ((enumerable2 % _M.DOT).Count_M_1_0[{System.String.__typeof}] % _M.DOT)());
                    ((list % _M.DOT).Add_M_0_8736 % _M.DOT)(""c"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, ((enumerable2 % _M.DOT).Count_M_1_0[{System.String.__typeof}] % _M.DOT)());
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
                    local l1 = ((((a % _M.DOT).Select_M_2_59060040[{System.Int32.__typeof, System.String.__typeof}] % _M.DOT)(System.Func[{System.Int32.__typeof, System.String.__typeof}]._C_0_16704(function(v) return ((v % _M.DOT).ToString_M_0_0 % _M.DOT)() end)) % _M.DOT).ToList_M_1_0[{System.String.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.List[{System.String.__typeof}].__is(l1));
                    local l2 = ((((a % _M.DOT).Select_M_2_57863580[{System.Int32.__typeof, System.Single.__typeof}] % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).ToFloat) % _M.DOT).ToList_M_1_0[{System.Single.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, System.Collections.Generic.List[{System.Single.__typeof}].__is(l2));
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
                    local result = ((((a % _M.DOT).Union_M_1_129809264[{System.Int32.__typeof}] % _M.DOT)(b) % _M.DOT).ToArray_M_1_0[{System.Int32.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, (result % _M.DOT).Length);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(1, (result % _M.DOT)[0]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, (result % _M.DOT)[1]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, (result % _M.DOT)[2]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(7, (result % _M.DOT)[3]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(9, (result % _M.DOT)[4]);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(11, (result % _M.DOT)[5]);
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
                    local input = (System.Array[{CsLuaTest.Collections.ClassWithProperties.__typeof}]._C_0_0() % _M.DOT).__Initialize({[0] = (CsLuaTest.Collections.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 13}), (CsLuaTest.Collections.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 7}), (CsLuaTest.Collections.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 9}), (CsLuaTest.Collections.ClassWithProperties._C_0_0() % _M.DOT).__Initialize({Number = 5})});
                    local ordered = ((((input % _M.DOT).OrderBy_M_2_443435820[{CsLuaTest.Collections.ClassWithProperties.__typeof, System.Int32.__typeof}] % _M.DOT)(System.Func[{CsLuaTest.Collections.ClassWithProperties.__typeof, System.Int32.__typeof}]._C_0_16704(function(v) return (v % _M.DOT).Number end)) % _M.DOT).ToArray_M_1_0[{CsLuaTest.Collections.ClassWithProperties.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, ((ordered % _M.DOT)[0] % _M.DOT).Number);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(7, ((ordered % _M.DOT)[1] % _M.DOT).Number);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(9, ((ordered % _M.DOT)[2] % _M.DOT).Number);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(13, ((ordered % _M.DOT)[3] % _M.DOT).Number);
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
                    local ints = ((((mixedCollection % _M.DOT).OfType_M_1_0[{System.Int32.__typeof}] % _M.DOT)() % _M.DOT).ToArray_M_1_0[{System.Int32.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, (ints % _M.DOT).Length);
                    local strings = ((((mixedCollection % _M.DOT).OfType_M_1_0[{System.String.__typeof}] % _M.DOT)() % _M.DOT).ToArray_M_1_0[{System.String.__typeof}] % _M.DOT)();
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, (strings % _M.DOT).Length);
                end
            });
            _M.IM(members, 'ToFloat', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Private',
                static = true,
                numMethodGenerics = 0,
                signatureHash = 3926,
                returnType = System.Single.__typeof,
                func = function(element, value)
                    return value;
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
                    (element % _M.DOT_LVL(typeObject.Level)).Value = ""str""  +_M.Add+  val;
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
                    (element % _M.DOT_LVL(typeObject.Level)).Value = ""int""  +_M.Add+  val;
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
                    (element % _M.DOT_LVL(typeObject.Level)).Value = ""object""  +_M.Add+  val;
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
                    (element % _M.DOT_LVL(typeObject.Level)).Result = (element % _M.DOT_LVL(typeObject.Level)).Result +_M.Add+ s1  +_M.Add+  s2;
                end,
            });
            _M.IM(members, 'Result', {
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
            _M.IM(members, 'Str', {
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""null"", (c % _M.DOT).Value);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""strTest"", (c1 % _M.DOT).Value);
                    local c2 = CsLuaTest.Constructors.Class1._C_0_3926(43);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""int43"", (c2 % _M.DOT).Value);
                    local c3 = CsLuaTest.Constructors.Class1._C_0_8572(43.7);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""object43.7"", (c3 % _M.DOT).Value);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""this1this2abc"", (c % _M.DOT).Result);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""abc"", (c % _M.DOT).Str);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(0, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticInt);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(0, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).Int);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticBool);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).Bool);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticString);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).String);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)((CsLuaTest.DefaultValues.AnEnum % _M.DOT).Something, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticEnum);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)((CsLuaTest.DefaultValues.AnEnum % _M.DOT).Something, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).Enum);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticAction);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).AnAction);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticFunc);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).AFunc);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass % _M.DOT).StaticClass);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, (CsLuaTest.DefaultValues.DefaultValuesClass._C_0_0() % _M.DOT).AClass);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, 2  +_M.Add+  3);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(-1, 2 - 3);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, 2 * 3);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, 10 / 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(7, math.mod(17, 10));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(144, bit.lshift(36, 2));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(9, bit.rshift(36, 2));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(8, bit.band(137, 26));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(155, bit.bor(137, 26));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(147, bit.bxor(137, 26));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, true and true);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, true and false);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, true or false);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, false or false);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 5 == 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, 10 == 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, 5 ~= 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 10 ~= 5);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 5 < 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, 10 < 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 5 <= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 10 <= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, 15 <= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, 10 > 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 15 > 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, 5 >= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 10 >= 10);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, 15 >= 10);
                    local value = nil;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Default"", value or ""Default"");
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(5, v1);
                    v1 = v1 - 3;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, v1);
                    v1 = v1 * 3;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(6, v1);
                    v1 = v1 / 2;
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, v1);
                    local v2 = 32;
                    v2 = math.mod(v2, 20);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(12, v2);
                    local v3 = 56;
                    v3 = bit.lshift(v3, 2);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(224, v3);
                    v3 = bit.rshift(v3, 1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(112, v3);
                    local v4 = 50;
                    v4 = bit.bor(v4, 30);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(62, v4);
                    v4 = bit.band(v4, 124);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(60, v4);
                    v4 = bit.bxor(v4, 34);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(30, v4);
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
            _M.IM(members, 'ambValue', {
                level = typeObject.Level,
                memberType = 'Field',
                scope = 'Private',
                static = false,
            });
            _M.IM(members, 'GetAmbValue', {
                level = typeObject.Level,
                memberType = 'Method',
                scope = 'Public',
                static = false,
                numMethodGenerics = 0,
                signatureHash = 0,
                returnType = System.String.__typeof,
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
                returnType = System.Boolean.__typeof,
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
                get = function(element,index)
                    return ""GetAtIndex""  +_M.Add+  index;
                end,
                set = function(element,index , value)
                    (element % _M.DOT_LVL(typeObject.Level)).Set = ""SetAtIndex""  +_M.Add+  index  +_M.Add+  ""Is""  +_M.Add+  value;
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
                set = function(element , value)
                    (CsLuaTest.BaseTest % _M.DOT).Output = value;
                end,
            });
            _M.IM(members, 'PropertyWithGetAndSet',{
                level = typeObject.Level,
                memberType = 'Property',
                scope = 'Public',
                static = false,
                returnType = System.String.__typeof;
                get = function(element)
                    return (CsLuaTest.BaseTest % _M.DOT).Output;
                end,
                set = function(element , value)
                    (CsLuaTest.BaseTest % _M.DOT).Output = value;
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
                returnType = System.String.__typeof,
                func = function(element, a, b)
                    (element % _M.DOT_LVL(typeObject.Level)).ACommonName = a;
                    local obj = CsLuaTest.General.ACommonName._C_0_8736(b);
                    return (element % _M.DOT_LVL(typeObject.Level)).ACommonName  +_M.Add+  (obj % _M.DOT).Value;
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
                    (CsLuaTest.General.GeneralTests % _M.DOT).Output = ""Action"";
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));
_M.ATN('CsLuaTest.GeneralTests','zzzzzz', _M.NE({
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""StaticMethodInt"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Action"", (element % _M.DOT_LVL(typeObject.Level)).Output);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(50, ((CsLuaTest.General.Inheriter % _M.DOT).GetConstValue_M_0_0 % _M.DOT)());
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""X"", ((classA % _M.DOT).GetAmbValue_M_0_0 % _M.DOT)());
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""Y"", ((classA % _M.DOT).GetAmbValue_M_0_0 % _M.DOT)());
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, ((c1 % _M.DOT).Equals_M_0_8572 % _M.DOT)(c2));
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, ((c1 % _M.DOT).Equals_M_0_8572 % _M.DOT)(c3));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(7, i);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(false, ((System.String % _M.DOT).IsNullOrEmpty_M_0_8736 % _M.DOT)(s));
                    local s2 = """";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(true, ((System.String % _M.DOT).IsNullOrEmpty_M_0_8736 % _M.DOT)(s2));
                    local i1 = ((System.Int32 % _M.DOT).Parse_M_0_8736 % _M.DOT)(""4"");
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(4, i1);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""AB"", s);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""B"", (c % _M.DOT).Value);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(0, (c % _M.DOT).IntProperty);
                    (c % _M.DOT).AutoProperty = ""A"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""A"", (c % _M.DOT).AutoProperty);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""GetValue"", (c % _M.DOT).PropertyWithGet);
                    (c % _M.DOT).PropertyWithGetAndSet = ""B"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).Output, ""B"");
                    (element % _M.DOT_LVL(typeObject.Level)).Output = ""C"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""C"", (c % _M.DOT).PropertyWithGetAndSet);
                    (c % _M.DOT).PropertyWithSet = ""D"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).Output, ""D"");
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(10, ((a % _M.DOT).CallWithSameClass_M_0_57882 % _M.DOT)(b));
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""CstorA"", (pClass % _M.DOT).InnerValue);
                    pClass = CsLuaTest.General.Partial._C_0_3926(1);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""CstorB"", (pClass % _M.DOT).InnerValue);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(1, ((pClass % _M.DOT).MethodA_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(2, ((pClass % _M.DOT).MethodB_M_0_0 % _M.DOT)());
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, ((pClass % _M.DOT).MethodC_M_0_0 % _M.DOT)());
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""GetAtIndexTest1"", (c1 % _M.DOT)[""Test1""]);
                    (c1 % _M.DOT)[""Test2""] = ""TheValue"";
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(""SetAtIndexTest2IsTheValue"", (c1 % _M.DOT).Set);
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
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(nil, len);
                    local value2 = ""abc"";
                    local len2 = _M.CA(value2,function(obj) return (obj % _M.DOT).Length; end);
                    ((element % _M.DOT_LVL(typeObject.Level)).Assert_M_0_21430 % _M.DOT)(3, len2);
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;
    end,
}));";

/*
_M.ATN('CsLuaTest.General','zzzzzz', _M.NE({
    
}));
    */
    }
}
