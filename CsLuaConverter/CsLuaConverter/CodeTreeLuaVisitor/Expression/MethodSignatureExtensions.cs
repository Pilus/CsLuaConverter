namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeKnowledgeRegistry;

    public static class MethodSignatureExtensions
    {
        private static int[] primes = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997 };

        public static void WriteSignature(this MethodKnowledge method, IIndentedTextWriterWrapper textWriter,
            IProviders providers)
        {
            var components = GetSignatureComponents(method);
            var nonGenericHash = components.Where(c => c.GenericReference == null).Sum(c => c.Coefficient*c.SignatureHash);
            var genericComponents = components.Where(c => c.GenericReference != null).ToArray();

            if (nonGenericHash > 0 || !genericComponents.Any())
            {
                textWriter.Write(nonGenericHash.ToString());

                if (genericComponents.Any())
                {
                    textWriter.Write("+");
                }
            }

            for (var index = 0; index < genericComponents.Length; index++)
            {
                var component = genericComponents[index];
                if (index > 0)
                {
                    textWriter.Write("+");
                }

                textWriter.Write($"({component.Coefficient}*");

                var scope = providers.GenericsRegistry.GetGenericScope(component.GenericReference);
                if (scope.Equals(GenericScope.Class))
                {
                    textWriter.Write("generics[genericsMapping['{0}']].__hash)", component.GenericReference);
                }
                else
                {
                    textWriter.Write("methodGenerics[methodGenericsMapping['{0}']].__hash)", component.GenericReference);
                }
            }
        }

        private static int GetSignatureHash(Type type)
        {
            var value = 0;
            var chars = type.Name.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                value = chars[i]*primes[i];
            }

            return value;
        }

        private static SignatureComponent[] GetSignatureComponents(MethodKnowledge method)
        {
            var list = new List<SignatureComponent>();
            var inputTypes = method.GetInputParameterTypes();
            for (var i = 0; i < inputTypes.Length; i++)
            {
                var inputType = inputTypes[i];
                AddSignatureComponents(primes[i], inputType, list);
            }

            return list.ToArray();
        }

        private static void AddSignatureComponents(int coefficient, Type type, List<SignatureComponent> components)
        {
            if (type.IsGenericParameter)
            {
                components.Add(new SignatureComponent(coefficient, type.Name));
            }
            else if (type.IsGenericType)
            {
                var hash = GetSignatureHash(type);
                var subCoefficient = coefficient*hash;
                var generics = type.GetGenericArguments();

                for (var i = 0; i < generics.Length; i++)
                {
                    AddSignatureComponents(subCoefficient*primes[i], generics[i], components);
                }
            }
            else
            {
                components.Add(new SignatureComponent(coefficient, GetSignatureHash(type)));
            }
        }

        private class SignatureComponent
        {
            public SignatureComponent(int coefficient, int hash)
            {
                this.Coefficient = coefficient;
                this.SignatureHash = hash;
            }

            public SignatureComponent(int coefficient, string genericRef)
            {
                this.Coefficient = coefficient;
                this.GenericReference = genericRef;
            }

            public int Coefficient;
            public int SignatureHash;
            public string GenericReference;
        }
    }
}