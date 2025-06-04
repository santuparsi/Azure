using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandsOnMVCEFCoreCodeFirst_Demo2.Entities
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "Char")]
        public string ProjectCode { get; set; }
        [Required]
        [StringLength(20)]
        public string ProjectName { get; set; }
    }
}
