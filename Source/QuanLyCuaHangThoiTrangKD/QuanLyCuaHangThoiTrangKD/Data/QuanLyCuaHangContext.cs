using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Data
{
    class QuanLyBanHangContext : DbContext
    {
        public QuanLyBanHangContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<LichLamViec> lichLamViecs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }


    }
}
