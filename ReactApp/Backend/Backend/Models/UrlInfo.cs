using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class UrlInfo
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string UrlString { get; set; }

        public string? ShortUrl { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        //public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public string CreatedBy { get; set; }

    }
}
