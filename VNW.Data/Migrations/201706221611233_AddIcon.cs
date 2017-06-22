namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhucLois", "Icon", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhucLois", "Icon");
        }
    }
}
