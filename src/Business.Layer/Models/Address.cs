namespace Business.Layer.Models
{
    public class Address : Entity
    {
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? Complement { get; set; }
        public string? PostalCode { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public Guid SupplierId { get; set; }


        /***************** Only to Support the Relation *******************/
        public Supplier Supplier { get; set; }
    }
}
