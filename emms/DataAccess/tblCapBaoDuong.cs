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
    
    public partial class tblCapBaoDuong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCapBaoDuong()
        {
            this.tblCapBaoDuongVatTus = new HashSet<tblCapBaoDuongVatTu>();
            this.tblTheBaoDuongs = new HashSet<tblTheBaoDuong>();
            this.tblCapBaoDuongCongViecs = new HashSet<tblCapBaoDuongCongViec>();
        }
    
        public int ID { get; set; }
        public string Ten { get; set; }
        public string GhiChu { get; set; }
        public int IDLoaiBaoDuong { get; set; }
        public double SoLuongMoc { get; set; }
        public string ShowInPrintPosition1 { get; set; }
        public string ShowInPrintPosition2 { get; set; }
        public Nullable<int> LevelInPrint { get; set; }
    
        public virtual tblLoaiBaoDuong tblLoaiBaoDuong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCapBaoDuongVatTu> tblCapBaoDuongVatTus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTheBaoDuong> tblTheBaoDuongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCapBaoDuongCongViec> tblCapBaoDuongCongViecs { get; set; }
    }
}