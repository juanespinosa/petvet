namespace VetPet.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePetsCatalog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NickName = c.String(maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 128),
                        BirthDate = c.DateTime(),
                        Picture = c.String(),
                        Breed = c.String(nullable: false, maxLength: 64),
                        Gender = c.String(nullable: false, maxLength: 1),
                        CustomerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pets");
        }
    }
}
