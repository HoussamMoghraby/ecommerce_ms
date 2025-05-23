using System;

namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToListAsync(cancellationToken);
        if (!products.Any())
        {
            throw new ProductNotFoundException(query.Category);
        }
        return new GetProductByCategoryResult(products);
    }
}
