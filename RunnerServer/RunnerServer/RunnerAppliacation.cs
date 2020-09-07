using ExitGames.Diagnostics.Monitoring;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using Photon.SocketServer;
using RunnerServer.Cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerServer
{
    class RunnerAppliacation : ApplicationBase
    {
        private static ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public static void Log(string str)
        {
            log.Info(str.ToString());
        }
        
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new RunnerClient(initRequest);
        }

        protected override void Setup()
        {
            InitLogging();
        }

        protected override void TearDown()
        {

        }

        protected virtual void InitLogging()
        {
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = "Runner" + this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
        }
    }
}
