using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWatcher.Application.Wrappers
{
    public class ServiceResponse<T>:ServiceResponseBase
    {
        public T Data { get; set; }
    }
}
