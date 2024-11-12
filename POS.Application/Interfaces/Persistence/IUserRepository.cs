using POS.Domain.Entities;

namespace POS.Application.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> UserByEmail(string email);
    Task<User> UserByUsername(string username);
}