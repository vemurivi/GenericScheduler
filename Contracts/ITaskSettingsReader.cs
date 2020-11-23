using GenericScheduler.Models;
using System.Collections.Generic;

namespace GenericScheduler.Contracts
{
    public interface ITaskSettingsReader
  {
      List<TaskInfo> GetScheduledTasks();
  }
}
