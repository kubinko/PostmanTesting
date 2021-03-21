namespace PostmanTesting.Domain
{
    /// <summary>
    /// Person model.
    /// </summary>
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long CreatedBy { get; set; }
    }
}
