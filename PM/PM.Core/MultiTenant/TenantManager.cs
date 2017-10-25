using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using PM.Core.Users;

namespace PM.Core.MultiTenant
{
    public class TenantManager:AbpTenantManager<Tenant,User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            AbpEditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore)
            : base(tenantRepository, 
                  tenantFeatureRepository, 
                  editionManager, 
                  featureValueStore)
        {
        }
    }
}
