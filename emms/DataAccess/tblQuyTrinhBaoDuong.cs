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
    
    public partial class tblQuyTrinhBaoDuong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblQuyTrinhBaoDuong()
        {
            this.tblTrangThietBis = new HashSet<tblTrangThietBi>();
        }
    
        public int ID { get; set; }
        public string MaQuyTrinh { get; set; }
        public int ThuTu { get; set; }
        public string GiaTri { get; set; }
        public int IDCapBaoDuong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTrangThietBi> tblTrangThietBis { get; set; }
    }
}