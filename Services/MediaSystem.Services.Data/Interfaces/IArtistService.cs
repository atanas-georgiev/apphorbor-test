namespace MediaSystem.Services.Data.Interfaces
{
    using System;
    using System.Linq;

    using MediaSystem.Models;

    public interface IArtistService
    {
        IQueryable<Artist> GetAll();

        Artist GetById(int id);

        void Add(string name, DateTime? dateOfBirth = null, string country = null);

        bool Update(int id, string name = null, DateTime? dateOfBirth = null, string country = null);

        bool DeleteById(int id);
    }
}
