namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproduct1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "isActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tb_Product", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "Detail", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "SeoDescription", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Product", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_Product", "SeoDescription", c => c.String());
            AlterColumn("dbo.tb_Product", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_Product", "Detail", c => c.String());
            AlterColumn("dbo.tb_Product", "Description", c => c.String());
            DropColumn("dbo.tb_Product", "isActive");
        }
    }
}
