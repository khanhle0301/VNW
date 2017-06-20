namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NganhNghes", "Alias", c => c.String(maxLength: 256));
            AddColumn("dbo.Tinhs", "Alias", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tinhs", "Alias");
            DropColumn("dbo.NganhNghes", "Alias");
        }
    }
}
