using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.XPath;
using NLog;

namespace sharpTS.core
{
    public partial class TestInterface
    {
        private readonly Dictionary<string,AssemblyInfo>


        protected virtual XElement LoadConfFile()
        {
            var mainElement = XDocument.Load(_confFile).Root;
            if (null == mainElement)
                throw new ApplicationException("Error in loading configuration file: " + _confFile);

            return mainElement;
        }




        private struct AssemblyInfo
        {
            public Assembly AssyPointer;
            public IApplicationControl AppCtrl;
            public bool Initialize { get; set; }
            public bool Terminate { get; set; }
            public bool InitializeDut { get; set; }
            public bool UninitializeDut { get; set; }
        }
    }
}
