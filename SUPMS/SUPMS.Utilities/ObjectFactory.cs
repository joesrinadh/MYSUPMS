using System;

namespace SUPMS.Infrastructure.Utilities
{
    public static class ObjectFactory<T, TClass>
        where TClass : T
    {
        public static Func<object[], T> Dispenser { get; set; }

        public static T CreateInstance(params object[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
 
            if (Dispenser != null)
            {
                return Dispenser(args);
            }
 
            return (T)Activator.CreateInstance(typeof(TClass), args);
        }

        public static T CreateInstance()
        {
            if (Dispenser != null)
            {
                return Dispenser(new object[] { });
            }
 
            return (T)Activator.CreateInstance(typeof(TClass), new object[] { });
        }
    }
}
