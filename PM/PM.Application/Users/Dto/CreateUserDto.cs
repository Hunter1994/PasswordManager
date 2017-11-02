using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.AutoMapper;
using PM.Core.Users;

namespace PM.Application.Users.Dto
{
    [AutoMapTo(typeof (User))]
    public class CreateUserDto
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

        public string[] RoleNames { get; set; }

        [Required]
        [StringLength(User.MaxPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }
    }
}
