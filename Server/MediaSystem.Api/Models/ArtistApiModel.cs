namespace MediaSystem.Api.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MediaSystem.Models;

    public class ArtistApiModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(25)]
        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
