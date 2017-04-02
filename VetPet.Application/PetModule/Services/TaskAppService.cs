namespace VetPet.Application.PetModule.Services
{
    using System;
    using System.Collections.Generic;
    using Domain.PetModule.Aggregate.TaskAgg;

    public class TaskAppService : ITaskAppService
    {
        readonly ITaskRepository _taskRepository;

        public TaskAppService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IEnumerable<Task> GetAll()
        {
            return _taskRepository.GetAll();
        }
        
        public Task Get(Guid id)
        {
            return _taskRepository.Get(id);
        }
        public void Dispose()
        {
            _taskRepository.Dispose();
        }
    }
}
