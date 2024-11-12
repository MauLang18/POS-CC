﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using POS.Application.Interfaces.Services;

namespace POS.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveFile(string container, IFormFile file)
    {
        var webRootPath = _env.WebRootPath;
        var scheme = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? _httpContextAccessor.HttpContext?.Request.Scheme;
        var host = _httpContextAccessor.HttpContext!.Request.Host;

        return await SaveFileAsync(container, file, webRootPath, scheme!, host.Value);
    }

    public async Task<string> EditFile(string container, IFormFile file, string route)
    {
        var webRootPath = _env.WebRootPath;
        var scheme = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? _httpContextAccessor.HttpContext?.Request.Scheme;
        var host = _httpContextAccessor.HttpContext!.Request.Host;

        return await EditFileAsync(container, file, route, webRootPath, scheme!, host.Value);
    }

    public async Task RemoveFile(string route, string container)
    {
        var webRootPath = _env.WebRootPath;

        await RemoveFileAsync(route, container, webRootPath);
    }

    private static async Task<string> SaveFileAsync(string container, IFormFile file, string webRootPath, string scheme, string host)
    {
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        string folder = Path.Combine(webRootPath, container);

        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, fileName);

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var content = memoryStream.ToArray();
            await File.WriteAllBytesAsync(path, content);
        }

        var currentUrl = $"{scheme}://{host}";
        var pathDb = Path.Combine(currentUrl, container, fileName).Replace("\\", "/");
        return pathDb;
    }

    private static async Task<string> EditFileAsync(string container, IFormFile file, string route, string webRootPath, string scheme, string host)
    {
        if (route is not null)
            await RemoveFileAsync(route, container, webRootPath);

        return await SaveFileAsync(container, file, webRootPath, scheme, host);
    }

    private static Task RemoveFileAsync(string route, string container, string webRootPath)
    {
        if (!string.IsNullOrEmpty(route)) return Task.CompletedTask;

        var fileName = Path.GetFileName(route);

        var directoryFile = Path.Combine(webRootPath, container, fileName);

        if (File.Exists(directoryFile)) File.Delete(directoryFile);

        return Task.CompletedTask;
    }
}