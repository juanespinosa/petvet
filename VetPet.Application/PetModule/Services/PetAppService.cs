namespace VetPet.Application.PetModule.Services
{
    using System;
    using System.Collections.Generic;
    using Domain.PetModule.Aggregate.PetAgg;
    using System.Linq;

    public class PetAppService : IPetAppService
    {
        readonly IPetRepository _petRepository;

        public PetAppService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public Pet AddNewPet(Pet pet)
        {
            if (pet.IsTransient())
            {
                pet.GenerateNewIdentity();
            }
            _petRepository.Add(pet);
            _petRepository.UnitOfWork.Commit();

            return pet;
        }
        public Pet GetPet(Guid id)
        {
            return _petRepository.Get(id);
        }

        public List<Pet> FindPets(int index, int count, Guid customerId)
        {
            return _petRepository.GetPagedByCustomer(index, count, customerId)
                .ToList();
        }

        public void RemovePet(Guid petId)
        {
            var pet = _petRepository.Get(petId);
            if(pet != null)
            {
                _petRepository.Remove(pet);
                _petRepository.UnitOfWork.Commit();
            }
        }

        public void UpdatePet(Pet pet)
        {
            var persisted = _petRepository.Get(pet.Id);
            if(persisted != null)
            {
                foreach(var task in pet.PetTasks)
                {
                    if (task.IsTransient())
                    {
                        task.GenerateNewIdentity();
                    }
                }
                _petRepository.Merge(persisted, pet);
                _petRepository.UnitOfWork.Commit();
            }
        }
        public void Dispose()
        {
            _petRepository.Dispose();
        }
    }
}
