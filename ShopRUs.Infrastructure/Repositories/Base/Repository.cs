//using Microsoft.EntityFrameworkCore;
//using ShopRUs.Core.Repositories.Base;
//using ShopRUs.Infrastructure.Data;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace ShopRUs.Infrastructure.Repositories.Base
//{
//    class Repository<T> : IRepository<T> where T : class
//    {
//        protected readonly ShopRUsDbContext _shopRUsDbContext;
//        public Repository(ShopRUsDbContext shopRUsDbContext)
//        {
//            _shopRUsDbContext = shopRUsDbContext;
//        }
//        public async Task<T> AddAsync(T entity)
//        {
//            await _shopRUsDbContext.Set<T>().AddAsync(entity);
//            await _shopRUsDbContext.SaveChangesAsync();
//            return entity;
//        }
//        public async Task DeleteAsync(T entity)
//        {
//            _shopRUsDbContext.Set<T>().Remove(entity);
//            await _shopRUsDbContext.SaveChangesAsync();
//        }
//        public async Task<IReadOnlyList<T>> GetAllAsync()
//        {
//            return await _shopRUsDbContext.Set<T>().ToListAsync();
//        }
//        public async Task<T> GetByIdAsync(int id)
//        {
//            return await _shopRUsDbContext.Set<T>().FindAsync(id);
//        }
//        public Task UpdateAsync(T entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
