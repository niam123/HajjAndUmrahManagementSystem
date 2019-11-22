using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HajAndUmrah.Models
{
    [MetadataType(typeof(MetaPackage_tbl))]
    public partial class Package_tbl
    {
    }
    public class MetaPackage_tbl
    {
        [Required]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }
        [Required]
        [Display(Name = "Package Group")]
        public string PackageGroup { get; set; }
        [Required]
        [Display(Name = "Number Of Days")]
        public int NoOfDays { get; set; }
        [Required]
        [Display(Name = "Travel Class")]
        public string TravelClass { get; set; }
        [Required]
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }
        [Required]
        [Display(Name = "Room Type")]
        public string RoomType { get; set; }
        [Required]
        [Display(Name = "Star Rate")]
        public string StarRate { get; set; }
        [Required]
        [Display(Name = "Package Price")]
        public int PackagePrice { get; set; }
        [Required]
        [Display(Name = "Package Type")]
        public string PackageType { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
    }
}