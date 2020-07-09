﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Volo.Abp.Identity.EntityFrameworkCore
{
    public class EFCoreIdentitySecurityLogRepository : EfCoreRepository<IIdentityDbContext, IdentitySecurityLog, Guid>, IIdentitySecurityLogRepository
    {
        public EFCoreIdentitySecurityLogRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<IdentitySecurityLog>> GetListAsync(
            string sorting = null,
            int maxResultCount = 50,
            int skipCount = 0,
            DateTime? startTime = null,
            DateTime? endTime = null,
            string applicationName = null,
            string identity = null,
            string action = null,
            string userName = null,
            string clientId = null,
            string correlationId = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            var query = GetListQuery(
                startTime,
                endTime,
                applicationName,
                identity,
                action,
                userName,
                clientId,
                correlationId
            );

            return await query.OrderBy(sorting ?? nameof(IdentitySecurityLog.CreationTime) + " desc")
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
            DateTime? startTime = null,
            DateTime? endTime = null,
            string applicationName = null,
            string identity = null,
            string action = null,
            string userName = null,
            string clientId = null,
            string correlationId = null,
            CancellationToken cancellationToken = default)
        {
            var query = GetListQuery(
                startTime,
                endTime,
                applicationName,
                identity,
                action,
                userName,
                clientId,
                correlationId
            );

            return  await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

          protected virtual IQueryable<IdentitySecurityLog> GetListQuery(
              DateTime? startTime = null,
              DateTime? endTime = null,
              string applicationName = null,
              string identity = null,
              string action = null,
              string userName = null,
              string clientId = null,
              string correlationId = null)
          {
              return DbSet.AsNoTracking()
                  .WhereIf(startTime.HasValue, securityLog => securityLog.CreationTime >= startTime)
                  .WhereIf(endTime.HasValue, securityLog => securityLog.CreationTime >= endTime)
                  .WhereIf(!applicationName.IsNullOrWhiteSpace(), securityLog => securityLog.ApplicationName == applicationName)
                  .WhereIf(!identity.IsNullOrWhiteSpace(), securityLog => securityLog.Identity == identity)
                  .WhereIf(!action.IsNullOrWhiteSpace(), securityLog => securityLog.Action == action)
                  .WhereIf(!userName.IsNullOrWhiteSpace(), securityLog => securityLog.UserName == userName)
                  .WhereIf(!clientId.IsNullOrWhiteSpace(), securityLog => securityLog.ClientId == clientId)
                  .WhereIf(!correlationId.IsNullOrWhiteSpace(), securityLog => securityLog.CorrelationId == correlationId);
          }
    }
}
