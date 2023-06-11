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

    [Table("HoaDon")]
    class HoaDon
    {
        [Key]
        public string MaHD { get; set; }

        public DateTime Ngaylap { get; set; }

        public int Calamviec { get; set; }

        [StringLength(30)]
        public string Tinhtrang { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        [StringLength(10)]
        public string MaKH { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("MaKH")]
        public virtual KhachHang KhachHang { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
