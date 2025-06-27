using Domain.InterFaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementions
{

    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {

        readonly Context context;

        readonly DbSet<TEntity> dbset;

        public BaseRepository(Context context)
        {
            this.context = context;
            dbset = context.Set<TEntity>();
        }



        public async Task<bool> DeleteById(TKey id)
        {
            var entity = await GetById(id);

            if (entity == null)
            {
                return false;
            }

            context.Remove(entity);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dbset.ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<bool> Insert(TEntity entity)
        {
            dbset.Add(entity);
            
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(TEntity entity)
        {
            dbset.Update(entity);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
