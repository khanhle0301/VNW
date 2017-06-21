namespace VNW.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<VNW.Data.VNWDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VNW.Data.VNWDbContext context)
        {
            CreateUser(context);
        }

        private void CreateUser(VNWDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new VNWDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new VNWDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "khanhle0301.it@gmail.com",
                Email = "khanhle0301.it@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Lê Khanh"
            };
            manager.Create(user, "123654$");
        }
    }
}