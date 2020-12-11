using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class User ///assistant
    {
        [Key]
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserContact { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRoles { get; set; }
        public int UserRoleId { get; set; }
    }
}