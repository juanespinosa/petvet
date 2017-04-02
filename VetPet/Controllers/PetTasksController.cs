namespace VetPet.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using VetPet.Application.PetModule.Services;
    using VetPet.Domain.PetModule.Aggregate.PetAgg;

    [RoutePrefix("pets/{petId:Guid}")]
    public class PetTasksController : Controller
    {
        readonly IPetAppService _petAppService;
        readonly ITaskAppService _taskAppService;

        Pet Pet { get; set; }

        public PetTasksController(
            IPetAppService petAppService,
            ITaskAppService taskAppService)
        {
            _petAppService = petAppService;
            _taskAppService = taskAppService;
        }

        [Route("tasks")]
        // GET: PetTasks
        public ActionResult Index(Guid petId)
        {
            return View(Pet);
        }
        
        [Route("tasks/create")]
        // GET: PetTasks/Create
        public ActionResult Create(Guid petId)
        {
            SetupTasks();
            ViewBag.PetId = Pet.Id;
            var petTask = new PetTask();
            petTask.GenerateNewIdentity();
            return View(petTask);
        }

        // POST: PetTasks/Create
        [HttpPost]
        [Route("tasks/create")]
        public ActionResult Create(Guid petId, Guid taskId, PetTask petTask)
        {
            try
            {
                petTask.Task = _taskAppService.Get(taskId);
                if (!ModelState.IsValid)
                {
                    return View(petTask);
                }

                Pet.PetTasks.Add(petTask);
                _petAppService.UpdatePet(Pet);

                return RedirectToAction("Index", new { petId = petId });
            }
            catch
            {
                SetupTasks();
                ViewBag.PetId = Pet.Id;
                return View(petTask);
            }
        }
        [Route("tasks/edit/{id:Guid}")]
        // GET: PetTasks/Edit/5
        public ActionResult Edit(Guid petId, Guid id)
        {
            ViewBag.PetId = Pet.Id;
            var petTask = Pet.PetTasks.First(pt => pt.Id.Equals(id));
            SetupTasks(petTask.Task.Id);
            return View(petTask);
        }

        // POST: PetTasks/Edit/5
        [HttpPost]
        [Route("tasks/edit/{id:Guid}")]
        public ActionResult Edit(Guid petId, Guid id, Guid taskId, PetTask petTask)
        {
            try
            {
                var toUpdate = Pet.PetTasks.First(pt => pt.Id.Equals(id));
                toUpdate.DateTime = petTask.DateTime;
                toUpdate.Price = petTask.Price;
                toUpdate.Task = _taskAppService.Get(taskId);
                if (!ModelState.IsValid)
                {
                    return View(petTask);
                }
                
                _petAppService.UpdatePet(Pet);

                return RedirectToAction("Index");
            }
            catch
            {
                SetupTasks();
                ViewBag.PetId = Pet.Id;
                return View();
            }
        }

        [Route("tasks/delete/{id:Guid}")]
        // GET: PetTasks/Delete/5
        public ActionResult Delete(Guid petId, Guid id)
        {
            ViewBag.PetId = Pet.Id;
            var petTask = Pet.PetTasks.First(pt => pt.Id.Equals(id));
            return View(petTask);
        }

        // POST: PetTasks/Delete/5
        [HttpPost]
        [Route("tasks/delete/{id:Guid}")]
        public ActionResult Delete(Guid petId, Guid id, PetTask petTask)
        {
            try
            {
                var todel = Pet.PetTasks.First(pt => pt.Id.Equals(id));
                if (todel != null)
                {
                    Pet.PetTasks.Remove(todel);
                    _petAppService.UpdatePet(Pet);
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                ViewBag.PetId = Pet.Id;
                return View(petTask);
            }
        }
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var petId = Guid.Parse(filterContext.ActionParameters["petId"].ToString());
            Pet = _petAppService.GetPet(petId);
        }

        private void SetupTasks(Guid? taskId = null)
        {
            var tasks = _taskAppService.GetAll();
            var options = new List<SelectListItem>();


            foreach(var task in tasks)
            {
                options.Add(new SelectListItem
                {
                    Text = string.Format("{0} ({1})", task.Name, task.Type),
                    Value = task.Id.ToString(),
                    Selected = taskId.HasValue && taskId.Value.Equals(task.Id)
                });
            }

            ViewBag.Tasks = options;
        }
    }
}
