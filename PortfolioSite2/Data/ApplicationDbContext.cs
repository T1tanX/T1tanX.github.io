using Microsoft.EntityFrameworkCore;
using PortfolioSite2.Models;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<Blog> Blogs { get; set; }
}