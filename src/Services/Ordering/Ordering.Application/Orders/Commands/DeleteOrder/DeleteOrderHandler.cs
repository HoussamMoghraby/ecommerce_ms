using System;
using BuildingBlocks.Exceptions;
using Ordering.Application.Data;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);
        Order? order = await dbContext.Orders.FindAsync(orderId, cancellationToken);
        if (order == null)
        {
            throw new NotFoundException($"Order with ID {orderId} not found.");
        }
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);
    }
}
