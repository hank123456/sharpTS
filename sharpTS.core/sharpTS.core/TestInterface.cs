using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using NLog;
using Microsoft.Win32.SafeHandles;
using System.Threading;
using System.Diagnostics;

namespace sharpTS.core
{
    public partial class TestInterface : IDisposable
    {
        private bool _disposed;
        private bool _dutIsInitialized;
        private bool _swIsInitialized;
        private string _confFile;

        private static Logger _logger;
        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Initializes the test system. Reads configurations, sets logging, initializes instruments etc.
        /// </summary>
        /// <param name="confFile">The configuration file </param>
        /// <param name="logLevel">The logging level</param>
        public void Initialize(string confFile, int logLevel)
        {
            try
            {
                if (_swIsInitialized) throw new ApplicationException("Software is already initialized!");

                Thread.CurrentThread.CurrentCulture     = System.Globalization.CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture   = System.Globalization.CultureInfo.InvariantCulture;

                InitializeLogging();

                var sw = Stopwatch.StartNew();

                _logger.Log(LogLevel.Info, "Initializing using configuration file: " + confFile);
                _logger.Log(LogLevel.Info, "Starting initializing......");

                _confFile = confFile;

            }catch(Exception ex)
            {
                
            }
            
        }
        private void InitializeLogging()
        {
            if (null == LogManager.Configuration) // if cant find config file, use below config.
            {
                var config = new NLog.Config.LoggingConfiguration();
                var logfile = new NLog.Targets.FileTarget() 
                { 
                    FileName = "", 
                    Name = "logfile" 
                };
                var logconsole = new NLog.Targets.ConsoleTarget() 
                { 
                    Name = "logconsole" 
                };

                config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Info, logconsole));
                config.LoggingRules.Add(new NLog.Config.LoggingRule("*", LogLevel.Info, logfile));

                LogManager.Configuration = config;//active
            }
            _logger = LogManager.GetCurrentClassLogger();
            _logger.Info("Logging initialized!");
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _handle.Dispose();
                //
                
            }
            _disposed = true;
        }
    }
}
