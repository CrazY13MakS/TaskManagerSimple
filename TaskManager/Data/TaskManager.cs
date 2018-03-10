using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public class TaskManager
    {
        TaskDbContex dbContex;
        public TaskManager(Data.TaskDbContex dbContex)
        {
            this.dbContex = dbContex;
        }
        public List<TaskItem> GetTasks()
        {
            var list = dbContex.Task.ToList();
            list.ForEach((x) =>
            {
                x.CanComplited = dbContex
                  .Task
                  .Where(y => y.ParentId == x.Id)
                  .All(y => y.Complited);
            });
            return list;
        }
        public bool StartTaskExecution(int taskId)
        {
            var task = dbContex.Task.FirstOrDefault(x => x.Id == taskId);
            if (task == null || task.Complited)
            {
                return false;
            }
            task.DateStart = DateTime.Now;
            task.InProgress = true;
            return dbContex.SaveChanges() == 1;
        }
        public bool FinishTask(int taskId)
        {
            var task = dbContex.Task.FirstOrDefault(x => x.Id == taskId);
            if (task == null || task.Complited)
            {
                return false;
            }
            var subTusks = dbContex.Task.Where(x => x.ParentId == task.Id);
            if (subTusks == null || subTusks.Any(x => !x.Complited))
            {
                return false;
            }
            task.InProgress = false;
            task.DateEnd = DateTime.Now;
            task.Complited = true;
            return dbContex.SaveChanges() == 1;
        }
        public bool AddSubTask(int parentId, TaskItem task)
        {
            var parentTask = dbContex.Task.FirstOrDefault(x => x.Id == parentId);
            if (parentTask == null || parentTask.Complited)
            {
                return false;
            }
            task.ParentId = parentId;
            dbContex.Task.Add(task);
            return dbContex.SaveChanges() == 1;

        }
    }
}
