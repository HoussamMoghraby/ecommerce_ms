using Mapster;
using MassTransit;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order Created Event Handled: {OrderId}", domainEvent.order.Id);
        OrderDto orderCreatedIntegrationEvent = domainEvent.order.Adapt<OrderDto>();
        // Publish the OrderCreatedIntegrationEvent to the message broker
        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
