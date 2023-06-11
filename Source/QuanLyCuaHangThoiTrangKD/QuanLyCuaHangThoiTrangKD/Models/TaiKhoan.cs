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

    [Table("TaiKhoan")]
    class TaiKhoan
    {
        [Key]
        public int MaTK { get; set; }

        [StringLength(20)]
        public string Tentaikhoan { get; set; }

        [StringLength(20)]
        public string Matkhau { get; set; }

        [StringLength(20)]
        public string Loai { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

    }
}
