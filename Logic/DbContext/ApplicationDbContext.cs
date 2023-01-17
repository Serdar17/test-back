using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Logic.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}