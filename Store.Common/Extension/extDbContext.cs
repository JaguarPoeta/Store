using Microsoft.EntityFrameworkCore;
using Store.Common.Entities;
using System;

namespace Store.Common.Extension
{
    public static class extDbContext
    {
        public static void Auditoria(this DbContext db)
        {
            db.EnsureAutoHistory(() => new AuditEntity()
            {
                User = Guid.NewGuid()
            });

           
        }
    }
}