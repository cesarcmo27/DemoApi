using System.Collections.Generic;
using System.Threading;

using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders
{
    public class List
    {
        public class Query : IRequest<List<Order>> { }

        public class Handler : IRequestHandler<Query, List<Order>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Order>> Handle(Query request, CancellationToken cancellationToken)
            {
              // string a = null;
               //int b =  a.Length;
                return await _context.Order.Include(x => x.OrderDetails).ToListAsync();
            }
        }
    }
}