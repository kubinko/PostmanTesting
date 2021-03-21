namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Command for adding person to workshop.
    /// </summary>
    public class AddPersonToWorkshopCommand
    {
        public long WorkshopId { get; set; }
        public long PersonId { get; set; }
    }
}
