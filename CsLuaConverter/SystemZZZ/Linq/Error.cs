namespace SystemZZZ.Linq
{
    using System;

    public static class Error
    {
        internal static Exception ArgumentArrayHasTooManyElements(object p0)
        {
            return (Exception)new ArgumentException(F("Parameter {0} has too many elements", p0));
        }

        internal static Exception ArgumentNotIEnumerableGeneric(object p0)
        {
            return (Exception)new ArgumentException(F("{0} is not IEnumerable<>;", p0));
        }

        internal static Exception ArgumentNotSequence(object p0)
        {
            return (Exception)new ArgumentException(F("{0} is not a sequence", p0));
        }

        internal static Exception ArgumentNotValid(object p0)
        {
            return (Exception)new ArgumentException(F("Argument {0} is not valid", p0));
        }

        internal static Exception IncompatibleElementTypes()
        {
            return (Exception)new ArgumentException("The two sequences have incompatible element types");
        }

        internal static Exception ArgumentNotLambda(object p0)
        {
            return (Exception)new ArgumentException(F("Argument {0} is not a LambdaExpression", p0));
        }

        internal static Exception MoreThanOneElement()
        {
            return (Exception)new InvalidOperationException("Sequence contains more than one element");
        }

        internal static Exception MoreThanOneMatch()
        {
            return (Exception)new InvalidOperationException("Sequence contains more than one matching element");
        }

        internal static Exception NoArgumentMatchingMethodsInQueryable(object p0)
        {
            return (Exception)new InvalidOperationException(F("There is no method '{0}' on class System.Linq.Enumerable that matches the specified arguments", p0));
        }

        internal static Exception NoElements()
        {
            return (Exception)new InvalidOperationException("Sequence contains no elements");
        }

        internal static Exception NoMatch()
        {
            return (Exception)new InvalidOperationException("Sequence contains no matching element");
        }

        internal static Exception NoMethodOnType(object p0, object p1)
        {
            return (Exception)new InvalidOperationException(F("There is no method '{0}' on type '{1}' that matches the specified arguments", p0, p1));
        }

        internal static Exception NoMethodOnTypeMatchingArguments(object p0, object p1)
        {
            return (Exception)new InvalidOperationException(F("There is no method '{0}' on type '{1}' that matches the specified arguments", p0, p1));
        }

        internal static Exception NoNameMatchingMethodsInQueryable(object p0)
        {
            return (Exception)new InvalidOperationException(F("There is no method '{0}' on class System.Linq.Queryable that matches the specified arguments", p0));
        }

        internal static Exception ArgumentNull(string paramName)
        {
            return (Exception)new ArgumentNullException(paramName);
        }

        internal static Exception ArgumentOutOfRange(string paramName)
        {
            return (Exception)new ArgumentOutOfRangeException(paramName);
        }

        internal static Exception NotImplemented()
        {
            return (Exception)new NotImplementedException();
        }

        internal static Exception NotSupported()
        {
            return (Exception)new NotSupportedException();
        }

        private static string F(string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }
}