#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace LoginRegister.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}