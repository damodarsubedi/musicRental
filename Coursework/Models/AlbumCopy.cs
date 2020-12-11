using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class AlbumCopy
    {

        [Key]
        public int AlbumCopyId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? IssueDate { get; set; }//question mark turn dateType into nullable 
        public string DvdCopyNumber { get; set; }
        [ForeignKey("AlbumId")]
        public virtual Album Albums { get; set; }
        public int AlbumId { get; set; }
        public string CopyStatus { get; set; }
    }
}