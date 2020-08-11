namespace GalleryServer.Data.Models.Repositories
{
    using GalleryServer.Data.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class DeletableEntityRepository<DeletableEntity> : BaseRepository<DeletableEntity>, IDeletableEntityRepository<DeletableEntity>
        where DeletableEntity : class, IDeletableEntity
    {
        public DeletableEntityRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IQueryable<DeletableEntity> All() => base.All().Where(x => !x.IsDeleted);

        public IQueryable<DeletableEntity> AllAsNoTracking() => base.All().Where(x => !x.IsDeleted);

        public IQueryable<DeletableEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

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
