using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces
{
    public interface IBaseRepository<TEntity,TKey> where TEntity : class
    {

        Task <List <TEntity>> GetAll();

        Task<TEntity> GetById(TKey id);

        Task<bool> DeleteById(TKey id);

        Task<bool> Insert(TEntity entity);

        Task<bool> Update(TEntity entity);

    }
}
