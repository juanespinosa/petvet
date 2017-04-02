using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VetPet.Domain.PetModule.Aggregate.PetAgg;

namespace VetPet.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        readonly IPetRepository _petRepository;

        public HomeController(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public ActionResult Index()
        {
            var pets = _petRepository.GetPagedByCustomer(0, 100, UserId()).ToList();
            ViewBag.TotalPrice = pets.SelectMany(p => p.PetTasks).Sum(pt => pt.Price);
            return View(pets);
        }

        private Guid UserId()
        {
            var userId = User.Identity.GetUserId();
            return Guid.Parse(userId);
        }
    }
}