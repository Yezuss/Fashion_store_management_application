using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class NhanVienController
    {
        /// <summary>
        /// Truy xuất tất cả thông tin nhân viên
        /// </summary>
        public List<NhanVien> getTatcaNhanvien()
        {
            return Common.Common.Intance.NhanViens.ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin lịch làm việc theo nhân viên và tháng làm việc
        /// </summary>
        public List<LichLamViec> getLLVbyNhanVien(NhanVien nhanvien, int thang)
        {
            return Common.Common.Intance.lichLamViecs.Where(x => x.NhanVien.MaNV == nhanvien.MaNV && x.Ngaylamviec.Month == thang).ToList();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin lương theo nhân viên 
        /// </summary>
        //public List<Luong> getLuongbyNhanVien(NhanVien nhanvien)
        //{
        //    return Common.Common.Intance.Luongs.Where(x => x.NhanVien.MaNV == nhanvien.MaNV).ToList();
        //}

        /// <summary>
        /// Truy xuất thông tin nhân viên theo tên hoặc mã nhân viên 
        /// </summary>
        public NhanVien getNhanVien(string tenhoacma)
        {
            return Common.Common.Intance.NhanViens.Where(x => x.Hovaten == tenhoacma || x.MaNV == tenhoacma.ToUpper()).FirstOrDefault();
        }

        /// <summary>
        /// Cập nhật thông tin nhân viên vào cơ sở dữ liệu 
        /// </summary>
        public void CapnhatNhanvien(NhanVien nhanvien)
        {
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Lưu thông tin nhân viên vào cơ sở dữ liệu 
        /// </summary>
        public void LuuNhanvien(NhanVien nhanvien)
        {
            Common.Common.Intance.NhanViens.Add(nhanvien);
            Common.Common.Intance.SaveChanges();
        }

        /// <summary>
        /// Truy xuất danh sách thông tin nhân viên theo tên hoặc mã nhân viên gần đúng        
        /// /// </summary>
        public List<NhanVien> Timkiem(string thongtinNV)
        {
            var dsTK = Common.Common.Intance.NhanViens
                .Where(x => x.Hovaten.Contains(thongtinNV.Trim().ToUpper())
                || x.MaNV.Contains(thongtinNV.Trim().ToUpper())).ToList();

            return dsTK;
        }

        /// <summary>
        /// Lưu thông tin lương vào cơ sở dữ liệu
        /// </summary>
        //public void LuuLuong(Luong luong)
        //{
        //    Common.Common.Intance.Luongs.Add(luong);
        //    Common.Common.Intance.SaveChanges();
        //}

    }
}
