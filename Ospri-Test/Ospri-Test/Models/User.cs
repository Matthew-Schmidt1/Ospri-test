using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OspriTest.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255,MinimumLength=1)]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 20)]
        public string Address { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBith { get; set; }
    }
}
