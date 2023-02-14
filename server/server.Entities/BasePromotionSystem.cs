using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities
{
    public class BasePromotionSystem
    {
        public Logger _log;
        public BasePromotionSystem(Logger log) { _log = log; }
    }
}
