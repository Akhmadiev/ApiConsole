namespace BackendTask1
{
    using BackendTask1.Dtos;
    using BackendTask1.Interfaces.Country;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Api;
    using Castle.Windsor;
    using Quartz;
    using System;
    using Quartz.Impl;
    using BackendTask1.Services.Country;
    using BackendTask1.Services.Region;
    using Castle.MicroKernel.Registration;
    using System.IO;

    public class Main
    {
        public Action<string> Message { get; set; }

        private IWindsorContainer _container;

        public Main()
        {
            _container = new Castle.Windsor.WindsorContainer();

            RegisterServices(_container);
        }

        public async Task Start()
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            var job = JobBuilder.Create<JobScheduler>().Build();
            job.JobDataMap.Add("MessageAction", Message);

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Register services in container
        /// </summary>
        /// <param name="container"></param>
        public void RegisterServices(IWindsorContainer container)
        {
            container.Register(Component.For<ICountryService>().ImplementedBy<RegionService>().Named("RegionService"));
            container.Register(Component.For<ICountryService>().ImplementedBy<CountryService>().Named("CountryService"));
        }
    }
}
