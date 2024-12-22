namespace RealEstateBackend.Models
{
    public class PropertyHistory
    {
        public DateTime  Date { get; set; } 
        public decimal Value { get; set; } //Property value at point in time
        public string Event { get; set; } //Sales, appraisals

    }
}
