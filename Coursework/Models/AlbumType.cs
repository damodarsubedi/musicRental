using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class AlbumType
    {
        [Key]
        public int AlbumTypeId { get; set; }
        public string AlbumTypeName { get; set; }

    }
}