spring.autoregistration
=======================

Fluent Registration for Spring .NET

Spring.AutoRegistration is Fluent registration for Spring .NET, that I ported, inspired by the Unity AutoRegistration (http://autoregistration.codeplex.com/).

Simple example:

var context = new GenericApplicationContext();
            context.Configure()
                .IncludeAssembly(x => x.FullName.StartsWith("Spring.AutoRegistration.Test"))
                .Include(x => x.ImplementsITypeName(), Then.Register())
                .ApplyAutoRegistration();
                
You can install using nuget:

https://www.nuget.org/stats/packages/Spring.AutoRegistration

More examples: 

http://blog.naskar.com.br/2012/12/springautoregistration-fluent.html
