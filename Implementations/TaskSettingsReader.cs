using GenericScheduler.Contracts;
using GenericScheduler.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GenericScheduler.Implementations
{
    public class TaskSettingsReader : ITaskSettingsReader
    {
        private readonly List<TaskInfo> _tasks;

        public TaskSettingsReader()
        {

            var path  = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaskConfiguration", "taskConfig"); ;
            _tasks = JsonConvert.DeserializeObject<List<TaskInfo>>(File.ReadAllText(path));
        }
        public List<TaskInfo> GetScheduledTasks()
        {
            return _tasks;
        }
    }
}
