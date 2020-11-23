using GenericScheduler.Contracts;
using GenericScheduler.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GenericScheduler.Implementations
{
    public class ScheduledTask : IScheduledTask
    {
        private readonly TaskInfo _taskInfo;
        private readonly ITaskSettingsReader _taskSettingsReader;
        private readonly ILogger<ScheduledTask> _logger;
        private readonly HttpClient _httpClient;

        public ScheduledTask(ITaskSettingsReader taskSettingsReader, TaskInfo taskInfo, ILogger<ScheduledTask> logger)
        {
            _taskSettingsReader = taskSettingsReader;
            _taskInfo = taskInfo;
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public virtual async Task ExecuteTask()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_taskInfo?.BodyData))
                {
                    var json = JsonConvert.SerializeObject((JObject.Parse(_taskInfo?.BodyData)));
                    var data = new StringContent(json, Encoding.UTF8, "application/json");


                    if (_taskInfo?.HttpVerb == HttpVerbType.POST)
                    {
                        using (HttpResponseMessage response = await _httpClient.PostAsync($"{_taskInfo?.ApiURL}", data))
                        {
                            if(response.IsSuccessStatusCode)
                            {
                                _logger.LogInformation($"Response is Successful for {_taskInfo.Name}");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Scheduler Task thrown error while running {_taskInfo.Name} and Error Message is :{ex.Message}");
                    throw;
            }
        }
    }
}
