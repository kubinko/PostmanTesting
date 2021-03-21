using System;

namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to get all person's workshops.
    /// </summary>
    public class GetPersonWorkshops
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Person ID.</param>
        public GetPersonWorkshops(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Person ID.
        /// </summary>
        public long Id { get; set; }

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
