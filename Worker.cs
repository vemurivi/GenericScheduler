using GenericScheduler.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericScheduler
{
    /// <summary>
    /// Worker will schedule tasks based on cron expression in 'CronSchedule' property in 'TaskInfo' class, which is generated from www.cronmaker.com.
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITaskSettingsReader _taskSettingsReader;
        private readonly IScheduledTaskFactory _scheduledTaskFactory;


        public Worker(ILogger<Worker> logger, ITaskSettingsReader taskSettingsReader, IScheduledTaskFactory scheduledTaskFactory)
        {
            _logger = logger;
            _scheduledTaskFactory = scheduledTaskFactory;
            _logger.LogInformation($"Worker started at {DateTime.Now}");
            _taskSettingsReader = taskSettingsReader;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
               
                foreach(var task in _taskSettingsReader.GetScheduledTasks())
                {
                    var expression = new CronExpression(task.CronSchedule);
                    if(task.NextRunSchedule.HasValue)
                    {
                        if (task.NextRunSchedule.Value.DateTime <= DateTime.Now.ToUniversalTime())
                        {
                            task.NextRunSchedule = null;
                            _logger.LogInformation($"Worker began to execute for task: {task.Name} at {DateTimeOffset.Now}");
                            var scheduledTask = _scheduledTaskFactory.GetTask(task);
                            await scheduledTask.ExecuteTask();
                        }
                    }
                    else
                    {
                        task.NextRunSchedule = expression.GetNextValidTimeAfter(DateTime.Now.ToUniversalTime());
                    }
                }
            }
        }

    }
}
