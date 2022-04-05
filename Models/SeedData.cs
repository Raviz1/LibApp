using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.MembershipTypes.Any())
                    SeedMembershipTypes(context);

                if (!context.Roles.Any())
                    SeedRoles(context);

                if (!context.Customers.Any())
                     SeedCustomers(userManager);

                if (!context.Genre.Any())
                    SeedGenres(context);

                if (!context.Books.Any())
                    SeedBooks(context);

                context.SaveChanges();
            }
        }

        private static void SeedBooks(ApplicationDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    GenreId = 1,
                    Name = "Fault in our stars",
                    AuthorName = "John Green",
                    ReleaseDate = DateTime.Parse("16/05/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 2,
                    Name = "The Hunger Games",
                    AuthorName = "Suzanne Collins",
                    ReleaseDate = DateTime.Parse("16/05/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 3,
                    Name = "Book3",
                    AuthorName = "Andy Weir",
                    ReleaseDate = DateTime.Parse("16/05/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 5
                },
                new Book
                {
                    GenreId = 4,
                    Name = "Book4",
                    AuthorName = "Suzanne Collins",
                    ReleaseDate = DateTime.Parse("16/05/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 7
                },
                new Book
                {
                    GenreId = 5,
                    Name = "Book5",
                    AuthorName = "Andy Weir",
                    ReleaseDate = DateTime.Parse("16/05/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 2
                },
                new Book
                {
                    GenreId = 6,
                    Name = "Book6",
                    AuthorName = "Andy Weir",
                    ReleaseDate = DateTime.Parse("16/05/2014"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 4
                }
          
            );
        }

        private static void SeedGenres(ApplicationDbContext context)
        {
            context.Genre.AddRange(
                new Genre
                {
                    Id = 1,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Biography"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Criminal"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Sci-Fi"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Thriller"
                },
                new Genre
                {
                    Id = 7,
                    Name = "Mystery"
                },
                new Genre
                {
                    Id = 8,
                    Name = "Horror"
                }
            );
        }

        private static void SeedCustomers(UserManager<Customer> userManager)
        {
            var hasher = new PasswordHasher<Customer>();

            var customer1 = new Customer
            {
                Name = "Adrian Scibisz",
                Email = "adrian.scibisz@gmail.com",
                NormalizedEmail = "adrian.scibisz@gmail.com",
                UserName = "adrian.scibisz@gmail.com",
                NormalizedUserName = "adrian.scibisz@gmail.com",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test123")
            };

  
            userManager.CreateAsync(customer1).Wait();
            userManager.AddToRoleAsync(customer1, "user").Wait();

            var customer2 = new Customer
            {
                Name = "Johan New",
                Email = "johan.new@gmail.com",
                NormalizedEmail = "johan.new@gmail.com",
                UserName = "johan.new@gmail.com",
                NormalizedUserName = "johan.new@gmail.com",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test123")
            };


            userManager.CreateAsync(customer2).Wait();
            userManager.AddToRoleAsync(customer2, "storemanager").Wait();

            var customer3 = new Customer
            {
                Name = "Jayson Born",
                Email = "jayson.born@gmail.com",
                NormalizedEmail = "jayson.born@gmail.com",
                UserName = "joe.snoe@gmail.comjayson.born@gmail.com",
                NormalizedUserName = "jayson.born@gmail.com",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test123")
            };


            userManager.CreateAsync(customer3).Wait();
            userManager.AddToRoleAsync(customer3, "owner").Wait();
        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "user"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "StoreManager",
                    NormalizedName = "storemanager"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Owner",
                    NormalizedName = "owner"
                }
            );

            context.SaveChanges();
        }

        private static void SeedMembershipTypes(ApplicationDbContext context)
        {
            context.MembershipTypes.AddRange(
                new MembershipType
                {
                    Id = 1,
                    Name = "Pay as You Go",
                    SignUpFee = 0,
                    DurationInMonths = 0,
                    DiscountRate = 0
                },
                new MembershipType
                {
                    Id = 2,
                    Name = "Monthly",
                    SignUpFee = 30,
                    DurationInMonths = 1,
                    DiscountRate = 10
                },
                new MembershipType
                {
                    Id = 3,
                    Name = "Quaterly",
                    SignUpFee = 90,
                    DurationInMonths = 3,
                    DiscountRate = 15
                },
                new MembershipType
                {
                    Id = 4,
                    Name = "Yearly",
                    SignUpFee = 300,
                    DurationInMonths = 12,
                    DiscountRate = 20
                }
            );

            context.SaveChanges();
        }
    }
}