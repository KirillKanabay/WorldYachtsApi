using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data.Entities;

namespace WorldYachts.Data
{
    public static class SeedData
    {
        public static void SeedBoats(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boat>().HasData(new[]
            {
                new Boat()
                {
                    Id = 1, Model = "RB Стандарт", TypeId = 1, NumberOfRowers = 1, Mast = false, Color = "Зеленый",
                    WoodId = 3, BasePrice = 60000, Vat = 18
                },
                new Boat()
                {
                    Id = 2, Model = "SB Стандарт", TypeId = 2, NumberOfRowers = 0, Mast = true, Color = "Белый",
                    WoodId = 3, BasePrice = 280000, Vat = 18
                },
                new Boat()
                {
                    Id = 3, Model = "SB Юниор", TypeId = 2, NumberOfRowers = 0, Mast = true, Color = "Красный",
                    WoodId = 4, BasePrice = 165000, Vat = 18
                },
                new Boat()
                {
                    Id = 4, Model = "G Эконом", TypeId = 3, NumberOfRowers = 6, Mast = false, Color = "Черный",
                    WoodId = 4, BasePrice = 550000, Vat = 18
                },
                new Boat()
                {
                    Id = 5, Model = "G Люкс", TypeId = 3, NumberOfRowers = 8, Mast = false, Color = "Синий", WoodId = 1,
                    BasePrice = 750000, Vat = 18
                },
                new Boat()
                {
                    Id = 6, Model = "G Супер Люкс", TypeId = 3, NumberOfRowers = 12, Mast = true, Color = "Коричневый",
                    WoodId = 1, BasePrice = 1080000, Vat = 18
                }
            });
        }
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new []
            {
                new User()
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    Role = "Admin",
                    Username = "admin",
                    Password = "admin",
                    UserId = 1
                },
                new User()
                {
                    Id = 2,
                    Email = "igor.avtoraskov@mail.ru",
                    Role = "Sales Person",
                    Username = "igor.avtoraskov",
                    Password = "123456",
                    UserId = 1
                },
                new User()
                {
                    Id = 3,
                    Email = "katya.ivanova@mail.ru",
                    Role = "Sales Person",
                    Username = "katya.ivanova",
                    Password = "123456",
                    UserId = 2
                },
                new User()
                {
                    Id = 4,
                    Email = "yana.sviridova@mail.ru",
                    Role = "Sales Person",
                    Username = "yana.sviridova",
                    Password = "123456",
                    UserId = 3
                },
                new User()
                {
                    Id = 5,
                    Email = "nina.packirova@mail.ru",
                    Role = "Sales Person",
                    Username = "nina.packirova@mail.ru",
                    Password = "123456" ,
                    UserId = 4
                },
                new User()
                {
                    Id = 6,
                    Email = "kirill.kanabay@gmail.com",
                    Role = "Sales Person",
                    Username = "sp",
                    Password = "123456",
                    UserId = 5
                },
            });
        }
        public static void SeedAdmins(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                FirstName = "Kirill",
                SecondName = "Kanabay"
            });
        }
        public static void SeedSalesPersons(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesPerson>().HasData(new[]
            {
                new SalesPerson() {Id = 1, FirstName = "Игорь", SecondName = "Авторасков"}, 
                new SalesPerson() {Id = 2, FirstName = "Екатерина", SecondName = "Иванова"}, 
                new SalesPerson() {Id = 3, FirstName = "Яна", SecondName = "Свиридова"}, 
                new SalesPerson() {Id = 4, FirstName = "Нина", SecondName = "Пацкирова"}, 
                new SalesPerson() {Id = 5, FirstName = "Кирилл", SecondName = "Канабай"}, 
            });
        }
        public static void SeedBoatWoods(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoatWood>().HasData(new[]{
                new BoatWood() {Id = 1, Wood = "Дуб"},
                new BoatWood() {Id = 2, Wood = "Береза"},
                new BoatWood() {Id = 3, Wood = "Ель"},
                new BoatWood() {Id = 4, Wood = "Сосна"},
                new BoatWood() {Id = 5, Wood = "Лиственница"}
            });
        }
        public static void SeedBoatTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoatType>().HasData(new[]{
                new BoatType() {Id = 1, Type = "Шлюпка"},
                new BoatType() {Id = 2, Type = "Парусная лодка"},
                new BoatType() {Id = 3, Type = "Галера"}});
        }
        public static void SeedPartners(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partner>().HasData(new[]
            {
                new Partner(){Id = 1, Name = "ООО\"Мемфис\"", Address = "Испания, Барселона", City = "Барселона"},
                new Partner(){Id = 2, Name = "ООО\"Рога и копыта\"", Address = "Санкт-петербург, Невский проспект, 41", City = "Санкт-Петербург"},
                new Partner(){Id = 3, Name = "ЗАО\"Онский сталелитейный завод\"", Address = "Онск, ул. Ленина, д 12", City = "Онск"},
                new Partner(){Id = 4, Name = "ООО\"Верфь\"", Address = "Санкт-Петербург, Северная верфь", City = "Санкт-Петербург"},
                new Partner(){Id = 5, Name = "ООО\"Призманти\"", Address = "Саратов, ул. Советская, д. 87", City = "Саратов"},
                new Partner(){Id = 6, Name = "ООО\"Кабель интрудшекн\"", Address = "г. Москва, Войковская ул., д. 13А", City = "Москва"},
                new Partner(){Id = 7, Name = "ООО\"Картова елице\"", Address = "г. Владивосток, ул. Карелии, д.2", City = "Владивосток"},
            });
        }
    }
}
