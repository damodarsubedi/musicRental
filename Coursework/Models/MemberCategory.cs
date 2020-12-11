using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{

    public class MemberCategory
    {
        [Key]
        public int MemberCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int LoanAvailable { get; set; }
        public int FinePerExtraDays { get; set; }
    }
}