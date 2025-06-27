namespace GG.Models
{
    public class ItemAvailability
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public bool IsAvailable { get; set; }
    }
}
