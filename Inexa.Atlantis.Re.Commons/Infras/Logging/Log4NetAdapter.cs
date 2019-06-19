using Inexa.Atlantis.Re.Commons.Infras.Configuration;
using log4net;
using log4net.Config;

namespace Inexa.Atlantis.Re.Commons.Infras.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly ILog _log;








        public Log4NetAdapter()
        {
            var config = new WebConfigApplicationSettings();
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(config.LoggerName);
        }

        public void Log(string message)
        {
            _log.Info(message);
        }
    }
}
