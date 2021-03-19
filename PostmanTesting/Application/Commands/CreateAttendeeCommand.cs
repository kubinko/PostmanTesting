﻿namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Command for creating attendee.
    /// </summary>
    public class CreateAttendeeCommand
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
