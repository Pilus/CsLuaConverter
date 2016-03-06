namespace CsLuaTest.Generics
{
    public class ClassWithSelfInGenericInterface : IInterfaceWithGenerics<ClassWithSelfInGenericInterface>
    {
        public bool Method(ClassWithSelfInGenericInterface self)
        {
            return this == self;
        }
    }
}