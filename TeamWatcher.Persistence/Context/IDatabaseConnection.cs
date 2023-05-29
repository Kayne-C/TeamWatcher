using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWatcher.Persistence.Context
{
    public interface IDatabaseConnection
    {
        void Connect();
        void Disconnect();
    }
}
