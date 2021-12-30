using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Extensions;
using IdentityRole = Silky.Identity.Domain.IdentityRole;

namespace Silky.Identity.Application.Role;

public class RoleAppService : IRoleAppService
{
    private readonly IdentityRoleManager _roleManager;
    private readonly ISession _session;

    public RoleAppService(IdentityRoleManager roleManager)
    {
        _roleManager = roleManager;
        _session = NullSession.Instance;
    }

    [UnitOfWork]
    public async Task CreateAsync(CreateRoleInput input)
    {
        var role = new IdentityRole(input.Name, input.RealName, _session.TenantId);
        await UpdateRoleByInput(role, input);
        (await _roleManager.CreateAsync(role)).CheckErrors();
    }

    public async Task UpdateAsync(UpdateRoleInput input)
    {
        var role = await _roleManager.GetByIdAsync(input.Id);
        await UpdateRoleByInput(role, input);
        (await _roleManager.UpdateAsync(role)).CheckErrors();
    }

    public async Task<GetRoleOutput> GetAsync(long id)
    {
        var role = await _roleManager.GetByIdAsync(id);
        return role.Adapt<GetRoleOutput>();
    }

    public async Task DeleteAsync(long id)
    {
        var role = await _roleManager.GetByIdAsync(id);
        (await _roleManager.DeleteAsync(role)).CheckErrors();
    }

    public async Task<PagedList<GetRolePageOutput>> GetPageAsync(GetRolePageInput input)
    {
        var pageRoles = await _roleManager.RoleRepository
            .Where(!input.Name.IsNullOrEmpty(),
                p => p.Name.Contains(input.Name, StringComparison.CurrentCultureIgnoreCase))
            .Where(!input.RealName.IsNullOrEmpty(),
                p => p.RealName.Contains(input.RealName))
            .ProjectToType<GetRolePageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return pageRoles;
    }

    private async Task UpdateRoleByInput(IdentityRole role, RoleDtoBase input)
    {
        role.IsDefault = input.IsDefault;
        role.IsPublic = input.IsPublic;
        role.Sort = input.Sort;
        (await _roleManager.SetRoleNameAsync(role, input.Name)).CheckErrors();
        (await _roleManager.SetRoleRealNameAsync(role, input.RealName)).CheckErrors();
    }
}