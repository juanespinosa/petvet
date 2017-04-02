namespace VetPet.Controllers
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Web.Mvc;
    using Application.PetModule.Services;
    using Domain.PetModule.Aggregate.PetAgg;

    public class PetsController : Controller
    {
        readonly IPetAppService _petAppService;

        public PetsController(IPetAppService petAppService)
        {
            _petAppService = petAppService;
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

        private Guid UserId()
        {
            var userId = User.Identity.GetUserId();
            return Guid.Parse(userId);
        }
    }
}
