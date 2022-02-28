using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;
using Silky.Saas.Application.Contracts.Edition.Dtos;
using Silky.Saas.Domain.Shared;

namespace Silky.Saas.Application.Contracts.Edition;

[ServiceRoute]
[Authorize]
public interface IEditionAppService
{
    /// <summary>
    /// 新增版本
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(SaasPermissions.Editions.Create)]
    [RemoveCachingIntercept(typeof(ICollection<GetEditionOutput>),"list",IgnoreMultiTenancy = true)]
    Task CreateAsync(CreateEditionInput input);

    /// <summary>
    /// 更新版本
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(SaasPermissions.Editions.Update)]
    [RemoveCachingIntercept(typeof(GetEditionEditOutput),"id:{0}",IgnoreMultiTenancy = true)]
    [RemoveCachingIntercept(typeof(ICollection<GetEditionOutput>),"list",IgnoreMultiTenancy = true)]
    Task UpdateAsync(UpdateEditionInput input);

    /// <summary>
    /// 删除版本信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(SaasPermissions.Editions.Delete)]
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetEditionEditOutput),"id:{0}",IgnoreMultiTenancy = true)]
    [RemoveCachingIntercept(typeof(ICollection<GetEditionOutput>),"list",IgnoreMultiTenancy = true)]
    Task DeleteAsync(long id);
    
    /// <summary>
    /// 实现检查版本是否存在接口
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("check")]
    Task<bool> CheckAsync(CheckEditionInput input);

    /// <summary>
    /// 分页查询版本信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetEditionPageOutput>> GetPageAsync(GetEditionPageInput input);

    /// <summary>
    /// 设置版本功能
    /// </summary>
    /// <param name="id">版本id</param>
    /// <param name="inputs">输入</param>
    /// <returns></returns>
    [HttpPut("{id:long}/features")]
    [Authorize(SaasPermissions.Editions.SetFeatures)]
    [RemoveCachingIntercept(typeof(GetEditionEditOutput),"id:{0}",IgnoreMultiTenancy = true)]
    [RemoveCachingIntercept(typeof(ICollection<GetEditionOutput>),"list",IgnoreMultiTenancy = true)]
    Task SetFeaturesAsync([CacheKey(0)]long id, ICollection<EditionFeatureDto> inputs);

    /// <summary>
    /// 通过Id获取版本信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}", IgnoreMultiTenancy = true)]
    Task<GetEditionEditOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 获取版本列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    [GetCachingIntercept("list", IgnoreMultiTenancy = true)]
    Task<ICollection<GetEditionOutput>> GetListAsync();

    /// <summary>
    /// 根据功能编码获取当前租户的功能特性
    /// </summary>
    /// <param name="featureCode"></param>
    /// <returns></returns>
    [GetCachingIntercept("featureCode:{0}")]
    [ProhibitExtranet]
    Task<GetEditionFeatureOutput> GetEditionFeatureAsync([CacheKey(0)]string featureCode);
    
    [GetCachingIntercept("featureCode:{0}:tenantId:{1}")]
    [ProhibitExtranet]
    Task<GetEditionFeatureOutput> GetTenantEditionFeatureAsync([CacheKey(0)]string featureCode,[CacheKey(1)]long tenantId);
}