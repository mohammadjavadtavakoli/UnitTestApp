using ASPCoreMVC.App.Data.Models;
using ASPCoreMVC.App.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreMVC.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _service;
        public HomeController(IBookService service)
        {
           _service = service;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            var result = _service.GetAll();
            return View(result);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(Guid id)
        {
            var result = _service.GetByID(id);
            if(result==null)
            {
                return NotFound();
            }
            
            return View(result);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book item)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _service.Add(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var result = _service.GetByID(id);
            return View(result);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _service.remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
