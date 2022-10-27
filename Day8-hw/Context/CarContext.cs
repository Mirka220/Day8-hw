using Day8_hw.Models;
using Microsoft.EntityFrameworkCore;

namespace Day8_hw.Context
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }
    }
}
