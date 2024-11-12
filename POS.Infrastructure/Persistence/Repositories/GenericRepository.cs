﻿using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;
using POS.Utilities.Static;

namespace POS.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public IQueryable<T> GetAllQueryable()
    {
        var response = _entity
            .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);
        return response;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var response = await _entity
            .Where(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null)
            .AsNoTracking()
            .ToListAsync();
        return response;
    }

    public async Task<IEnumerable<T>> GetSelectAsync()
    {
        var getAll = await _entity
            .Where(x => x.State.Equals((int)StateTypes.Activo) && x.AuditDeleteUser == null && x.AuditDeleteDate == null)
            .AsNoTracking()
            .ToListAsync();

        return getAll;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var response = await _entity
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id &&
            x.AuditDeleteUser == null && x.AuditDeleteDate == null);
        return response!;
    }

    public async Task CreateAsync(T entity)
    {
        entity.AuditCreateUser = 1;
        entity.AuditCreateDate = DateTime.UtcNow;
        await _context.AddAsync(entity);
    }

    public void UpdateAsync(T entity)
    {
        entity.AuditUpdateUser = 1;
        entity.AuditUpdateDate = DateTime.UtcNow;
        _context.Update(entity);
        _context.Entry(entity).Property(x => x.AuditCreateUser).IsModified = false;
        _context.Entry(entity).Property(x => x.AuditCreateDate).IsModified = false;
    }

    public async Task DeleteAsync(int id)
    {
        T entity = await GetByIdAsync(id);
        entity.AuditDeleteUser = 1;
        entity.AuditDeleteDate = DateTime.UtcNow;
        _context.Update(entity);
    }
}