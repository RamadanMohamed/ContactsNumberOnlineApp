using ContactNumbersOnlineApplication.Models;

namespace ContactNumbersOnlineApplication.IRepository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int? id);
        Task<Contact> AddAsync(Contact contact);
        Task<Contact> UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
        Task<PaginatedList<Contact>> GetPaginatedAsync(int pageNumber, int pageSize, string searchString);
        Task<bool> LockContactAsync(int contactId);
        Task<bool> UnlockContactAsync(int contactId);

    }

}
