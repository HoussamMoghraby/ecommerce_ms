using System;
using BasketAPI.Dtos;
using BuildingBlocks.Messaging.Events;
using JasperFx.Core;
using MassTransit;

namespace BasketAPI.Basket.CheckoutBasket;


public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSucess);
public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto is required");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotNull().WithMessage("User name is required");
        RuleFor(x => x.BasketCheckoutDto.CustomerId).NotEmpty().WithMessage("Customer Id is required");
        RuleFor(x => x.BasketCheckoutDto.TotalPrice).GreaterThan(0).WithMessage("Total Price must be greater than 0");
    }
}
public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken);
        if (basket == null)
            return new CheckoutBasketResult(false);

        var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await basketRepository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);
        return new CheckoutBasketResult(true);
    }
}
