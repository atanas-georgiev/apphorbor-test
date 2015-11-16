namespace MediaSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
