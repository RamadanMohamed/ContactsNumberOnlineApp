namespace ContactNumbersOnlineApplication.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public bool? IsLocked { get; set; } = false; // For locking mechanism
  

    }
}
