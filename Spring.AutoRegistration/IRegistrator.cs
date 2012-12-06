namespace Spring.AutoRegistration
{
    using System;

    using Spring.Context.Support;

    public interface IRegistrator
    {
        void Register(Type type, AbstractApplicationContext context);
    }
}
