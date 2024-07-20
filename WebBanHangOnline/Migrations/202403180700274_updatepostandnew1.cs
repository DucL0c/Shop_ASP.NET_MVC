namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepostandnew1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Posts", "Category_Id", "dbo.tb_Category");
            DropIndex("dbo.tb_Posts", new[] { "Category_Id" });
            RenameColumn(table: "dbo.tb_Posts", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.tb_Posts", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Posts", "CategoryId");
            AddForeignKey("dbo.tb_Posts", "CategoryId", "dbo.tb_Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Posts", "CategoryId", "dbo.tb_Category");
            DropIndex("dbo.tb_Posts", new[] { "CategoryId" });
            AlterColumn("dbo.tb_Posts", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.tb_Posts", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.tb_Posts", "Category_Id");
            AddForeignKey("dbo.tb_Posts", "Category_Id", "dbo.tb_Category", "Id");
        }
    }
}
