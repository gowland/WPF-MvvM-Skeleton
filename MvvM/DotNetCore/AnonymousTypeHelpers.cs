using System;

namespace DotNetCore
{
    public static class AnonymousTypeHelpers
    {
        /*
         * Lifted from: https://blogs.msdn.microsoft.com/ericlippert/2007/01/12/lambda-expressions-vs-anonymous-methods-part-three/
         *
         * Make a function for a lambda that returns an anonymous type
         * Example:
         * var f = MakeFunction(c=>c.Name, new {Name="", Age=0, Address=""} );
         */
        private static Func<TA, TR> MakeFunction<TA, TR>(Func<TA, TR> f) { return f; }
    }
}
