using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Common.Interfaces
{
   public interface IAudit
    {
        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}
