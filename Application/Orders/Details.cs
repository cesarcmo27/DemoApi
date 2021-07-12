using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders
{
    public class Details
    {
        public class Query : IRequest<Order>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Order>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Order> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Order.Include(x => x.OrderDetails).SingleOrDefaultAsync(x => x.Id == request.Id);
            }
        }
    
    }
}