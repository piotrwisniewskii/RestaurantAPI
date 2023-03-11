﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _context.Restaurants
                .Include(r=>r.Address)
                .Include(r=>r.Dishes)
                .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return Ok(restaurantsDtos);
        }

        [HttpPost]
        public ActionResult CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();

            return Created($"/api/restaurant/{restaurant.Id}",null);
        }


        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get(int id)
        {
            var restaurant = _context.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return Ok(restaurantDto);
        }
    }
}
