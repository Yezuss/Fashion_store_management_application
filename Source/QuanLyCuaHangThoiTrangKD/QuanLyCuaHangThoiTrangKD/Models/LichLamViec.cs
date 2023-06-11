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

    [Table("LichLamViec")]
    class LichLamViec
    {
        [Key]
        public int MaLLV { get; set; }

        public DateTime Ngaylamviec { get; set; }

        public int Tongsocalamviec { get; set; }

        public string MaNV { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

    }
}
