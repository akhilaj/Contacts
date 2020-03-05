using System;
using System.ComponentModel.DataAnnotations;

namespace ContactLibrary.API.Entities
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        public StatusCode Status { get; set; }
    }

    public enum StatusCode
    {
        Inactive = 0,
        Active = 1
    }
}
