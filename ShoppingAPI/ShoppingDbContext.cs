using System;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Models;

namespace ShoppingAPI
{
	public class ShoppingDbContext : DbContext
	{
		public ShoppingDbContext(DbContextOptions options): base (options)
		{
            this.Database.EnsureCreated();
        }

		public DbSet<User>? Users { get; set; }
		public DbSet<Product>? Products { get; set; }
		public DbSet<Brand>? Brands { get; set; }
		public DbSet<ProductBrand>? ProductBrands { get; set; }
		public DbSet<Role>? Roles { get; set; }
		public DbSet<SuperMarket>? SuperMarkets { get; set; }
		public DbSet<SuperMarketProductBrand>? SuperMarketProductBrands { get; set; }
		public DbSet<ShoppingList>? ShoppingLists { get; set; }
		public DbSet<ShoppingDetailList>? ShoppingDetailLists { get; set; }

    }
}

