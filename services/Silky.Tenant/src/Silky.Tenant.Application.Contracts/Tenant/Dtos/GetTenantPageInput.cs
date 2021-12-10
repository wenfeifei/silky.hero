﻿using Silky.Hero.Common.Dtos;
using Silky.Hero.Common.Enums;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class GetTenantPageInput : PageDtoBase
{
    public string Name { get; set; }

    public Status? Status { get; set; }
}