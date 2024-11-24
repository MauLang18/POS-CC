namespace POS.Application.Interfaces.Services;

public interface IVaultSecretService
{
    Task<string> GetSecret(string secretPath);
}