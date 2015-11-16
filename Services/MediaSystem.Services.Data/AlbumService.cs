namespace MediaSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MediaSystem.Models;
    using MediaSystem.Services.Data.Interfaces;

    using StudentSystem.Data;

    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> albums;
        private readonly IRepository<Producer> producers;
        private readonly IRepository<Artist> artists;
        private readonly IRepository<Song> songs;

        public AlbumService(IRepository<Album> albums, IRepository<Producer> producers, IRepository<Artist> artists, IRepository<Song> songs)
        {
            this.albums = albums;
            this.producers = producers;
            this.artists = artists;
            this.songs = songs;
        }

        public IQueryable<Album> GetAll()
        {
            return this.albums.All();
        }

        public Album GetById(int id)
        {
            return this.albums.GetById(id);
        }

        public void Add(
            string title,
            int? year = null,
            string producerName = null,
            ICollection<int> artistsIds = null,
            ICollection<int> songsId = null)
        {
            ICollection<Artist> a = new HashSet<Artist>();
            ICollection<Song> s = new HashSet<Song>();

            if (songsId != null)
            {
                foreach (var songId in songsId)
                {
                    try
                    {
                        s.Add(this.songs.GetById(songId));
                    }
                    catch (NotSupportedException)
                    {
                        // nothing, do not add the song
                    }                
                }
            }

            if (artistsIds != null)
            {
                foreach (var artistId in artistsIds)
                {
                    try
                    {
                        a.Add(this.artists.GetById(artistId));
                    }
                    catch (NotSupportedException)
                    {
                        // nothing, do not add the song
                    }
                }
            }

            this.albums.Add(new Album()
                                {
                                    Title = title, Year = year, Producer = this.GetProducer(producerName), 
                                    Artists = a, Songs = s
                                });
            this.albums.SaveChanges();
        }

        public bool Update(
            int id,
            string title,
            int? year = null,
            string producerName = null,
            ICollection<int> artistsIds = null,
            ICollection<int> songsId = null)
        {
            ICollection<Artist> a = new HashSet<Artist>();
            ICollection<Song> s = new HashSet<Song>();

            if (songsId != null)
            {
                foreach (var songId in songsId)
                {
                    try
                    {
                        s.Add(this.songs.GetById(songId));
                    }
                    catch (NotSupportedException)
                    {
                        // nothing, do not add the song
                    }
                }
            }

            if (artistsIds != null)
            {
                foreach (var artistId in artistsIds)
                {
                    try
                    {
                        a.Add(this.artists.GetById(artistId));
                    }
                    catch (NotSupportedException)
                    {
                        // nothing, do not add the song
                    }
                }
            }

            var album = this.GetById(id);

            if (album == null)
            {
                return false;
            }

            if (title != null)
            {
                album.Title = title;
            }

            if (year != null)
            {
                album.Year = year;
            }

            if (producerName != null)
            {
                album.Producer = this.GetProducer(producerName);
            }

            if (artistsIds != null)
            {
                album.Artists = a;
            }

            if (songsId != null)
            {
                album.Songs = s;
            }

            this.albums.Update(album);
            this.albums.SaveChanges();
            return true;
        }

        public bool DeleteById(int id)
        {
            var album = this.GetById(id);

            if (album == null)
            {
                return false;
            }
            else
            {
                this.albums.Delete(album);
                this.albums.SaveChanges();
                return true;
            }
        }

        private Producer GetProducer(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            Producer producer;

            var res = this.producers.All().FirstOrDefault(x => x.Name == name);
            if (res == null)
            {
                producer = new Producer()
                {
                    Name = name
                };
            }
            else
            {
                producer = res;
            }

            return producer;
        }
    }
}
