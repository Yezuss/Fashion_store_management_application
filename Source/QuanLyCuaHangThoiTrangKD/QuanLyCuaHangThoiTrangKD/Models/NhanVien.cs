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

    [Table("NhanVien")]
    class NhanVien
    {
        [Key]
        [StringLength(10)]
        public string MaNV { get; set; }

        [StringLength(50)]
        [Required]
        public string Hovaten { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Ngaysinh { get; set; }

        [StringLength(200)]
        public string Diachi { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Gioitinh { get; set; }

        public float Luongtheogio { get; set; }

        public float Phucap { get; set; }

        [StringLength(20)]
        public string Chucvu { get; set; }

        public virtual ICollection<LichLamViec> LichLamViecs { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }

    }
}
