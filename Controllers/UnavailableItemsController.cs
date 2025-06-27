using GG.Data;
using GG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/UnavailableItems/byBrandAndCity
        [HttpPost("byBrandAndCity")]
        public async Task<IActionResult> GetUnavailableItemsByBrandAndCity([FromBody] UnavailableItemsByBrandAndCityRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RestaurantBrand) || string.IsNullOrEmpty(request.City))
                return BadRequest("Invalid request.");

            var locations = await _context.Locations
                .Where(l => l.RestaurantBrand.Name == request.RestaurantBrand && l.City == request.City)
                .ToListAsync();

            if (!locations.Any())
                return NotFound("No locations found for the selected brand and city.");

            var unavailableItems = await _context.ItemAvailabilities
                .Where(ia => locations.Select(l => l.Id).Contains(ia.LocationId) && !ia.IsAvailable)
                .Include(ia => ia.Item)
                .Select(ia => new
                {
                    ia.Item.Name,
                    Reason = "Not available" 
                })
                .ToListAsync();

            return Ok(unavailableItems);
        }
    }
}
