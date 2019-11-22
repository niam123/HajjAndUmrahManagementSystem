using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HajAndUmrah.Models
{
    [MetadataType(typeof(MetaService))]
    public partial class Service_tbl
    {
    }
    public class MetaService
    {
        [Required]
        [Display(Name = "Airlines Name")]
        public string AirlinesName { get; set; }
        [Required]
        [Display(Name = "Deprature Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DepratureDate { get; set; }
        [Required]
        [Display(Name = "Deprature Time")]
        public System.TimeSpan DepratureTime { get; set; }
        [Required]
        [Display(Name = "Arrival Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime ArrivalDate { get; set; }
        [Required]
        [Display(Name = "Arrival Time")]
        public System.TimeSpan ArrivalTime { get; set; }
        [Required]
        [Display(Name = "Bus Name")]
        public string BusName { get; set; }
        [Required]
        [Display(Name = "Bus Deprature Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BusDepratureDate { get; set; }
        [Required]
        [Display(Name = "Bus Deprature Time")]
        public System.TimeSpan BusDepratureTime { get; set; }
        [Required]
        [Display(Name = "Bus Arrival Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BusArrivalDate { get; set; }
        [Required]
        [Display(Name = "Bus Arrival Time")]
        public System.TimeSpan BusArrivalTime { get; set; }
      
    }
}