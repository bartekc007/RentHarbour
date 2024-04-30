using MediatR;

namespace Basket.Application.Domains.Basket.UpdateBasket
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, UpdateBasketResult>
    {
        public async Task<UpdateBasketResult> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            return new UpdateBasketResult();
        }
    }
}
