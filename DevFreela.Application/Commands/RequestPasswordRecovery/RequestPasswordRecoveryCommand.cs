using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.RequestPasswordRecovery
{
    public class RequestPasswordRecoveryCommand : IRequest<ResultViewModel>
    {
        public string Email { get; set; }
    }
}
