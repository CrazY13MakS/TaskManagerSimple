using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Data
{
    public partial class TaskManager
    {
        public String InsertUser(User user)
        {
            dbContex.User.Add(user);
            return dbContex.SaveChanges() == 1 ? "Ok" : "Unknown Error";
        }

        public User GetUser(int id)
        {
            return dbContex.User.FirstOrDefault(x => x.Id == id);
        }
        public String UpdateUser(User user)
        {           
            return dbContex.SaveChanges() == 1 ? "Ok" : "Unknown Error";
        }
        public String DeleteUser(int id)
        {
            var user = dbContex.User.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return "User not found";
            }
            var tasks = dbContex.Task.Where(x => x.UserId == user.Id).ToList();
            tasks.ForEach(x => { x.UserId = null; });
            dbContex.User.Remove(user);
            return dbContex.SaveChanges() == tasks.Count+1 ? "Ok" : "Unknown Error";
        }
    }
}
