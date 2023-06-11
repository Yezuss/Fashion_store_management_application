using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class HoaDonController
    {
        /// <summary>
        ///Cập nhật thông tin hóa đơn vào cơ sở dữ liệu
        /// </summary>
        public void CapnhatHoadon()
        {
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Lưu thông tin hóa đơn vào cơ sở dữ liệu
        /// </summary>
        public void LuuHoaDon(HoaDon hoadon)
        {
            Common.Common.Intance.HoaDons.Add(hoadon);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Hủy thông tin hóa đơn 
        /// </summary>
        public void HuyHoaDon(HoaDon hoadon)
        {
            HoaDon hd;
            if(hoadon != null)
            {
                hd = getHoadon(hoadon.MaHD);
                if (hd != null)
                {
                    var dscthd = getCTHDbyMaHD(hd.MaHD);
                    Common.Common.Intance.HoaDons.Remove(hd);
                    Common.Common.Intance.ChiTietHoaDons.RemoveRange(dscthd);
                }
            }
        }

        /// <summary>
        /// Truy xuất thông tin hóa đơn theo mã 
        /// </summary>
        public HoaDon getHoadon(string ma)
        {
            return Common.Common.Intance.HoaDons.Find(ma);
        }

        /// <summary>
        /// Truy xuất tất cả thông tin hóa đơn
        /// </summary>
        public List<HoaDon> getTatcaHoadon()
        {
            return Common.Common.Intance.HoaDons.ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo tình trạng
        /// </summary>
        public List<HoaDon> getHoadonbyTinhtrang(string tinhtrang)
        {
            return Common.Common.Intance.HoaDons.Where(x => x.Tinhtrang == "Đang chờ").ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo tên nhân viên
        /// </summary>
        public List<HoaDon> getHoadonbyTenNV(string tennv)
        {
            NhanVien nv = getNhanVien(tennv);
            return Common.Common.Intance.HoaDons.Where(x => x.MaNV == nv.MaNV).ToList();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin nhân viên
        /// </summary>
        public List<NhanVien> getTatcaNhanvien()
        {
            return Common.Common.Intance.NhanViens.ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo tên hoặc SĐT khách hàng
        /// </summary>
        public List<HoaDon> getHoadonbySDThoacTenKH(string sdthoacten)
        {
            return Common.Common.Intance.HoaDons.Where(x => x.KhachHang.Hovaten.Contains(sdthoacten.TrimStart().TrimEnd()) || x.KhachHang.SDT == sdthoacten).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo ngày lập
        /// </summary>
        public List<HoaDon> getHoadonbyNgaylap(DateTime ngay)
        {
            return Common.Common.Intance.HoaDons.Where(x => x.Ngaylap.Day == ngay.Day && x.Ngaylap.Month == ngay.Month && x.Ngaylap.Year == ngay.Year).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin chi tiết hóa đơn theo mã hóa đơn
        /// </summary>
        public List<ChiTietHoaDon> getCTHDbyMaHD(string mahd)
        {
            return Common.Common.Intance.ChiTietHoaDons.Where(x => x.MaHD == mahd).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin tài khoản theo mã tài khoản
        /// </summary>
        public TaiKhoan getTaiKhoanByMaTK(int ma)
        {
            return Common.Common.Intance.TaiKhoans.Find(ma);
        }

        /// <summary>
        /// Truy xuất danh sách thông tin sản phẩm theo tên gần đúng hoặc mã
        /// </summary>
        public List<SanPham> TimkiemDanhsachSanPham(string tenhoacma)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.TenSP.Contains(tenhoacma.ToUpper()) || x.MaSP.Contains(tenhoacma.Trim().ToUpper())).ToList();
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
        /// Truy xuất thông tin khách hàng theo tên hoặc mã khách hàng
        /// </summary>
        public KhachHang geTKhachhang(string tenhoacma)
        {
            return Common.Common.Intance.KhachHangs.Where(x => x.Hovaten.ToLower().Contains(tenhoacma.ToLower()) || x.MaKH == tenhoacma.ToUpper()).FirstOrDefault();
        }

        //public List<KhachHang> TimkiemDanhsachKhachhang(string tenhoacsdt)
        //{
        //    return Common.Common.Intance.KhachHangs.Where(x => x.Hovaten.ToLower().Contains(tenhoacsdt.ToLower()) || x.SDT == tenhoacsdt.ToUpper()).ToList();
        //}

        /// <summary>
        /// Truy xuất danh sách thông tin khách hàng theo tên gần đúng hoặc SĐT chính xác
        /// </summary>
        public KhachHang TimkiemKhachhang(string tenhoacsdt)
        {
            return Common.Common.Intance.KhachHangs.Where(x => x.Hovaten.ToLower() == tenhoacsdt.ToLower() || x.SDT == tenhoacsdt.TrimStart().TrimEnd()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin nhân viên theo tên hoặc mã nhân viên
        /// </summary>
        public NhanVien getNhanVien(string tenhoacma)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.Hovaten.ToLower() == tenhoacma.ToLower() || x.MaNV == tenhoacma.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin số lượng chi tiết hóa đơn theo mã hóa đơn
        /// </summary>
        public int getSolongCTHD(HoaDon hoadon)
        {
            return Common.Common.Intance.ChiTietHoaDons.Where(x => x.MaHD == hoadon.MaHD).ToList().Count;
        }

        /// <summary>
        /// Truy xuất danh sách thông tin nhân viên theo mã tài khoản
        /// </summary>
        public NhanVien getNhanvienbyMaTK(TaiKhoan taiKhoan)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.MaNV == taiKhoan.MaNV).FirstOrDefault();
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
        /// Truy xuất nhóm danh sách thông tin chi tiết hóa đơn theo mã sản phẩm  
        /// </summary>
        public List<CTByMaSP> LoadDanhsachCTHDByMaSP()
        {
            List<CTByMaSP> ds = new List<CTByMaSP>();
            var dsCTHD = Common.Common.Intance.ChiTietHoaDons
                .GroupBy(x => x.SanPham.MaSP)
                .Select(z => new { MaSPHD = z.Key, TongSL = z.Sum(y => y.Soluong) })
                .OrderByDescending(z => z.TongSL);
            foreach (var ct in dsCTHD)
            {
                ds.Add(new CTByMaSP() { Masp = ct.MaSPHD, TongSL = ct.TongSL });
            }
            return ds;
        }

        /// <summary>
        /// Truy xuất nhóm danh sách thông tin chi tiết phiếu nhập theo mã sản phấm 
        /// </summary>
        public List<CTByMaSP> LoadDanhsachCTPNByMaSP()
        {
            List<CTByMaSP> ds = new List<CTByMaSP>();
            var dsCTPN = Common.Common.Intance.ChiTietPhieuNhaps
                .GroupBy(x => x.SanPham.MaSP)
                .Select(z => new { MaSPPN = z.Key, TongSL = z.Sum(y => y.Soluong) })
                .OrderByDescending(z => z.TongSL);
            foreach (var ct in dsCTPN)
            {
                ds.Add(new CTByMaSP() { Masp = ct.MaSPPN, TongSL = ct.TongSL });
            }
            return ds;
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm vào cơ sở dữ liệu 
        /// </summary>
        public void CapnhatSanpham()
        {
            Common.Common.Intance.SaveChanges();
        }
    }
}
