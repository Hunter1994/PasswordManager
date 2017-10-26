using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using PM.Core.MultiTenant;
using PM.Core.Users;

namespace PM.Core.Features
{
    public class FeaturesValueStore:AbpFeatureValueStore<Tenant,User>
    {
        public FeaturesValueStore(
            ICacheManager cacheManager,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            IRepository<Tenant> tenantRepository,
            IRepository<EditionFeatureSetting, long> editionFeatureRepository, 
            IFeatureManager featureManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                cacheManager, 
                tenantFeatureRepository, 
                tenantRepository, 
                editionFeatureRepository, 
                featureManager,
                unitOfWorkManager)
        {
        }
    }
}
