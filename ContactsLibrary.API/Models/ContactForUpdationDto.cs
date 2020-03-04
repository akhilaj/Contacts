using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactLibrary.API.Models
{
    public class ContactForUpdationDto : ContactForCreationDto
    {
        public Guid Id { get; set; }
    }
}
