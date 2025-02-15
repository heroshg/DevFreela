using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class InsertProjectHandlerTests
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Success_NSubstitute()
        {
            // Arrange
            const int ID = 1;
            var repository = Substitute.For<IProjectRepository>();
            repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(ID));

            var command = FakeDataHelper.CreateFakeInsertProjectCommand();
            var handler = new InsertProjectHandler(repository);
            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess);
            await repository.Received(1).Add(Arg.Any<Project>());
        }

        [Fact]
        public async Task InputDataAreOk_Insert_Success_Moq()
        {
            // Arrange
            const int ID = 1;


            var repository = Mock
                .Of<IProjectRepository>(r => r.Add(It.IsAny<Project>()) == Task.FromResult(ID));

            var command = new InsertProjectCommand
            {
                Title = "Project A",
                Description = "Descrição do Projeto",
                TotalCost = 20000,
                IdClient = 1,
                IdFreelancer = 2
            };


            var handler = new InsertProjectHandler(repository);

            // Act
            var result = await handler.Handle(command, new CancellationToken());


            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(ID);

            Mock.Get(repository).Verify(m => m.Add(It.IsAny<Project>()), Times.Once);
        }
    }
}
