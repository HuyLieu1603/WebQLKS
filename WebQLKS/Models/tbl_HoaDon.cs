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
    
    public partial class tbl_HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_HoaDon()
        {
            this.tbl_ChiTietThongKe = new HashSet<tbl_ChiTietThongKe>();
            this.tbl_DichVuDaDat = new HashSet<tbl_DichVuDaDat>();
        }
    
        public string MaHD { get; set; }
        public Nullable<System.DateTime> NgayThanhToan { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public string MaKH { get; set; }
        public string MaPhieuThuePhong { get; set; }
        public string MaNV { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ChiTietThongKe> tbl_ChiTietThongKe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DichVuDaDat> tbl_DichVuDaDat { get; set; }
        public virtual tbl_PhieuThuePhong tbl_PhieuThuePhong { get; set; }
        public virtual tbl_KhachHang tbl_KhachHang { get; set; }
        public virtual tbl_NhanVien tbl_NhanVien { get; set; }
    }
}
