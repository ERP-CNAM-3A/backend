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

        // Register MiddlewareRegistrationService in the dependency injection container.
        builder.Services.AddSingleton<MiddlewareRegistrationService>();

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

        // Middleware registration AFTER the application is fully built
        // Using an IHost callback that runs after the app is ready
        var scope = app.Services.CreateScope();
        var registrationService = scope.ServiceProvider.GetRequiredService<MiddlewareRegistrationService>();

        // Register asynchronously once the app is ready
        app.Lifetime.ApplicationStarted.Register(async () =>
        {
            await registrationService.RegisterApiAsync(); // Call the registration service
        });

        // Run the application.
        await app.RunAsync();
    }
}