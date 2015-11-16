namespace MediaSystem.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using MediaSystem.Api.Models;
    using MediaSystem.Data;
    using MediaSystem.Models;
    using MediaSystem.Services.Data;
    using MediaSystem.Services.Data.Interfaces;

    using StudentSystem.Data;

    public class SongController : ApiController
    {
        private readonly ISongService songService;

        public SongController()
        {
            var db = new MediaSystemContext();
            this.songService = new SongService(new EfGenericRepository<Song>(db), new EfGenericRepository<Genre>(db));
        }

        public IHttpActionResult Get()
        {
            var result = this.songService
                .GetAll()
                .ToList()
                .Select(MapSong)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.songService.GetById(id);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(MapSong(result));
        }

        public IHttpActionResult Post(SongApiModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (song == null)
            {
                return this.BadRequest("No data");
            }

            this.songService.Add(song.Title, song.ArtistId, song.Genre, song.Year);

            return this.Ok();
        }

        public IHttpActionResult Put(int id, SongApiModel song)
        {
            if (song == null)
            {
                return this.BadRequest("No data");
            }

            if (this.songService.Update(id, song.Title, song.ArtistId, song.Genre, song.Year))
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        public IHttpActionResult Delete(int id)
        {
            if (this.songService.DeleteById(id))
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        private static SongApiModel MapSong(Song song)
        {
            string genreName;
            if (song.Genre == null)
            {
                genreName = null;
            }
            else
            {
                genreName = song.Genre.Name;
            }

            return new SongApiModel() { Id = song.Id, Title = song.Title, Year = song.Year, Genre = genreName, ArtistId = song.ArtistId };
        }
    }
}
