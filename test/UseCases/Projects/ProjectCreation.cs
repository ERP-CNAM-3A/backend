using Application.UseCases.Projects.Commands;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace UseCases.Projects
{
    public class CreateProject_CommandHandlerTests
    {
        private readonly IMediator _mediator;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IRessourceRepository> _ressourceRepositoryMock;

        public CreateProject_CommandHandlerTests()
        {
            // Setup Dependency Injection
            var services = new ServiceCollection();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _ressourceRepositoryMock = new Mock<IRessourceRepository>();

            services.AddSingleton(_projectRepositoryMock.Object);
            services.AddSingleton(_ressourceRepositoryMock.Object);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProject_CommandHandler).Assembly));

            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Should_Create_Project_And_Return_It_With_Valid_Data()
        {
            // Arrange
            var command = new CreateProject_Command(
                "Salut c zizou",
                ProjectType.Development,
                SaleType.Order,
                DateTime.UtcNow.AddDays(30),
                15,
                ProjectStatus.Inactive,
                new Ressource[] { }
            );

            // Act
            var result = await _mediator.Send(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.name, result.Name);
            Assert.Equal(command.projectType, result.ProjectType);
            Assert.Equal(command.saleType, result.SaleType);
            Assert.Equal(command.dueDate, result.DueDate);
            Assert.Equal(command.daysRequired, result.DaysRequired);
            Assert.Equal(command.status, result.Status);
            Assert.Equal(command.ressources, result.Ressources);

            // Verify that the repository's Add method was called
            _projectRepositoryMock.Verify(repo => repo.Add(It.Is<Project>(p =>
                p.Name == command.name &&
                p.ProjectType == command.projectType &&
                p.SaleType == command.saleType &&
                p.DueDate == command.dueDate &&
                p.DaysRequired == command.daysRequired &&
                p.Status == command.status &&
                p.Ressources == command.ressources
            )), Times.Once);
        }

    }
}
