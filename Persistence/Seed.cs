using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {


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
        }
    }
}