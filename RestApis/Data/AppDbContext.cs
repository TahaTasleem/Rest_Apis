using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestApis.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;

namespace RestApis.Data;
public class AppDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public AppDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }
    /*public DbQuery<User> User { get; set; }
    public DbQuery<Order> Order { get; set; }
    public DbQuery<Product> Product { get; set; }
    public DbQuery<Category> Category { get; set; }
    public DbQuery<Review> Review { get; set; }*/

    public DbSet<User> User { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Review> Review { get; set; }
}
