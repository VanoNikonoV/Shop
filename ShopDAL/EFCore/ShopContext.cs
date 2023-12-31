﻿using Microsoft.EntityFrameworkCore;
using ShopDAL.Models;

namespace ShopDAL.EFCore
{
    public class ShopContext : DbContext
    {
        public ShopContext() { }

        public ShopContext(DbContextOptions options):base(options) { }

        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=shop; Trusted_Connection=True;",
                providerOptions => { providerOptions.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null); });
        }

        /// <summary>
        /// Насторойка для моделирования сущностей в EFCore
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Имя таблицы в БД
            modelBuilder.Entity<Customer>()
                .ToTable("customers");

            //Имена столбцов в БД и максимальная длина
            modelBuilder.Entity<Customer>(bd =>
            {
                bd.Property(i => i.Id).HasColumnName("id_customer");
                bd.Property(i => i.FirstName).HasColumnName("first_name").HasMaxLength(50);
                bd.Property(i => i.MiddleName).HasColumnName("middle_name").HasMaxLength(50);
                bd.Property(i => i.LastName).HasColumnName("last_name").HasMaxLength(50);
                bd.Property(i => i.Telefon).HasColumnName("telefon").HasMaxLength(12);
                bd.Property(i => i.E_mail).HasColumnName("e_mail").HasMaxLength(50);
            });
            //Индекс по свойству E-mail
            modelBuilder.Entity<Customer>()
                .HasIndex(e => e.E_mail)
                .IsUnique();

            //Имя таблицы в БД
            modelBuilder.Entity<Order>()
               .ToTable("orders");

            //Имена столбцов в БД и максимальная длина
            modelBuilder.Entity<Order>(bd =>
            {
                bd.Property(i => i.Id).HasColumnName("id_product");
                bd.Property(i => i.CustomerE_mail).HasColumnName("e_mail").HasMaxLength(50);
                bd.Property(i => i.ProductCode).HasColumnName("product_code").HasMaxLength(50);
                bd.Property(i => i.ProductName).HasColumnName("name_product").HasMaxLength(50);
            });

            //установка ограничения внешнего ключа
            modelBuilder.Entity<Order>()
                .HasOne(u => u.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(u => u.CustomerE_mail)
                .HasPrincipalKey(c => c.E_mail)
                .IsRequired();

        }
    }
}
