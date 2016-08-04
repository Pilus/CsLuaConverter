namespace CsLuaTest.Generics
{
    using System;
    using System.Collections.Generic;

    public class ClassUsingGenericsInMethods<T>
    {
        private MethodsWithGeneric<T, int> inner;

        public ClassUsingGenericsInMethods()
        {
            this.inner = new MethodsWithGeneric<T, int>();
        } 

        public bool UseClassGenericAsMethodGeneric()
        {
            var list = new List<T>();
            var returnValue = this.inner.GenericReturnType<List<T>>(list);
            return list == returnValue;
        }

        public bool UseMethodGenericAsMethodGeneric<T3>(T3 value)
        {
            var list = new List<T3>() {value};
            var returnValue = this.inner.GenericReturnType<List<T3>>(list);
            return list == returnValue;
        }

        public bool UseClassGenericInLambda(T correctValue, T falseValue)
        {
            var value = this.MethodWithGenericAndLambda(i => i == 43 ? correctValue : falseValue);
            return value.Equals(correctValue);
        }

        private T3 MethodWithGenericAndLambda<T3>(Func<int, T3> selector)
        {
            return selector(43);
        }

        public void InvokingAmbMethodDependingOnClassGeneric(T obj)
        {
            this.inner.AmbGenericMethod(obj);
            this.inner.AmbGenericMethod(43);
        }
    }
}