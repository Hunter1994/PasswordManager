using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PM.Core.Users;

namespace PM.Application.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto:EntityDto<long>
    {
        [Required]
        [StringLength(User.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [StringLength(User.MaxEmailAddressLength)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public string[] Roles { get; set; }

    }
}
