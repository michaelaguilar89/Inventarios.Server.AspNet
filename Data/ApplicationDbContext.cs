using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolutionPal.RazorPages.Models;

namespace Inventarios.Server.AspNet.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Store> stores { get; set; }
        public DbSet<Storage_History> storage_historys { get; set; }
        public DbSet<ProductionGeneral> productions { get; set; }
    }
}
