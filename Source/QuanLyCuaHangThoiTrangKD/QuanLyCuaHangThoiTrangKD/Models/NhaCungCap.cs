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

    [Table("NhaCungCap")]
    class NhaCungCap
    {
        [Key]
        public string MaNCC { get; set; }

        [StringLength(50)]
        public string TenNCC { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(200)]
        public string Diachi { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}
