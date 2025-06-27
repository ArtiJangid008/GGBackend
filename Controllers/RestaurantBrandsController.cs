using GG.Data;
using GG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantBrandsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantBrandsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            return await _context.RestaurantBrands
                .Select(b => b.Name)
                .ToListAsync();
        }

        // GET: api/RestaurantBrands/{brandName}/Cities
        [HttpGet("{brandName}/Cities")]
        public async Task<ActionResult<IEnumerable<string>>> GetCitiesForBrand(string brandName)
        {
            var brand = await _context.RestaurantBrands
                .Include(b => b.Locations)
                .FirstOrDefaultAsync(b => b.Name == brandName);

            if (brand == null)
                return NotFound();

            return brand.Locations
                .Select(l => l.Name) 
                .Distinct()
                .ToList();
        }

    }
}
