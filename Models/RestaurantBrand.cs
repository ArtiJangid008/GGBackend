namespace GG.Models
{
    public class RestaurantBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
