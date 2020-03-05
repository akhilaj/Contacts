using System;

namespace ContactLibrary.API.Models
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public int Age { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }
    }
}
