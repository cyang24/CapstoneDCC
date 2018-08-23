using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Capstone_HairSalon.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Stylist> Stylists { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ServiceTotal> ServiceTotals { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<RentFee> RentFees { get; set; }
        public DbSet<RentPayment> RentPayments { get; set; }
        public DbSet<RentTransaction> RentTransactions { get; set; }
        public virtual DbSet<Event> Events { get; set; }
    }
}