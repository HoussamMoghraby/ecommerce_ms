namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 100;
    public string Value { get; } = default!;
    private OrderName(string value) => Value = value;
    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, DefaultLength);
        return new OrderName(value);
    }
}
