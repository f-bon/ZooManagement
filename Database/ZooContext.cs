using Microsoft.EntityFrameworkCore;
using ZooManagement.Models;

namespace ZooManagement.Database;

public class ZooContext: DbContext{

    public DbSet<Species> Species {get;set;}
    public DbSet<Animal> Animal{get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source=ZooDb.db");
        // optionsBuilder.UseSqlite("Filename=MyDatabase.db");
}