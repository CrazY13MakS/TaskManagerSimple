using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public String Description { get; set; }
        public bool Complited { get; set; }
        [NotMapped]
        public bool CanComplited { get; set; }
        public User User { get; set; }
        public bool InProgress { get; set; }
    }
}
