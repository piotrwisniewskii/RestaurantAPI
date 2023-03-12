using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Controllers;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDbContext context, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _context.Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant no found");
            }

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            _logger.LogInformation($"all Restaurants were shown");

            var restaurants = _context.Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDtos;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Restaurant with id: {id} DELETE action invoked");
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant no found");
            }

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }

        public void Update(int id,UpdateRestaurantDto dto)
        {
            var restaurant = _context
               .Restaurants
               .FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant no found");
            }


            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _context.SaveChanges();
        }
    }
}
