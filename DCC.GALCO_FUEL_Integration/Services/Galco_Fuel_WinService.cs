using DCC.GALCO_FUEL_Integration.Controllers;
using System.ComponentModel;
using System.Diagnostics;

namespace DCC.GALCO_FUEL_Integration.Services
{
    public class Galco_Fuel_WinService : BackgroundService
    {
        DocumentCreation _obj;
        string IsDebug = string.Empty;
        private readonly Common xcommon;


        public Galco_Fuel_WinService(IConfiguration configuration)
        {
            _obj = new DocumentCreation(configuration);
            xcommon = new Common(configuration);
            IsDebug = configuration["AppConfig:IsDebug"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (IsDebug == "Y")
                xcommon.ErrorLog("Services Started at : " + DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (IsDebug == "Y")
                        xcommon.ErrorLog("ExecuteAsync step 1 start : " + DateTimeOffset.Now);

                    var res = _obj.CreateTargetDocument();

                    if (IsDebug == "Y")
                        xcommon.ErrorLog("ExecuteAsync step 2 completed : " + DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    xcommon.ErrorLog("ExecuteAsync exception : " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            if (IsDebug == "Y")
                xcommon.ErrorLog("Service stopped at: " + DateTimeOffset.Now);

            return base.StopAsync(stoppingToken);
        }

        /*
        public void WriteMessage(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.ToShortDateString().Replace('/', '_') + ".txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(message);
                    }
                }
                else
                {
                    using (var fileStream = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(fileStream))
                        {
                            writer.WriteLine($"{DateTime.Now} : {message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine("Exception = " + ex.Message);
                }
            }
        }

       */

    }
}
