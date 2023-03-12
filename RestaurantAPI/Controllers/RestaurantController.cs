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
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(IRestaurantService restaurantService, ILogger<RestaurantController> logger)
        {
            _restaurantService = restaurantService;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {

            _logger.LogInformation($"all Restaurants were shown");
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation($"Restaurant with id: {id} DELETE action invoked");

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

        [HttpPut("{id}")]
        public ActionResult Update(UpdateRestaurantDto dto, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _restaurantService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
