namespace VetPet.Domain.PetModule.Aggregate.PetAgg
{
    using Seedwork;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using TaskAgg;

    public class Pet : Entity
    {
        [MaxLength(32)]
        [Display(Name = "Apodo")]
        public string NickName { get; set; }
        [Required]
        [MaxLength(128)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Foto")]
        public string Picture { get; set; }
        [Required]
        [MaxLength(64)]
        [Display(Name = "Raza")]
        public string Breed { get; set; }
        [Required]
        [MaxLength(6)]
        [Display(Name = "Género")]
        public string Gender { get; set; }
        [Required]
        public Guid CustomerId { get; set; }

        private HashSet<PetTask> _petTasks;
        public virtual ICollection<PetTask> PetTasks
        {
            get
            {
                if (_petTasks == null)
                    _petTasks = new HashSet<PetTask>();
                return _petTasks;
            }
            set
            {
                _petTasks = new HashSet<PetTask>(value);
            }
        }

        public CurrentTaskInfo GetTasksInfo()
        {
            var current = new CurrentTaskInfo();

            var tasks = PetTasks.Where(pt => pt.DateTime >= DateTime.Today && pt.DateTime < DateTime.Today.AddDays(1)).OrderBy(pt => pt.DateTime).ToList();
            var tasksLeft = PetTasks.Where(pt => pt.DateTime >= DateTime.Now && pt.DateTime < DateTime.Today.AddDays(1)).OrderBy(pt => pt.DateTime).ToList();

            current.NextTask = tasksLeft.FirstOrDefault();
            current.TotalPrice = tasks.Sum(pt => pt.Price);
            current.TasksDone = tasks.Count() - tasksLeft.Count();
            current.PendingTasks = tasksLeft.Count();
            current.PercentageTasksDone = tasks.Count() > 0 ? current.TasksDone * 100 / tasks.Count() : 100;

            return current;
        }
    }

    public class CurrentTaskInfo
    {
        public PetTask NextTask { get; set; }
        public double TotalPrice { get; set; }

        public int PendingTasks { get; set; }
        public int TasksDone { get; set; }

        public int PercentageTasksDone { get; set; }
    }
}
