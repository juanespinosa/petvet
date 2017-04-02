namespace VetPet.Domain.PetModule.Aggregate.PetAgg
{
    using System;
    using Seedwork.Specification;
    using System.Linq;

    public static class PetSpecifications
    {
        public static ISpecification<Pet> ByCustomer(Guid customerId)
        {
            Specification<Pet> specification = new TrueSpecification<Pet>();

            specification &= new DirectSpecification<Pet>((p) => p.CustomerId.Equals(customerId));

            return specification;
        }

        public static ISpecification<Pet> ByApi(string name, string type)
        {
            Specification<Pet> specification = new TrueSpecification<Pet>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                specification &= new DirectSpecification<Pet>((p) => p.Name.ToUpper().Equals(name.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                specification &= new DirectSpecification<Pet>((p) => p.PetTasks.Any(t => t.Task.Type.ToUpper().Contains(type.ToUpper())));
            }

            return specification;
        }
    }
}
