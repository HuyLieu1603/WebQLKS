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
    
    public partial class tbl_DichVu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DichVu()
        {
            this.tbl_DichVuDaDat = new HashSet<tbl_DichVuDaDat>();
        }
    
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public string MaLoaiDV { get; set; }
        public string MoTa { get; set; }
    
        public virtual tbl_LoaiDichVu tbl_LoaiDichVu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DichVuDaDat> tbl_DichVuDaDat { get; set; }
    }
}
