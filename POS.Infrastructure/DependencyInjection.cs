﻿using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Interfaces.Persistence;
using POS.Application.Interfaces.Services;
using POS.Infrastructure.Persistence.Contexts;
using POS.Infrastructure.Persistence.Repositories;
using POS.Infrastructure.Services;

namespace POS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(ApplicationDbContext).Assembly.FullName;

        services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("Connection"),
                b => b.MigrationsAssembly(assembly)));

        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        // Registro de dependencias de repositorios y servicios
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
        //services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IGenerateExcelService, GenerateExcelService>();
        services.AddTransient<IGeneratePdfService, GeneratePdfService>();

        // Registrar IConverter con SynchronizedConverter para la generación de PDF
        services.AddSingleton<IConverter, SynchronizedConverter>(provider =>
            new SynchronizedConverter(new PdfTools()));

        return services;
    }
}
