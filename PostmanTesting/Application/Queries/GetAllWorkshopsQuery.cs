using System;

namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to get all workshops.
    /// </summary>
    public class GetAllWorkshopsQuery
    {
        /// <summary>
        /// Workshop.
        /// </summary>
        public class Workshop
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public decimal Price { get; set; }
        }
    }
}
