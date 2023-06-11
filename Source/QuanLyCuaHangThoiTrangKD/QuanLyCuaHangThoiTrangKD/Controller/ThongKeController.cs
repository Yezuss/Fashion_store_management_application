using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class ThongKeController
    {
        /// <summary>
        /// Truy xuất nhóm thông tin chi tiết hóa đơn theo mã sản phẩm
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
        /// Truy xuất nhóm thông tin chi tiết phiếu nhập theo mã sản phẩm
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
        /// Truy xuất nhóm thông tin chi tiết phiếu nhập theo loại sản phẩm
        /// </summary>
        public List<LoaiSP_SLTon> LoadDanhsachCTPNByLoaiSP()
        {
            List<LoaiSP_SLTon> ds = new List<LoaiSP_SLTon>();
            var dsCTPN = Common.Common.Intance.ChiTietPhieuNhaps
                .GroupBy(x => new { x.SanPham.LoaiSP })
                .Select(z => new { Loaisp = z.Key.LoaiSP, TongSL = z.Sum(y => y.Soluong) })
                .OrderByDescending(z => z.TongSL);
            foreach (var ct in dsCTPN)
            {
                ds.Add(new LoaiSP_SLTon() { Loaisp = ct.Loaisp, Tong = ct.TongSL });
            }
            return ds;
        }

        /// <summary>
        /// Truy xuất nhóm thông tin chi tiết hóa đơn theo loại sản phẩm
        /// </summary>
        public List<LoaiSP_SLTon> LoadDanhsachCTHDByLoaiSP()
        {
            List<LoaiSP_SLTon> ds = new List<LoaiSP_SLTon>();
            var dsCTHD = Common.Common.Intance.ChiTietHoaDons
                .GroupBy(x => new { x.SanPham.LoaiSP })
                .Select(z => new { Loaisp = z.Key.LoaiSP, TongSL = z.Sum(y => y.Soluong) })
                .OrderByDescending(z => z.TongSL);
            foreach (var ct in dsCTHD)
            {
                ds.Add(new LoaiSP_SLTon() { Loaisp = ct.Loaisp, Tong = ct.TongSL });
            }
            return ds;
        }


        /// <summary>
        /// Truy xuất nhóm thông tin chi tiết hóa đơn theo tên sản phẩm
        /// </summary>
        public List<LoaiSP_SLTon> LoadDanhsachCTHDByTenSP()
        {
            List<LoaiSP_SLTon> ds = new List<LoaiSP_SLTon>();
            var dsCTHD = Common.Common.Intance.ChiTietHoaDons
                .GroupBy(x => x.SanPham.TenSP)
                .Select(z => new { Tensp = z.Key, TongSL = z.Sum(y => y.Soluong) });
            foreach (var ct in dsCTHD)
            {
                ds.Add(new LoaiSP_SLTon() { Loaisp = ct.Tensp, Tong = ct.TongSL });
            }
            return ds;
        }

        /// <summary>
        /// Truy xuất danh sách thông tin chi tiết hóa đơn theo mã hóa đơn
        /// </summary>
        public List<ChiTietHoaDon> getCTHDbyMaHD(string mahd)
        {
            return Common.Common.Intance.ChiTietHoaDons.Where(x => x.MaHD == mahd).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin chi tiết phiếu nhập theo mã phiếu nhập
        /// </summary>
        public List<ChiTietPhieuNhap> getCTPNbyMaPN(int mapn)
        {
            return Common.Common.Intance.ChiTietPhieuNhaps.Where(x => x.MaPN == mapn).ToList();
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm vào cơ sở dữ liệu
        /// </summary>
        public void CapnhatSanpham()
        {
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Truy xuất thông tin sản phẩm
        /// </summary>
        public SanPham getSanPham(string tenhoacma)
        {
            return Common.Common.Intance.SanPhams.Where(x => x.TenSP == tenhoacma.ToUpper() || x.MaSP == tenhoacma.Trim().ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin nhân viên theo mã tài khoản
        /// </summary>
        public NhanVien getNhanvienbyMaTK(TaiKhoan taiKhoan)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.MaNV == taiKhoan.MaNV).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin tài khoản theo mã tài khoản
        /// </summary>
        public TaiKhoan getTaiKhoanByMaTK(int ma)
        {
            return Common.Common.Intance.TaiKhoans.Find(ma);
        }

        /// <summary>
        /// Truy xuất tất cả thông tin hóa đơn 
        /// </summary>
        public List<HoaDon> getTatcaHoadon()
        {
            return Common.Common.Intance.HoaDons.ToList();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin sản phẩm 
        /// </summary>
        public List<SanPham> getTatcaSanpham()
        {
            return Common.Common.Intance.SanPhams.ToList();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin nhân viên
        /// </summary>
        public List<NhanVien> getTatcaNhanvien()
        {
            return Common.Common.Intance.NhanViens.ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo tháng và năm
        /// </summary>
        public List<HoaDon> getHoadonbyThangNam(int thang, int nam)
        {
            return Common.Common.Intance.HoaDons.Where(x => x.Ngaylap.Month == thang && x.Ngaylap.Year == nam).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo năm
        /// </summary>
        public List<HoaDon> getHoadonbyNam(int nam)
        {
            return Common.Common.Intance.HoaDons.Where(x => x.Ngaylap.Year == nam).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo năm
        /// </summary>
        public List<PhieuNhap> getPhieunhapbyNam(int nam)
        {
            return Common.Common.Intance.PhieuNhaps.Where(x => x.Ngaylap.Year == nam).ToList();
        }


        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo mã hoặc tên nhân viên
        /// </summary>
        public List<HoaDon> getHoadonbyMaNVhoacten(string manvhoacten)
        {
            NhanVien nv = getNhanVien(manvhoacten);
            return Common.Common.Intance.HoaDons.Where(x => x.MaNV == nv.MaNV).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin hóa đơn theo ca làm việc
        /// </summary>
        public List<HoaDon> getHoadonbyCalamviec(int calamviec)
        {
            return Common.Common.Intance.HoaDons.Where(x => x.Calamviec  == calamviec).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin phiếu nhập theo tên nhà cung cấp
        /// </summary>
        public List<PhieuNhap> getPhieunhapbyTenNCC(string ten)
        {
            return Common.Common.Intance.PhieuNhaps.Where(x => x.NhaCungCap.TenNCC.ToLower() == ten.ToLower()).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin chi tiết phiếu nhập theo tên sản phẩm
        /// </summary>
        public List<ChiTietPhieuNhap> getCTPNbyTenSP(string ten)
        {
            SanPham sp = Common.Common.Intance.SanPhams.Where(x => x.TenSP == ten.ToUpper()).FirstOrDefault();
            
            return Common.Common.Intance.ChiTietPhieuNhaps.Where(x => x.MaSP == sp.MaSP).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin chi tiết hóa đơn theo tên sản phẩm
        /// </summary>
        public List<ChiTietHoaDon> getCTHDbyTenSP(string ten)
        {
            SanPham sp = Common.Common.Intance.SanPhams.Where(x => x.TenSP == ten.ToUpper()).FirstOrDefault();

            return Common.Common.Intance.ChiTietHoaDons.Where(x => x.MaSP == sp.MaSP).ToList();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin chi tiết hóa đơn
        /// </summary>
        public List<ChiTietHoaDon> getCTHD()
        {
            return Common.Common.Intance.ChiTietHoaDons.ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin phiếu nhập theo tháng và năm
        /// </summary>
        public List<PhieuNhap> getPhieunhapbyThangNam(int thang, int nam)
        {
            return Common.Common.Intance.PhieuNhaps.Where(x => x.Ngaylap.Month == thang && x.Ngaylap.Year == nam).ToList();
        }

        /// <summary>
        /// Truy xuất thông tin khách hàng
        /// </summary>
        public KhachHang geTKhachhang(string tenhoacma)
        {
            return Common.Common.Intance.KhachHangs.Where(x => x.Hovaten.ToLower() == tenhoacma.ToLower() || x.MaKH == tenhoacma.ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin nhân viên
        /// </summary>
        public NhanVien getNhanVien(string tenhoacma)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.Hovaten == tenhoacma.ToLower() || x.MaNV == tenhoacma.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin số lượng chi tiết hóa đơn theo hóa đơn
        /// </summary>
        public int getSolongCTHD(HoaDon hoadon)
        {
            return Common.Common.Intance.ChiTietHoaDons.Where(x => x.MaHD == hoadon.MaHD).ToList().Count;
        }

        /// <summary>
        /// Truy xuất thông tin nhà cung cấp
        /// </summary>
        public NhaCungCap getNhaCungCap(string tenhoacma)
        {
            return Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC == tenhoacma || x.MaNCC == tenhoacma).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin nhà cung cấp
        /// </summary>
        public List<NhaCungCap> getTatcaNhaCungCap()
        {
            return Common.Common.Intance.NhaCungCaps.ToList();
        }

    }
}
