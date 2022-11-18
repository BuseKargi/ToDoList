using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
    public class TaskStatusList
    {
        public List<Task> Task { get; set; }
        public List<Status> Status { get; set; }


    }
}
