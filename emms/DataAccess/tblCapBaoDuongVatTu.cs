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
    
    public partial class tblCapBaoDuongVatTu
    {
        public int ID { get; set; }
        public int IDCapBaoDuong { get; set; }
        public int IDVatTu { get; set; }
    
        public virtual tblCapBaoDuong tblCapBaoDuong { get; set; }
        public virtual tblVatTu tblVatTu { get; set; }
    }
}