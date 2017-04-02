namespace VetPet.Controllers
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Web.Mvc;
    using Application.PetModule.Services;
    using Domain.PetModule.Aggregate.PetAgg;
    using System.Collections.Generic;
    using Models;
    using System.Web.Security;

    [Authorize]
    public class PetsController : Controller
    {
        readonly IPetAppService _petAppService;
        private ApplicationUserManager _userManager;

        public PetsController(IPetAppService petAppService,
            ApplicationUserManager userManager)
        {
            _petAppService = petAppService;
            _userManager = userManager;
        }

        // GET: Pets
        public ActionResult Index(int pageIndex = 0, int pageCount = 5)
        {
            var pets = _petAppService.FindPets(pageIndex, pageCount, UserId());
            return View(pets);
        }

        // GET: Pets/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            var pet = new Pet
            {
                CustomerId = UserId()
            };
            return View(pet);
        }

        // POST: Pets/Create
        [HttpPost]
        public ActionResult Create(Pet pet)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(pet);
                }
                _petAppService.AddNewPet(pet);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(pet);
            }
        }

        // GET: Pets/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_petAppService.GetPet(id));
        }

        // POST: Pets/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Pet pet)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(pet);
                }
                pet.ChangeCurrentIdentity(id);
                _petAppService.UpdatePet(pet);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(Guid id)
        {

            return View(_petAppService.GetPet(id));
        }

        // POST: Pets/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Pet pet)
        {
            try
            {
                _petAppService.RemovePet(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Tasks(string name, string task, int count = 10, int page = 1)
        {
            var result = new List<object>();

            var pets = _petAppService.FindPets(name, task, page, count);

            foreach (var pet in pets)
            {
                foreach(var petTask in pet.PetTasks)
                {
                    result.Add(new
                    {
                        TipoTarea = petTask.Task.Type,
                        NombreTarea = petTask.Task.Name,
                        NombreCompleto = pet.CustomerId,
                        Edad = pet.Age,
                        NombreMascota = pet.Name,
                        Raza = pet.Breed,
                        FechaTarea = petTask.DateTime.ToString()
                    });
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private Guid UserId()
        {
            var userId = User.Identity.GetUserId();
            return Guid.Parse(userId);
        }
    }
}
