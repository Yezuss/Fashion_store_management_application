using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class KhachHangController
    {
        /// <summary>
        /// Cập nhật thông tin khách hàng vào cơ sở dữ liệu
        /// </summary>
        public void CapnhatKhachhang(KhachHang khachhang)
        {
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Lưu thông tin khách hàng vào cơ sở dữ liệu
        /// </summary>
        public void LuuKhachhang(KhachHang khachhang)
        {
            Common.Common.Intance.KhachHangs.Add(khachhang);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin khách hàng theo tên hoặc mã khách hàng gần đúng
        /// </summary>
        public List<KhachHang> Timkiem(string thongtinKH)
        {
            var dsTK = Common.Common.Intance.KhachHangs
                .Where(x => x.Hovaten.Contains(thongtinKH.Trim().ToUpper())
                || x.MaKH.Contains(thongtinKH.Trim().ToUpper())).ToList();

            return dsTK;
        }

        /// <summary>
        /// Truy xuất thông tin khách hàng theo tên hoặc mã khách hàng 
        /// </summary>
        public KhachHang geTKhachhang(string tenhoacma)
        {
            return Common.Common.Intance.KhachHangs.Where(x => x.Hovaten == tenhoacma || x.MaKH == tenhoacma).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin khách hàng
        /// </summary>
        public List<KhachHang> getTatcaKhachhang()
        {
            return Common.Common.Intance.KhachHangs.ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo mã hóa đơn 
        /// </summary>
        public List<HoaDon> getDanhsachHoadon(string ma)
        {
            return Common.Common.Intance.HoaDons.Where(x => x.MaKH == ma).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin chi tiết hóa đơn theo mã hóa đơn
        /// </summary>
        public List<ChiTietHoaDon> getCTHDbyMaHD(string mahd)
        {
            return Common.Common.Intance.ChiTietHoaDons.Where(x => x.MaHD == mahd).ToList();
        }

        /// <summary>
        /// Truy xuất thông tin sản phẩm theo tên hoặc mã sản phẩm
        /// </summary>
        public SanPham getSanPham(string tenhoacma)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.TenSP == tenhoacma.ToUpper() || x.MaSP == tenhoacma.Trim().ToUpper()).FirstOrDefault();
        }
    }
}
