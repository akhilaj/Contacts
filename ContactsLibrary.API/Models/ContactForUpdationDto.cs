using System;
using System.ComponentModel.DataAnnotations;

namespace ContactLibrary.API.Models
{
    public class ContactForUpdationDto : ContactForCreationDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
