namespace BackendTask1.Services
{
    using Quartz;
    using Quartz.Impl;
    using System;
    using System.ServiceProcess;
    using System.Threading.Tasks;

    /// <summary>
    /// Window's service
    /// </summary>
    public partial class WindowService : ServiceBase
    {
        public Action<string> Message { get; set; }

        public WindowService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var task = Start();
            task.Wait();
        }

        private async Task Start()
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

        protected override void OnStop()
        {

        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "ApiWindowService";
        }
    }
}