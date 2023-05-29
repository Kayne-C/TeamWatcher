using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWatcher.Domain.Common;

namespace TeamWatcher.Domain.Entities
{
    public class Team : BaseEntity
    { 
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }
}
