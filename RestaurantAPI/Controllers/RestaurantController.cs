using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _context;
        public RestaurantController(RestaurantDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = _context.Restaurants.ToList();
            return Ok(restaurants);
        }


        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == id);

            if(restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
    }
}
