using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace PayperClip.Models
{
    public enum Payment
    {
        blik, PayU, transfer
    }

    public class Order
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MinLength(2, ErrorMessage = "To short name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {0}")]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MinLength(2, ErrorMessage = "To short name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {0}")]
        public string Surname { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Address { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public Payment Payment { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "LoyaltyPoints cannot be less than zero")]
        public int LoyaltyPoints { get; set; }
        [NotMapped]
        public List<(Article, int)> OrderedItems { get; set; }
        
    }
}