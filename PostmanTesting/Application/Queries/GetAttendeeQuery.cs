namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to get attendee by ID.
    /// </summary>
    public class GetAttendeeQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Attendee id.</param>
        public GetAttendeeQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Attendee ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Attendee.
        /// </summary>
        public class Attendee
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
}
