﻿using log4net;
using log4net.Config;

namespace TrialTask_Bees.Logging
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