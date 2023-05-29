using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWatcher.Application.Wrappers
{
    public  class ServiceResponseBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
