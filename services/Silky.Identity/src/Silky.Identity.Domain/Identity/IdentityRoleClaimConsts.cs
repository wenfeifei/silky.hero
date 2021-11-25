﻿namespace Silky.Identity.Domain;

public static class IdentityRoleClaimConsts
{
    public static int MaxClaimTypeLength { get; set; } = IdentityUserClaimConsts.MaxClaimTypeLength;

    public static int MaxClaimValueLength { get; set; } = IdentityUserClaimConsts.MaxClaimValueLength;
}