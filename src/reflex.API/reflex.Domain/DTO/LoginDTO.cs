﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.DTO
{
    public class LoginDTO
    {
        public string PhoneNumber { get; set; } 
        public OtpType otpType { get; set; }
    }
}
