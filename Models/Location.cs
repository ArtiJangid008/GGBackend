namespace GG.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int RestaurantBrandId { get; set; }
        public RestaurantBrand RestaurantBrand { get; set; }
        public ICollection<ItemAvailability> ItemAvailabilities { get; set; }
    }
}
