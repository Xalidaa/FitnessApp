using FitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.DAL.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
    }
}
