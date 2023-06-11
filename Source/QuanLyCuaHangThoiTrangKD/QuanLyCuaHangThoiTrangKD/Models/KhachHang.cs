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

    [Table("KhachHang")]
    class KhachHang
    {
        [Key]
        public string MaKH { get; set; }

        [StringLength(50)]
        public string Hovaten { get; set; }
        [StringLength(10)]
        public string Gioitinh { get; set; }
        [StringLength(20)]
        public string SDT { get; set; }
        [StringLength(200)]
        public string Diachi { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
