namespace Project_Shoe_Stock.Migrations.ContextB
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_Shoe_Stock.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ContextB";
        }

        protected override void Seed(Project_Shoe_Stock.Models.ApplicationDbContext context)
        {
            var ustore = new UserStore<IdentityUser>(context);
            var rstore = new RoleStore<IdentityRole>(context);
            var umanager = new UserManager<IdentityUser>(ustore);
            var rmanager = new RoleManager<IdentityRole>(rstore);
            string[] roles = { "Admin", "Users" };
            foreach(var role in roles)
            {
                if(!context.Roles.Any(r=> r.Name == role))
                {
                    rmanager.Create(new IdentityRole(role));
                }
            }
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                
                
               var user = new IdentityUser { UserName = "Admin" };

               umanager.Create(user, "@Open1234");

                umanager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
