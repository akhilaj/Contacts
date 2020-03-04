using AutoMapper;
using ContactLibrary.API.Helpers;
using ContactLibrary.API.Models;
using ContactLibrary.API.ResourceParameters;
using ContactLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactLibrary.API.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactLibraryRepository _contactLibraryRepository;
        private readonly IMapper _mapper;

        public ContactsController(IContactLibraryRepository ContactLibraryRepository,
            IMapper mapper)
        {
            _contactLibraryRepository = ContactLibraryRepository ??
                throw new ArgumentNullException(nameof(ContactLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts(
            [FromQuery] ContactsResourceParameters contactsResourceParameters)
        {
            var contactsFromRepo = await _contactLibraryRepository.GetContactsAsync(contactsResourceParameters).ConfigureAwait(false);
            return Ok(_mapper.Map<IEnumerable<ContactDto>>(contactsFromRepo));
        }

        [HttpGet("{contactId}", Name ="GetContact")]
        public async Task<IActionResult> GetContact(Guid contactId)
        {
            var contactFromRepo = await _contactLibraryRepository.GetContactAsync(contactId).ConfigureAwait(false);

            if (contactFromRepo == null)
            {
                return NotFound();
            }
             
            return Ok(_mapper.Map<ContactDto>(contactFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> CreateContact(ContactForCreationDto contact)
        {
            var contactEntity = _mapper.Map<Entities.Contact>(contact);
            _contactLibraryRepository.AddContact(contactEntity);
            await _contactLibraryRepository.SaveAsync().ConfigureAwait(false);

            var contactToReturn = _mapper.Map<ContactDto>(contactEntity);
            return CreatedAtRoute("GetContact",
                new { contactId = contactToReturn.Id },
                contactToReturn);
        }

        // PUT api/contacts/{guid}
        [HttpPut("{contactId}")]
        public async Task<IActionResult> PutAsync(Guid contactId, [FromBody] Entities.Contact contact)
        {
            if (ModelState.IsValid && contactId == contact?.Id)
            {
                var contactToUpdate = await _contactLibraryRepository.GetContactAsync(contactId).ConfigureAwait(false);
                if (contactToUpdate != null)
                {
                    _contactLibraryRepository.UpdateContact(contact);

                    return new NoContentResult();
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("{contactId}")]
        public async Task<ActionResult> DeleteContact(Guid contactId)
        {
            var contactFromRepo = await _contactLibraryRepository.GetContactAsync(contactId).ConfigureAwait(false);

            if (contactFromRepo == null)
            {
                return NotFound();
            }

            _contactLibraryRepository.DeleteContact(contactFromRepo);

            await _contactLibraryRepository.SaveAsync().ConfigureAwait(false);

            return NoContent();
        }
    }
}
