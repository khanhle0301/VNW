namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletIcon : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PhucLois", "Icon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhucLois", "Icon", c => c.String(maxLength: 256));
        }
    }
}
