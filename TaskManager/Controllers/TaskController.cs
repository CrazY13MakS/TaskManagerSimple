using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManager.Data;
using TaskManager.Data.Extensions;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskDbContex _context;
        Data.TaskManager manager;

        public TasksController(TaskDbContex context)
        {
            _context = context;
            manager = new Data.TaskManager(_context);
        }
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(manager.GetTasks(), loadOptions);
        }

        [HttpGet]
        public object GetStatuses(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(manager.GetTaskStatuses(), loadOptions);
        }


        [HttpPost]
        public IActionResult InsertTask(string values)
        {
            var task = new TaskItem();
            JsonConvert.PopulateObject(values, task);

            if (!TryValidateModel(task))
                return BadRequest(ModelState.GetFullErrorMessage());

            if (manager.InsertTask(task))
            {
                return Ok();
            }
            return BadRequest("Unknown error");
        }

        [HttpPut]
        public IActionResult UpdateTask(string values)
        {
            var task = new TaskItem();
            JsonConvert.PopulateObject(values, task);

            if (!TryValidateModel(task))
                return BadRequest(ModelState.GetFullErrorMessage());

            if (manager.InsertTask(task))
            {
                return Ok();
            }
            return BadRequest("Unknown error");
        }






        // GET: Tasks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}