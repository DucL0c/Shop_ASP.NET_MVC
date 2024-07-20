namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesizeandcolor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifyedDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tb_Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifyedDate = c.DateTime(nullable: false),
                        ModifierBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.tb_Review", "CreatedBy", c => c.String());
            AddColumn("dbo.tb_Review", "ModifyedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Review", "ModifierBy", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Colors", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_Sizes", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_Sizes", new[] { "ProductId" });
            DropIndex("dbo.tb_Colors", new[] { "ProductId" });
            DropColumn("dbo.tb_Review", "ModifierBy");
            DropColumn("dbo.tb_Review", "ModifyedDate");
            DropColumn("dbo.tb_Review", "CreatedBy");
            DropTable("dbo.tb_Sizes");
            DropTable("dbo.tb_Colors");
        }
    }
}
