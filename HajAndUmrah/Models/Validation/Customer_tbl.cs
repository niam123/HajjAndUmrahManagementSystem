using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HajAndUmrah.Models
{
    [MetadataType(typeof(MetaCustomer_tbl))]
    public partial class Customer_tbl
    {
    }
    public class MetaCustomer_tbl
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name ")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        [Display(Name = "City ")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Postal Code ")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Passport ")]
        public string Passport { get; set; }
        [Required]
        [Display(Name = "Designation ")]
        public string Designation { get; set; }
        [Required]
        [Display(Name = "Phone ")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Required]
        [Display(Name = "Number Of Adult Travellers ")]
        public int NoOfAdultTravellers { get; set; }

        [Display(Name = "Number Of Child Travellers ")]
        public Nullable<int> NoOfChildTravellers { get; set; }
        [Required]
        [Display(Name = "Total Price ")]
        public Nullable<int> TotalPrice { get; set; }

    }
}