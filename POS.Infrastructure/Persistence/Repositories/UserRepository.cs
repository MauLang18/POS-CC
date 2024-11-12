using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

namespace POS.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> UserByEmail(string email)
    {
        var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email!.Equals(email));
        return user!;
    }

    public async Task<User> UserByUsername(string username)
    {
        var user = await _context.Users
            .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName!.Equals(username));
        return user!;
    }
}