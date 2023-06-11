using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCuaHangThoiTrangKD.Models
{
        /**
     * Người thực hiện: Văn Khải
     * Ngày khởi tạo: 11/11/2021
     * **/

    [Table("SanPham")]
    class SanPham
    {
        [Key]
        public string MaSP { get; set; }

        [StringLength(50)]
        public string TenSP { get; set; }

        [StringLength(50)]
        public string Chatlieu { get; set; }

        [StringLength(5)]
        public string Kichthuoc { get; set; }

        [StringLength(20)]
        public string Mausac { get; set; }

        [StringLength(30)]
        public string LoaiSP { get; set; }

        [StringLength(20)]
        public string Donvi { get; set; }

        [StringLength(30)]
        public string Tinhtrang { get; set; }

        public float Dongianhap { get; set; }

        public float Dongia { get; set; }

        public byte[] Hinhanh { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}
