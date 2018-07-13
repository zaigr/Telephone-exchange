using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Billing.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        TEntity GetEntity(Func<TEntity, bool> selector);
        void AddEntity(TEntity entity);

        bool RemoveEntity(TEntity entity);
        bool RemoveEntity(Func<TEntity, bool> selector);

        IEnumerable<TEntity> GetAllEntities();
    }
}
