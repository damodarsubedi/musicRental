using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Member///noraml user
    {
        [Key]
        public int MemberID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public string PhoneNo { get; set; }

        [ForeignKey("MemberCategoryId")]
        public virtual MemberCategory Membercategories { get; set; }
        public int MemberCategoryId { get; set; }
    }
}
