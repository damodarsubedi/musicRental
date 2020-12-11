using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public virtual IEnumerable<Album> Albums { get; set; }
    }
}