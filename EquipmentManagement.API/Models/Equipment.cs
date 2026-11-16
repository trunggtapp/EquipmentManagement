namespace EquipmentManagement.API.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentCode { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Status { get; set; } = "Available";
        public string? Note { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<BorrowDetail> BorrowDetails { get; set; }
        = new List<BorrowDetail>();

    }
}
