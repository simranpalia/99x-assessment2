using System.Collections.Generic;
using _99xAssessment2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_99xAssessment2.Startup))]
namespace _99xAssessment2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesIfNotExists();
        }
        
        private void CreateRolesIfNotExists()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var rolesToBeAvailable = new List<string>()
            {
                Utils.Constants.RoleAdmin,
                Utils.Constants.RoleSuperUser
            };

            foreach (var role in rolesToBeAvailable)
            {
                if (!roleManager.RoleExists(role))
                    roleManager.Create(new IdentityRole(role));
            }
        }
    }
}
