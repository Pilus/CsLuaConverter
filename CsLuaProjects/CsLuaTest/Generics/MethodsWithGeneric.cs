namespace CsLuaTest.Generics
{
    using System;

    public class MethodsWithGeneric<T1,T2>
    {
        public void GenericMethod(T1 x)
        {
            BaseTest.Output = "GenericMethodT1";
        }

        public void GenericMethod(T2 x)
        {
            BaseTest.Output = "GenericMethodT2";
        }

        public T3 GenericReturnType<T3>(object value)
        {
            return (T3)value;
        }

        public string GenericAtMethod<T3>(T3 obj)
        {
            return obj.ToString();
        }

        public string GenericAtAmbMethod<T3, T4>(T3 obj, T4 obj2)
        {
            return obj.ToString() + obj2.ToString() + "A";
        }

        public string GenericAtAmbMethod<T3>(T3 obj, string obj2)
        {
            return obj.ToString() + obj2.ToString() + "B";
        }

        public static string StaticMethodWithGenerics<T3>(T3 value)
        {
            return value.GetType().Name;
        }

        /*
        public T3 MethodWithGenericAsObjectGenericInArg<T3>(Func<T3> f)
        {
            return f();
        } */
    }
}