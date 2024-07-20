namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabasee1103 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_News", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_News", "SeoTitle", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_News", "SeoDescription", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_News", "SeoKeywords", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_News", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_News", "SeoDescription", c => c.String());
            AlterColumn("dbo.tb_News", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_News", "Alias", c => c.String());
        }
    }
}
