namespace Spring.AutoRegistration.Test.Example
{
    public class Bar : IBar
    {
        [Inject]
        public IFoo Foo { get; set; }
    }
}
