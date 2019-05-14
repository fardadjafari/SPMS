namespace Project_Recipient_Application.Migrations
{
    using Project_Recipient_Application.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_Recipient_Application.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "RecivedProject.Models.DatabaseContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Project_Recipient_Application.Models.DatabaseContext context)
        {
            DatabaseContext db = new DatabaseContext();
            string myhash = FormsAuthentication.HashPasswordForStoringInConfigFile("1378", "MD5");
            if (db.Roles.Count() == 0)
            {
                Role role = new Role()
                {
                    RoleName = "admin",
                    RoleTitle = "مدیر",
                };

                db.Roles.Add(role);
                db.SaveChanges();

                User user = new User()
                {
                    RoleId = role.Id,
                    Name = "firstUser",
                    Family = "User",
                    Activeate = true,
                    Username = "adminfirst",
                    Password = myhash,
                    fullpermision = true
                };
                db.Useres.Add(user);
                db.SaveChanges();
            }


            
        }
    }
}
