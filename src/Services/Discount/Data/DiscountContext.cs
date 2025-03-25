using System;
using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon(1, "iPhone 16", "Iphone 16", 1000),
            new Coupon(2, "Samsung S4", "Samsung S4", 800)
        );
    }
}
