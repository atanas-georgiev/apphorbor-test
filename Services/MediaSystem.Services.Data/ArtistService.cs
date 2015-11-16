namespace MediaSystem.Services.Data
{
    using System;
    using System.Linq;

    using Interfaces;

    using Models;

    using StudentSystem.Data;

    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> artists;
        private readonly IRepository<Country> countries;

        public ArtistService(IRepository<Artist> artists, IRepository<Country> countries)
        {
            this.artists = artists;
            this.countries = countries;
        }

        public IQueryable<Artist> GetAll()
        {
            return this.artists.All();
        }

        public Artist GetById(int id)
        {
            return this.artists.GetById(id);
        }

        public void Add(string name, DateTime? dateOfBirth = null, string country = null)
        {
            this.artists.Add(new Artist() { Name = name, DateOfBirth = dateOfBirth, Country = this.GetCountry(country) });
            this.artists.SaveChanges();
        }

        public bool Update(int id, string name = null, DateTime? dateOfBirth = null, string country = null)
        {
            var artist = this.GetById(id);

            if (artist == null)
            {
                return false;
            }
            else
            {
                if (name != null)
                {
                    artist.Name = name;
                }

                if (dateOfBirth != null)
                {
                    artist.DateOfBirth = (DateTime)dateOfBirth;
                }

                if (country != null)
                {
                    artist.Country = this.GetCountry(country);
                }

                this.artists.Update(artist);
                this.artists.SaveChanges();
                return true;
            }           
        }

        public bool DeleteById(int id)
        {
            var artist = this.GetById(id);

            if (artist == null)
            {
                return false;
            }
            else
            {
                this.artists.Delete(artist);
                this.artists.SaveChanges();
                return true;
            }
        }

        private Country GetCountry(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            Country country;

            var res = this.countries.All().FirstOrDefault(x => x.Name == name);
            if (res == null)
            {
                country = new Country()
                {
                    Name = name
                };
            }
            else
            {
                country = res;
            }

            return country;
        }
    }
}
