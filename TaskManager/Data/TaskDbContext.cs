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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.HasMany(x => x.Tasks).WithOne(x => x.User);
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.HasOne(d => d.User).WithMany(x => x.Tasks);
                entity.Property(x => x.Description).IsRequired();
                entity.HasOne(x => x.TaskStatus).WithMany(x => x.Tasks).IsRequired();
              //  entity.HasOne(x => x.ParentTask).WithMany(x =>x.Tasks).HasForeignKey(x=>x.ParentTaskId).IsRequired(false);
            });
            modelBuilder.Entity<Models.TaskStatus>(entity=>
                {
                    entity.HasKey(x => x.Id);
                    entity.Property(x => x.Name).IsRequired();
                    entity.HasMany(x => x.Tasks).WithOne(x => x.TaskStatus);
                });
        }
       
        public DbSet<User> User { get; set; }
        public DbSet<Models.TaskItem> Task { get; set; }
        public DbSet<Models.TaskStatus> TaskStatus { get; set; }
    }
}
