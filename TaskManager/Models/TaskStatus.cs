using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{

    public class TaskStatus
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
    public enum TaskStatusEnum
    {
        NotStarted,
        InProgress,
        Complited,
        Underfind
    }
}
