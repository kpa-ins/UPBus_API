using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UPBus_API.Entities;

namespace UPBus_API
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
           : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=UPBus;User Id=sa;Password=123;MultipleActiveResultSets=true;TrustServerCertificate=true;");
            }
        }


        public DbSet<Bus> Bus { get; set; }
        public DbSet<Gate> Gate { get; set; }
        public DbSet<IncomeType> IncomeType { get; set; }
        public DbSet<ExpenseType> ExpenseType { get; set; }
        public DbSet<TrackType> TrackType { get; set; }
        public DbSet<DailyGateExpense> DailyGateExpense { get; set; }
        public DbSet<DailyGateIncome> DailyGateIncome { get; set; }
        public DbSet<GasStation> GasStation { get; set; }

        public DbSet<DailyPlan> DailyPlan { get; set; }

    }
}
