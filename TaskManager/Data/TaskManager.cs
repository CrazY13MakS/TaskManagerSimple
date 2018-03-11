using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;
using Microsoft.EntityFrameworkCore.Migrations;
namespace TaskManager.Data
{
    public partial class TaskManager
    {
        TaskDbContex dbContex;
        public TaskManager(Data.TaskDbContex dbContex)
        {
            this.dbContex = dbContex;
            dbContex.Database.EnsureCreated();
        }
        public TaskItem GetTaskItem(int id)
        {
            return dbContex.Task.FirstOrDefault(x => x.Id == id);
        }
        public List<TaskItem> GetTasks()
        {
            var list = dbContex.Task.ToList();
            return list;
        }
        public bool InsertTask(TaskItem task)
        {
            if (task.ParentTaskId == 0)
            {
                task.ParentTaskId = null;
            }
            dbContex.Task.Add(task);
            var res = dbContex.SaveChanges() == 1;
            return res;
        }
        public String UpdateTask(int prevStatus, TaskItem task)
        {
            TaskItem taskUpdate = dbContex.Task.FirstOrDefault(x => x.Id == task.Id);
            if (taskUpdate == null)
            {
                return "Task for update not found in DB";
            }
            var status = dbContex.TaskStatus.FirstOrDefault(x => x.Name == "Completed");
            int statusComplId = status == null ? 3 : status.Id;// here must be const for "Complited" 
            if (task.TaskStatusId == statusComplId && !CanSetComplited(taskUpdate, statusComplId))
            {
                return "Not all sub-tasks are completed";
            }
            if (prevStatus == statusComplId && task.TaskStatusId != prevStatus && CanSetNotComplited(task, statusComplId))
            {
                return "Parent task is already completed. Edit them first";
            }
            return dbContex.SaveChanges() == 1 ? "Ok" : "Unknown Error";

        }
        public bool DeleteTask(int id)
        {
            var task = dbContex.Task.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return false;
            }
            var subTasks = GetSubTaskItems(id);
            subTasks.Add(task);
            dbContex.Task.RemoveRange(subTasks);
            return dbContex.SaveChanges() == subTasks.Count;
        }

        private bool CanSetNotComplited(TaskItem task, int statusId)
        {
            var taskItem = dbContex.Task.FirstOrDefault(x => x.Id == task.Id);
            return task == null || task.TaskStatusId != statusId;
        }
        private bool CanSetComplited(TaskItem task, int statusId)
        {
            List<TaskItem> tasks = GetSubTaskItems(task.Id);
            var res = tasks.All(x => x.TaskStatusId == statusId);
            return res;
        }



        public List<Models.TaskStatus> GetTaskStatuses()
        {
            return dbContex.TaskStatus.ToList();
        }

        private List<TaskItem> GetSubTaskItems(int taskId)
        {
            var items = dbContex.Task.Where(x => x.ParentTaskId == taskId).ToList();
            List<TaskItem> temp = new List<TaskItem>(items);
            foreach (var item in items)
            {
                temp.AddRange(GetSubTaskItems(item.Id));
            }
            return temp;
        }

        private List<TaskItem> GetParentItems(int parentTaskId)
        {
            List<TaskItem> temp = new List<TaskItem>();
            do
            {
                var task = dbContex.Task.FirstOrDefault(x => x.Id == parentTaskId);
                if (task != null)
                {
                    temp.Add(task);
                }
                else
                {
                    return temp;
                }
            } while (true);
        }

        public List<User> GetUsers()
        {
            var list = dbContex.User.ToList();
            list.ForEach(x => x.FullName = $"{x.FirstName} {x.LastName}");
            return list;
        }
    }
}
