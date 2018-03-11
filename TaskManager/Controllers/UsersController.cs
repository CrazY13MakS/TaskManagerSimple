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
    public class UsersController : Controller
    {

        private readonly TaskDbContex _context;
        Data.TaskManager manager;

        public UsersController(TaskDbContex context)
        {
            _context = context;
            manager = new Data.TaskManager(_context);
        }
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public object GetUsers(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(manager.GetUsers(), loadOptions);
        }

        [HttpPost]
        public IActionResult InsertUser(string values)
        {
            var user = new User();
            JsonConvert.PopulateObject(values, user);

            if (!TryValidateModel(user))
                return BadRequest(ModelState.GetFullErrorMessage());

            String res = manager.InsertUser(user);
            if (res=="Ok")
            {
                return Ok();
            }
            return BadRequest(res);
        }

        [HttpPut]
        public IActionResult UpdateUser(int key, string values)
        {
            User user = manager.GetUser(key);
            JsonConvert.PopulateObject(values, user);
            if (!TryValidateModel(user))
                return BadRequest(ModelState.GetFullErrorMessage());
            var res = manager.UpdateUser(user);
            if (res == "Ok")
            {
                return Ok();
            }
            return BadRequest(res);
        }



        [HttpDelete]
        public IActionResult DeleteTask(int key)
        {
            var res = manager.DeleteUser(key);
            if (res=="Ok")
            {
                return Ok();
            }
            return BadRequest(res);
        }
    }
}