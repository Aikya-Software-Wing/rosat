using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RoSAT.Models;

[assembly: OwinStartupAttribute(typeof(RoSAT.Startup))]
namespace RoSAT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if(!roleManager.RoleExists("admin"))
            {
                var role = new IdentityRole();
                role.Name = "admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@rnsit.com";
                string password = "P@ssw0rd";

                var user1 = userManager.Create(user, password);
                if(user1.Succeeded)
                {
                    userManager.AddToRole(user.Id, "admin");
                }
            }

            if(!roleManager.RoleExists("faculty"))
            {
                var role = new IdentityRole();
                role.Name = "faculty";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("student"))
            {
                var role = new IdentityRole();
                role.Name = "student";
                roleManager.Create(role);
            }

        }
    }
}
