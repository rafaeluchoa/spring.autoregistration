namespace Spring.AutoRegistration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Spring.Context.Support;

    public class AutoRegistration : IAutoRegistration
    {
        private readonly AbstractApplicationContext _context;

        private readonly IList<Predicate<Assembly>> _assemblyIncludeFilters;
        private readonly IList<Predicate<Assembly>> _assemblyExcludeFilters;

        private readonly IList<RegistrationEntry> _registrations;
        private readonly IList<Predicate<Type>> _typeExcludeFilters;

        public AutoRegistration(AbstractApplicationContext context)
        {
            _context = context;

            _assemblyIncludeFilters = new List<Predicate<Assembly>>();
            _assemblyExcludeFilters = new List<Predicate<Assembly>>();

            _registrations = new List<RegistrationEntry>();
            _typeExcludeFilters = new List<Predicate<Type>>();
        }

        public IAutoRegistration IncludeAssembly(Predicate<Assembly> assemblyFilter)
        {
            _assemblyIncludeFilters.Add(assemblyFilter);
            return this;
        }

        public IAutoRegistration Include(Predicate<Type> typeFilter, IRegistrator registrator)
        {
            _registrations.Add(new RegistrationEntry(typeFilter, registrator, _context));
            return this;
        }

        public IAutoRegistration ExcludeAssembly(Predicate<Assembly> assemblyFilter)
        {
            _assemblyExcludeFilters.Add(assemblyFilter);
            return this;
        }

        public IAutoRegistration Exclude(Predicate<Type> typeFilter)
        {
            _typeExcludeFilters.Add(typeFilter);
            return this;
        }

        public IAutoRegistration ExcludeSystemAssemblies()
        {
            ExcludeAssembly(
                a =>
                a.GetName().FullName.StartsWith("System") 
                    || a.GetName().FullName.StartsWith("mscorlib"));
            return this;
        }

        public void ApplyAutoRegistration()
        {
            var types = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(a => !_assemblyExcludeFilters.Any(f => f(a)))
                    .Where(a => _assemblyIncludeFilters.Any(f => f(a)))
                    .SelectMany(a => a.GetTypes())
                    .Where(t => !_typeExcludeFilters.Any(f => f(t)));

            foreach (var type in types)
            {
                foreach (var registrator in _registrations)
                {
                    registrator.RegisterIfSatisfiesFilter(type);
                }
            }
        }
    }

    public class RegistrationEntry
    {
        private readonly Predicate<Type> _typeFilter;

        private readonly IRegistrator _registrator;

        private readonly AbstractApplicationContext _context;

        public RegistrationEntry(
            Predicate<Type> typeFilter, IRegistrator registrator, AbstractApplicationContext context)
        {
            _typeFilter = typeFilter;
            _registrator = registrator;
            _context = context;
        }

        public void RegisterIfSatisfiesFilter(Type type)
        {
            if (_typeFilter(type))
            {
                _registrator.Register(type, _context);
            }
        }
    }
}
