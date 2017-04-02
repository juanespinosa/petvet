namespace VetPet.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPetTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PetTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Price = c.Double(),
                        Task_Id = c.Guid(nullable: false),
                        Pet_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.Task_Id)
                .ForeignKey("dbo.Pets", t => t.Pet_Id)
                .Index(t => t.Task_Id)
                .Index(t => t.Pet_Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetTasks", "Pet_Id", "dbo.Pets");
            DropForeignKey("dbo.PetTasks", "Task_Id", "dbo.Tasks");
            DropIndex("dbo.PetTasks", new[] { "Pet_Id" });
            DropIndex("dbo.PetTasks", new[] { "Task_Id" });
            DropTable("dbo.Tasks");
            DropTable("dbo.PetTasks");
        }
    }
}
