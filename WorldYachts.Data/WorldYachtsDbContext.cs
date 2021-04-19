﻿using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data.Entities;

namespace WorldYachts.Data
{
    public class WorldYachtsDbContext:DbContext
    {
        #region Таблицы сущностей
        /// <summary>
        /// Таблица партнеров
        /// </summary>
        public DbSet<Partner> Partners { get; set; }
        /// <summary>
        /// Таблица пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Таблица покупателей
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
        /// <summary>
        /// Таблица менеджеров
        /// </summary>
        public DbSet<SalesPerson> SalesPersons { get; set; }
        /// <summary>
        /// Таблица администраторов
        /// </summary>
        public DbSet<Admin> Admins { get; set; }
        /// <summary>
        /// Таблица лодок
        /// </summary>
        public DbSet<Boat> Boats { get; set; }
        /// <summary>
        /// Таблица типов лодок
        /// </summary>
        public DbSet<BoatType> BoatTypes { get; set; }
        /// <summary>
        /// Таблица типов дерева для лодок
        /// </summary>
        public DbSet<BoatWood> BoatWoods { get; set; }
        /// <summary>
        /// Таблица аксессуаров
        /// </summary>
        public DbSet<Accessory> Accessories { get; set; }
        /// <summary>
        /// Таблица совместимости аксессуаров и лодок
        /// </summary>
        public DbSet<AccessoryToBoat> AccessoryToBoats { get; set; }

        /// <summary>
        /// Таблица заказов
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        
        /// <summary>
        /// Таблица деталей заказа
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion

        public WorldYachtsDbContext(DbContextOptions<WorldYachtsDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Связи

            #region AccessoryToBoat

            modelBuilder.Entity<AccessoryToBoat>()
                .HasOne(a => a.Accessory)
                .WithMany(t => t.AccessoryToBoat)
                .HasForeignKey(m => m.AccessoryId);

            #endregion

            #region OrderDetail

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Accessory)
                .WithMany(a => a.OrderDetails)
                .HasForeignKey(od => od.AccessoryId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            #endregion

            #region Order

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Boat)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BoatId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.SalesPerson)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.SalesPersonId);
            #endregion

            #endregion


            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                FirstName = "Kirill",
                SecondName = "Kanabay"
            });
            
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin@gmail.com", 
                Role = "Admin", 
                Username = "admin", 
                Password = "admin",
                UserId = 1
            });
            
            modelBuilder.Entity<BoatWood>().HasData(new []{
                new BoatWood() {Id = 1, Wood = "Ель"},
                new BoatWood() {Id = 2, Wood = "Береза"},
                new BoatWood() {Id = 3, Wood = "Сосна"},
                new BoatWood() {Id = 4, Wood = "Дуб"}

            });

            modelBuilder.Entity<BoatType>().HasData(new []{
                new BoatType() {Id = 1, Type = "Шлюпка"},
                new BoatType() {Id = 2, Type = "Парусная лодка"},
                new BoatType() {Id = 3, Type = "Галера"}});

            modelBuilder.Entity<Partner>().HasData(new[]
            {
                new Partner(){Id = 1, Name = "ООО\"Мемфис\"", Address = "Испания, Барселона", City = "Барселона"}, 
            });
        }
    }
}
