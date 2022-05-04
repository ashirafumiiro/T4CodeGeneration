using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BaseRepository<T, TDTO> : IDisposable, IRepository<T, TDTO> where T : class
    {
        private readonly DbSet<T> _table;
        private readonly EmployeeContext _context;
        protected readonly ILogger logger;

        public BaseRepository(EmployeeContext context, ILogger logger)
        {
            _context = context;
            this.logger = logger;
            _table = _context.Set<T>();
        }
        protected EmployeeContext Context => _context;

        public T Add(T entity)
        {
            _table.Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            SaveChanges();
            return entity;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public List<T> GetAll() => _table.ToList();

        public T GetOne(int? id)
        {
            return _table.Find(id);
        }

        public virtual T Update(TDTO entity)
        {
            throw new NotImplementedException();
        }

        internal void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"DbUpdateException error details - {e?.InnerException?.InnerException?.Message}");

                foreach (var eve in e.Entries)
                {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                }
                logger.LogError(e, sb.ToString());
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
