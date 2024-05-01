﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.DTO
{
    public class SignUpDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string UserName { get; set; }
        [Required]

        public string EmailAddress { get; set; }
        [Required]


        public string PhoneNumber { get; set; }

    }
}
