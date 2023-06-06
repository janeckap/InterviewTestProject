using External.ThirdParty.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using TranslationManagement.Application.Repositories;
using TranslationManagement.Application.Services;
using TranslationManagement.Application.Services.FileProcessing;
using TranslationManagement.Application.Services.UnreliableNotification;
using TranslationManagement.Data;

namespace TranslationManagement.Api
{
    public class Program
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<UnreliableNotificationService>();
            services.AddScoped<IUnreliableNotificationWrapper, UnreliableNotificationWrapper>();
            services.AddScoped<IFileProcessorFactory, FileProcessorFactory>();
            services.AddScoped<ITranslatorRepository, TranslatorRepository>();
            services.AddScoped<ITranslationJobRepository, TranslationJobRepository>();
            services.AddScoped<ITranslatorService, TranslatorService>();
            services.AddScoped<ITranslationJobService, TranslationJobService>();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=TranslationAppDatabase.db"));

            RegisterServices(builder.Services);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
