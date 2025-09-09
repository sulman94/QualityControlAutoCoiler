using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Logger
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LoggerManager));
        public LoggerManager()
        {
            try
            {
                XmlDocument log4netConfig = new XmlDocument();

                var configFile = GetConfigFile();
                //log4netConfig.Load(fs);

                var repo = LogManager.CreateRepository(
                        Assembly.GetEntryAssembly(),
                        typeof(log4net.Repository.Hierarchy.Hierarchy));

                XmlConfigurator.Configure(repo, configFile);

                // The first log to be written 
                _logger.Info("Log System Initialized");

            }
            catch (Exception ex)
            {
                _logger.Error("Error", ex);
            }
        }
        // Logging functionality happens here
        public void LogInformation(string message)
        {
            _logger.Info(message);
        }


        private static FileInfo GetConfigFile()
        {
            FileInfo configFile = null;

            // Search config file
            var configFileNames = new[] { "Config/log4net.config", "log4net.config" };

            foreach (var configFileName in configFileNames)
            {
                configFile = new FileInfo(configFileName);

                if (configFile.Exists) break;
            }

            // https://stackoverflow.com/questions/26545919/sql-jobs-or-task-scheduler-call-log4net-does-not-write-log-file/34072145
            if (configFile == null || !configFile.Exists)
            {
                var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
                var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "Config/log4net.config");
                configFile = new FileInfo(log4NetConfigFilePath);
            }

            if (configFile == null || !configFile.Exists) throw new NullReferenceException("Log4net config file not found.");
            return configFile;
        }

    }
}
