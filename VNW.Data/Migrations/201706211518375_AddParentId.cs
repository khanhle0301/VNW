namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NganhNghes", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NganhNghes", "ParentId");
        }
    }
}
