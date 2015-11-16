using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        private ICollection<Artist> artists;
        private ICollection<Song> songs;

        public Album()
        {
            this.artists = new HashSet<Artist>();
            this.songs = new HashSet<Song>();
        }
         
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        public int? Year { get; set; }

        public int? ProducerId { get; set; }

        public virtual Producer Producer { get; set; }

        public virtual ICollection<Artist> Artists
        {
            get { return this.artists; }
            set { this.artists = value; }
        }

        public virtual ICollection<Song> Songs
        {
            get { return this.songs; }
            set { this.songs = value; }
        }
    }
}
