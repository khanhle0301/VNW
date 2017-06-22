namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TinTuyenDungs", "ViewCount", c => c.Int());
            DropColumn("dbo.TinTuyenDungs", "View");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TinTuyenDungs", "View", c => c.Int(nullable: false));
            DropColumn("dbo.TinTuyenDungs", "ViewCount");
        }
    }
}
