using Application;
using Infrastructure;
using Infrastructure.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Setup application mediator for MediatR.
        builder.Services.ApplicationMediator();

        // Setup infrastructure dependency injection.
        builder.Services.InfrastructureDependencyInjection();

        // Build the application.
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Register asynchronously once the app is ready
        app.Lifetime.ApplicationStarted.Register(async () =>
        {
            // Directly retrieve the service from app.Services
            var registrationService = app.Services.GetRequiredService<MiddlewareRegistrationService>();
            await registrationService.RegisterApiAsync();
        });

        // Run the application.
        await app.RunAsync();
    }
}