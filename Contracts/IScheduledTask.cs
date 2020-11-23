using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericScheduler.Contracts
{
    public interface IScheduledTask
    {
        Task ExecuteTask();
    }
}
