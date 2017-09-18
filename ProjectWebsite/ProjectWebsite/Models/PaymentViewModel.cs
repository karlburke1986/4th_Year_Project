using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWebsite.Models
{
    public class PaymentViewModel
    {
        [Required]
        [Display(Name = "Card Holders Name")]
        public string CardHolder { get; set; }        
        [Required]
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }
        [Required]
        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }      
        public string City { get; set; }
        [Required]
        public string County { get; set; }
        [Required]
        [Display(Name = "Credit/Debit Card Number")]
        [DataType(DataType.CreditCard)]
        public string CardNum { get; set; }
        [Required]
        [Display(Name = "Expiry Month")]
        public string ExpiryMonth { get; set; }
        [Required]
        [Display(Name = "Expiry Year")]
        public string ExpiryYear { get; set; }
        [Required]
        [Display(Name = "CCV Number")]
        [Range(100, 999, ErrorMessage = "Value must be entered")]
        public int cvv { get; set; }
    }
}