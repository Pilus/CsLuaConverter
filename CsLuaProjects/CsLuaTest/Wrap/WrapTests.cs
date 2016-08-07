namespace CsLuaTest.Wrap
{
    using CsLuaFramework;
    using CsLuaFramework.Wrapping;
    using Lua;

    class WrapTests : BaseTest
    {
        
        public WrapTests()
        {
            Name = "Wrap";
            this.Tests["WrapSimpleInterface"] = WrapSimpleInterface;
            this.Tests["TestWrapAndUnwrap"] = TestWrapAndUnwrap;
            this.Tests["WrapInheritingInterface"] = WrapInheritingInterface;
            this.Tests["WrapGenericInterface"] = WrapGenericInterface;
            this.Tests["WrapInheritingInterfaceWithGenericInterface"] = WrapInheritingInterfaceWithGenericInterface;
            this.Tests["WrapInheritingInterfaceWithProvideSelf"] = WrapInheritingInterfaceWithProvideSelf;
            this.Tests["WrapHandleMultipleValues"] = WrapHandleMultipleValues;
            this.Tests["WrapGenericWithProperty"] = WrapGenericWithProperty;
            this.Tests["WrapHandleMultipleValues"] = WrapHandleMultipleValues;
            this.Tests["WrapHandleRecursiveWrapping"] = WrapHandleRecursiveWrapping;
            this.Tests["WrapWithTargetTypeTranslation"] = WrapWithTargetTypeTranslation;
            this.Tests["NonWrappedAsPropertyInWrappedObject"] = NonWrappedAsPropertyInWrappedObject;
            this.Tests["WrappedObjectWithPartialInterface"] = WrappedObjectWithPartialInterface;
            this.Tests["WrappedObjectWithInterfaceWithIndexer"] = WrappedObjectWithInterfaceWithIndexer;
            this.Tests["WrapperReplacesActionAndFuncWithLuaFunctions"] = WrapperReplacesActionAndFuncWithLuaFunctions;
            this.Tests["WrapperDoesNotWrapAReturedLuaTableIfExpectingItOrObject"] = WrapperDoesNotWrapAReturedLuaTableIfExpectingItOrObject;
        }


        private static void WrapSimpleInterface()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("interfaceImplementation = { Method = function(str) return 'OK' .. str; end, Value = 10, MethodVoid = function() end, };");
            var interfaceImplementation = wrapper.Wrap<ISimpleInterface>("interfaceImplementation");

            Assert("OKInput", interfaceImplementation.Method("Input"));
            Assert(10, interfaceImplementation.Value);

            interfaceImplementation.Value = 20;
            Assert(20, interfaceImplementation.Value);

            interfaceImplementation.MethodVoid(true);
        }

        private static void WrapGenericInterface()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("interfaceImplementation = { Method = function(n) return 'OK' .. n; end, };");
            var interfaceImplementation = wrapper.Wrap<IInterfaceWithGenerics<int>>("interfaceImplementation");

            Assert("OK10", interfaceImplementation.Method(10));
        }

        private static void WrapGenericWithProperty()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("interfaceImplementation = { Method = function() end, Property = { Value = 43, Method = function() end}, };");
            var interfaceImplementation = wrapper.Wrap<IInterfaceWithGenerics<ISimpleInterface>>("interfaceImplementation");

            var inner = interfaceImplementation.Property;
            Assert(43, inner.Value);
        }

        private static void WrapInheritingInterface()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("interfaceImplementation = { Method = function(str) return 'OK' .. str; end, Value = 10, };");
            var interfaceImplementation = wrapper.Wrap<IInheritingInterface>("interfaceImplementation");

            Assert("OKInput", interfaceImplementation.Method("Input"));
            Assert(10, interfaceImplementation.Value);

            interfaceImplementation.Value = 20;
            Assert(20, interfaceImplementation.Value);
        }

        private static void WrapInheritingInterfaceWithGenericInterface()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("interfaceImplementation = { Method = function(n) return 'OK' .. n; end, };");
            var interfaceImplementation = wrapper.Wrap<IInheritingInterfaceWithGenerics<string,int>>("interfaceImplementation");

            Assert("OK10", interfaceImplementation.Method(10));
        }

        private static void WrapInheritingInterfaceWithProvideSelf()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("interfaceImplementation = { Method = function(self, str) return 'OK' .. str; end, Method2 = function(self,a,b,c) return tostring(a)..tostring(b)..tostring(c); end  };");
            var interfaceImplementation = wrapper.Wrap<ISetSelfInterface>("interfaceImplementation");

            Assert("OKmore", interfaceImplementation.Method("more"));
            Assert("nilbc", interfaceImplementation.Method2(null, "b", "c"));
        }

        private static void WrapHandleMultipleValues()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("interfaceImplementation = { Method = function() return 'OK', 43, true; end, };");
            var interfaceImplementation = wrapper.Wrap<IInterfaceWithMultipleReturnValues<bool>>("interfaceImplementation");

            var multiple = interfaceImplementation.Method();
            Assert("OK", multiple.Value1);
            Assert(43, multiple.Value2);
            Assert(true, multiple.Value3);
        }

        private static void WrapHandleRecursiveWrapping()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            Environment.ExecuteLuaCode(@"
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
            ");

            var wrapper = new Wrapper();

            var C = wrapper.Wrap<IInterfaceWithWrappedValues>("C");
            Assert('c', C.GetValue());

            var B = C.Inner;
            Assert('b', B.GetValue());

            var A = B.GetInner();
            Assert('a', A.GetValue());
        }

        private static void WrapWithTargetTypeTranslation()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            Environment.ExecuteLuaCode(@"
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
            ");

            var wrapper = new Wrapper();

            var producer = wrapper.Wrap<IProducer>("P", table => ((table["IsA"] != null) ? typeof(IA) : typeof(IB)));

            var a = producer.Produce("A");
            Assert(true, a is IA);
            Assert(true, ((IA)a).IsA());

            var b = (IB)producer.Produce("B");
            Assert(true, b.IsB());
        }

        private static void NonWrappedAsPropertyInWrappedObject()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            var wrapper = new Wrapper();

            Environment.ExecuteLuaCode("A = {};");

            var obj = wrapper.Wrap<INonWrappedProperty>("A");
            var cA = new ClassA() {Value = "ok"};
            obj.Property = cA;

            Assert(true, obj.Property == cA);

            cA.Value = "2";

            Assert("2", obj.Property.Value);

            Environment.ExecuteLuaCode("A2 = A");
            var objRef2 = wrapper.Wrap<INonWrappedProperty>("A2");
            Assert(true, objRef2.Property == cA);
        }

        private static void WrappedObjectWithPartialInterface()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            Environment.ExecuteLuaCode(@"
                P = {
                    MethodA = function()
                        return 'MA';
                    end,
                    MethodB = function()
                        return 'MB';
                    end
                };
            ");
            var wrapper = new Wrapper();

            var obj = wrapper.Wrap<IPartial>("P");

            Assert("MA", obj.MethodA());
            Assert("MB", obj.MethodB());
        }

        private static void WrappedObjectWithInterfaceWithIndexer()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            Environment.ExecuteLuaCode(@"
                P = { Value1 = 'V1' };
            ");
            var wrapper = new Wrapper();

            var obj = wrapper.Wrap<IInterfaceWithIndexer>("P");

            Assert("V1", obj["Value1"]);

            obj["Value2"] = "V2";

            Assert("V2", obj["Value2"]);
        }

        private static void WrapperReplacesActionAndFuncWithLuaFunctions()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            Environment.ExecuteLuaCode(@"
                P = { 
                    Method = function(f, a)
                        return type(f) == 'function' and type(a) == 'function';
                    end
                };
            ");
            var wrapper = new Wrapper();

            var obj = wrapper.Wrap<IInterfaceWithMethod>("P");


            bool inputValue;
            Assert(true, obj.Method((int x) => x < 10, input => inputValue = input));
        }

        private static void WrapperDoesNotWrapAReturedLuaTableIfExpectingItOrObject()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            Environment.ExecuteLuaCode(@"
                P = { 
                    ReturnObject = function()
                        return { X = true };
                    end,
                    ReturnLuaTable = function()
                        return { Y = true };
                    end
                };
            ");
            var wrapper = new Wrapper();

            var obj = wrapper.Wrap<IReturningNativeTypes>("P");

            var t1 = obj.ReturnLuaTable();
            Assert(true, t1 is NativeLuaTable);
            Assert(true, t1["Y"]);

            var t2 = obj.ReturnObject();
            Assert(true, t2 is NativeLuaTable);
            Assert(true, (t2 as NativeLuaTable)["X"]);
        }

        private static void TestWrapAndUnwrap()
        {
            if (!Environment.IsExecutingAsLua)
            {
                return;
            }

            Environment.ExecuteLuaCode(@"
                O = { 
                    Value = 43;
                };
            ");

            var wrapper = new Wrapper();

            var obj = wrapper.Wrap<IReturningNativeTypes>("O");

            var table = wrapper.Unwrap(obj);

            Assert(43, table["Value"]);
        }
    }
}
