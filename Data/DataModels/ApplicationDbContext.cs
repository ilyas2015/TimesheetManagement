using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.DataModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.DataModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<TsWeekTemplate> TsWeekTemplates { get; set; }
        public IDbSet<TsEntry> TsEntries { get; set; }
        public IDbSet<TsDayEntry> TsDayEntries { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}