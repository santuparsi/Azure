using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandsOnMVCEFCoreCodeFirst_Demo2.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //identity not applied
        [Required(ErrorMessage ="Pls Enter name")]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(50)]
        [Column("EmployeeName", TypeName = "varchar")]
        public string Name { get; set; }
        [StringLength(5)]
        [Column(TypeName = "Char")]
        [ForeignKey("Project")] //applied ForeignKey refrences with Project Entity
        public string ProjectCode { get; set; }
        //Navigation Prop(use to defind relation in between entitie like employee and project
        public Project Project { get; set; }
    }
}
