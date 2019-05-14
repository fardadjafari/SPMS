using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Recipient_Application.Models
{
    public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> students { get; set; }

        public DbSet<Project> projects { get; set; }

        public DbSet<User> Useres { get; set; }


    }
}