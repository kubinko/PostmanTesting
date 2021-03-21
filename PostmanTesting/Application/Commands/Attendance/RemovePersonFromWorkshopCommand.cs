namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Command for removing person from workshop.
    /// </summary>
    public class RemovePersonFromWorkshopCommand
    {
        public long WorkshopId { get; set; }
        public long PersonId { get; set; }
    }
}
