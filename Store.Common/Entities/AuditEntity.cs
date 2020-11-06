using Microsoft.EntityFrameworkCore;
using System;

namespace Store.Common.Entities
{
    public class AuditEntity : AutoHistory
    {
        public Guid User { get; set; }
    }
}