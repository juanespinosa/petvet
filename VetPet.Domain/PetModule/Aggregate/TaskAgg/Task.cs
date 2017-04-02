namespace VetPet.Domain.PetModule.Aggregate.TaskAgg
{
    using Seedwork;
    using System.ComponentModel.DataAnnotations;

    public class Task : Entity
    {
        [Required]
        [MaxLength(32)]
        [Display(Name="Tipo de tarea")]
        public string Type { get; set; }
        [Required]
        [MaxLength(32)]
        [Display(Name = "Nombre de la tarea")]
        public string Name { get; set; }
    }
}
