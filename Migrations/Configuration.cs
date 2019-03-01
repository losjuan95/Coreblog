namespace coreblog.Migrations
{
    using coreblog.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    
    internal sealed class Configuration : DbMigrationsConfiguration<coreblog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(coreblog.Models.ApplicationDbContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }


            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            //create as few users in the project
            var userManager = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u=> u.Email== "juancarloscorea95@gmail.com"))
            {
                
                


                userManager.Create(new ApplicationUser
                {
                    UserName = "juancarloscorea95@gmail.com",
                    Email = "juancarloscorea95@gmail.com",
                    FirstName = "Juan Carlos",
                    LastName ="Corea",
                    DisplayName = "Jcorea1" 

                }, "Hollirose95");
            }
            if (!context.Users.Any(u=> u.Email=="JTwichell@mailinator.com"))
            {
                


                userManager.Create(new ApplicationUser
                {
                    UserName = "JTwichell@mailinator.com",
                    Email = "JTwichell@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Jtwich"

                }, "PassWord1");
            }
           





            //associate a user with a role
            var userId = userManager.FindByEmail("juancarloscorea95@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

             userId = userManager.FindByEmail("JTwichell@Mailinator.com").Id;
            userManager.AddToRole(userId, "Moderator");
        }
    //i remember this stuff now , 

    }
}
