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
        private static readonly int[] Primes = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997 };

        public static void WriteSignature(this MethodKnowledge method, IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            method.GetInputParameterTypeKnowledge().WriteSignature(textWriter, providers);
        }

        public static void WriteSignature(this TypeKnowledge[] inputTypes, IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var components = GetSignatureComponents(inputTypes, providers);
            WriteSignature(components, textWriter, providers);
        }

        public static void WriteSignature(this TypeKnowledge type, IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var components = GetSignatureComponents(type, providers);
            WriteSignature(components, textWriter, providers);
        }

        private static void WriteSignature(SignatureComponent[] components, IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            
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

            var methodGenericIndex = 0;
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
                    textWriter.Write("generics[genericsMapping['{0}']].signatureHash)", component.GenericReference);
                }
                else if (scope.Equals(GenericScope.Method))
                {
                    textWriter.Write("methodGenerics[methodGenericsMapping['{0}']].signatureHash)", component.GenericReference);
                }
                else
                {
                    textWriter.Write($"{Primes[methodGenericIndex + 20]})");
                    methodGenericIndex++;
                }
            }
        }

        private static int GetSignatureHash(TypeKnowledge type)
        {
            return GetSignatureHash(type.Name);
        }

        private static int GetSignatureHash(string name)
        {
            var value = 0;
            var chars = name.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                value = value + (chars[i] * Primes[i]);
            }

            return value;
        }

        private static SignatureComponent[] GetSignatureComponents(TypeKnowledge[] inputTypes, IProviders providers)
        {
            var list = new List<SignatureComponent>();
            for (var i = 0; i < inputTypes.Length; i++)
            {
                var inputType = inputTypes[i];
                AddSignatureComponents(Primes[i], inputType, list, providers);
            }

            return list.ToArray();
        }

        private static SignatureComponent[] GetSignatureComponents(TypeKnowledge type, IProviders providers)
        {
            var list = new List<SignatureComponent>();
            AddSignatureComponents(1, type, list, providers);
            return list.ToArray();
        }

        private static void AddSignatureComponents(int coefficient, TypeKnowledge type, List<SignatureComponent> components, IProviders providers)
        {
            if (type.IsGenericParameter)
            {
                if (providers.GenericsRegistry.IsGeneric(type.Name) && providers.GenericsRegistry.GetGenericScope(type.Name) != GenericScope.MethodDeclaration)
                { 
                    components.Add(new SignatureComponent(coefficient, type.Name));
                }
                else
                {
                    components.Add(new SignatureComponent(coefficient, 1));
                }
            }
            else if (type.IsGenericType)
            {
                var hash = GetSignatureHash(type);
                var subCoefficient = coefficient*hash;
                var generics = type.GetGenerics();

                for (var i = 0; i < generics.Length; i++)
                {
                    AddSignatureComponents(subCoefficient*Primes[i], generics[i], components, providers);
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