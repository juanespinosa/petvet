namespace VetPet.Domain.PetModule.Aggregate.PetAgg
{
    using Seedwork;
    using System;
    using System.ComponentModel.DataAnnotations;
    using TaskAgg;

    public class PetTask : Entity
    {
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public double Price { get; set; }
        
        public virtual Task Task { get; set; }
    }
}
