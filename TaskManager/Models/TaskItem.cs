using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{

    public class TaskItem
    {
        public int Id { get; set; }
        //  public int? ParentTask { get; set; }
       // public TaskItem ParentTask { get; set; }


        [Display( Name = "Date start")]
        [ DataType(DataType.DateTime) ]
        public DateTime? DateStart { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateEnd { get; set; }
        [Required]
        public String Title { get; set; }     
        [Required]
        public String Description { get; set; }
        [Required]
        public int TaskStatusId { get; set; }
        public int? UserId { get; set; }
        [NotMapped]
        public bool CanComplited { get; set; }
        public User User { get; set; }
        [NotMapped]
        public bool HasSubTask { get; set; }
        [NotMapped]
        public TaskStatus TaskStatus { get; set; }
        [NotMapped]
        public List<TaskItem> Tasks { get; set; }
     //   public TaskItem ParentTask { get; set; }

        public int? ParentTaskId { get; set; }
        public bool CanComplitedTask()
        {
            if(Tasks==null)
            {
                return true;
            }
            return Tasks.Any(x => !x.CanComplitedTask());
        }
    }
}
