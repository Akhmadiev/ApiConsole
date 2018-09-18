namespace BackendTask1.Services
{
    using System;
    using System.IO;
    using System.ServiceProcess;
    using System.Threading;

    /// <summary>
    /// Window's service
    /// </summary>
    public partial class WindowService : ServiceBase
    {
        public WindowService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var path = $"{Path.GetTempPath()}api_{DateTime.Now.ToString("dd.MM.yyyy hh.mm.ss")}.txt";

            using (var sw = File.CreateText(path))
            {
                sw.WriteLine("START");
            }

            var data = Api.Api.GetExternalResponse("https://api.myjson.com/bins/nkcgg");
            SaveInTemp(data.Result);

            Thread.Sleep(1000);
        }

        private void SaveInTemp(string data)
        {
            var path = $"{Path.GetTempPath()}api_{DateTime.Now.ToString("dd.MM.yyyy hh.mm.ss")}.txt";

            using (var sw = File.CreateText(path))
            {
                sw.WriteLine(data);
            }
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
            this.ServiceName = "Service12";
        }
    }
}