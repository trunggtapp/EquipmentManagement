namespace EquipmentManagement.API.Models
{
    public class Borrower
    {
        public int BorrowerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public ICollection<BorrowRecord> BorrowRecords { get; set; }
            = new List<BorrowRecord>();
    }
}
