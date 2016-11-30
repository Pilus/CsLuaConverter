namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using CsLuaConverter.Context;

    using Microsoft.CodeAnalysis.CSharp;

    public class TypeSwitch
    {
        private readonly Dictionary<Type, Action<CSharpSyntaxNode, IIndentedTextWriterWrapper, IContext>> matches = new Dictionary<Type, Action<CSharpSyntaxNode, IIndentedTextWriterWrapper, IContext>>();
        private readonly Action<CSharpSyntaxNode, IIndentedTextWriterWrapper, IContext> defaultAction;

        public TypeSwitch(Action<CSharpSyntaxNode, IIndentedTextWriterWrapper, IContext> defaultAction)
        {
            this.defaultAction = defaultAction;
        }

        public TypeSwitch Case<T>(Action<T, IIndentedTextWriterWrapper, IContext> action) where T: CSharpSyntaxNode
        {
            this.matches.Add(typeof(T), (obj, textWriter, context) => action((T)obj, textWriter, context));
            return this;
        }

        public void Write(CSharpSyntaxNode obj, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var type = obj.GetType();
            while (type != null)
            {
                if (this.matches.ContainsKey(type))
                {
                    this.matches[type](obj, textWriter, context);
                    return;
                }
                type = type.BaseType;
            }
            
            this.defaultAction(obj, textWriter, context);
        }
    }
}