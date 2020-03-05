using System;
using System.ComponentModel.DataAnnotations;

namespace ContactLibrary.API.Models
{
    public class ContactForCreationDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
