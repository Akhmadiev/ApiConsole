namespace BackendTask1
{
    using BackendTask1.Dtos;
    using BackendTask1.EntityModels;
    using BackendTask1.Interfaces.Country;
    using BackendTask1.Services;
    using BackendTask1.Services.Region;
    using System;
    using System.IO;
    using System.ServiceProcess;

    class Program
    {
        static void Main(string[] args)
        {
            //StartWindowService();
            //StartConsole();
        }

        static void StartWindowService()
        {
            var windowService = new WindowService();
            windowService.Message += MessageWindow;

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                windowService
            };
            ServiceBase.Run(ServicesToRun);
        }

        static void StartConsole()
        {
            var main = new Main();
            main.Message += MessageConsole;

            var regionService = main.Container.Resolve<IService>("RegionService");
            var countryService = main.Container.Resolve<IService>("CountryService");

            #region save regions

            //var parsingRegionResult = main.ParsingResult<RegionEntity, CountryDto>(regionService, "https://api.myjson.com/bins/nkcgg");
            //main.Save(regionService, parsingRegionResult);

            #endregion

            #region save countries

            //var parsingCountryResult = main.ParsingResult<CountryEntity, CountryDto>(regionService, "https://api.myjson.com/bins/nkcgg");
            //main.Save(countryService, parsingCountryResult);

            #endregion

            #region start background task

            //main.Start();
            //while (true) { }

            #endregion
        }

        static void MessageConsole(string data)
        {
            Console.Clear();
            Console.WriteLine(data);
        }

        static void MessageWindow(string data)
        {
            var path = $"C:\\Temp\\api_{DateTime.Now.ToString("dd.MM.yyyy hh.mm.ss")}.txt";

            using (var sw = File.CreateText(path))
            {
                sw.WriteLine(data);
            }
        }
    }
}