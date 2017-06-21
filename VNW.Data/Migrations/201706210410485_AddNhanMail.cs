namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNhanMail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "NhanMail", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "NhanMail");
        }
    }
}
