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
    
    public partial class tbl_Phong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Phong()
        {
            this.tbl_PhieuThuePhong = new HashSet<tbl_PhieuThuePhong>();
        }
    
        public string MaPhong { get; set; }
        public Nullable<int> SoPhong { get; set; }
        public string MaLoaiPhong { get; set; }
        public string MaTrangThai { get; set; }
        public string HinhAnh { get; set; }
    
        public virtual tbl_LoaiPhong tbl_LoaiPhong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PhieuThuePhong> tbl_PhieuThuePhong { get; set; }
        public virtual tbl_TrangThaiPhong tbl_TrangThaiPhong { get; set; }
    }
}
