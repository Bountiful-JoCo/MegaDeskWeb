using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskWeb.Models
{
    public class DeskQuote
    {
        public int DeskQuoteID { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z0-9 '-]*$", ErrorMessage = "Enter Last name using only letters, apostrophe or hyphen")]
        [Display(Name = "Last Name")]

        [Required]
        public string LastName { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z0-9 '-]*$", ErrorMessage = "Enter First name using only letters, apostrophe or hyphen")]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Range(24, 96)]
        [Required]
        public int Width { get; set; }
        [Range(12, 48)]
        [Required]
        public int Depth { get; set; }
        [Range(0, 7)]
        [Required]
        public int Drawers { get; set; }
        [Required]
        [Display(Name = "Rush Order")]
        public int RushOrderDays { get; set; }
        [Required]
        [Display(Name=" Surface Material")]
        public string DeskMaterial { get; set; }
        [Display(Name="Quote Date")]
        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; }
        public int DeskPrice { get; set; }
        
    }
}
