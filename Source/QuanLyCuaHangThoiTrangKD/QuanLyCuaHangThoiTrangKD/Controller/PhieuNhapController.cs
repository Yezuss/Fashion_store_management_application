using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class PhieuNhapController
    {
        public void LuuPhieunhap(PhieuNhap phieu)
        {
            Common.Common.Intance.PhieuNhaps.Add(phieu);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin phiếu nhập 
        /// </summary>
        public List<PhieuNhap> getTatcaPhieunhap()
        {
            return Common.Common.Intance.PhieuNhaps.ToList();
        }

        /// <summary>
        /// Truy xuất thông tin phiếu nhập
        /// </summary>
        public PhieuNhap getPhieunhap(int MaPN)
        {
            return Common.Common.Intance.PhieuNhaps.Find(MaPN);
        }

        /// <summary>
        /// Truy xuất danh sách thông tin phiếu nhập theo mã sản phẩm
        /// </summary>
        public List<PhieuNhap> getPhieunhapbyMaSP(string masp)
        {
            var dsctpn = Common.Common.Intance.ChiTietPhieuNhaps.Where(x => x.MaSP == masp).ToList();

            return Common.Common.Intance.PhieuNhaps.Where(x => dsctpn.Any(y => y.MaPN == x.MaPN) == true).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin phiểu nhập theo tên nhân viên
        /// </summary>
        public List<PhieuNhap> getPhieunhapbyTenNV(string tennv)
        {
            NhanVien nv = getNhanVien(tennv);
            return Common.Common.Intance.PhieuNhaps.Where(x => x.MaNV == nv.MaNV).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin phiếu nhập theo tên hoặc SĐT nhà cung cấp
        /// </summary>
        public List<PhieuNhap> getPhieunhapbySDThoacTenNCC(string tenhoacsdt)
        {
            return Common.Common.Intance.PhieuNhaps.Where(x => x.NhaCungCap.TenNCC.Contains(tenhoacsdt.TrimStart().TrimEnd()) || x.NhaCungCap.SDT == tenhoacsdt).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin phiếu nhập theo ngày lập
        /// </summary>
        public List<PhieuNhap> getPhieunhapbyNgaylap(DateTime ngay)
        {
            return Common.Common.Intance.PhieuNhaps.Where(x => x.Ngaylap.Day == ngay.Day && x.Ngaylap.Month == ngay.Month && x.Ngaylap.Year == ngay.Year).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin chi tiết phiếu nhập theo mã phiếu nhập 
        /// </summary>
        public List<ChiTietPhieuNhap> getCTPNbyMaPN(int mapn)
        {
            return Common.Common.Intance.ChiTietPhieuNhaps.Where(x => x.MaPN == mapn).ToList();
        }

        /// <summary>
        /// Truy xuất thông tin nhà cung cấp  
        /// </summary>
        public NhaCungCap getNhaCungCap(string tenhoacma)
        {
            return Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC == tenhoacma || x.MaNCC == tenhoacma).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin nhà cung cấp gần đúng
        /// </summary>
        public NhaCungCap TimkiemNhaCungCap(string tenhoacma)
        {
            return Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC.ToLower().Contains(tenhoacma.ToLower()) || x.MaNCC == tenhoacma.ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin nhà cung cấp theo tên gần đúng hoặc mã nhà cung cấp
        /// </summary>
        public List<NhaCungCap> TimkiemDanhsachNhaCungCap(string tenhoacma)
        {
            return Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC.ToLower().Contains(tenhoacma.ToLower()) || x.MaNCC == tenhoacma.ToUpper()).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin nhà cung cấp theo tên gần đúng hoặc SĐT nhâ cung cấp 
        /// </summary>
        public List<NhaCungCap> TimkiemDanhsachNhaCungCapByTenhoacSDT(string tenhoacsdt)
        {
            return Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC.ToLower().Contains(tenhoacsdt.ToLower()) || x.SDT == tenhoacsdt.ToUpper()).ToList();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin sản phẩm 
        /// </summary>
        public List<NhaCungCap> getTatcaNhaCungCap()
        {
            return Common.Common.Intance.NhaCungCaps.ToList();
        }

        /// <summary>
        /// Truy xuất thông tin số lượng chi tiết phiếu nhập của phiếu nhập
        /// </summary>
        public int getSolongCTPN(PhieuNhap pn)
        {
            return Common.Common.Intance.ChiTietPhieuNhaps.Where(x => x.MaPN == pn.MaPN).ToList().Count;
        }

        //==================================================================

        /// <summary>
        /// Truy xuất thông tin tài khoản theo mã tài khoản
        /// </summary>
        public TaiKhoan getTaiKhoanByMaTK(int ma)
        {
            return Common.Common.Intance.TaiKhoans.Find(ma);
        }

        /// <summary>
        /// Truy xuất thông tin nhân viên theo tài khoản
        /// </summary>
        public NhanVien getNhanvienbyMaTK(TaiKhoan taiKhoan)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.MaNV == taiKhoan.MaNV).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin nhân viên
        /// </summary>
        public List<NhanVien> getTatcaNhanvien()
        {
            return Common.Common.Intance.NhanViens.ToList();
        }

        /// <summary>
        /// Truy xuất thông tin nhân viên theo tên hoặc mã nhân viên
        /// </summary>
        public NhanVien getNhanVien(string tenhoacma)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.Hovaten == tenhoacma.ToLower() || x.MaNV == tenhoacma.Trim().ToLower()).FirstOrDefault();
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
        /// Lưu thông tin sản phẩm vào cơ sở dữ liệu
        /// </summary>
        public void LuuSanpham(SanPham sanpham)
        {
            Common.Common.Intance.SanPhams.Add(sanpham);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra thông tin sản phẩm tồn tại trong cơ sở dữ liệu 
        /// </summary>
        public bool KiemtraSanphamTontai(SanPham sp)
        {
            return Common.Common.Intance.SanPhams.Any(x => x.TenSP == sp.TenSP && x.Chatlieu == sp.Chatlieu && x.Mausac == sp.Mausac && x.Kichthuoc == sp.Kichthuoc && x.LoaiSP == sp.LoaiSP && x.Donvi == sp.Donvi && x.Dongianhap == sp.Dongianhap);
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
        /// Truy xuất nhóm danh sách thông tin chi tiết phiếu nhập theo mã sản phẩm
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

    }
}
