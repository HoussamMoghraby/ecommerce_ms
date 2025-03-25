using Grpc.Core;
using Discount.gRPC.Protos;
using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Discount.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Discount created for product {coupon.ProductName}");
        return coupon.Adapt<CouponModel>();
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Discount deleted for product {coupon.ProductName}");

        return new DeleteDiscountResponse() { Success = true };
    }
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon is null)
            return new CouponModel();
        logger.LogInformation($"Discount fetched for product {coupon.ProductName}");
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.Coupon.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Discount updated for product {coupon.ProductName}");
        return coupon.Adapt<CouponModel>();
    }
}
