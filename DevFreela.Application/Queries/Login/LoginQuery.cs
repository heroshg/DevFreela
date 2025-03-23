using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.Login
{
    public class LoginQuery
    {
        public string Token { get; set; }

        public LoginQuery(string token)
        {
            Token = token;
        }
    }
}
