﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Samy.Models;

[assembly: OwinStartupAttribute(typeof(Samy.Startup))]
namespace Samy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
  
            if (!roleManager.RoleExists("administrador"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "administrador";
                roleManager.Create(role);
              
                var user = new ApplicationUser();
                user.UserName = "jesusstivensuarez@gmail.com";
                user.Email = "jesusstivensuarez@gmail.com";

                string userPWD = "diosestodo00";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "administrador");
                }
            }

        }
    }
}
