namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlias1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TinTuyenDungs", "Alias", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TinTuyenDungs", "Alias");
        }
    }
}
