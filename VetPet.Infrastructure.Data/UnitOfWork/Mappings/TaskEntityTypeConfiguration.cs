namespace VetPet.Infrastructure.Data.UnitOfWork.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using VetPet.Domain.PetModule.Aggregate.TaskAgg;
    
    class TaskEntityTypeConfiguration
        : EntityTypeConfiguration<Task>
    {
        public TaskEntityTypeConfiguration()
        {
            HasKey(t => t.Id);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);
            Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
