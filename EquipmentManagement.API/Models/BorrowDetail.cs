namespace EquipmentManagement.API.Models
{
    public class BorrowDetail
    {
        public int BorrowDetailId { get; set; }
        public int BorrowRecordId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string ReturnCondition { get; set; } = string.Empty;

        public BorrowRecord BorrowRecord { get; set; } = null!;
        public Equipment Equipment { get; set; } = null!;
    }
}
