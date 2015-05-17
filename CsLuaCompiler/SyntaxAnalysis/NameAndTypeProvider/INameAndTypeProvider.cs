namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    using System;
    using System.Collections.Generic;
    using CsToLua.SyntaxAnalysis;

    internal interface INameAndTypeProvider
    {
        void SetNamespaces(string currentNamespace, IEnumerable<string> namespaces);
        List<ScopeElement> CloneScope();
        void SetScope(List<ScopeElement> scope);
        void AddToScope(ScopeElement element);
        string LoopupFullNameOfType(string name);
        TypeResult LookupType(IEnumerable<string> names);
        void AddMembersToScope(Type type);
        void AddAllInheritedMembersToScope(Type type);
        string LoopupFullNameOfType(IEnumerable<string> names, bool chooseClassReference);
        string LoopupFullNameOfType(IEnumerable<string> names, bool chooseClassReference, bool chooseTypeName);
        string GetDefaultValue(string typeName, bool isNullable);
        string GetDefaultValue(string typeName);
        string LookupVariableName(IEnumerable<string> names);
        void SetGenerics(IEnumerable<string> generics);
        bool IsGeneric(string name);
    }
}