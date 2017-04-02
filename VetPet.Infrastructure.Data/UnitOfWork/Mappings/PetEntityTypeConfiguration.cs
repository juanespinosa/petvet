namespace VetPet.Infrastructure.Data.UnitOfWork.Mappings
{
    using Domain.PetModule.Aggregate.PetAgg;
    using System.Data.Entity.ModelConfiguration;

    class PetEntityTypeConfiguration
        : EntityTypeConfiguration<Pet>
    {
        public PetEntityTypeConfiguration()
        {
            HasKey(e => e.Id);

            Property(p => p.BirthDate)
                .IsOptional();
            Property(p => p.Breed)
                .HasMaxLength(64)
                .IsRequired();
            Property(p => p.CustomerId)
                .IsRequired();
            Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(6);
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(128);
            Property(p => p.NickName)
                .IsOptional()
                .HasMaxLength(32);
            Property(p => p.Picture)
                .IsOptional()
                .IsMaxLength();

            HasMany(p => p.PetTasks)
                .WithRequired();
        }
    }

    class PetTaskEntityTypeConfiguration
        : EntityTypeConfiguration<PetTask>
    {
        public PetTaskEntityTypeConfiguration()
        {
            HasKey(pt => pt.Id);

            Property(pt => pt.DateTime)
                .IsRequired();
            Property(pt => pt.Price)
                .IsOptional();

            HasRequired(pt => pt.Task)
                .WithMany();
        }
    }
}
