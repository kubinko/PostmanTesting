using System;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Command for creating workshop.
    /// </summary>
    public class CreateWorkshopCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
