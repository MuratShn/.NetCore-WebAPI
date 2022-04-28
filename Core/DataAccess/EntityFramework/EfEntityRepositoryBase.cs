using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntitiyRepository<TEntity> 
        where TEntity: class, IEntitiy, new() 
        where TContext:DbContext ,new()
    {

        public void Add(TEntity entitiy)
        {
            using (TContext context = new TContext()) //içinde yazılan nesneler bittiğinde bellekten anında atılır
            {
                var addedEntity = context.Entry(entitiy); //veriyi kaynakla ilişkilendirdim (refenransı yakalama )?
                addedEntity.State = EntityState.Added; // ekle
                context.SaveChanges();
            }

        }

        public void Delete(TEntity entitiy)
        {
            using (TContext context = new TContext()) //içinde yazılan nesneler bittiğinde bellekten anında atılır
            {
                var deletedEntity = context.Entry(entitiy);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //if (filter == null)
                //    return context.Set<Product>().ToList();
                //else
                //{

                //}
                //Nullsa burası                                      // Değilse burası where filter ile filtreyi uyguluyoruz 
                return filter == null 
                    ? context.Set<TEntity>().ToList() 
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entitiy)
        {
            using (TContext context = new TContext()) //içinde yazılan nesneler bittiğinde bellekten anında atılır
            {
                var updateddEntity = context.Entry(entitiy);
                updateddEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
