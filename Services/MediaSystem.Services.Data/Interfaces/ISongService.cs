namespace MediaSystem.Services.Data.Interfaces
{
    using System.Linq;

    using MediaSystem.Models;

    public interface ISongService
    {
        IQueryable<Song> GetAll();

        Song GetById(int id);

        void Add(string title, int? artistId = null, string genre = null, int? year = null);

        bool Update(int id, string title = null, int? artistId = null, string genre = null, int? year = null);

        bool DeleteById(int id);
    }
}
