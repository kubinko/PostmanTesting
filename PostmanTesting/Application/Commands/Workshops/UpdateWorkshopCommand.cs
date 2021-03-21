using System;
using System.Text.Json.Serialization;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Command for updating workshop.
    /// </summary>
    public class UpdateWorkshopCommand
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
