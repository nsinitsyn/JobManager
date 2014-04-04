using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Tests.Utilities
{
    public static class Logger
    {
        public static readonly ILog Log = LogManager.GetLogger("MainLogger");  

        static Logger()
        {
            XmlConfigurator.Configure();
        }
    }
}
