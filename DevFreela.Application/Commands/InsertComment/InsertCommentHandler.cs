﻿using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
       
        private readonly IProjectRepository _repository;
        public InsertCommentHandler( IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.IdProject);

            if (project is null)
            {
                return ResultViewModel.Error("O projeto não existe");
            }

            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _repository.AddComment(comment);
            return ResultViewModel.Success();
        }
    }
}
