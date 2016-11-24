namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using System.Collections.Generic;
    using CsLuaConverter.Context;

    public class TypeSwitch
    {
        private readonly Dictionary<Type, Delegate> matches = new Dictionary<Type, Delegate>();
        private readonly Action<object, IIndentedTextWriterWrapper, IContext> defaultAction;

        public TypeSwitch(Action<object, IIndentedTextWriterWrapper, IContext> defaultAction)
        {
            this.defaultAction = defaultAction;
        }

        public TypeSwitch Case<T>(Action<T, IIndentedTextWriterWrapper, IContext> action)
        {
            this.matches.Add(typeof(T), action);
            return this;
        }

        public void Write(object obj, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var type = obj.GetType();
            while (type != null)
            {
                if (this.matches.ContainsKey(type))
                {
                    this.matches[type].DynamicInvoke(obj, textWriter, context);
                    return;
                }
                type = type.BaseType;
            }
            
            this.defaultAction(obj, textWriter, context);
        }
    }
}