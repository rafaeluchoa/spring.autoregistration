namespace Spring.AutoRegistration
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class If
    {
        public static bool DecoratedWith<TAttr>(this Type type)
            where TAttr : Attribute
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.GetCustomAttributes(false).Any(a => a.GetType() == typeof(TAttr));
        }

        public static bool DecoratedWith<TAttr>(this PropertyInfo propertyInfo)
            where TAttr : Attribute
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }

            return propertyInfo.GetCustomAttributes(false).Any(a => a.GetType() == typeof(TAttr));
        }

        public static bool Implements<TContract>(this Type type) where TContract : class
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.GetInterfaces().Any(i => i == typeof(TContract));
        }

        public static bool ImplementsOpenGeneric(this Type type, Type contract)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (contract == null)
            {
                throw new ArgumentNullException("contract");
            }

            if (!contract.IsInterface)
            {
                throw new ArgumentException("Provided contract has to be an interface", "contract");
            }

            if (!contract.IsGenericTypeDefinition)
            {
                throw new ArgumentException("Provided contract has to be an open generic", "contract");
            }

            return type.GetInterfaces().Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == contract));
        }

        public static bool ImplementsITypeName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
                

            return type.GetInterfaces().Any(i => i.Name.StartsWith("I") 
                && i.Name.Remove(0, 1) == type.Name);
        }

        public static bool ImplementsSingleInterface(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.GetInterfaces().Count() == 1;
        }

        public static bool Any(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return true;
        }

        public static bool Is<T>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type == typeof(T);
        }

        public static bool IsAssignableFrom<T>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.IsAssignableFrom(typeof(T));
        }

        public static bool AnyAssembly(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            return true;
        }

        public static bool ContainsType<T>(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            return typeof(T).Assembly == assembly;
        }
    }
}