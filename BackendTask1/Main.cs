namespace BackendTask1
{
    using BackendTask1.Interfaces.Country;
    using System.Threading.Tasks;
    using Castle.Windsor;
    using Quartz;
    using System;
    using Quartz.Impl;
    using BackendTask1.Services.Region;
    using Castle.MicroKernel.Registration;
    using BackendTask1.Dtos;
    using BackendTask1.EntityModels;
    using System.Collections.Generic;

    /// <summary>
    /// Main class for work with save, parse data
    /// </summary>
    public class Main
    {
        public Action<string> Message { get; set; }

        public IWindsorContainer Container;

        public Main()
        {
            Container = new Castle.Windsor.WindsorContainer();

            RegisterServices(Container);
        }

        /// <summary>
        /// Start background task which every one minute return result from public api in string format
        /// </summary>
        public async void Start()
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            var job = JobBuilder.Create<JobScheduler>().Build();
            job.JobDataMap.Add("MessageAction", Message);

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Save entities in database
        /// </summary>
        public void Save<T>(IService service, List<T> entities) where T : Entity
        {
            service.Save(entities);
        }

        /// <summary>
        /// Save entity in database
        /// </summary>
        public void Save<T>(IService service, T entity) where T : Entity
        {
            service.Save(entity);
        }

        /// <summary>
        /// Parsing data from public Api to entity<T>
        /// </summary>
        public List<T> ParsingResult<T, T2>(IService service, string url) where T : Entity where T2 : class
        {
            var data = Api.Api.GetData<T2>(url);

            return service.ParsingResult<T, T2>(data);
        }

        /// <summary>
        /// Register services in container
        /// </summary>
        /// <param name="container"></param>
        public void RegisterServices(IWindsorContainer container)
        {
            container.Register(Component.For<IService>().ImplementedBy<RegionService>().Named("RegionService"));
            container.Register(Component.For<IService>().ImplementedBy<CountryService>().Named("CountryService"));
        }
    }
}
