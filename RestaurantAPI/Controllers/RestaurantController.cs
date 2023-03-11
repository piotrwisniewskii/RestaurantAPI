using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isDeleted = _restaurantService.Delete(id);

            if (isDeleted)
            {
            return NoContent();
            }

            return NotFound();

        }

        [HttpPost]
        public ActionResult CreateRestaurant(CreateRestaurantDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}",null);
        }


        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get(int id)
        {
            var restauarant = _restaurantService.GetById(id);

            if (restauarant is null)
            {
                return NotFound();
            }

            return Ok(restauarant);
        }
    }
}
