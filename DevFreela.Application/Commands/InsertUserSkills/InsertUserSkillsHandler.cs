using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    public class InsertUserSkillsHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel<int>>
    {
        private readonly IUserSkillsRepository _repository;
        public InsertUserSkillsHandler(IUserSkillsRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
            var userSkills =  request.SkillsIds
                .Select(s => new UserSkill(request.Id, s))
                .ToList();

            await _repository.AddRange(userSkills);

            return ResultViewModel<int>.Success(request.Id);
        }
    }
}
