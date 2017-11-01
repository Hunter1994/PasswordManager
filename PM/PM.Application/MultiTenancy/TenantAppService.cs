using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using PM.Application.MultiTenancy.Dto;
using PM.Core.Authorization.Roles;
using PM.Core.Editions;
using PM.Core.MultiTenant;
using PM.Core.Users;

namespace PM.Application.MultiTenancy
{
    public class TenantAppService :  AsyncCrudAppService<Tenant, TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        public TenantAppService(
            IRepository<Tenant, int> repository,
            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,
            RoleManager roleManager,
            IAbpZeroDbMigrator abpZeroDbMigrator
            ) : base(repository)
        {
            _editionManager = editionManager;
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
        }

        public override async Task<TenantDto> Create(CreateTenantDto input)
        {
            CheckCreatePermission();



        }
    }
}
