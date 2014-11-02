using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreTracker.API.Model.Repo
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get();
        T GetById(int id);
        void AddElement(T element);
        void DeleteElement(int id);
        void DeleteRange(IEnumerable<T> elements);
        void UpdateElement(T element);
        void Save();
        Task SaveAsync();

    }

    public class DbRepository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly ScoreTrackerContext ctx;
        private readonly DbSet<T> dbSet;

        public DbRepository(ScoreTrackerContext ctx)
        {
            this.ctx = ctx;
            dbSet = ctx.Set<T>();

        }


        public IQueryable<T> Get()
        {
            return dbSet;
        }


        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void AddElement(T element)
        {
            dbSet.Add(element);
        }

        public void DeleteElement(int id)
        {
            var element = GetById(id);
            if (element != null)
            {
                dbSet.Remove(element);
            }

        }

        public void DeleteRange(IEnumerable<T> elements)
        {
            dbSet.RemoveRange(elements);
        }

        public void UpdateElement(T element)
        {
            ctx.Entry(element).State = EntityState.Modified;
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public Task SaveAsync()
        {
            return ctx.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
