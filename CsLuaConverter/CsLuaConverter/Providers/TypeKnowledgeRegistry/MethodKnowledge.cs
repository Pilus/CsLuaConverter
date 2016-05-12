namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [DebuggerDisplay("MethodKnowledge: {method}")]
    public class MethodKnowledge : IKnowledge
    {
        private readonly bool isExtension;
        private readonly MethodBase method;
        private readonly Type[] inputTypes;
        private readonly Type returnType;
        private Dictionary<Type, TypeKnowledge> methodGenericMapping;

        public MethodKnowledge(MethodBase method)
        {
            var x = (method as MethodInfo)?.ReturnType.Name == "T3";
            this.methodGenericMapping = new Dictionary<Type, TypeKnowledge>();
            this.method = method;
        }

        public MethodKnowledge(bool isExtension, Type returnType, params Type[] inputTypes)
        {
            this.isExtension = isExtension;
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
            return (this.method as MethodInfo)?.GetGenericArguments().Length ?? 0;
        }

        public int GetNumberOfArgs()
        {
            return this.GetInputParameterTypes().Length;
        }

        public bool IsExtension()
        {
            return this.isExtension;
        }

        public bool IsParams()
        {
            return this.method?.GetParameters().LastOrDefault()?.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any() ?? false;
        }

        public void RegisterExplicitMethodGenericsMapping(TypeKnowledge[] generics)
        {
            if (this.method == null)
            {
                throw new Exception("Could not apply generics to non methodInfo based MethodKnowledge.");
            }

            var genericArgs = this.method.GetGenericArguments();

            for (var index = 0; index < genericArgs.Length; index++)
            {
                this.methodGenericMapping[genericArgs[index]] = generics[index];
            }
        }

        public bool ResolveImplicitMethodGenerics(TypeKnowledge[] inputTypes)
        {
            if (this.method == null)
            {
                throw new Exception("Could not apply generics to non methodInfo based MethodKnowledge.");
            }

            var genericArgs = this.method.GetGenericArguments();

            var methodInputTypes = this.method.GetParameters().Select(p => p.ParameterType).ToArray();

            for (int index = 0; index < methodInputTypes.Length; index++)
            {
                var methodParameterType = methodInputTypes[index];
                var inputType = inputTypes[index];
                this.ResolveImplicitMethodGenerics(methodParameterType, inputType);
            }

            return genericArgs.All(g => this.methodGenericMapping.ContainsKey(g));
        }

        private void ResolveImplicitMethodGenerics(Type methodParameterType, TypeKnowledge inputType)
        {
            if (inputType == null)
            {
                return;
            }

            if (!methodParameterType.IsGenericParameter && !methodParameterType.IsGenericType)
            {
                return;
            }

            if (methodParameterType.IsGenericParameter)
            {
                this.methodGenericMapping[methodParameterType] = inputType;
                return;
            }

            var methodParameterGenericTypes = methodParameterType.GetGenericArguments();
            var inputGenericTypes = inputType.GetGenerics();

            for (int index = 0; index < methodParameterGenericTypes.Length; index++)
            {
                var methodParameterGenericType = methodParameterGenericTypes[index];
                var inputGenericType = inputGenericTypes[index];
                this.ResolveImplicitMethodGenerics(methodParameterGenericType, inputGenericType);
            }
        }

        public bool FitsArguments(Type[] types)
        {
            var isParams = this.IsParams();
            var parameterTypes = this.GetInputParameterTypes();

            for (var index = 0; index < types.Length; index++)
            {
                var type = types[index];

                if (type == null)
                {
                    continue;
                }

                var parameterType = parameterTypes[Math.Min(index, parameterTypes.Length - 1)];

                if (isParams && index >= parameterTypes.Length)
                {
                    var paramType =
                        parameterType.GetInterface(typeof (IEnumerable<object>).Name).GetGenericArguments().Single();
                    if (!InputTypeFitsParameterType(type, paramType))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!InputTypeFitsParameterType(type, parameterType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool InputTypeFitsParameterType(Type inputType, Type parameterType)
        {
            if (parameterType.IsAssignableFrom(inputType))
            {
                return true;
            }

            if (!parameterType.IsGenericParameter && !parameterType.IsGenericType && !inputType.IsGenericParameter)
            {
                return false;
            }

            if (parameterType.IsGenericParameter || inputType.IsGenericParameter)
            {
                return true;
            }

            if (!inputType.IsGenericType)
            {
                return false;
            }

            var parameterTypeGenerics = parameterType.GetGenericArguments();
            var inputTypeGenerics = inputType.GetGenericArguments();

            for (int index = 0; index < parameterTypeGenerics.Length; index++)
            {
                var parameterTypeGeneric = parameterTypeGenerics[index];
                var inputTypeGeneric = inputTypeGenerics[index];

                if (!InputTypeFitsParameterType(inputTypeGeneric, parameterTypeGeneric))
                {
                    return false;
                }
            }

            return true;
        }

        public TypeKnowledge[] GetInputArgs()
        {
            return this.GetInputParameterTypes().Select(p => new TypeKnowledge(this.ApplyMethodGenerics(p))).ToArray();
        }

        public TypeKnowledge GetReturnType()
        {
            
            if (this.method != null)
            {
                var methodInfo = this.method as MethodInfo;
                return methodInfo != null
                    ? new TypeKnowledge(this.ApplyMethodGenerics(methodInfo.ReturnType))
                    : null;
            }

            return new TypeKnowledge(this.ApplyMethodGenerics(this.returnType));
        }


        private Type ApplyMethodGenerics(Type type)
        {
            if (!type.IsGenericParameter && !type.IsGenericType)
            {
                return type;
            }

            if (type.IsGenericParameter)
            {
                return this.methodGenericMapping[type].GetTypeObject();
            }

            var genericArgs = type.GetGenericArguments();
            var changed = false;

            for(var index = 0; index < genericArgs.Length; index++)
            {
                var genericArg = this.ApplyMethodGenerics(genericArgs[index]);
                if (genericArgs[index] != genericArg)
                {
                    genericArgs[index] = genericArg;
                    changed = true;
                }
            }

            if (changed == false)
            {
                return type;
            }

            var genericTypeDef = type.GetGenericTypeDefinition();
            return genericTypeDef.MakeGenericType(genericArgs);
        }

        

        private Type[] GetInputParameterTypes()
        {
            if (this.method != null)
            {
                return this.method.GetParameters().Select(p => p.ParameterType).ToArray();
            }

            return this.inputTypes;
        }

        public int? GetScore(Type[] types)
        {
            var isParams = this.IsParams();
            var parameters = this.GetInputParameterTypes();

            var score = 0;
            for (var index = 0; index < parameters.Length; index++)
            {
                var type = types[index];

                if (type == null)
                {
                    continue;
                }

                var parameter = parameters[Math.Min(index, parameters.Length - 1)];

                int? parScore = 0;
                if (isParams && index >= parameters.Length)
                {
                    var paramType = parameter.GetInterface(typeof(IEnumerable<object>).Name).GetGenericArguments().Single();
                    parScore = ScoreForHowWellOtherTypeFits(paramType, type);
                }
                else
                {
                    parScore = ScoreForHowWellOtherTypeFits(parameter, type);
                }

                if (parScore == null)
                {
                    return null;
                }

                score += (int) parScore;
            }

            return score;
            
        }

        private static int? ScoreForHowWellOtherTypeFits(Type type, Type otherType)
        {
            if (type.IsGenericParameter)
            {
                return 1;
            }

            var c = 0;

            while (!type.IsAssignableFrom(otherType))
            {
                otherType = otherType.BaseType;

                if (otherType == null)
                {
                    return null;
                }

                c++;
            }

            return type == otherType ? c : c + 1;
        }


        public bool FilterByNumberOfLambdaArgs(int?[] numOfArgs)
        {
            var inputParameters = this.GetInputParameterTypes();
            for (var index = 0; index < this.GetInputParameterTypes().Length; index++)
            {
                var numArgs = numOfArgs[index];

                if (numArgs == null)
                {
                    continue;
                }

                var inputParameterType = inputParameters[index];

                var del = GetDelegate(inputParameterType);

                if (del == null)
                {
                    return false;
                }

                var lambdaInputCount = del.GetMethod("Invoke").GetParameters().Length;
                if (lambdaInputCount != numArgs)
                {
                    return false;
                }
            }

            return true;
        }

        private static System.Type GetDelegate(System.Type type)
        {
            if (type.BaseType == typeof(MulticastDelegate))
            {
                return type;
            }

            return type.BaseType?.BaseType != null ? GetDelegate(type.BaseType) : null;
        }
    }
}