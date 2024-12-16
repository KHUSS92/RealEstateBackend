namespace RealEstateBackend.Models
{
    public class Property
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int SquareFeet { get; set; }
        public string Location { get; set; }

    }
}
