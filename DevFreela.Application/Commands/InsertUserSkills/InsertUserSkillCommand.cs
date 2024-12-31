using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkills
{
    public class InsertUserSkillCommand : IRequest<ResultViewModel<int>>
    {
        public int[] SkillsIds { get; set; }
        public int Id { get; set; }
    }
}
