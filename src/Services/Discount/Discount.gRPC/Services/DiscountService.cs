using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services;
public class DiscountService
    (DiscountContext dbContext, ILogger<DiscountService> logger) 
    : DiscountProService.DiscountProServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon  = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon == null) 
            coupon = new Models.Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc"};

        logger.LogInformation("Discount is retrieve for product : {productName}, Amount: {amount}"
            , coupon.ProductName,coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid request"));

        dbContext.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
           .Coupons
           .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Not Found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Deleted is completed: {productName}"
            , coupon.ProductName);

        return new DeleteDiscountResponse{ Success = true };
    }
}

