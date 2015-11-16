namespace MediaSystem.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;

    using MediaSystem.Models;

    public interface IAlbumService
    {
        IQueryable<Album> GetAll();

        Album GetById(int id);

        void Add(string title, int? year = null, string producerName = null, ICollection<int> artistsIds = null, ICollection<int> songsId = null);

        bool Update(int id, string title, int? year = null, string producerName = null, ICollection<int> artistsIds = null, ICollection<int> songsId = null);

        bool DeleteById(int id);
    }
}
