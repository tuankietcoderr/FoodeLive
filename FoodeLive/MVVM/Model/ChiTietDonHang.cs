//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodeLive.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiTietDonHang()
        {
            this.ThongBaos = new HashSet<ThongBao>();
        }
    
        public string MaDonHang { get; set; }
        public string MaMonAn { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<byte> TrangThai { get; set; }
    
        public virtual MonAn MonAn { get; set; }
        public virtual DonHang DonHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
