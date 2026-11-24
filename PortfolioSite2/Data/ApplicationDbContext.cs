using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PortfolioSite2.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser> {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<Blog> Blogs { get; set; }
}