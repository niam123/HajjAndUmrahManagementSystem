//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HajAndUmrah.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackageGroup_tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PackageGroup_tbl()
        {
            this.Package_tbl = new HashSet<Package_tbl>();
        }
    
        public int PackageGroupId { get; set; }
        public string PackageGroupName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package_tbl> Package_tbl { get; set; }
    }
}
