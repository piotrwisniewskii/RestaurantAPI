using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using System.Runtime.CompilerServices;

namespace RestaurantAPI.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasData(
                      new Restaurant()
                      {
                          Id = 1,
                          Name = "KFC",
                          Category = "Fast Food",
                          Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in America",
                          ContactEmail = "contact@kfc.com",
                          HasDelivery = true,
                          AddressId = 1
                      },
                     new Restaurant()
                     {
                         Id = 2,
                         Name = "McDonald Szewska",
                         Category = "Fast Food",
                         Description =
                        "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                         ContactEmail = "contact@mcdonald.com",
                         HasDelivery = true,
                         AddressId = 2

                     });

            modelBuilder.Entity<Address>().HasData(
                      new Address()
                      {
                          Id = 1,
                          City = "Kraków",
                          Street = "Szewska 2",
                          PostalCode = "30-001"

                      },
                     new Address()
                     {
                         Id = 2,
                         City = "Kraków",
                         Street = "Długa 5",
                         PostalCode = "30-001"
                     });

            modelBuilder.Entity<Dish>().HasData(
                      new Dish()
                      {
                          Id = 1,
                          Name = "Nashville Hot chicken",
                          Price = 10.30M,
                          RestaurantId = 1,
                      },
                     new Dish()
                     {
                         Id = 2,
                         Name = "Chicken Nuggets",
                         Price = 5.30M,
                         RestaurantId = 1,
                     });
        }
    }
}
