using AutoMapper;
using ContactLibrary.API.Helpers;
using ContactLibrary.API.Models;
using ContactLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactLibrary.API.Controllers
{
    [ApiController]
    [Route("api/contactcollections")]
    public class ContactCollectionsController : ControllerBase
    {
        private readonly IContactLibraryRepository _contactLibraryRepository;
        private readonly IMapper _mapper;

        public ContactCollectionsController(IContactLibraryRepository contactLibraryRepository,
            IMapper mapper)
        {
            _contactLibraryRepository = contactLibraryRepository ??
                throw new ArgumentNullException(nameof(contactLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name ="GetContactCollection")]
        public async Task<IActionResult> GetContactCollection(
        [FromRoute]
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var contactEntities = await _contactLibraryRepository.GetContactsAsync(ids).ConfigureAwait(false);

            if (ids.Count() != contactEntities.Count())
            {
                return NotFound();
            }

            var contactsToReturn = _mapper.Map<IEnumerable<ContactDto>>(contactEntities);

            return Ok(contactsToReturn);
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<ContactDto>>> CreateContactCollection(
            IEnumerable<ContactForCreationDto> contactCollection)
        {
            var contactEntities = _mapper.Map<IEnumerable<Entities.Contact>>(contactCollection);
            foreach (var contact in contactEntities)
            {
                _contactLibraryRepository.AddContact(contact);
            }

            await _contactLibraryRepository.SaveAsync().ConfigureAwait(false);

            var contactCollectionToReturn = _mapper.Map<IEnumerable<ContactDto>>(contactEntities);
            var idsAsString = string.Join(",", contactCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetContactCollection",
             new { ids = idsAsString },
             contactCollectionToReturn);
        }
    }
}
 
