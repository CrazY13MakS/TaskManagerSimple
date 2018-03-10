using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class TaskDbContex : DbContext
    {
        public TaskDbContex()
        {

        }
        public TaskDbContex(DbContextOptions<TaskDbContex> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Models.TaskItem> Task { get; set; }
    }
}
