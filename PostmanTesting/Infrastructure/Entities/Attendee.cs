﻿using System;

namespace PostmanTesting.Infrastructure.Entities
{
    /// <summary>
    /// Attendee DB model.
    /// </summary>
    public class Attendee
    {
        public long Id { get; set; }
        public long WorkshopId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset LastModifiedTimestamp { get; set; }
        public long LastModifiedBy { get; set; }
    }
}
