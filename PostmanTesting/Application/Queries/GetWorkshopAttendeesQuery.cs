namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to get workshop attendees.
    /// </summary>
    public class GetWorkshopAttendeesQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Workshop ID.</param>
        public GetWorkshopAttendeesQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Workshop ID.
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
            public string Email { get; set; }
        }
    }
}
