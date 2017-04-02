namespace VetPet.Infrastructure.Data.PetModule
{
    using UnitOfWork;
    using Domain.PetModule.Aggregate.PetAgg;
    using Seedwork;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PetRepository
        : Repository<Pet>, IPetRepository
    {
        public PetRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public IEnumerable<Pet> GetPagedByCustomer(int pageIndex, int pageCount, Guid customerId)
        {
            var currentUOW = UnitOfWork as MainBCUnitOfWork;

            return currentUOW.Pets
                                .Where(p => p.CustomerId.Equals(customerId))
                                .OrderBy(p => p.NickName)
                                .Skip(pageIndex * pageCount)
                                .Take(pageCount);
        }
    }
}
