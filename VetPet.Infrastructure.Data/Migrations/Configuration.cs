namespace VetPet.Infrastructure.Data.Migrations
{
    using Domain.PetModule.Aggregate.TaskAgg;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VetPet.Infrastructure.Data.UnitOfWork.MainBCUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VetPet.Infrastructure.Data.UnitOfWork.MainBCUnitOfWork context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var t1 = new Task { Type = "Campo", Name = "Salida al parque" };
            t1.ChangeCurrentIdentity(Guid.Parse("83bcadb2-726a-49f8-8a13-eb2abd964c0f"));
            var t2 = new Task { Type = "Campo", Name = "Entrenamiento" };
            t2.ChangeCurrentIdentity(Guid.Parse("9cfcde67-9692-409f-8277-89766f92e48d"));
            var t3 = new Task { Type = "Campo", Name = "Vacunas" };
            t3.ChangeCurrentIdentity(Guid.Parse("301e0c4d-c3a6-4c8c-9674-215eac45901d"));
            var t4 = new Task { Type = "Casa", Name = "Inspección general" };
            t4.ChangeCurrentIdentity(Guid.Parse("87dd560d-9649-4aac-84f2-b9f27c695007"));
            var t5 = new Task { Type = "Casa", Name = "Alimentación" };
            t5.ChangeCurrentIdentity(Guid.Parse("23cb87b3-f981-4c11-a503-2347df10bb93"));

            context.Tasks.AddOrUpdate(
                t => t.Id,
                t1, t2, t3, t4, t5
                );
        }
    }
}
