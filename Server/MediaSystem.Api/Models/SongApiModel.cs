namespace MediaSystem.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using MediaSystem.Models;

    public class SongApiModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MinLength(2)]
        [MaxLength(25)]
        public string Genre { get; set; }

        [Range(1500, 2500)]
        public int? Year { get; set; }
        
        public int? ArtistId { get; set; }
    }
}
