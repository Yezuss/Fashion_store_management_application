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

    [Table("ChiTietHoaDon")]
    class ChiTietHoaDon
    {
        [Key]
        public int MaCTHD { get; set; }

        public int Soluong { get; set; }

        [StringLength(10)]
        public string MaSP { get; set; }

        public string MaHD { get; set; }

        [ForeignKey("MaSP")]
        public virtual SanPham SanPham { get; set; }

        [ForeignKey("MaHD")]
        public virtual HoaDon HoaDon { get; set; }
    }
}
