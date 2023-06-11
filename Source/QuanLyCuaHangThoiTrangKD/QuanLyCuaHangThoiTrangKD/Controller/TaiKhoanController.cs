using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class TaiKhoanController
    {
        /// <summary>
        /// Truy xuất danh sách thông tin nhân viên chưa cấp tài khoản
        /// </summary>
        public List<NhanVien> getNhanVienChuaCapTaikhoan()
        {
            return Common.Common.Intance.NhanViens.Where(x => x.TaiKhoans.FirstOrDefault() == null).ToList();
        }

        /// <summary>
        /// Truy xuất thông tin nhân viên
        /// </summary>
        public NhanVien getNhanVien(string tenhoacma)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.Hovaten == tenhoacma || x.MaNV == tenhoacma.ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất tất cả thông tin tài khoản 
        /// </summary>
        public List<TaiKhoan> getTatcaTaikhoan()
        {
            return Common.Common.Intance.TaiKhoans.ToList();
        }

        /// <summary>
        /// Truy xuất thông tin tài khoản
        /// </summary>
        public TaiKhoan getTaiKhoan(string taikhoan)
        {
            return Common.Common.Intance.TaiKhoans.Where(x => x.Tentaikhoan == taikhoan.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// Truy xuất thông tin tài khoản theo mã tài khoản
        /// </summary>
        public TaiKhoan getTaiKhoanByMaTK(int ma)
        {
            return Common.Common.Intance.TaiKhoans.Find(ma);
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản vào cơ sở dữ liệu 
        /// </summary>
        public void CapnhatTaikhoan(TaiKhoan taikhoan)
        {

            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Lưu thông tin tài khoản vào cơ sở dữ liệu 
        /// </summary>
        public void LuuTaikhoan(TaiKhoan taikhoan)
        {
            Common.Common.Intance.TaiKhoans.Add(taikhoan);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Đăng nhập vào chương trình theo tên và mật khẩu tài khoản
        /// </summary>
        public TaiKhoan Dangnhap(string tentaikhoan, string matkhau)
        {
            return Common.Common.Intance.TaiKhoans.Where(x => x.Tentaikhoan == tentaikhoan.ToLower()
                        && x.Matkhau == matkhau.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// Cập nhật thông tin mật khẩu mặc định ("123abc") 
        /// </summary>
        public void Datlaimatkhau(TaiKhoan taikhoan)
        {
            taikhoan.Matkhau = "123abc";
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin mật khẩu mới
        /// </summary>
        public void Thaydoimatkhau(TaiKhoan taikhoan, string matkhaumoi)
        {
            taikhoan.Matkhau = matkhaumoi;
            CapnhatTaikhoan(taikhoan);
        }

        /// <summary>
        /// Lưu thông tin lịch làm việc vào cơ sở dữ liệu
        /// </summary>
        public void LuuLichlamviec(LichLamViec llv)
        {
            Common.Common.Intance.lichLamViecs.Add(llv);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin lịch làm việc vào cơ sở dữ liệu
        /// </summary>
        public void CapnhatLichlamviec()
        {
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Xóa thông tin lịch làm việc theo mã lịch làm việc 
        /// </summary>
        public bool XoaCalamviec(int mallv)
        {
            try
            {
                LichLamViec llv = Common.Common.Intance.lichLamViecs.Find(mallv);
                Common.Common.Intance.lichLamViecs.Remove(llv);
                Common.Common.Intance.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Truy xuất thông tin lich làm việc theo nhân viên và ngày làm việc 
        /// </summary>        
        public LichLamViec TimkiemLLVByNVvaNgaylap(string manv, DateTime ngaylam)
        {
            return Common.Common.Intance.lichLamViecs.Where(x => x.MaNV == manv && x.Ngaylamviec.Day == ngaylam.Day && x.Ngaylamviec.Month == ngaylam.Month && x.Ngaylamviec.Year == ngaylam.Year).FirstOrDefault();
        }
    }
}
