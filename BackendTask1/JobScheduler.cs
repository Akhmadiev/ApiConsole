namespace BackendTask1
{
    using System;
    using System.Threading.Tasks;
    using BackendTask1.Services;
    using Quartz;
    using static BackendTask1.Main;

    public class JobScheduler : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var data = await Api.Api.GetExternalResponse("https://api.myjson.com/bins/nkcgg");

            var messageAction = (Action<string>)context.JobDetail.JobDataMap.Get("MessageAction");

            messageAction?.Invoke(data);
        }
    }
}