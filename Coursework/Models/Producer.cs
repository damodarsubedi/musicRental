using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProducerName { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProducerAddress { get; set; }
        [Required]
        public String ProducerPhone { get; set; }
    }
}