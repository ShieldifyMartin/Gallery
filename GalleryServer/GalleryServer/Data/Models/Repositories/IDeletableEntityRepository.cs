namespace GalleryServer.Data.Models.Repositories
{
    using GalleryServer.Data.Models.Base;
    using System.Linq;

    public interface IDeletableEntityRepository<T>
    {
        IQueryable<T> All();

        IQueryable<T> AllAsNoTracking();

        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);

        void Undelete(T entity);

        void Delete(T entity);
    }
}
