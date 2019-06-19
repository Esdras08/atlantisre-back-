namespace Inexa.Atlantis.Re.Commons.Infras.Logging
{
    public static class LoggingFactory
    {
        private static ILogger _logger;

        public static void InitializeLogFactory(ILogger logger)
        {
            _logger = logger;
        }

        public static ILogger GetLogger()
        {
            return _logger ?? (_logger = new Log4NetAdapter());
        }
    }
}
