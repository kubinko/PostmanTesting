using System;

namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to get all user's workshops.
    /// </summary>
    public class GetAllUserWorkshopsQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">User ID.</param>
        public GetAllUserWorkshopsQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// User ID.
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
