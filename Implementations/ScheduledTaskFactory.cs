using GenericScheduler.Contracts;
using GenericScheduler.Models;
using System;

namespace GenericScheduler.Implementations
{
    public class ScheduledTaskFactory : IScheduledTaskFactory
    {
        private readonly Func<TaskInfo, IScheduledTask> _scheduledTask;

        public ScheduledTaskFactory(Func<TaskInfo,IScheduledTask> scheduledTask)
        {
            _scheduledTask = scheduledTask;
        }
        public IScheduledTask GetTask(TaskInfo taskInfo)
        {
            return _scheduledTask(taskInfo);
        }
    }
}
