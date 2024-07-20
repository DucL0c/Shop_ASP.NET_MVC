namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorder1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Email", c => c.String());
            AddColumn("dbo.tb_Order", "TypePayment", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "CustomerId", c => c.String());
            AddColumn("dbo.tb_Order", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "Status");
            DropColumn("dbo.tb_Order", "CustomerId");
            DropColumn("dbo.tb_Order", "TypePayment");
            DropColumn("dbo.tb_Order", "Email");
        }
    }
}
