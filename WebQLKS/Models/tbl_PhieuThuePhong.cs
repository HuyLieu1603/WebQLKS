//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebQLKS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_PhieuThuePhong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_PhieuThuePhong()
        {
            this.tbl_ChiTieuPhieuThue = new HashSet<tbl_ChiTieuPhieuThue>();
            this.tbl_HoaDon = new HashSet<tbl_HoaDon>();
        }
    
        public string MaPhieuThuePhong { get; set; }
        public Nullable<System.DateTime> NgayBatDauThue { get; set; }
        public string MaPhong { get; set; }
        public Nullable<int> SLKhach { get; set; }
        public Nullable<int> SLKhachNuocNgoai { get; set; }
        public Nullable<System.DateTime> NgayKetThucThue { get; set; }
        public string TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ChiTieuPhieuThue> tbl_ChiTieuPhieuThue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HoaDon> tbl_HoaDon { get; set; }
        public virtual tbl_Phong tbl_Phong { get; set; }
    }
}
