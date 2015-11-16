namespace MediaSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Producer
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
