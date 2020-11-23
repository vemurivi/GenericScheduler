using System;
using System.Collections.Generic;
using System.Text;

namespace GenericScheduler.Models
{
   public class TaskInfo
   {
        public string Name { get; set; }
        public string WorkerPath { get; set; }
        public string CronSchedule { get; set; }
        public DateTimeOffset? NextRunSchedule { get; set; }
        public string ApiURL { get; set; }
        public HttpVerbType HttpVerb { get; set; }
        public string BodyData { get; set; }
   }

   public enum HttpVerbType
    {
        GET=1,
        POST=2,
        Patch=3,
        Put=4,
        Delete=5
    }
}
