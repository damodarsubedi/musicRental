using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Album ///refered as DVD
    {
        [Key]
        public int AlbumId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleasedDate { get; set; }
        public int NoofSongs { get; set; }
        public int TotalLength { get; set; }
        public int CopyNumber { get; set; }
        public int StandardCharge { get; set; }
        [NotMapped] //this doesnt make column in that table (below coverImage)
        public HttpPostedFile CoverImage { get; set; }
        public bool AgeRestricted { get; set; }
        public string CoverImagePath { get; set; }
        [ForeignKey("AlbumTypeId")]
        public virtual AlbumType AlbumTypes { get; set; }
        public int AlbumTypeId { get; set; }
        public string Studio { get; set; }
        public virtual IEnumerable<Artist> Artists { get; set; } /// navigation properties
    }
}