namespace VetPet.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGenderLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pets", "Gender", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pets", "Gender", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
