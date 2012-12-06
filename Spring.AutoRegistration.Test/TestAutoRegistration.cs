
namespace Spring.AutoRegistration.Test
{
    using FluentAssertions;

    using NUnit.Framework;

    using Spring.AutoRegistration.Test.Example;
    using Spring.Context.Support;

    [TestFixture]
    public class TestAutoRegistration
    {
        #region Generic Application Context

        [Test]
        public void RegisterITypeName()
        {
            // Arrange
            var context = new GenericApplicationContext();
            context.Configure()
                .IncludeAssembly(x => x.FullName.StartsWith("Spring.AutoRegistration.Test"))
                .Include(x => x.ImplementsITypeName(), Then.Register())
                .ApplyAutoRegistration();

            // Act
            var foo = context.GetObject("Foo") as IFoo;

            // Assert
            foo.Should().NotBeNull();
        }

        [Test]
        public void RegisterITypeNameAsSingleton()
        {
            // Arrange
            var context = new GenericApplicationContext();
            context.Configure()
                .IncludeAssembly(x => x.FullName.StartsWith("Spring.AutoRegistration.Test"))
                .Include(x => x.ImplementsITypeName(), Then.Register().UsingSingleton())
                .ApplyAutoRegistration();

            // Act
            var foo = context.GetObject("Foo") as IFoo;
            var foo1 = context.GetObject("Foo") as IFoo;

            // Assert
            foo.Should().Equals(foo1);
        }

        [Test]
        public void RegisterPropertiesByName()
        {
            // Arrange
            var context = new GenericApplicationContext();
            context.Configure()
                .IncludeAssembly(x => x.FullName.StartsWith("Spring.AutoRegistration.Test"))
                .Include(x => x.ImplementsITypeName(), Then.Register().UsingSingleton().InjectByProperty(If.DecoratedWith<InjectAttribute>))
                .ApplyAutoRegistration();

            // Act
            var bar = context.GetObject("Bar") as Bar;

            // Assert
            bar.Foo.Should().NotBeNull();
        }

        #endregion

        #region Xml Application Context

        [Test]
        public void LoadXml()
        {
            // Arrange
            var context = new XmlApplicationContext("assembly://Spring.AutoRegistration.Test/Spring.AutoRegistration.Test/SpringObjectsSimple.xml");

            // Act
            var repository = context.GetObject("Repository") as IRepository;

            // Assert
            repository.Should().NotBeNull();
        }

        [Test]
        public void LoadXmlWithAutoRegistration()
        {
            // Arrange
            var context = new XmlApplicationContext("assembly://Spring.AutoRegistration.Test/Spring.AutoRegistration.Test/SpringObjectsSimple.xml");
            context.Configure()
                .IncludeAssembly(x => x.FullName.StartsWith("Spring.AutoRegistration.Test"))
                .Include(x => x.ImplementsITypeName(), Then.Register().UsingSingleton())
                .ApplyAutoRegistration();

            // Act
            var repository = context.GetObject("Repository") as IRepository;
            var foo = context.GetObject("Foo") as IFoo;

            // Assert
            repository.Should().NotBeNull();
            foo.Should().NotBeNull();
        }

        #endregion
    }
}
