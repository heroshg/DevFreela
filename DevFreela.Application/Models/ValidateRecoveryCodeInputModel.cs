﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Models
{
    public class ValidateRecoveryCodeInputModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
