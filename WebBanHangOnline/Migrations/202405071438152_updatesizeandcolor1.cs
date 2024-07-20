namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesizeandcolor1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Colors", "CreatedBy");
            DropColumn("dbo.tb_Colors", "CreatedDate");
            DropColumn("dbo.tb_Colors", "ModifyedDate");
            DropColumn("dbo.tb_Colors", "ModifierBy");
            DropColumn("dbo.tb_Review", "CreatedBy");
            DropColumn("dbo.tb_Review", "ModifyedDate");
            DropColumn("dbo.tb_Review", "ModifierBy");
            DropColumn("dbo.tb_Sizes", "CreatedBy");
            DropColumn("dbo.tb_Sizes", "CreatedDate");
            DropColumn("dbo.tb_Sizes", "ModifyedDate");
            DropColumn("dbo.tb_Sizes", "ModifierBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Sizes", "ModifierBy", c => c.String());
            AddColumn("dbo.tb_Sizes", "ModifyedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Sizes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Sizes", "CreatedBy", c => c.String());
            AddColumn("dbo.tb_Review", "ModifierBy", c => c.String());
            AddColumn("dbo.tb_Review", "ModifyedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Review", "CreatedBy", c => c.String());
            AddColumn("dbo.tb_Colors", "ModifierBy", c => c.String());
            AddColumn("dbo.tb_Colors", "ModifyedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Colors", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Colors", "CreatedBy", c => c.String());
        }
    }
}
