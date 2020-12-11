using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class AlbumProducer
    {

        [Key]
        public int AlbumProducerId { get; set; }
        [ForeignKey("AlbumId")]
        public virtual Album Albums { get; set; }
        public int AlbumId { get; set; }

        [ForeignKey("ProducerId")]
        public virtual Producer Producers { get; set; }
        public int ProducerId { get; set; }
    }
}