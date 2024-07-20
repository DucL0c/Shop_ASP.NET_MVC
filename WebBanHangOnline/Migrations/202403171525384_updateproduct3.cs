namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproduct3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Product", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "Detail", c => c.String(maxLength: 250));
        }
    }
}
