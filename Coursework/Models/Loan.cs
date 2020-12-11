using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Members { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("AlbumCopyId")]
        public virtual AlbumCopy AlbumCopies { get; set; }
        public int AlbumCopyId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime IssuedDate { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReturnedDate { get; set; }
        public int TotalCharge { get; set; }
        public string LoanTypes { get; set; }
    }
}