namespace Business.Layer.Models
{
    public class Product : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }
    }
}
