namespace VetPet.Domain.PetModule.Aggregate.PetAgg
{
    using System;
    using Seedwork.Specification;

    public static class PetSpecifications
    {
        public static ISpecification<Pet> ByCustomer(Guid customerId)
        {
            Specification<Pet> specification = new TrueSpecification<Pet>();

            specification &= new DirectSpecification<Pet>((p) => p.CustomerId.Equals(customerId));

            return specification;
        }
    }
}
