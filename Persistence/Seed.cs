using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any())
            {
                 var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Cesar Ceron",
                        UserName = "cesar",
                        Email = "cesar.ceron@gmail.com"
                    },
                    new AppUser
                    {
                     
                        DisplayName = "Fran Moya",
                        UserName = "fran",
                        Email = "fran@test.com"
                    }
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");//must be strong password
                }
            }

            if (!context.Order.Any())
            {
                var orderList = new List<Order>()
                {
                    new Order{
                        OrderDate = DateTime.Now,
                        ClientId = 1,
                        OrderDetails = new List<OrderDetail>(){
                            new OrderDetail(){
                                IdProducto = 1,
                                Count = 1,
                                Amount = 10.0m

                            }
                        }
                    },

                    new Order{
                        OrderDate = DateTime.Now,
                        ClientId = 2,
                        OrderDetails = new List<OrderDetail>(){
                            new OrderDetail(){
                                IdProducto = 2,
                                Count = 3,
                                Amount = 45.0m

                            },
                            new OrderDetail(){
                                IdProducto = 3,
                                Count = 1,
                                Amount = 15.1m

                            }
                        }
                    }
                };

                await context.Order.AddRangeAsync(orderList);
                await context.SaveChangesAsync();


            }
            if (!context.ItemVendor.Any())
            {

                var hospital = new Hospital
                {
                    Name = "main hospital",
                    Address = "av 123 st avenue",
                    Pharmacies = new List<Pharmacy>(){
                        new Pharmacy
                         {
                            Name = "my pharmacy 1",
                            Address = "another address near my place"
                        }
                    }
                };


                var vendor = new ItemVendor
                {
                    Name = "Cesar Enterprises",
                    Address = "av 345 st avenue",
                    Items = new List<Item>()
                     {
                        new Item{

                                UPC = "upc 1",
                                Description = "item 1",
                                MinimumOrderQuantity = 5,
                                PurchaseUnitMeasure = "Tablets",
                                Cost = 34.56m
                        },
                        new Item{
                            UPC = "upc 2",
                                Description = "item 2",
                                MinimumOrderQuantity = 1,
                                PurchaseUnitMeasure = "Capsule",
                                Cost = 1.5m
                        }
                      }
                };



                await context.Hospital.AddAsync(hospital);

                await context.ItemVendor.AddAsync(vendor);

                await context.SaveChangesAsync();
            }
        }
    }
}