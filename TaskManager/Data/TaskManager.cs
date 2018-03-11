using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class TaskManager
    {
        TaskDbContex dbContex;
        public TaskManager(Data.TaskDbContex dbContex)
        {
            this.dbContex = dbContex;
            dbContex.Database.EnsureCreated();
        }
        public List<TaskItem> GetTasks()
        {

            var list = dbContex.Task.ToList();
            var status = dbContex.TaskStatus.FirstOrDefault(x => x.Name == "Complited");
            int statusId = status != null ? status.Id : (int)TaskStatusEnum.Complited;

            list.ForEach((x) =>
            {
                x.CanComplited = x.CanComplitedTask();
            });
            return list;
        }
        public bool InsertTask(TaskItem task)
        {
            
            dbContex.Task.Add(task);
            return dbContex.SaveChanges() == 1;
        }
        public String UpdateTask(TaskItem taskUpdate)
        {
            var task = dbContex.Task.FirstOrDefault(x => x.Id == taskUpdate.Id);
            if (task == null)
            {
                return "Task for update not found in DB";
            }
            if (taskUpdate.TaskStatus.Name=="Complited"&&!task.CanComplitedTask())
            {
                return "Not all sub-tasks are completed";
            }
            task.DateEnd = taskUpdate.DateEnd;
            task.DateStart = taskUpdate.DateStart;
            task.Description = taskUpdate.Description;
            task.Title = taskUpdate.Title;
            task.TaskStatus = taskUpdate.TaskStatus;

            return dbContex.SaveChanges() == 1 ? "Ok" : "Unknown Error";
           
        }


        public List<Models.TaskStatus> GetTaskStatuses()
        {
            return dbContex.TaskStatus.ToList();
        }



        public List<User> GetUsers()
        {
            var list = dbContex.User.ToList();
            list.ForEach(x=>x.FullName=$"{x.FirstName} {x.LastName}");
            return list;
        }
        //public bool StartTaskExecution(int taskId)
        //{
        //    var task = dbContex.Task.FirstOrDefault(x => x.Id == taskId);
        //    if (task == null || task.Complited)
        //    {
        //        return false;
        //    }
        //    task.DateStart = DateTime.Now;
        //    task.InProgress = true;
        //    return dbContex.SaveChanges() == 1;
        //}
        //public bool FinishTask(int taskId)
        //{
        //    var task = dbContex.Task.FirstOrDefault(x => x.Id == taskId);
        //    if (task == null || task.Complited)
        //    {
        //        return false;
        //    }
        //    var subTusks = dbContex.Task.Where(x => x.ParentId == task.Id);
        //    if (subTusks == null || subTusks.Any(x => !x.Complited))
        //    {
        //        return false;
        //    }
        //    task.InProgress = false;
        //    task.DateEnd = DateTime.Now;
        //    task.Complited = true;
        //    return dbContex.SaveChanges() == 1;
        //}
        //public bool AddSubTask(int parentId, TaskItem task)
        //{
        //    var parentTask = dbContex.Task.FirstOrDefault(x => x.Id == parentId);
        //    if (parentTask == null || parentTask.Complited)
        //    {
        //        return false;
        //    }
        //    task.ParentId = parentId;
        //    dbContex.Task.Add(task);
        //    return dbContex.SaveChanges() == 1;

        //}
    }
}
