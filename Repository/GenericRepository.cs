using Microsoft.EntityFrameworkCore;
using myFirtsAzureWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace myFirtsAzureWebApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext Contexto;
        public GenericRepository(DataContext contexto)
        {
            this.Contexto = contexto;
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            this.Contexto.Set<T>().RemoveRange(entities);
            await SaveAllAsync().ConfigureAwait(false);
            if (entities != null && entities.Count() > 0)
            {
                this.Contexto.Set<T>().RemoveRange(entities);
                await SaveAllAsync().ConfigureAwait(false);
            }
        }

        public async Task<bool> DeleterAsync(T entity)
        {
            this.Contexto.Set<T>().Remove(entity);
            return await SaveAllAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = AddFilter(filter);
            return await GetAsync(query).ConfigureAwait(false);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, IEnumerable<string> inclutions)
        {
            IQueryable<T> consulta = AddFilter(filter);
            if (inclutions != null)
            {
                foreach (var incude in inclutions)
                {
                    consulta = consulta.Include(incude);
                }
            }
            var resultado = await GetAsync(consulta).ConfigureAwait(false);
            return (resultado != null) ? resultado.FirstOrDefault() : null;
        }

        public Task<T> GetAsync(IEnumerable<string> inclutions)
        {
            return GetAsync(null, inclutions);
        }

        private IQueryable<T> AddFilter(Expression<Func<T, bool>> filtro = null)
        {
            IQueryable<T> consulta = this.Contexto.Set<T>();
            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }
            return consulta;
        }

        private async Task<IEnumerable<T>> GetAsync(IQueryable<T> query)
        {
            return await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetFirtsAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = AddFilter(filter);
            var result = await GetAsync(query).ConfigureAwait(false);
            return result != null ? result.FirstOrDefault() : null;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            await this.Contexto.Set<T>().AddAsync(entity).ConfigureAwait(false);
            return await SaveAllAsync().ConfigureAwait(false);
        }

        public async Task<IQueryable<T>> LoadAll()
        {
            IQueryable<T> query = this.Contexto.Set<T>();
            var result = await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
            return (result != null) ? result.AsQueryable() : null;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            Contexto.Set<T>().Update(entity);
            return await SaveAllAsync().ConfigureAwait(false);
        }

        public async Task<bool> SaveAllAsync()
        {
            Contexto.ChangeTracker.DetectChanges();

            foreach (var entry in Contexto.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {

                    entry.Property("FechaModificacion").CurrentValue = DateTime.Now;
                }
            }
            return await Contexto.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}
