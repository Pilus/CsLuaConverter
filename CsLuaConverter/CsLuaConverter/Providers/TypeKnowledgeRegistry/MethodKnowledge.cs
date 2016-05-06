namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class MethodKnowledge : IKnowledge
    {
        private readonly MethodBase method;
        private readonly Type[] inputTypes;
        private readonly Type returnType;

        public MethodKnowledge(MethodBase method)
        {
            this.method = method;
        }

        public MethodKnowledge(Type returnType, params Type[] inputTypes)
        {
            this.returnType = returnType;
            this.inputTypes = inputTypes;
        }

        public MethodKnowledge()
        {
            this.returnType = typeof(void);
            this.inputTypes = new Type[] {};
        }

        public TypeKnowledge ToTypeKnowledge()
        {
            return new TypeKnowledge(this.method);
        }

        public int GetNumberOfMethodGenerics()
        {
            return this.method?.GetGenericArguments().Length ?? 0;
        }

        public int GetNumberOfArgs()
        {
            return this.method?.GetParameters().Length ?? this.inputTypes.Length;
        }

        public bool IsParams()
        {
            return this.method?.GetParameters().LastOrDefault()?.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any() ?? false;
        }

        public bool FitsArguments(Type[] types)
        {
            
            if (this.method != null)
            {
                var isParams = this.IsParams();
                var parameters = this.method.GetParameters();

                for (var index = 0; index < types.Length; index++)
                {
                    var type = types[index];

                    if (type == null)
                    {
                        continue;
                    }

                    var parameter = parameters[Math.Min(index, parameters.Length - 1)].ParameterType;

                    if (isParams && index >= parameters.Length)
                    {
                        var paramType =
                            parameter.GetInterface(typeof (IEnumerable<object>).Name).GetGenericArguments().Single();
                        if (!paramType.IsAssignableFrom(type))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!parameter.IsAssignableFrom(type))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int index = 0; index < types.Length; index++)
                {
                    var type = types[index];
                    var parameter = this.inputTypes[index];
                    if (!parameter.IsAssignableFrom(type))
                    {
                        return false;
                    }
                    
                }
            }

            return true;
        }

        public TypeKnowledge[] GetInputArgs()
        {
            if (this.method != null)
            {
                return this.method.GetParameters().Select(p => new TypeKnowledge(p.ParameterType)).ToArray();
            }

            return this.inputTypes.Select(t => new TypeKnowledge(t)).ToArray();
        }

        public TypeKnowledge GetReturnType()
        {
            
            if (this.method != null)
            {
                var methodInfo = this.method as MethodInfo;
                return methodInfo != null
                    ? new TypeKnowledge(methodInfo.ReturnType)
                    : null;
            }

            return new TypeKnowledge(this.returnType);
        }
    }
}