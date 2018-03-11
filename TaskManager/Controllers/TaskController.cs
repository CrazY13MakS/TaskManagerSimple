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
        public IActionResult UpdateTask(int key, string values)
        {
            TaskItem task = manager.GetTaskItem(key);
            int prevStatusId = task.TaskStatusId;
            JsonConvert.PopulateObject(values, task);
            if (!TryValidateModel(task))
                return BadRequest(ModelState.GetFullErrorMessage());
            var res = manager.UpdateTask(prevStatusId,task);
            if (res=="Ok")
            {
                return Ok();
            }
            return BadRequest(res);
        }

        

        [HttpDelete]
        public IActionResult DeleteTask(int key)
        {
            
            if (manager.DeleteTask(key))
            {
                return Ok();
            }
            return BadRequest("Unknown error");
        }
               
    }
}