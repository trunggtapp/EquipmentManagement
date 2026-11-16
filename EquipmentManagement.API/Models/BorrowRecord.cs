namespace EquipmentManagement.API.Models
{
    public class BorrowRecord
    {
        public int BorrowRecordId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string? Notes { get; set; }

        public Borrower Borrower { get; set; } = null!;
        public ICollection<BorrowDetail> BorrowDetails { get; set; }
            = new List<BorrowDetail>();
    }
}
