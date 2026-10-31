using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program {
    static void Main(string[] args) {
        // 1. Initialiseer de WebApplication builder
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // Forceer de applicatie om op jouw specifieke poort te luisteren (http://localhost:5050)
        app.Urls.Add("http://localhost:5050");
        Console.WriteLine("http://localhost:5050/api/student");
        Console.WriteLine("http://localhost:5050/api/user");
        Console.WriteLine("http://localhost:5050/api/character");
        Console.WriteLine("http://localhost:5050/swagger");
        // 4. ACTIVEER CONTROLLERS: Zorg dat de routes naar je controllers werken
        app.MapControllers();
        app.UseHttpsRedirection();
        app.Run();
    }
}