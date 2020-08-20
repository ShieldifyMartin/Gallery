namespace GalleryServer.Data.Models.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : class
    {       
        IQueryable<TEntity> All();
        
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);  
    }
}
