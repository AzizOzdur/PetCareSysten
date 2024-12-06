using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services.Bases;
using BLL.Models;
using BLL.DAL;
using BLL.Services;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class SpeciesController : MvcController
    {
        // Service injections:
        private readonly ISpeciesService _speciesService;

        /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
        //private readonly IService<{Entity}, {Entity}Model> _{Entity}Service;

        public SpeciesController(
			ISpeciesService speciesService

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //, Service<{Entity}, {Entity}Model> {Entity}Service
        )
        {
            _speciesService = speciesService;

            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //_{Entity}Service = {Entity}Service;
        }

        // GET: Species
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _speciesService.Query().ToList();
            return View(list);
        }

        // GET: Species/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _speciesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. {Entity} may be replaced with the related entiy name in the controller and views. */
            //ViewBag.{Entity}Ids = new MultiSelectList(_{Entity}Service.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Species/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Species/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SpeciesModel species)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _speciesService.Create(species.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = species.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(species);
        }

        // GET: Species/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _speciesService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Species/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SpeciesModel species)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _speciesService.Update(species.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = species.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(species);
        }

        // GET: Species/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _speciesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Species/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _speciesService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
