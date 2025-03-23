using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ResultViewModel>
    {
        private readonly IMemoryCache _cache;
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public ChangePasswordHandler(IMemoryCache cache, IUserRepository repository, IAuthService authService)
        {
            _cache = cache;
            _repository = repository;
            _authService = authService;
        }

        public async Task<ResultViewModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = $"RecoveryCode:{request.Email}";
            if (!_cache.TryGetValue(cacheKey, out string? code) || code != request.Code)
            {
                return ResultViewModel.Error("Erro");
            }

            _cache.Remove(cacheKey);

            var user = await _repository.GetUserByEmail(request.Email);

            if (user is null)
            {
                return ResultViewModel.Error("Erro");
            }

            var hash = _authService.ComputeHash(request.NewPassword);
            user.UpdatePassword(hash);
            await _repository.Update();
            return ResultViewModel.Success();
        }
    }
}
