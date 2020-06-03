
using PredictCapacityUsingMathNet.Schedule;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace PredictCapacityUsingMathNet
{
    public class PracticeService
    {
        public void OnStart()
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "XmlConfiguredInstance",
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                ["quartz.threadPool.threadCount"] = "1",
                ["quartz.plugin.xml.type"] = "Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin, Quartz.Plugins",
                ["quartz.plugin.xml.fileNames"] = "~/Job.xml",
                // this is the default 
                ["quartz.plugin.xml.FailOnFileNotFound"] = "true",
                // this is not the default 
                ["quartz.plugin.xml.failOnSchedulingError"] = "true"
            };
            // Grab the Scheduler instance from the Factory 
            IScheduler scheduler = new StdSchedulerFactory(properties).GetScheduler().Result;

            // and start it off
            scheduler.Start();

        }
        public void OnStop()
        {
            // Custom logic
        }
        
    }
}
