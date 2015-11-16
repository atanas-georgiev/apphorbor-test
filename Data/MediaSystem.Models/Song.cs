namespace MediaSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        public int? GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        [Range(1500, 2500)]
        public int? Year { get; set; }

        [Required]
        public int? ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
