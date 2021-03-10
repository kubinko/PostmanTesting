using System;

namespace PostmanTesting.Infrastructure.Entities
{
    /// <summary>
    /// Workshop DB model.
    /// </summary>
    public class Workshop
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public long AddressId { get; set; }
        public decimal Price { get; set; }
        public int AttendeesCount { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset LastModifiedTimestamp { get; set; }
        public long LastModifiedBy { get; set; }
    }
}
