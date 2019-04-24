namespace Harvin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoçãoAtributoRua : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Estares", "EstarRua");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Estares", "EstarRua", c => c.String());
        }
    }
}
