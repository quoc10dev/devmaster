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
    
    public partial class tblFunction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblFunction()
        {
            this.tblRights = new HashSet<tblRight>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool ShowInMenu { get; set; }
        public Nullable<int> GroupMenu { get; set; }
        public string ImageMenu { get; set; }
        public Nullable<int> IndexShowInPermission { get; set; }
        public string Url { get; set; }
        public string ShortName { get; set; }
        public string Icon_css { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRight> tblRights { get; set; }
    }
}