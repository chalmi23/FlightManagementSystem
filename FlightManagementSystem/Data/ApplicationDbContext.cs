using Microsoft.EntityFrameworkCore;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Flight> Flights { get; set; }
    }
}
