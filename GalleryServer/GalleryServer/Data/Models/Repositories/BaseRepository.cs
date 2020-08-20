using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryServer.Data.Models.Repositories
{
    public class BaseRepository<Entity> : IRepository<Entity>
        where Entity : class
    {
        public BaseRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<Entity>();
        }

        protected DbSet<Entity> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public virtual IQueryable<Entity> All() => this.DbSet;

        public async Task AddAsync(Entity entity) => await this.DbSet.AddAsync(entity).AsTask();

        public void Update(Entity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(Entity entity) => this.DbSet.Remove(entity);        
    }
}
