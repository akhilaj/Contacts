using ContactLibrary.API.Entities;
using ContactLibrary.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactLibrary.API.Services
{
    public interface IContactLibraryRepository
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> GetContactAsync(Guid contactId);
        Task<IEnumerable<Contact>> GetContactsAsync(IEnumerable<Guid> contactIds);

        Task<IEnumerable<Contact>> GetContactsAsync(ContactsResourceParameters contactsResourceParameters);
        void AddContact(Contact contact);
        void DeleteContact(Contact contact);
        void UpdateContact(Contact contact);
        Task<bool> ContactExistsAsync(Guid contactId);
        Task<bool> SaveAsync();
    }
}
