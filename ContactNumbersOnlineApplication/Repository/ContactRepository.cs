using ContactNumbersOnlineApplication.IRepository;
using ContactNumbersOnlineApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactNumbersOnlineApplication.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int? id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task DeleteAsync(int id)
        {
            var contactToDelete = await _context.Contacts.FindAsync(id);
            if (contactToDelete != null)
            {
                _context.Contacts.Remove(contactToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<PaginatedList<Contact>> GetPaginatedAsync(int pageNumber, int pageSize, string searchString)
        {
            IQueryable<Contact> source = _context.Contacts.AsNoTracking(); // Use AsNoTracking for read-only operations

            if (!string.IsNullOrEmpty(searchString))
            {
                source = source.Where(c => c.Name.Contains(searchString));
            }

            return await PaginatedList<Contact>.CreateAsync(source, pageNumber, pageSize);
        }
        public async Task<bool> LockContactAsync(int contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);
            if (contact != null && !contact.IsLocked.Value)
            {
                contact.IsLocked = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UnlockContactAsync(int contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);
            if (contact != null && contact.IsLocked.Value)
            {
                contact.IsLocked = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }

}
