//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblRight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRight()
        {
            this.tblRightsInRoles = new HashSet<tblRightsInRole>();
        }
    
        public int ID { get; set; }
        public int IdFunction { get; set; }
        public string RightName { get; set; }
        public string RightCode { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    
        public virtual tblFunction tblFunction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRightsInRole> tblRightsInRoles { get; set; }
    }
}