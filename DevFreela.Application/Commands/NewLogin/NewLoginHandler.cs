using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.NewLogin
{
    public class NewLoginHandler : IRequestHandler<NewLoginCommand, ResultViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public NewLoginHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel> Handle(NewLoginCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.Password);

            var user = await _userRepository.GetUser(request.Email, hash);
            if (user is null)
            {
                return ResultViewModel.Error("Erro ao realizar login");
            }

            var token = _authService.GenerateToken(request.Email, request.Role);

            var loginViewModel = new LoginViewModel(token);

            return ResultViewModel<LoginViewModel>.Success(loginViewModel);
        }
    }
}
