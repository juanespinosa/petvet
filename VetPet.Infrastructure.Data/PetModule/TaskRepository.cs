namespace VetPet.Infrastructure.Data.PetModule
{
    using System.Collections.Generic;
    using Domain.PetModule.Aggregate.TaskAgg;
    using Seedwork;
    using UnitOfWork;
    using System.Linq;

    public class TaskRepository
        : Repository<Task>, ITaskRepository
    {
        public TaskRepository(MainBCUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public override IEnumerable<Task> GetAll()
        {
            return base.GetSet().OrderBy(t => t.Type).ThenBy(t => t.Name);
        }
    }
}
