namespace VetPet.Domain.PetModule.Aggregate.PetAgg
{
    using Seedwork;
    using System;
    using System.ComponentModel.DataAnnotations;

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
    }
}
