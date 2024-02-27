using ContactNumbersOnlineApplication.IRepository;
using Microsoft.AspNetCore.SignalR;

namespace ContactNumbersOnlineApplication.SignalR
{
    public class ContactLockHub : Hub, IContactLockHub
    {
        private readonly IContactRepository _contactRepository;

        public ContactLockHub(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task LockContact(int contactId)
        {
            var success = await _contactRepository.LockContactAsync(contactId);
            if (success)
            {
                await Clients.All.SendAsync("ContactLocked", contactId);
            }
        }

        public async Task UnlockContact(int contactId)
        {
            var success = await _contactRepository.UnlockContactAsync(contactId);
            if (success)
            {
                await Clients.All.SendAsync("ContactUnlocked", contactId);
            }
        }
    }



}
