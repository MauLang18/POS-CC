﻿using POS.Application.Interfaces.Services;
using System.Net.Http.Headers;

namespace POS.Infrastructure.Services;

public class VaultSecretService : IVaultSecretService
{
    public async Task<string> GetSecret(string secretPath)
    {
        var vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN");
        if (string.IsNullOrEmpty(vaultToken))
        {
            throw new InvalidOperationException("Vault token is not provided.");
        }

        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("https://vault.customcodecr.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", vaultToken);

            var response = await httpClient.GetAsync($"/v1/{secretPath}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}