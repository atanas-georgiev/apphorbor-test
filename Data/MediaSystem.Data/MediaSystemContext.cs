namespace MediaSystem.Data
{
    using System.Data.Entity;

    using MediaSystem.Models;

    using StudentSystem.Data;

    public class MediaSystemContext : DbContext, IMediaSystemContext
    {
        public MediaSystemContext() :
            base("db2781eecddbba4724baaca55200a845c2")
        {
        }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Artist> Artists { get; set; }

        public IDbSet<Song> Songs { get; set; }

        public IDbSet<Genre> Genres { get; set; }

        public IDbSet<Producer> Producers { get; set; }

        public IDbSet<Country> Countries { get; set; }
    }
}
