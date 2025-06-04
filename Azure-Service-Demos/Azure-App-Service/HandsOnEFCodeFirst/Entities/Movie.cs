using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HandsOnEFCodeFirst.Entities
{
    public class Movie
    {
       [Key]
        public int MovieId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        [Required]
        public string Language { get; set; }
        public int ReleaseYear { get; set; }
        [StringLength(50)]
        [Required]
        public string Actor { get; set; }
        [StringLength(50)]
        [Required]
        public string Director { get; set; }
        [StringLength(350)]
        [Required]
        public string BannerUrl { get; set; }
    }
}
