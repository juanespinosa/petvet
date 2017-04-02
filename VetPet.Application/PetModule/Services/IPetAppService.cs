namespace VetPet.Application.PetModule.Services
{
    using Domain.PetModule.Aggregate.PetAgg;
    using System;
    using System.Collections.Generic;

    public interface IPetAppService
        : IDisposable
    {
        Pet AddNewPet(Pet pet);

        Pet GetPet(Guid id);

        void UpdatePet(Pet pet);

        void RemovePet(Guid petId);

        List<Pet> FindPets(int index, int count, Guid customerId);

        List<Pet> FindPets(string name, string task, int index, int count);
    }
}
