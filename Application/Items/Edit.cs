using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Items
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string UPC { get; set; }
            public string Description { get; set; }
            public int? MinimumOrderQuantity { get; set; }

            public string PurchaseUnitMeasure { get; set; }
            public decimal? Cost { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UPC).NotEmpty().Length(12).Custom((x, context) =>
                {
                    if ((!(long.TryParse(x, out long value))))
                    {
                        context.AddFailure($"{x} is not a valid number");
                    }
                });
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.MinimumOrderQuantity).NotEmpty();
                RuleFor(x => x.PurchaseUnitMeasure).NotEmpty();
                RuleFor(x => x.Cost).NotEmpty();
               
            }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var item = await _context.Item.FindAsync(request.Id);
                if (item == null)
                    throw new Exception("Item not found");

                int i = request.MinimumOrderQuantity ?? 0;

                item.UPC = request.UPC ?? item.UPC;
                item.Description = request.Description ?? item.Description;
                if (request.MinimumOrderQuantity.HasValue)
                    item.MinimumOrderQuantity = request.MinimumOrderQuantity ?? item.MinimumOrderQuantity;
                item.PurchaseUnitMeasure = request.PurchaseUnitMeasure ?? item.PurchaseUnitMeasure;
                if (request.Cost.HasValue)
                    item.Cost = request.Cost ?? item.Cost;


                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving items");
            }
        }
    }
}