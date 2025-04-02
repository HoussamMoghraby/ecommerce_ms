namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
    private ProductId(Guid value) => Value = value;

    internal static ProductId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("ProductId cannot be null");
        }
        return new ProductId(value);
    }
}
