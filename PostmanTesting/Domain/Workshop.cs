using System;

namespace PostmanTesting.Domain
{
    /// <summary>
    /// Workshop model.
    /// </summary>
    public class Workshop
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int AttendeesCount { get; set; }
    }
}
