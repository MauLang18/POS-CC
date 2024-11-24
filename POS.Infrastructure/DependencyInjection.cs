﻿using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using POS.Application.Commons.Config;
using POS.Application.Interfaces.Authentication;
using POS.Application.Interfaces.Persistence;
using POS.Application.Interfaces.Services;
using POS.Infrastructure.Authentication;
using POS.Infrastructure.Persistence.Contexts;
using POS.Infrastructure.Persistence.Repositories;
using POS.Infrastructure.Services;

namespace POS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var secretService = serviceProvider.GetRequiredService<IVaultSecretService>();

        var secretJson = secretService.GetSecret("CustomCodeAPI/data/ConnectionStrings").GetAwaiter().GetResult();
        var SecretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
        var Config = SecretResponse?.Data?.Data;

        var assembly = typeof(ApplicationDbContext).Assembly.FullName;

        services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(Config!.Connection,
                b => b.MigrationsAssembly(assembly)));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IPurchaseDetailRepository, PurchaseDetailRepository>();
        services.AddScoped<IQuoteDetailRepository, QuoteDetailRepository>();
        services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICodeRepository, CodeRepository>();

        services.AddTransient<IOrderingQuery, OrderingQuery>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IFileStorageService, FileStorageService>();
        services.AddTransient<IGenerateCodeService, GenerateCodeService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IGenerateExcelService, GenerateExcelService>();
        services.AddTransient<IGeneratePdfService, GeneratePdfService>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddSingleton<IConverter, SynchronizedConverter>(provider =>
            new SynchronizedConverter(new PdfTools()));

        return services;
    }
}
