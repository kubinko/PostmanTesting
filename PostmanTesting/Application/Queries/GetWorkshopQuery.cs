using System;
using System.Text.Json.Serialization;

namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to get workshop by ID.
    /// </summary>
    public class GetWorkshopQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Workshop id.</param>
        public GetWorkshopQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Workshop ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Workshop.
        /// </summary>
        public class Workshop
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            public decimal Price { get; set; }
            public int AttendeesCount { get; set; }
            [JsonIgnore]
            public long CreatedBy { get; set; }
        }
    }
}
