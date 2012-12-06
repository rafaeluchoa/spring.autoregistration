namespace Spring.AutoRegistration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Spring.Context.Support;
    using Spring.Objects.Factory.Support;

    public class FluentRegistration : IFluentRegistration
    {
        private readonly IList<Action<Type, ObjectDefinitionBuilder>> _actions;
        private Func<Type, string> _nameResolver;

        public FluentRegistration()
        {
            _nameResolver = x => x.Name;
            _actions = new List<Action<Type, ObjectDefinitionBuilder>>();
        }

        public void Register(Type type, AbstractApplicationContext context)
        {
            var builder = ObjectDefinitionBuilder.GenericObjectDefinition(type);

            foreach (var action in _actions)
            {
                action(type, builder);
            }

            context.RegisterObjectDefinition(_nameResolver(type), builder.ObjectDefinition);
        }

        public IFluentRegistration InjectByProperty(Func<PropertyInfo, bool> filterProperty)
        {
            _actions.Add((type, builder) =>
                {
                    foreach (var property in type.GetProperties().Where(filterProperty))
                    {
                        builder.AddPropertyReference(property.Name, property.Name);
                    }        
                });
            return this;
        }

        public IFluentRegistration UsingSingleton()
        {
            _actions.Add((type, builder) => builder.SetSingleton(true));
            return this;
        }

        public IFluentRegistration UsingPrototype()
        {
            _actions.Add((type, builder) => builder.ObjectDefinition.Scope = "prototype");
            return this;
        }

        public IFluentRegistration WithName(string name)
        {
            _nameResolver = x => name;
            return this;
        }

        public IFluentRegistration WithName(Func<Type, string> nameResolver)
        {
            _nameResolver = nameResolver;
            return this;
        }

        public IFluentRegistration WithTypeName()
        {
            _nameResolver = x => x.Name;
            return this;
        }
    }
}
