namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditChucDanh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TinTuyenDungs", "ChucDanh", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.TinTuyenDungs", "ChucDang");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TinTuyenDungs", "ChucDang", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.TinTuyenDungs", "ChucDanh");
        }
    }
}
