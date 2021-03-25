namespace PostmanTesting.Application.Queries
{
    /// <summary>
    /// Query to check whether invoice was generated.
    /// </summary>
    public class CheckInvoiceGeneratedQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="workshopId">Workshop ID.</param>
        /// <param name="personId">Person ID.</param>
        public CheckInvoiceGeneratedQuery(long workshopId, long personId)
        {
            WorkshopId = workshopId;
            PersonId = personId;
        }

        /// <summary>
        /// Workshop ID.
        /// </summary>
        public long WorkshopId { get; set; }

        /// <summary>
        /// Person ID.
        /// </summary>
        public long PersonId { get; set; }
    }
}
