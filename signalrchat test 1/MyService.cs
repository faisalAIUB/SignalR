using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;


namespace signalrchat_test_1
{
    public class MyService
    {
        public static void Start()
        {
           //Task<IScheduler> scheduler = StdSchedulerFactory.GetDefaultScheduler();
           // scheduler.Start();

           // IJobDetail job = JobBuilder.Create<MyJob>().Build();

           // ITrigger trigger = TriggerBuilder.Create()
           //     .WithDailyTimeIntervalSchedule
           //       (s =>
           //          s.WithIntervalInHours(24)
           //         .OnEveryDay()
           //         .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
           //       )
           //     .Build();

           // scheduler.ScheduleJob(job, trigger);
        }
    }
}