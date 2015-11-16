namespace StudentSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using MediaSystem.Models;

    public interface IMediaSystemContext
    {
        IDbSet<Album> Albums { get; set; }

        IDbSet<Artist> Artists { get; set; }

        IDbSet<Song> Songs { get; set; }

        IDbSet<Genre> Genres { get; set; }

        IDbSet<Producer> Producers { get; set; }

        IDbSet<Country> Countries { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
