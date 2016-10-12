
namespace CsLuaConverterTests
{
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CsLuaConverter;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.PartialElement;
    using CsLuaConverter.Providers.TypeKnowledgeRegistry;

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
            providers.Context = new Context(); // TODO: Move the semantic knowledge out of context so it can be tested that none of the content in context is still needed.
            var analyzer = new Analyzer(new CodeTreeVisitor(providers));

            var namespaces = analyzer.GetNamespaces(project).ToArray();
            var comparingTextWriter = new ComparingTextWriter(CsLuaCompiled);
            namespaces.First().WritingAction(new IndentedTextWriterWrapper(comparingTextWriter));
        }

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
                    (element% _M.DOT_LVL(typeObject.Level)).Tests = System.Collections.Generic.Dictionary[{System.String.__typeof, System.Action.__typeof}]._C_0_0();
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
                    ((lineWriter% _M.DOT).WriteLine_M_0_8736 % _M.DOT)((element % _M.DOT_LVL(typeObject.Level)).Name);
                    (lineWriter% _M.DOT).indent = (lineWriter% _M.DOT).indent + 1;
                    for _,testName in (((element% _M.DOT_LVL(typeObject.Level)).Tests% _M.DOT).Keys%_M.DOT).GetEnumerator() do
                        local test = ((element% _M.DOT_LVL(typeObject.Level)).Tests % _M.DOT)[testName];
                        if ((element % _M.DOT_LVL(typeObject.Level)).ContinueOnError) then
                            _M.Try(
                                function()
                                    (element % _M.DOT_LVL(typeObject.Level)).TestCount = (element % _M.DOT_LVL(typeObject.Level)).TestCount + 1;
                                    ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput % _M.DOT)();
                                    (test % _M.DOT)();
                                    ((lineWriter% _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName  +_M.Add+  "" Success"");
                                end,
                                {
                                    {
                                        type = CsLuaTest.TestIgnoredException.__typeof,
                                        func = function(ex)
                                            (element % _M.DOT_LVL(typeObject.Level)).IgnoreCount = (element % _M.DOT_LVL(typeObject.Level)).IgnoreCount + 1;
                                            ((lineWriter% _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName  +_M.Add+  "" Ignored"");
                                        end,
                                    },
                                    {
                                        type = System.Exception.__typeof,
                                        func = function(ex)
                                            (element % _M.DOT_LVL(typeObject.Level)).FailCount = (element % _M.DOT_LVL(typeObject.Level)).FailCount + 1;
                                            ((lineWriter% _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName  +_M.Add+  "" Failed"");
                                            (lineWriter% _M.DOT).indent = (lineWriter% _M.DOT).indent + 1;
                                            for _,errorLine in ((((ex% _M.DOT).Message% _M.DOT).Split_M_0_18252 % _M.DOT)('\n')%_M.DOT).GetEnumerator() do
                                                ((lineWriter% _M.DOT).WriteLine_M_0_8736 % _M.DOT)(errorLine);
                                            end
                                            (lineWriter% _M.DOT).indent = (lineWriter% _M.DOT).indent - 1;
                                        end,
                                    },
                                },
                                nil
                            );
                        else
                            (element % _M.DOT_LVL(typeObject.Level)).TestCount = (element % _M.DOT_LVL(typeObject.Level)).TestCount + 1;
                            ((lineWriter% _M.DOT).WriteLine_M_0_8736 % _M.DOT)(testName);
                            ((element % _M.DOT_LVL(typeObject.Level)).ResetOutput % _M.DOT)();
                            (test % _M.DOT)();
                        end
                    end
                    (lineWriter% _M.DOT).indent = (lineWriter% _M.DOT).indent - 1;
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
                    local expectedValue = ((Lua.Strings% _M.DOT).tostring_M_0_8572 % _M.DOT)(expectedValueObj);
                    local actualValue = ((Lua.Strings% _M.DOT).tostring_M_0_8572 % _M.DOT)(actualValueObj);
                    if (expectedValue ~= actualValue) then
                        _M.Throw(System.Exception._C_0_8736(((Lua.Strings% _M.DOT).format_M_0_70110 % _M.DOT)(""Incorrect value. Expected: '{0}' got: '{1}'."", expectedValue, actualValue)));
                    end
                end
            });
            return members;
        end
        return 'Class', typeObject, getMembers, constructors, elementGenerator, nil, initialize;";
    }
}
