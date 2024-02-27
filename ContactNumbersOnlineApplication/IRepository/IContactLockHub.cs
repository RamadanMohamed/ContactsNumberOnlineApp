namespace ContactNumbersOnlineApplication.IRepository
{
    public interface IContactLockHub
    {
      Task UnlockContact(int contactId);
       Task LockContact(int contactId);
    }
}
