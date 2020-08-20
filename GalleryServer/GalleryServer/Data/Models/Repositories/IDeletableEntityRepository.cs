namespace GalleryServer.Data.Models.Repositories
{
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Data.Models.Base;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDeletableEntityRepository<T> : IRepository<T>
        where T : class, IDeletableEntity
    {
        IQueryable<T> All();        

        IQueryable<T> AllWithDeleted();
        
        void HardDelete(T entity);

        void Undelete(T entity);

        void Delete(T entity);
    }
}
