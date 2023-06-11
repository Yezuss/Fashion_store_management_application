using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class SanPhamController
    {
        /// <summary>
        /// Cập nhật thông tin sản phẩm vào cơ sở dữ liệu 
        /// </summary>
        public void CapnhatSanpham(SanPham sanpham)
        {
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Lưu thông tin sản phẩm vào cơ sở dữ liệu 
        /// </summary>
        public void LuuSanpham(SanPham sanpham)
        {
            Common.Common.Intance.SanPhams.Add(sanpham);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin theo tên hoặc mã sản phẩm gần đúng
        /// </summary>
        public List<SanPham> Timkiem(string thongtintimkiem)
        {
            List<SanPham> dsSanpham = Common.Common.Intance.SanPhams
                    .Where(x => x.TenSP.Contains(thongtintimkiem.ToUpper())
                    || x.MaSP.Contains(thongtintimkiem.Trim().ToUpper())).ToList();

            return dsSanpham;
        }

        /// <summary>
        /// Truy xuất danh sách thông tin sản phẩm theo chất liệu
        /// </summary>
        public List<SanPham> TimkiemtheoChatlieu(string chatlieu)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.Chatlieu.Contains(chatlieu.TrimStart().TrimEnd())).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin sản phẩm theo màu sắc
        /// </summary>
        public List<SanPham> TimkiemtheoMausac(string mausac)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.Mausac == mausac).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin sản phẩm theo loại sản phẩm
        /// </summary>
        public List<SanPham> TimkiemtheoLoaiSP(string loaisp)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.LoaiSP == loaisp).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin sản phẩm theo kích thước
        /// </summary>
        public List<SanPham> TimkiemtheoKichthuoc(string kichthuoc)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.Kichthuoc == kichthuoc).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin sản phẩm theo tên hoặc mã sản phẩm
        /// </summary>
        public List<SanPham> getDanhsachSanPham(string tenhoacma)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.TenSP == tenhoacma.ToUpper() || x.MaSP == tenhoacma.Trim().ToUpper()).ToList();
        }

        /// <summary>
        /// Truy xuất thông tin sản phẩm theo tên hoặc mã sản phẩm
        /// </summary>
        public SanPham getSanPham(string tenhoacma)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.TenSP == tenhoacma.ToUpper() || x.MaSP == tenhoacma.Trim().ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin sản phẩm 
        /// </summary>
        public List<SanPham> getTatcaSanpham()
        {
            return Common.Common.Intance.SanPhams.ToList();
        }

        /// <summary>
        /// Truy xuất thông tin nhà cung cấp 
        /// </summary>
        public NhaCungCap getNhaCungCap(string tenhoacma)
        {
            return Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC == tenhoacma || x.MaNCC == tenhoacma.ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin nhà cung cấp gần đúng
        /// </summary>
        public NhaCungCap TimkiemNhaCungCap(string tenhoacma)
        {
            return Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC.ToLower().Contains(tenhoacma.ToLower()) || x.MaNCC == tenhoacma.ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin nhà cung cấp 
        /// </summary>
        public List<NhaCungCap> getTatcaNhaCungCap()
        {
            return Common.Common.Intance.NhaCungCaps.ToList();
        }

        /// <summary>
        /// Cập nhật thông tin nhà cung cấp vào cơ sở dữ liệu
        /// </summary>
        public void CapnhatNhacungcap()
        {
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Lưu thông tin nhà cung cấp vào cơ sở dữ liệu
        /// </summary>
        public void LuuNhacungcap(NhaCungCap ncc)
        {
            Common.Common.Intance.NhaCungCaps.Add(ncc);
            Common.Common.Intance.SaveChanges();
        }
    }
}
