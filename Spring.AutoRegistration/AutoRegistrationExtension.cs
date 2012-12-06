namespace Spring.AutoRegistration
{
    using System;

    using Spring.Context.Support;

    public static class ApplicationContextExtension
    {
        public static IAutoRegistration Configure(this AbstractApplicationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
                
            return new AutoRegistration(context);
        }
    }
}
