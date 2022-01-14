using BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories.Base;
using BreadCommunityWeb.EventNotificator.Domain.Base;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Repositories.Base
{
    public class ServiceConfigRepositoryBase<T> : IConfigServiceRepository<T> where T: EntityBase
    {
        private readonly SQLiteDataContext _dataContext;
        private readonly DbSet<T> _dbSet;
        public ServiceConfigRepositoryBase(SQLiteDataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.Where(c => c.Id == id).FirstOrDefaultAsync();
            if(entity != null)
            {
                _dbSet.Remove(entity);
            }
            await _dataContext.SaveChangesAsync();
            
        }

        public async Task<T?> Get(int id)
        {
            return await _dbSet.Where(c => c.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Save(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<T?> Update(int id, T entity)
        {
            await _dataContext.SaveChangesAsync();

            return await _dbSet.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
