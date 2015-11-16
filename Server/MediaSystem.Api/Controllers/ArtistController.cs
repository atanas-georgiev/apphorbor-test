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

    public class ArtistController : ApiController
    {
        private readonly IArtistService artistService;

        public ArtistController()
        {
            var db = new MediaSystemContext();
            this.artistService = new ArtistService(new EfGenericRepository<Artist>(db), new EfGenericRepository<Country>(db));
        }

        public IHttpActionResult Get()
        {
            var result = this.artistService
                .GetAll()
                .ToList()
                .Select(MapArtist)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.artistService.GetById(id);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(MapArtist(result));
        }

        public IHttpActionResult Post(ArtistApiModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (artist == null)
            {
                return this.BadRequest("No data");
            }

            this.artistService.Add(artist.Name, artist.DateOfBirth, artist.Country);

            return this.Ok();
        }

        public IHttpActionResult Put(int id, ArtistApiModel artist)
        {
            if (artist == null)
            {
                return this.BadRequest("No data");
            }

            if (this.artistService.Update(id, artist.Name, artist.DateOfBirth, artist.Country))
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        public IHttpActionResult Delete(int id)
        {
            if (this.artistService.DeleteById(id))
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        private static ArtistApiModel MapArtist(Artist artist)
        {
            string countryName;
            if (artist.Country == null)
            {
                countryName = null;
            }
            else
            {
                countryName = artist.Country.Name;
            }

            return new ArtistApiModel() { Id = artist.Id, Name = artist.Name, Country = countryName, DateOfBirth = artist.DateOfBirth };
        }
    }
}
