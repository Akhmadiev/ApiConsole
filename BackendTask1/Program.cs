namespace BackendTask1
{
    using BackendTask1.Services;
    using System;
    using System.ServiceProcess;

    class Program
    {
        static void Main(string[] args)
        {
            #region service

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new WindowService()
            //};
            //ServiceBase.Run(ServicesToRun);

            #endregion

            #region console

            var main = new Main();
            main.Message += Message;

            main.Start2();

            //var t = main.Start();
            //while (true) { }

            #endregion
        }

        static void Message(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }
    }
}