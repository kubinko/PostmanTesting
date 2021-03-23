using System.Text.Json.Serialization;

namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to get person by ID.
    /// </summary>
    public class GetPersonQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Person id.</param>
        public GetPersonQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Person ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Person.
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
            [JsonIgnore]
            public long CreatedBy { get; set; }
        }
    }
}
