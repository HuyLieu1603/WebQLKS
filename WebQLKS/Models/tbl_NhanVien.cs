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
    
    public partial class tbl_NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_NhanVien()
        {
            this.tbl_DichVuDaDat = new HashSet<tbl_DichVuDaDat>();
            this.tbl_HoaDon = new HashSet<tbl_HoaDon>();
            this.tbl_PhieuThuePhong = new HashSet<tbl_PhieuThuePhong>();
            this.tbl_ThongKe = new HashSet<tbl_ThongKe>();
            this.tbl_PhieuThuePhong = new HashSet<tbl_PhieuThuePhong>();
        }
    
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string MaCV { get; set; }
    
        public virtual tbl_ChucVu tbl_ChucVu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DichVuDaDat> tbl_DichVuDaDat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HoaDon> tbl_HoaDon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PhieuThuePhong> tbl_PhieuThuePhong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ThongKe> tbl_ThongKe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PhieuThuePhong> tbl_PhieuThuePhong { get; set; }
    }
}
