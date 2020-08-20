namespace GalleryServer.Data.Models.Repositories
{
    using GalleryServer.Data.Models.Base;
    using GalleryServer.Data.Models.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DeletableEntityRepository<DeletableEntity> : BaseRepository<DeletableEntity>, IDeletableEntityRepository<DeletableEntity>
        where DeletableEntity : class, IDeletableEntity
    {
        public DeletableEntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IQueryable<DeletableEntity> All() => base.All().Where(x => !x.IsDeleted);
   
        public IQueryable<DeletableEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

        public async Task AddAsync(DeletableEntity entity) 
            => await this.DbSet.AddAsync(entity).AsTask();               

        public void HardDelete(DeletableEntity entity) => base.Delete(entity);

        public void Undelete(DeletableEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }

        public override void Delete(DeletableEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}
