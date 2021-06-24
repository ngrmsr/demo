using Demo.DAL.DbContexts;
using Demo.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Repository(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Repository<TEntity>>();
        }
        public int Count()
        {
            var q = _context.Set<TEntity>().AsNoTracking().Count();
            return q;
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            var q = _context.Set<TEntity>().AsNoTracking().Where(predicate).Count();
            return q;
        }


        public Task<int> CountAsync()
        {
            var q = _context.Set<TEntity>().AsNoTracking().CountAsync();
            return q;
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var q = _context.Set<TEntity>().AsNoTracking().Where(predicate).CountAsync();
            return q;
        }

        public int Delete(TEntity entity)
        {
            var query = _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
            _logger.Log(LogLevel.Information, "delete {id}", query.Entity.Id);
            return query.Entity.Id;
        }

        public int Delete(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
            _logger.Log(LogLevel.Information, "delete {id}", entities.ToString());
            return 1;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _context.Set<TEntity>().Remove(_context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate));
            _context.SaveChanges();
            _logger.Log(LogLevel.Information, "deleted");
            return query.Entity.Id;
        }

        public int DeleteAll()
        {
            _context.Set<TEntity>().RemoveRange(_context.Set<TEntity>().AsNoTracking());
            _context.SaveChanges();
            _logger.Log(LogLevel.Information, "delete all ");
            return 1;
        }

        public async Task<int> DeleteAllAsync()
        {
            _context.Set<TEntity>().RemoveRange(_context.Set<TEntity>().AsNoTracking());
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "delete all ");
            return 1;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            var query = _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "deleted {id}");
            return query.Entity.Id;
        }

        public async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "deleted range");
            return 1;
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _context.Set<TEntity>().Remove(await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate));
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "deleted");
            return query.Entity.Id;
        }

        public IEnumerable<TEntity> Find(params Expression<Func<TEntity, dynamic>>[] includePaths)
        {
            var q = _context.Set<TEntity>().AsNoTracking();
            q = includePaths.Aggregate(q, (current, path) => current.Include(path));
            return q.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includePaths)
        {
            var q = _context.Set<TEntity>().AsNoTracking().Where(predicate);
            q = includePaths.Aggregate(q, (current, path) => current.Include(path));
            return q.ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(params Expression<Func<TEntity, dynamic>>[] includePaths)
        {
            var query = _context.Set<TEntity>().AsNoTracking();
            query = includePaths.Aggregate(query, (current, path) => current.Include(path));
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includePaths)
        {
            var q = _context.Set<TEntity>().AsNoTracking().Where(predicate);
            q = includePaths.Aggregate(q, (current, path) => current.Include(path));
            return await q.ToListAsync();
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includePaths)
        {
            var query = _context.Set<TEntity>().AsNoTracking().Where(predicate);
            query = includePaths.Aggregate(query, (current, path) => current.Include(path));
            return query.FirstOrDefault();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includePaths)
        {
            var query = _context.Set<TEntity>().AsNoTracking().Where(predicate);
            query = includePaths.Aggregate(query, (current, path) => current.Include(path));
            return await query.FirstOrDefaultAsync();
        }

        public int Insert(TEntity entity)
        {
            var query = _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            _logger.Log(LogLevel.Information, "insert {id}", query.Entity.Id);
            return query.Entity.Id;
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            var query = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return query.Entity.Id;
        }

        public async void InsertAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public int Update(TEntity entity)
        {
            var query = _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return query.Entity.Id;
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            var query = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return query.Entity.Id;
        }

        public async void UpdateAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
