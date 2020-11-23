using GenericScheduler.Models;

namespace GenericScheduler.Contracts
{
    public interface IScheduledTaskFactory
    {
        IScheduledTask GetTask(TaskInfo taskInfo);
    }
}
