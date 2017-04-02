namespace VetPet.Application.PetModule.Services
{
    using Domain.PetModule.Aggregate.TaskAgg;
    using System;
    using System.Collections.Generic;
    public interface ITaskAppService
        : IDisposable
    {
        IEnumerable<Task> GetAll();

        Task Get(Guid id);
    }
}
