using Inventory_Management.Common.Repositories;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Common
{
    public abstract class BaseRequestHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TEntity : BaseModel
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        protected readonly IRepository<TEntity> _repository;
        public BaseRequestHandler(RequestParameters<TEntity> requestParameters)
        {
            _mediator = requestParameters.Mediator;
            _userState = requestParameters.UserState;
            _repository = requestParameters.Repository;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
