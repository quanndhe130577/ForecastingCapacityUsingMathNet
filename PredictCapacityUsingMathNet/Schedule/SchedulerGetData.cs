using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictCapacityUsingMathNet.Schedule
{
    public class SchedulerGetData
    {

        IScheduler scheduler;
        /*IJobDetail job;
        ITrigger trigger;*/
        public async void Start()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            // get a scheduler, start the schedular before triggers or anything else
            scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            // create job
            IJobDetail job_getData = JobBuilder.Create<JobGetData>()
                    .WithIdentity("job1")
                    .Build();

            // create trigger
            ITrigger trigger_getData = TriggerBuilder.Create()
                .WithIdentity("trigger1")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
                .Build();
            // create job
            IJobDetail job_PredictData = JobBuilder.Create<JobPredictData>()
                    .WithIdentity("job2")
                    .Build();
            // create trigger
            ITrigger trigger_predictData = TriggerBuilder.Create()
                .WithIdentity("trigger2")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                .Build();

            // Schedule the job using the job and trigger 
            await scheduler.ScheduleJob(job_getData, trigger_getData);
            await scheduler.ScheduleJob(job_PredictData, trigger_predictData);

        }

        /* public async bool checkScheduleStart()
         {
             scheduler = await schedulerFactory.GetScheduler();
             return scheduler.IsStarted;


         }*/

        public void Stop()
        {
            if (scheduler.IsStarted)
            {
                scheduler.Shutdown();
            }
        }
    }
}
