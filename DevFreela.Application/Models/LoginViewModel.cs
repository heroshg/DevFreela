﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Models
{
    public class LoginViewModel
    {
        public string Token { get; set; }

        public LoginViewModel(string token)
        {
            Token = token;
        }
    }
}
