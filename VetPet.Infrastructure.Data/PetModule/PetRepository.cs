namespace VetPet.Infrastructure.Data.PetModule
{
    using UnitOfWork;
    using Domain.PetModule.Aggregate.PetAgg;
    using Seedwork;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    public class PetRepository
        : Repository<Pet>, IPetRepository
    {
        public PetRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public IEnumerable<Pet> GetPagedByApi(int pageIndex, int pageCount, string name, string task)
        {
            var currentUOW = UnitOfWork as MainBCUnitOfWork;

            var query = currentUOW.Pets
                                .Include("PetTasks")
                                .Include("PetTasks.Task");


            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.ToUpper().Contains(name.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(task))
            {
                query = query.Where(p => p.PetTasks.Any(t => t.Task.Name.ToUpper().Contains(task.ToUpper())));
            }

            return query
                .OrderBy(p => p.CustomerId)
                .Skip(pageIndex * pageCount)
                                .Take(pageCount);
        }

        public IEnumerable<Pet> GetPagedByCustomer(int pageIndex, int pageCount, Guid customerId)
        {
            var currentUOW = UnitOfWork as MainBCUnitOfWork;

            return currentUOW.Pets
                                .Include("PetTasks")
                                .Include("PetTasks.Task")
                                .Where(p => p.CustomerId.Equals(customerId))
                                .OrderBy(p => p.NickName)
                                .Skip(pageIndex * pageCount)
                                .Take(pageCount);
        }
    }
}
