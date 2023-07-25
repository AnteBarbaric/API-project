using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Models;
using V01_SignIn.Data.Models;

namespace V01_SignIn.Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbSet<T> dbSet;
        protected TESTContext dataContext;

        public Repository(TESTContext dataContext)
        {
            this.dataContext = dataContext;
            this.dbSet = dataContext.Set<T>();
        }

        #region IRepository<T> Members

        public IDbContextTransaction BeginTransaction(OperationResult result)
        {
            try
            {
                IDbContextTransaction transaction = this.dataContext.Database.BeginTransaction();
                return transaction;
            }
            catch(Exception e)
            {
                result.Errors.Add(e.Message);
            }
            return null;
        }

        public void Commit(IDbContextTransaction transaction, OperationResult result)
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
            }
        }

        public void Insert(T entity, OperationResult result)
        {
            try
            {
                this.dbSet.Add(entity);
                this.dataContext.SaveChanges();
            }
            catch(Exception e)
            {
                result.Errors.Add(e.Message);
                if(e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
        }

        public void Insert(T entity)
        {
            try
            {
                this.dbSet.Add(entity);
                this.dataContext.SaveChanges();
            }
            catch
            {
                
            }
        }

        public void Update(T entity, OperationResult result)
        {
            try
            { 
                this.dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
        }

        public void Update(T entity)
        {
            try
            {
                this.dataContext.SaveChanges();
            }
            catch
            {
               
            }
        }

        public void Delete(T entity, OperationResult result)
        {
            try
            {
                this.dbSet.Remove(entity);
                this.dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate, OperationResult result)
        {
            try
            { 
                return this.dbSet.Where(predicate);
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
            return null;
        }

        public IQueryable<T> GetAll(OperationResult result)
        {
            try
            {
                return this.dbSet;
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
            }

            return null;
        }

        public T GetById(int id, OperationResult result)
        {
            // Sidenote: the == operator throws NotSupported Exception!
            // 'The Mapping of Interface Member is not supported'
            // Use .Equals() instead
            try
            {
                return this.dbSet.Single(e => e.ID.Equals(id));
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
            }
            return null;
        }
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return this.dbSet.Where(predicate);
            }
            catch
            {
            }
            return null;
        }

        public void InsertAll(IEnumerable<T> entities, OperationResult result)
        {
            try
            {
                this.dbSet.AddRange(entities);
                this.dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
        }
        public void DeleteAll(IEnumerable<T> entities, OperationResult result)
        {
            try
            {
                this.dbSet.RemoveRange(entities);
                this.dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return this.dbSet;
            }
            catch
            {
            }

            return null;
        }

        public T GetById(int id)
        {
            // Sidenote: the == operator throws NotSupported Exception!
            // 'The Mapping of Interface Member is not supported'
            // Use .Equals() instead
            try
            {
                return this.dbSet.Single(e => e.ID.Equals(id));
            }
            catch
            {
            }
            return null;
        }
        #endregion
    }
}
