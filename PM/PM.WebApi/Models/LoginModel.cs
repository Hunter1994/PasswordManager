﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.WebApi.Models
{
    public class LoginModel
    {
        public string TenancyName { get; set; }
        [Required]
        public string UsernameOrEmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
