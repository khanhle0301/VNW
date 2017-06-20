namespace VNW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CapBacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CongTyNganhNghes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CongTyId = c.Int(nullable: false),
                        NganhNgheId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CongTys", t => t.CongTyId, cascadeDelete: true)
                .ForeignKey("dbo.NganhNghes", t => t.NganhNgheId, cascadeDelete: true)
                .Index(t => t.CongTyId)
                .Index(t => t.NganhNgheId);
            
            CreateTable(
                "dbo.CongTys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        QuyMo = c.String(maxLength: 256),
                        DienThoai = c.String(maxLength: 256),
                        DiaChi = c.String(maxLength: 256),
                        ThongTin = c.String(),
                        Logo = c.String(maxLength: 256),
                        HinhAnh = c.String(storeType: "xml"),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NganhNghes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CongTyPhucLois",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CongTyId = c.Int(nullable: false),
                        PhucLoiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CongTys", t => t.CongTyId, cascadeDelete: true)
                .ForeignKey("dbo.PhucLois", t => t.PhucLoiId, cascadeDelete: true)
                .Index(t => t.CongTyId)
                .Index(t => t.PhucLoiId);
            
            CreateTable(
                "dbo.PhucLois",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KyNangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tinhs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TinTuyenDungKyNangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TinTuyenDungId = c.Int(nullable: false),
                        KyNangId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KyNangs", t => t.KyNangId, cascadeDelete: true)
                .ForeignKey("dbo.TinTuyenDungs", t => t.TinTuyenDungId, cascadeDelete: true)
                .Index(t => t.TinTuyenDungId)
                .Index(t => t.KyNangId);
            
            CreateTable(
                "dbo.TinTuyenDungs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChucDang = c.String(nullable: false, maxLength: 256),
                        CapBacId = c.Int(nullable: false),
                        TuLuong = c.Int(nullable: false),
                        DenLuong = c.Int(nullable: false),
                        HienThiLuong = c.Boolean(nullable: false),
                        MoTa = c.String(),
                        YeuCau = c.String(),
                        NguoiLienHe = c.String(maxLength: 256),
                        Email = c.String(maxLength: 256),
                        CongTyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CapBacs", t => t.CapBacId, cascadeDelete: true)
                .ForeignKey("dbo.CongTys", t => t.CongTyId, cascadeDelete: true)
                .Index(t => t.CapBacId)
                .Index(t => t.CongTyId);
            
            CreateTable(
                "dbo.TinTuyenDungNganhNghes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TinTuyenDungId = c.Int(nullable: false),
                        NganhNgheId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NganhNghes", t => t.NganhNgheId, cascadeDelete: true)
                .ForeignKey("dbo.TinTuyenDungs", t => t.TinTuyenDungId, cascadeDelete: true)
                .Index(t => t.TinTuyenDungId)
                .Index(t => t.NganhNgheId);
            
            CreateTable(
                "dbo.TinTuyenDungTinhs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TinTuyenDungId = c.Int(nullable: false),
                        TinhId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tinhs", t => t.TinhId, cascadeDelete: true)
                .ForeignKey("dbo.TinTuyenDungs", t => t.TinTuyenDungId, cascadeDelete: true)
                .Index(t => t.TinTuyenDungId)
                .Index(t => t.TinhId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TinTuyenDungTinhs", "TinTuyenDungId", "dbo.TinTuyenDungs");
            DropForeignKey("dbo.TinTuyenDungTinhs", "TinhId", "dbo.Tinhs");
            DropForeignKey("dbo.TinTuyenDungNganhNghes", "TinTuyenDungId", "dbo.TinTuyenDungs");
            DropForeignKey("dbo.TinTuyenDungNganhNghes", "NganhNgheId", "dbo.NganhNghes");
            DropForeignKey("dbo.TinTuyenDungKyNangs", "TinTuyenDungId", "dbo.TinTuyenDungs");
            DropForeignKey("dbo.TinTuyenDungs", "CongTyId", "dbo.CongTys");
            DropForeignKey("dbo.TinTuyenDungs", "CapBacId", "dbo.CapBacs");
            DropForeignKey("dbo.TinTuyenDungKyNangs", "KyNangId", "dbo.KyNangs");
            DropForeignKey("dbo.CongTyPhucLois", "PhucLoiId", "dbo.PhucLois");
            DropForeignKey("dbo.CongTyPhucLois", "CongTyId", "dbo.CongTys");
            DropForeignKey("dbo.CongTyNganhNghes", "NganhNgheId", "dbo.NganhNghes");
            DropForeignKey("dbo.CongTyNganhNghes", "CongTyId", "dbo.CongTys");
            DropIndex("dbo.TinTuyenDungTinhs", new[] { "TinhId" });
            DropIndex("dbo.TinTuyenDungTinhs", new[] { "TinTuyenDungId" });
            DropIndex("dbo.TinTuyenDungNganhNghes", new[] { "NganhNgheId" });
            DropIndex("dbo.TinTuyenDungNganhNghes", new[] { "TinTuyenDungId" });
            DropIndex("dbo.TinTuyenDungs", new[] { "CongTyId" });
            DropIndex("dbo.TinTuyenDungs", new[] { "CapBacId" });
            DropIndex("dbo.TinTuyenDungKyNangs", new[] { "KyNangId" });
            DropIndex("dbo.TinTuyenDungKyNangs", new[] { "TinTuyenDungId" });
            DropIndex("dbo.CongTyPhucLois", new[] { "PhucLoiId" });
            DropIndex("dbo.CongTyPhucLois", new[] { "CongTyId" });
            DropIndex("dbo.CongTyNganhNghes", new[] { "NganhNgheId" });
            DropIndex("dbo.CongTyNganhNghes", new[] { "CongTyId" });
            DropTable("dbo.TinTuyenDungTinhs");
            DropTable("dbo.TinTuyenDungNganhNghes");
            DropTable("dbo.TinTuyenDungs");
            DropTable("dbo.TinTuyenDungKyNangs");
            DropTable("dbo.Tinhs");
            DropTable("dbo.KyNangs");
            DropTable("dbo.PhucLois");
            DropTable("dbo.CongTyPhucLois");
            DropTable("dbo.NganhNghes");
            DropTable("dbo.CongTys");
            DropTable("dbo.CongTyNganhNghes");
            DropTable("dbo.CapBacs");
        }
    }
}
