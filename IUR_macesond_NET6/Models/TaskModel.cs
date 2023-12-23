using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IUR_macesond_NET6.ViewModels;

namespace IUR_macesond_NET6.Models
{
    class TaskModel
    {
        public string TaskName { get; set; }
        public bool HasNotificationTime { get; set; }
        public TimeOnly NotificationTime { get; set; }
        public Difficulty TaskDifficulty { get; set; }

        public string TaskNote { get; set; }

        public bool MarkedForCompletion { get; set; }
        public bool TaskNoteVisibility { get; set; }


        public TaskModel() {

            // Default values
            TaskName = "";
            HasNotificationTime = false;
            NotificationTime = new TimeOnly(15,30);
            TaskDifficulty = Difficulty.Easy;
            TaskNote = "";
            MarkedForCompletion = false;
            TaskNoteVisibility = false;
        }

        public void SetAttributes(TaskViewModel taskViewModel)
        {
            TaskName = taskViewModel.TaskName;
            HasNotificationTime = taskViewModel.HasNotificationTime;
            NotificationTime = taskViewModel.NotificationTime;
            TaskDifficulty = taskViewModel.TaskDifficulty;
            TaskNote = taskViewModel.TaskNote;
            MarkedForCompletion = taskViewModel.MarkedForCompletion;
            TaskNoteVisibility = taskViewModel.TaskNoteVisibility;
        }
    }
}
