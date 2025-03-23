using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUserLogin
{
    public class GetUserLoginHandler : IRequestHandler<GetUserLoginQuery, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        public GetUserLoginHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(GetUserLoginQuery request, CancellationToken cancellationToken)
        {

            var user = await _repository.GetUser(request.Email, request.Password);
            
            if(user is null)
            {
                return ResultViewModel.Error("Credênciais não encontradas");
            }
            return ResultViewModel.Success();
        }
    }
}
