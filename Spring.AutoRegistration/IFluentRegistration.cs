namespace Spring.AutoRegistration
{
    using System;
    using System.Reflection;

    public interface IFluentRegistration : IRegistrator
    {
        IFluentRegistration UsingSingleton();

        IFluentRegistration UsingPrototype();

        IFluentRegistration WithName(string name);

        IFluentRegistration WithName(Func<Type, string> nameResolver);

        IFluentRegistration WithTypeName();

        IFluentRegistration InjectByProperty(Func<PropertyInfo, bool> filterProperty);
    }
}
