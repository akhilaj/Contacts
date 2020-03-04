using ContactLibrary.API.DbContexts;
using ContactLibrary.API.Entities;
using ContactLibrary.API.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactLibrary.API.Services
{
    public class ContactLibraryRepository : IContactLibraryRepository, IDisposable
    {
        private readonly ContactLibraryContext _context;

        public ContactLibraryRepository(ContactLibraryContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

      

        public void AddContact(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            // the repository fills the id (instead of using identity columns)
            contact.Id = Guid.NewGuid();


            _context.Contacts.Add(contact);
        }

        public async Task<bool> ContactExistsAsync(Guid contactId)
        {
            if (contactId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(contactId));
            }

            return await _context.Contacts.AnyAsync(a => a.Id == contactId).ConfigureAwait(false);
        }

        public void DeleteContact(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            _context.Contacts.Remove(contact);
        }
        
        public async Task<Contact> GetContactAsync(Guid contactId)
        {
            if (contactId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(contactId));
            }

            return await _context.Contacts.FirstOrDefaultAsync(a => a.Id == contactId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.ToListAsync().ConfigureAwait(false);
        }
         
        public async Task<IEnumerable<Contact>> GetContactsAsync(IEnumerable<Guid> contactIds)
        {
            if (contactIds == null)
            {
                throw new ArgumentNullException(nameof(contactIds));
            }

            return await _context.Contacts.Where(a => contactIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(ContactsResourceParameters contactsResourceParameters)
        {
            if (contactsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(contactsResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(contactsResourceParameters.SearchQuery))
            {
                return await GetContactsAsync().ConfigureAwait(false);
            }

            var collection = _context.Contacts as IQueryable<Contact>;

            if (!string.IsNullOrWhiteSpace(contactsResourceParameters.SearchQuery))
            {

                var searchQuery = contactsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.FirstName.Contains(searchQuery,StringComparison.OrdinalIgnoreCase)
                                                   || a.LastName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            return await collection.ToListAsync().ConfigureAwait(false);
        }

        public void UpdateContact(Contact contact)
        {
            // no code in this implementation
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync().ConfigureAwait(false) >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
               // dispose resources when needed
            }
        }
    }
}
