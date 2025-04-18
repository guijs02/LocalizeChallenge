using Domain.Entites;
using LocalizeApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalizeApi.Context
{
    public class LocalizeDbContext(DbContextOptions<LocalizeDbContext> options)
                                    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Company> Module { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
