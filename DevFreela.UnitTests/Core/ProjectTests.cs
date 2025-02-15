using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using FluentAssertions;

namespace DevFreela.UnitTests.Core
{
    public class ProjectTests
    {
        [Fact]
        //Given - when - then
        public void ProjectIsCreated_Start_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 1000);

            // Act
            project.Start();

            // Assert
            project.Status.Should().Be(ProjectStatusEnum.InProgress);
            project.StartedAt.Should().NotBeNull();

        }

        [Fact]
        public void ProjectIsInInvalidState_Start_ThrowsException()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 1000);
            project.Start();

            // Act + Assert
            Action? start = project.Start;

            start.Should().
                Throw<InvalidOperationException>()
                .WithMessage(Project.INVALID_STATE_MESSAGE);
        }

        [Fact]
        public void ProjectIsCreated_Complete_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 1000);
            project.Start();
            // Act
            project.Complete();

            // Assert
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.NotNull(project.CompletedAt);

            Assert.True(ProjectStatusEnum.Completed == project.Status);
            Assert.False(project.CompletedAt is null);
        }

        [Fact]
        public void ProjectIsInInvalidState_Complete_ThrowsException()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 1000);
            project.Start();
            project.Complete();

            // Act + Assert
            var complete = project.Complete;

            var exception = Assert.Throws<InvalidOperationException>(complete);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
            
        }

        [Fact]
        public void ProjectIsInProgress_SetPaymentPending_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 1000);
            project.Start();

            // Act 
            project.SetPaymentPending();

            // Assert
            Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
            Assert.True(ProjectStatusEnum.PaymentPending == project.Status);
        }

        [Fact]
        public void ProjectIsInInvalidState_SetPaymentPending_ThrowsException()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 1000);

            // Act
            var setPaymentPending = project.SetPaymentPending;


            // Assert
            var exception = Assert.Throws<InvalidOperationException>(setPaymentPending);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message );
        }

        [Fact]
        public void ProjectIsCreated_Update_Success()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do Projeto", 1, 2, 1000);
            var projectUpdated = new Project("Projeto A update", "Descrição do Projeto", 1, 2, 1000);
            // Act
            project.Update(projectUpdated.Title, projectUpdated.Description, projectUpdated.TotalCost);

            // Assert
            Assert.Equal(project.Title, projectUpdated.Title);
            Assert.Equal(project.Description, projectUpdated.Description);
            Assert.Equal(project.TotalCost, projectUpdated.TotalCost);

        }
    }
}
