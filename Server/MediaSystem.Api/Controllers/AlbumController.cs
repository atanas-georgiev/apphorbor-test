namespace MediaSystem.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using MediaSystem.Api.Models;
    using MediaSystem.Data;
    using MediaSystem.Models;
    using MediaSystem.Services.Data;
    using MediaSystem.Services.Data.Interfaces;

    using StudentSystem.Data;

    public class AlbumController : ApiController
    {
        private readonly IAlbumService albumService;

        public AlbumController()
        {
            var db = new MediaSystemContext();
            this.albumService = new AlbumService(new EfGenericRepository<Album>(db), new EfGenericRepository<Producer>(db), new EfGenericRepository<Artist>(db), new EfGenericRepository<Song>(db));
        }

        public IHttpActionResult Get()
        {
            var result = this.albumService
                .GetAll()
                .ToList()
                .Select(MapAlbum)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.albumService.GetById(id);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(MapAlbum(result));
        }

        public IHttpActionResult Post(AlbumApiModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (album == null)
            {
                return this.BadRequest("No data");
            }

            this.albumService.Add(album.Title, album.Year, album.Producer, album.ArtistsIds, album.SongsIds);

            return this.Ok();
        }

        public IHttpActionResult Put(int id, AlbumApiModel album)
        {
            if (album == null)
            {
                return this.BadRequest("No data");
            }

            if (this.albumService.Update(id, album.Title, album.Year, album.Producer, album.ArtistsIds, album.SongsIds))
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        public IHttpActionResult Delete(int id)
        {
            if (this.albumService.DeleteById(id))
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        private static AlbumApiModel MapAlbum(Album album)
        {
            string producerName;

            if (album.Producer == null)
            {
                producerName = null;
            }
            else
            {
                producerName = album.Producer.Name;
            }

            return new AlbumApiModel() { Id = album.Id, Title = album.Title, Producer = producerName, Year = album.Year,
                ArtistsIds = album.Artists.Select(a => a.Id).ToList(),
                SongsIds = album.Songs.Select(s => s.Id).ToList() };
        }
    }
}
