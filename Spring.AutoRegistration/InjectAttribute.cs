namespace Spring.AutoRegistration
{
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Method)]
    public class InjectAttribute : System.Attribute
    {
        public InjectAttribute(string name)
        {
            Name = name;
        }

        public InjectAttribute()
        {
        }

        public string Name { get; set; }
    }
}
