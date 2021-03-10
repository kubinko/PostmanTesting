using System;

namespace PostmanTesting.Infrastructure.Entities
{
    /// <summary>
    /// Attendee DB model.
    /// </summary>
    public class Attendee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long AddressId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset LastModifiedTimestamp { get; set; }
        public long LastModifiedBy { get; set; }
    }
}
