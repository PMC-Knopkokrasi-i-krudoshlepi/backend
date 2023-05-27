using DPOBackend.Models;
using DPOBackend.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace DPOBackend.Db;

public class TestDbContext: DbContext
{
    public DbSet<TestModel> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<UserModel> Users { get; set; }

    public DbSet<ImageModel> Images { get; set; }
     
    public TestDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=all.db");
    }
}