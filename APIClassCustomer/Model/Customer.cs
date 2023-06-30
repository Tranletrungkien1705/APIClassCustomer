using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIClassCustomer.Model
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        [MinLength(3)]
        public string? FullName { get; set; }
        [Required]
        public DateOnly Birthday { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+[@][A-Za-z]+[.][A-Za-z]+?", ErrorMessage ="NOt Type Email")]
        public string? Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string? UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [ForeignKey(nameof(Class.Id))]
        public int ClassId { get; set; }
        public virtual Class? Classes { get; set; }
    }
}
