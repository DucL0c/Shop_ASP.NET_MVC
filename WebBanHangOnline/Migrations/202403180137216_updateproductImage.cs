namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproductImage : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tb_ProductImage)", newName: "tb_ProductImage");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tb_ProductImage", newName: "tb_ProductImage)");
        }
    }
}
