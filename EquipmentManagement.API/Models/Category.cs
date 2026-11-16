namespace EquipmentManagement.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Equipment> Equipments { get; set; } 
            = new List<Equipment>();
    }
}
