namespace MediaSystem.Services.Data
{
    using System.Linq;

    using MediaSystem.Models;
    using MediaSystem.Services.Data.Interfaces;

    using StudentSystem.Data;

    public class SongService : ISongService
    {
        private readonly IRepository<Song> songs;        
        private readonly IRepository<Genre> genres;

        public SongService(IRepository<Song> songs, IRepository<Genre> genres)
        {
            this.songs = songs;            
            this.genres = genres;
        }

        public IQueryable<Song> GetAll()
        {
            return this.songs.All();
        }

        public Song GetById(int id)
        {
            return this.songs.GetById(id);
        }

        public void Add(string title, int? artistId = null, string genre = null, int? year = null)
        {
            this.songs.Add(new Song() { Title = title, ArtistId = artistId, Genre = this.GetGenre(genre), Year = year });
            this.songs.SaveChanges();
        }

        public bool Update(int id, string title = null, int? artistId = null, string genre = null, int? year = null)
        {
            var song = this.GetById(id);

            if (song == null)
            {
                return false;
            }
            else
            {
                if (title != null)
                {
                    song.Title = title;
                }

                if (artistId != null)
                {
                    song.ArtistId = artistId;
                }

                if (genre != null)
                {
                    song.Genre = this.GetGenre(genre);
                }

                if (year != null)
                {
                    song.Year = year;
                }

                this.songs.Update(song);
                this.songs.SaveChanges();
                return true;
            }
        }

        public bool DeleteById(int id)
        {
            var song = this.GetById(id);

            if (song == null)
            {
                return false;
            }
            else
            {
                this.songs.Delete(song);
                this.songs.SaveChanges();
                return true;
            }
        }

        private Genre GetGenre(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            Genre genre;

            var res = this.genres.All().FirstOrDefault(x => x.Name == name);
            if (res == null)
            {
                genre = new Genre()
                {
                    Name = name
                };
            }
            else
            {
                genre = res;
            }

            return genre;
        }
    }
}
