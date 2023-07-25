using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Models;
using V01_SignIn.Models;

namespace V01_SignIn.Data
{
    public interface IRepository<T>
    {
        void Insert(T entity, OperationResult result);
        void Update(T entity, OperationResult result);
        void Delete(T entity, OperationResult result);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate, OperationResult result);
        IQueryable<T> GetAll(OperationResult result);
        T GetById(int id, OperationResult result);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
    }
}
