using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace PizzaSquare.Lib
{
    public partial class PizzaSquareContext : DbContext
    {
        public PizzaSquareContext()
        {
        }

        public PizzaSquareContext(DbContextOptions<PizzaSquareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cheeses> Cheeses { get; set; }
        public virtual DbSet<Crusts> Crusts { get; set; }
        public virtual DbSet<OrderPizzas> OrderPizzas { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pizzas> Pizzas { get; set; }
        public virtual DbSet<Sauces> Sauces { get; set; }
        public virtual DbSet<Sizes> Sizes { get; set; }
        public virtual DbSet<StorePizzas> StorePizzas { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Toppings> Toppings { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = configBuilder.Build();

                optionsBuilder = new DbContextOptionsBuilder<PizzaSquareContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaSquareDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cheeses>(entity =>
            {
                entity.ToTable("Cheeses", "PizzaSquare");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Cheeses__72E12F1B54A331AD")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Crusts>(entity =>
            {
                entity.ToTable("Crusts", "PizzaSquare");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Crusts__72E12F1BAFDD2D30")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<OrderPizzas>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.PizzaId })
                    .HasName("PK__Order_Pi__6372EBF734BAFE43");

                entity.ToTable("Order_Pizzas", "PizzaSquare");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPizzas)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order_Piz__order__6FE99F9F");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OrderPizzas)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order_Piz__pizza__70DDC3D8");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders", "PizzaSquare");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("total_price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Orders__store_id__6C190EBB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__user_id__6B24EA82");
            });

            modelBuilder.Entity<Pizzas>(entity =>
            {
                entity.ToTable("Pizzas", "PizzaSquare");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheeseId).HasColumnName("cheese_id");

                entity.Property(e => e.CrustId).HasColumnName("crust_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SauceId).HasColumnName("sauce_id");

                entity.Property(e => e.SizeId).HasColumnName("size_id");

                entity.Property(e => e.Topping1Id).HasColumnName("topping_1_id");

                entity.Property(e => e.Topping2Id).HasColumnName("topping_2_id");

                entity.HasOne(d => d.Cheese)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.CheeseId)
                    .HasConstraintName("FK__Pizzas__cheese_i__60A75C0F");

                entity.HasOne(d => d.Crust)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.CrustId)
                    .HasConstraintName("FK__Pizzas__crust_id__5FB337D6");

                entity.HasOne(d => d.Sauce)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.SauceId)
                    .HasConstraintName("FK__Pizzas__sauce_id__619B8048");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK__Pizzas__size_id__628FA481");

                entity.HasOne(d => d.Topping1)
                    .WithMany(p => p.PizzasTopping1)
                    .HasForeignKey(d => d.Topping1Id)
                    .HasConstraintName("FK__Pizzas__topping___6383C8BA");

                entity.HasOne(d => d.Topping2)
                    .WithMany(p => p.PizzasTopping2)
                    .HasForeignKey(d => d.Topping2Id)
                    .HasConstraintName("FK__Pizzas__topping___6477ECF3");
            });

            modelBuilder.Entity<Sauces>(entity =>
            {
                entity.ToTable("Sauces", "PizzaSquare");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Sauces__72E12F1BE39E25F4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Sizes>(entity =>
            {
                entity.ToTable("Sizes", "PizzaSquare");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Sizes__72E12F1B55CE2740")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<StorePizzas>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.PizzaId })
                    .HasName("PK__Store_Pi__87D92AD218F5F380");

                entity.ToTable("Store_Pizzas", "PizzaSquare");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.PizzaId).HasColumnName("pizza_id");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.StorePizzas)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Store_Piz__pizza__68487DD7");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StorePizzas)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Store_Piz__store__6754599E");
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.ToTable("Stores", "PizzaSquare");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Stores__72E12F1B411D3DCD")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Toppings>(entity =>
            {
                entity.ToTable("Toppings", "PizzaSquare");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Toppings__72E12F1B6F1C46E5")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "PizzaSquare");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__F3DBC57219731FFF")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
