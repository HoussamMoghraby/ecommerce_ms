using Mapster;
using MassTransit;
using Microsoft.FeatureManagement;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order Created Event Handled: {OrderId}", domainEvent.order.Id);
        OrderDto orderCreatedIntegrationEvent = domainEvent.order.Adapt<OrderDto>();
        // Publish the OrderCreatedIntegrationEvent to the message broker
        if (featureManager.IsEnabledAsync("OrderFullfilment").Result)
        {
            logger.LogInformation("Publishing Order Created Integration Event: {OrderId}", orderCreatedIntegrationEvent.Id);
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
        else
        {
            logger.LogWarning("Feature flag for Order Created Integration Event is disabled.");
            return;
        }
    }
}
