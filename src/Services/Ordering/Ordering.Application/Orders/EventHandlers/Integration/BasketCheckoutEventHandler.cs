
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEvent> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        //Create Order from BasketCheckoutEvent
        //Start Order fullfilling process
        logger.LogInformation("BasketCheckoutEvent Handled: {IntegrationEvent}", context.Message);
        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    public CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent basketCheckoutEvent)
    {
        var shippingAddress = new AddressDto(
            basketCheckoutEvent.FirstName,
            basketCheckoutEvent.LastName,
            basketCheckoutEvent.EmailAddress,
            basketCheckoutEvent.AddressLine,
            basketCheckoutEvent.Country,
            basketCheckoutEvent.State,
            basketCheckoutEvent.ZipCode);

        var billingAddress = new AddressDto(basketCheckoutEvent.FirstName,
            basketCheckoutEvent.LastName,
            basketCheckoutEvent.EmailAddress,
            basketCheckoutEvent.AddressLine,
            basketCheckoutEvent.Country,
            basketCheckoutEvent.State,
            basketCheckoutEvent.ZipCode);

        var payment = new PaymentDto(
            basketCheckoutEvent.CardName,
            basketCheckoutEvent.CardNumber,
            basketCheckoutEvent.Expiration,
            basketCheckoutEvent.CVV,
            basketCheckoutEvent.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderItems = basketCheckoutEvent.Items.Select(x => new OrderItemDto(orderId, x.ProductId, x.Quantity, x.Price)).ToList();

        var order = new OrderDto(
            orderId,
            basketCheckoutEvent.CustomerId,
            $"Order-{orderId}",
            shippingAddress,
            billingAddress,
            payment,
            OrderStatus.Pending,
            orderItems);

        return new CreateOrderCommand(order);
    }
}
