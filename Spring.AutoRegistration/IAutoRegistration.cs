namespace Spring.AutoRegistration
{
    using System;
    using System.Reflection;

    public interface IAutoRegistration
    {
        IAutoRegistration IncludeAssembly(Predicate<Assembly> assemblyFilter);

        IAutoRegistration Include(Predicate<Type> typeFilter, IRegistrator registrator);

        IAutoRegistration Exclude(Predicate<Type> typeFilter);

        IAutoRegistration ExcludeAssembly(Predicate<Assembly> assemblyFilter); 

        IAutoRegistration ExcludeSystemAssemblies();

        void ApplyAutoRegistration();
    }
}
