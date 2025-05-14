using System;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtos(this IEnumerable<Order> orders)
    {
        return orders.Select(o => new OrderDto(

            Id: o.Id.Value,
            CustomerId: o.CustomerId.Value,
            OrderName: o.OrderName.Value,
            ShippingAddress: new AddressDto
            (
                FirstName: o.ShippingAddress.FirstName,
                LastName: o.ShippingAddress.LastName,
                EmailAddress: o.ShippingAddress.EmailAddress!,
                AddressLine: o.ShippingAddress.AddressLine,
                Country: o.ShippingAddress.Country,
                State: o.ShippingAddress.State,
                ZipCode: o.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDto
            (
                FirstName: o.BillingAddress.FirstName,
                LastName: o.BillingAddress.LastName,
                EmailAddress: o.BillingAddress.EmailAddress!,
                AddressLine: o.BillingAddress.AddressLine,
                Country: o.BillingAddress.Country,
                State: o.BillingAddress.State,
                ZipCode: o.BillingAddress.ZipCode
            ),
            Payment: new PaymentDto
            (
                CardName: o.Payment.CardName,
                CardNumber: o.Payment.CardNumber,
                Expiration: o.Payment.Expiration,
                Cvv: o.Payment.CVV,
                PaymentMethod: o.Payment.PaymentMethod
            ),
            Status: o.Status,
            OrderItems: o.OrderItems.Select(oi => new OrderItemDto
            (
                OrderId: oi.OrderId.Value,
                ProductId: oi.ProductId.Value,
                Quantity: oi.Quantity,
                Price: oi.Price
            )).ToList()
        )).ToList();
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
                    Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
    }
}
