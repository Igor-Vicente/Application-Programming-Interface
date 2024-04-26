namespace Business.Layer.Models
{
    public class Product : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }
        public Guid SupplierId { get; set; }

        /***************** Only to Support the Relation *******************/
        public Supplier Supplier { get; set; }
    }
}
