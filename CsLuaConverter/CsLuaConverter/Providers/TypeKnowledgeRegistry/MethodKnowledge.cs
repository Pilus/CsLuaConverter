namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using GenericsRegistry;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [DebuggerDisplay("MethodKnowledge: {method}")]
    public class MethodKnowledge : IKnowledge
    {
        private readonly bool isExtension;
        private readonly MethodBase method;
        private readonly Type[] inputTypes;
        private readonly Type returnType;
        private readonly Type[] genericsTypes;
        private readonly Dictionary<Type, TypeKnowledge> methodGenericMapping;

        public MethodKnowledge(MethodBase method)
        {
            this.methodGenericMapping = new Dictionary<Type, TypeKnowledge>();
            this.method = method;
        }

        public MethodKnowledge(bool isExtension, Type returnType, Type[] genericsTypes, Type[] inputTypes)
        {
            this.isExtension = isExtension;
            this.returnType = returnType;
            this.genericsTypes = genericsTypes;
            this.inputTypes = inputTypes;
            this.methodGenericMapping = new Dictionary<Type, TypeKnowledge>();
        }

        public MethodKnowledge()
        {
            this.returnType = typeof(void);
            this.inputTypes = new Type[] {};
        }


        public override int GetHashCode()
        {
            if (this.method != null)
            {
                return this.method.Name.GetHashCode() +
                       this.method.GetParameters().Sum(p => p.ParameterType.GetHashCode()) +
                       (this.method.IsGenericMethod ? this.method.GetGenericArguments().Sum(p => p.GetHashCode()) : 0);

            }
            else
            {
                return this.returnType.GetHashCode() + this.returnType.GetHashCode() +
                       this.genericsTypes.Sum(t => t.GetHashCode()) + this.inputTypes.Sum(t => t.GetHashCode());
            }
        }

        public TypeKnowledge ToTypeKnowledge()
        {
            return new TypeKnowledge(this.method);
        }

        public int GetNumberOfMethodGenerics()
        {
            return (this.method as MethodInfo)?.GetGenericArguments().Length ?? this.genericsTypes?.Length ?? 0;
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
                //throw new Exception("Could not apply generics to non methodInfo based MethodKnowledge.");
                return;
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
                for (int index = 0; index < this.inputTypes.Length; index++)
                {
                    var methodParameterType = this.inputTypes[index];
                    var inputType = inputTypes[index];
                    this.ResolveImplicitMethodGenerics(methodParameterType, inputType);
                }

                return this.genericsTypes.All(g => this.methodGenericMapping.ContainsKey(g));
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

        public TypeKnowledge[] GetResolvedMethodGenerics()
        {
            if (this.method != null)
            {
                return this.method.GetGenericArguments().Select(gt => this.methodGenericMapping[gt]).ToArray();
            }

            return this.genericsTypes.Select(gt => this.methodGenericMapping[gt]).ToArray();
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

                if (isParams && index >= parameterTypes.Length - 1)
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
            if (inputType == typeof (Nullable))
            {
                return true;
            }

            if (parameterType.IsAssignableFrom(inputType) || IsImplicitConvertable(inputType, parameterType))
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

            if (parameterTypeGenerics.Length != inputTypeGenerics.Length)
            {
                return false;
            }

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

        private static readonly Dictionary<Type, List<Type>> ConversionMatrix = new Dictionary<Type, List<Type>>()
        {
            { typeof(float), new List<Type>() { typeof(long), typeof(double), typeof(int) } },
            { typeof(long), new List<Type>() { typeof(float), typeof(double), typeof(int) } },
            { typeof(double), new List<Type>() { typeof(float), typeof(long), typeof(int) } },
        };

        private static bool IsImplicitConvertable(Type type, Type targetType)
        {
            List<Type> allowed;
            return ConversionMatrix.TryGetValue(targetType, out allowed) && allowed.Contains(type);
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

        public Type[] GetGenericTypes()
        {
            return this.genericsTypes ?? this.method.GetGenericArguments();
        }

        private Type ApplyMethodGenerics(Type type)
        {
            if (!type.IsGenericParameter && !type.IsGenericType)
            {
                return type;
            }

            if (type.IsGenericParameter)
            {
                return this.methodGenericMapping.ContainsKey(type) ? this.methodGenericMapping[type].GetTypeObject() : type;
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

        public TypeKnowledge[] GetInputParameterTypeKnowledge()
        {
            if (this.method != null)
            {
                return this.method.GetParameters().Select(p => p.ParameterType).Select(t => new TypeKnowledge(t)).ToArray();
            }

            return this.inputTypes.Select(t => new TypeKnowledge(t)).ToArray();
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
                if (isParams && index >= parameters.Length - 1)
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

            if (otherType == typeof (Nullable))
            {
                // Determine the best as the most advanced class.
                var i = type.IsInterface ? 5 : 0;
                while (type.BaseType != null)
                {
                    type = type.BaseType;
                    i++;
                }

                return 100 - i;
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

        public bool IsGetType()
        {
            return this.method?.Name == "GetType" && this.method.DeclaringType == typeof (object);
        }

        public string GetSignatureString()
        {
            if (this.method != null)
            {
                return GetSignatureString(this.method as MethodInfo);
            }

            var args = string.Join(",", this.inputTypes.Select(GetSignatureString));
            return $"{GetSignatureString(this.returnType)} X({args})";
        }

        private static string GetSignatureString(MethodInfo method)
        {
            var args = string.Join(",", method.GetParameters().Select(p => GetSignatureString(p.ParameterType)));
            return $"{GetSignatureString(method.ReturnType)} {method.Name}({args})";
        }

        private static string GetSignatureString(Type type)
        {
            if (type.IsGenericParameter)
            {
                return "T";
            }

            if (!type.IsGenericType)
            {
                return type.ToString();
            }

            var args = string.Join(",", type.GetGenericArguments().Select(GetSignatureString));
            return type.Namespace + "." + type.Name + "[" + args + "]";
        }
    }
}