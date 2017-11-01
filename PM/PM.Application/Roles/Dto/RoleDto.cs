using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PM.Core.Authorization.Roles;

namespace PM.Application.Roles.Dto
{
    [AutoMapFrom(typeof(Role)),AutoMapTo(typeof(Role))]
    public class RoleDto:EntityDto<int>
    {
        [Required]
        [StringLength(Role.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(Role.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public bool IsStatic { get; set; }

        public List<string> Permissions { get; set; }
    }
}
