namespace VetPet.Domain.PetModule.Aggregate.PetAgg
{
    using Seedwork;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base contract for bank account repository
    /// <see cref="Seedwork.IRepository{BankAccount}"/>
    /// </summary>
    public interface IPetRepository
        : IRepository<Pet>
    {
        IEnumerable<Pet> GetPagedByCustomer(int pageIndex, int pageCount, Guid customerId);

        IEnumerable<Pet> GetPagedByApi(int pageIndex, int pageCount, string name, string task);
    }
}
