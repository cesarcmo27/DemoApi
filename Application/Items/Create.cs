using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Items
{
    public class Create
    {
        public class Command : IRequest
        {
            public string UPC { get; set; }
            public string Description { get; set; }
            public int MinimumOrderQuantity { get; set; }

            public string PurchaseUnitMeasure { get; set; }
            public decimal Cost { get; set; }

            public Guid ItemVendorId { get; set; }
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
                RuleFor(x => x.ItemVendorId).NotEmpty();
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
                var item = new Item
                {
                    UPC = request.UPC,
                    Description = request.Description,
                    MinimumOrderQuantity = request.MinimumOrderQuantity,
                    PurchaseUnitMeasure = request.PurchaseUnitMeasure,
                    Cost = request.Cost,
                    ItemVendorId = request.ItemVendorId

                };
                _context.Item.Add(item);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");

            }
        }

    }
}