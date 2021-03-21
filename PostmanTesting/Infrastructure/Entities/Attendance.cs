namespace PostmanTesting.Infrastructure.Entities
{
    /// <summary>
    /// Attendance DB model.
    /// </summary>
    public class Attendance
    {
        public long Id { get; set; }
        public long WorkshopId { get; set; }
        public long PersonId { get; set; }
    }
}
