using Microsoft.EntityFrameworkCore;


namespace ProjectWG.Models
{
    public class UsedVehicleContext:DbContext
    {
        public UsedVehicleContext(DbContextOptions option):base(option){ }


        public DbSet<Users> User {  get; set; }

        public DbSet<Vehicles> Vehicle { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
