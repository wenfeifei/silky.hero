﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Identity.Domain;

public interface IIdentityRoleRepository : IRepository<IdentityRole>
{
    Task<IdentityRole> FindByNormalizedNameAsync(
        string normalizedRoleName,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );

    Task<List<IdentityRole>> GetListAsync(
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        string filter = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );
    Task<List<IdentityRole>> GetListAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default
    );

    Task<List<IdentityRole>> GetDefaultOnesAsync(
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string filter = null,
        CancellationToken cancellationToken = default
    );
}