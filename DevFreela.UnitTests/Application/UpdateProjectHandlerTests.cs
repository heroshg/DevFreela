using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class UpdateProjectHandlerTests 
    {
        [Fact]
        public async Task ProjectExists_Update_IsSuccess_NSubstitute()
        {
            // Arrange
            var project = new Project("Projeto A", "Descricao", 1, 2, 50000);
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new UpdateProjectHandler(repository);

            var command = new UpdateProjectCommand(1, project.Title, project.Description, project.TotalCost);

            // Act
            var result = await handler.Handle(command, new CancellationToken());


            // Assert
            Assert.True(result.IsSuccess);
            await repository.Received(1).GetById(Arg.Any<int>());
            await repository.Received(1).Update(Arg.Is<Project>(p =>
            p.Title == project.Title &&
            p.Description == project.Description &&
            p.TotalCost == project.TotalCost
            ));
        }

        [Fact]
        public async Task ProjectDoesNotExists_Update_Error_Substitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult<Project?>(null));

            var handler = new UpdateProjectHandler(repository);

            var command = new UpdateProjectCommand(1,"Projeto A", "Descricao", 50000);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSuccess);
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }
    }
}
