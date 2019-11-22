using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HajAndUmrah.Models
{
    public class DashBoard
    {
        public List<Package_tbl> Package { get; set; }
        public List<Customer_tbl> Customer { get; set; }
        public List<Payment_tbl> Payment { get; set; }

    }
}