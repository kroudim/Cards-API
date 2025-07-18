using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
  {
  public class CardContext : DbContext
    {

    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public CardContext(DbContextOptions<CardContext> options)
            : base(options)
      {

      }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
      modelBuilder.Entity<Card>()
       .HasData(
         new Card("Card 1", "Description 1", "Color 1",  "ToDo")
               {
               Id = 1,
               UserId = 1,
               },
         new Card("Card 2", "Description 2", "Color 2", "ToDo")
               {
               Id = 2,
               UserId = 2,
           },
         new Card("Card 3", "Description 3", "Color 3",  "ToDo")
               {
               Id = 3,
               UserId = 3,
           });

      modelBuilder.Entity<User>()
       .HasData(
         new User("user1@email.com", "password 1", "Admin" )
                   {
                   Id = 1,
                   },
         new User("user2@email.com", "password 2", "Member")
                   {
                   Id = 2,
                   },
         new User("user3@email.com", "password 3", "Member")
                   {
                   Id = 3,
                   });


      base.OnModelCreating(modelBuilder);
      }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlite("connectionstring");
    //    base.OnConfiguring(optionsBuilder);
    //}
    }
  }
