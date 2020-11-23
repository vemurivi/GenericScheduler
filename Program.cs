using GenericScheduler.Contracts;
using GenericScheduler.Implementations;
using GenericScheduler.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace GenericScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              
                .ConfigureServices((hostContext, services) =>
                {
                    IServiceProvider serviceProvider = services.BuildServiceProvider();
                    ILogger<ScheduledTask> logger = serviceProvider.GetRequiredService<ILogger<ScheduledTask>>();
                    services.AddHostedService<Worker>();
                    services.AddSingleton(typeof(ITaskSettingsReader), typeof(TaskSettingsReader));
                    services.AddSingleton(typeof(ILogger<ScheduledTask>), typeof(Logger<ScheduledTask>));
                    services.AddTransient(typeof(IScheduledTaskFactory), typeof(ScheduledTaskFactory));
                    services.AddTransient<Func<TaskInfo, IScheduledTask>>((provider) =>
                    {
                        return new Func<TaskInfo, IScheduledTask>(
                            (taskInfo) => new ScheduledTask(new TaskSettingsReader(), taskInfo, logger));
                    });
                });

    }

}

