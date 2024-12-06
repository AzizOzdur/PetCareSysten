using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class PetsController : MvcController
    {
        // Service injections:
        private readonly IService<Pet, PetModel> _petService;
        private readonly IService<Species, SpeciesModel> _speciesService;
      

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public PetsController(
			IService<Pet, PetModel> petService
            , IService<Species, SpeciesModel> speciesService
            

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _petService = petService;
            _speciesService = speciesService;
            

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Pets
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _petService.Query().ToList();
            return View(list);
        }

        // GET: Pets/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _petService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["SpeciesId"] = new SelectList(_speciesService.Query().ToList(), "Record.Id", "Name");
            
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Pets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PetModel pet)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _petService.Create(pet.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = pet.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(pet);
        }

        // GET: Pets/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _petService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Pets/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PetModel pet)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _petService.Update(pet.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = pet.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(pet);
        }

        // GET: Pets/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _petService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Pets/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _petService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
