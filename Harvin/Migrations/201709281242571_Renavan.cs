namespace Harvin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renavan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Automoveis", "AutomovelRenavan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Automoveis", "AutomovelRenavan");
        }
    }
}
