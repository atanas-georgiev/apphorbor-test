namespace MediaSystem.Api.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MediaSystem.Models;

    public class AlbumApiModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Producer { get; set; }

        public ICollection<int> ArtistsIds;

        public ICollection<int> SongsIds;
    }
}
