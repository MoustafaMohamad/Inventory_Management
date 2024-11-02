using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Common.Products.Queries
{
    public record IsProductExists(int ID) : IRequest<bool>;

    public class IsProductExistsQueryHandeler : BaseRequestHandler<Product, IsProductExists, bool>
    {
        public IsProductExistsQueryHandeler(RequestParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<bool> Handle(IsProductExists request, CancellationToken cancellationToken)
        {
            var result = await _repository.FirstOrDefaultAsync(p => p.ID == request.ID);

            if (result is null )
            {
                return false;
            }

            return false;
        }
    }
}
