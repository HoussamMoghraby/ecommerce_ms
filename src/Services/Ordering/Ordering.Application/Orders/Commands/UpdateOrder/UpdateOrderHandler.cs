
using BuildingBlocks.Exceptions;
using Ordering.Application.Data;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        Order? order = await dbContext.Orders.FindAsync(orderId, cancellationToken);
        if (order == null)
        {
            throw new NotFoundException($"Order with ID {orderId} not found.");
        }
        order.Update(
            orderName: OrderName.Of(command.Order.OrderName),
            shippingAddress: Address.Of(command.Order.ShippingAddress.FirstName, command.Order.ShippingAddress.LastName, command.Order.ShippingAddress.EmailAddress, command.Order.ShippingAddress.AddressLine, command.Order.ShippingAddress.Country, command.Order.ShippingAddress.State, command.Order.ShippingAddress.ZipCode),
            billingAddress: Address.Of(command.Order.BillingAddress.FirstName, command.Order.BillingAddress.LastName, command.Order.BillingAddress.EmailAddress, command.Order.BillingAddress.AddressLine, command.Order.BillingAddress.Country, command.Order.BillingAddress.State, command.Order.BillingAddress.ZipCode),
            payment: Payment.Of(command.Order.Payment.CardName, command.Order.Payment.CardNumber, command.Order.Payment.Expiration, command.Order.Payment.Cvv, command.Order.Payment.PaymentMethod),
            status: command.Order.Status
        );
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }
}
