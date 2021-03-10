namespace PostmanTesting.Infrastructure.Entities
{
    /// <summary>
    /// Address DB model.
    /// </summary>
    public class Address
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
