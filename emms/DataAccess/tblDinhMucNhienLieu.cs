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
    
    public partial class tblDinhMucNhienLieu
    {
        public int ID { get; set; }
        public int IDTrangThietBi { get; set; }
        public double SoLuongDinhMuc { get; set; }
        public System.DateTime NgayBatDauApDung { get; set; }
    
        public virtual tblTrangThietBi tblTrangThietBi { get; set; }
    }
}