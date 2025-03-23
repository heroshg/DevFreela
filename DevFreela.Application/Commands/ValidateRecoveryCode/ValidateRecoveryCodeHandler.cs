using DevFreela.Application.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ValidateRecoveryCode
{
    public class ValidateRecoveryCodeHandler : IRequestHandler<ValidateRecoveryCodeCommand, ResultViewModel>
    {
        private readonly IMemoryCache _cache;

        public ValidateRecoveryCodeHandler(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<ResultViewModel> Handle(ValidateRecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = $"RecoveryCode:{request.Email}";
            
            if (!_cache.TryGetValue(cacheKey, out string? code) || code != request.Code)
            {
                return ResultViewModel.Error("Falhou");
            }

            return ResultViewModel.Success();
        }
    }
}
