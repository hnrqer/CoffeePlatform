using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User_Proj> User_Projs { get; set; }
        public DbSet<SharedProject> SharedProjects { get; set; }
        public DbSet<TutorialAccess> TutorialAcess { get; set; }

    }
}

