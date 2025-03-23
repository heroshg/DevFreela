using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.RequestPasswordRecovery
{
    public class RequestPasswordRecoveryHandler : IRequestHandler<RequestPasswordRecoveryCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        public RequestPasswordRecoveryHandler(IUserRepository repository, IMemoryCache cache, IEmailService emailService)
        {
            _repository = repository;
            _cache = cache;
            _emailService = emailService;
        }

        public async Task<ResultViewModel> Handle(RequestPasswordRecoveryCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByEmail(request.Email);

            if (user is null)
            {
                return ResultViewModel.Error("Falhou");
            }

            var code = new Random().Next(100000, 999999).ToString();

            var cacheKey = $"RecoveryCode:{request.Email}";

            _cache.Set(cacheKey, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(user.Email, "Recovery Code", $"Your recovery code: {code}");
            return ResultViewModel.Success();
        }
    }
}
