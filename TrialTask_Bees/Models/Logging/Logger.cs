using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees.Models.Logging
{
    public static class Logger
    {
        private static ILog log = LogManager.GetLogger("LOGGER");

        static Logger()
        {
            InitLogger();
        }

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}