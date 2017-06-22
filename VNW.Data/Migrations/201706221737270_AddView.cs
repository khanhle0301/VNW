namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TinTuyenDungs", "View", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TinTuyenDungs", "View");
        }
    }
}
