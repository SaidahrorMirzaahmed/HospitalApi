using HospitalApi.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenge.Domain.Commons;
using Tenge.Service.Helpers;

namespace Tenge.Service.Extensions;

public static class AuditableExtension
{
    public static void Create(this Auditable auditable)
    {
        auditable.CreatedAt = DateTime.UtcNow;
        auditable.CreatedByUserId = HttpContextHelper.UserId;
    }

    public static void Update(this Auditable auditable)
    {
        auditable.UpdatedAt = DateTime.UtcNow;
        auditable.UpdatedByUserId = HttpContextHelper.UserId;
    }

    public static void Delete(this Auditable auditable)
    {
        auditable.DeletedAt = DateTime.UtcNow;
        auditable.DeletedByUserId = HttpContextHelper.UserId;
    }
}