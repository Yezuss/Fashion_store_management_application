namespace QuanLyCuaHangThoiTrangKD.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using QuanLyCuaHangThoiTrangKD.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<QuanLyCuaHangThoiTrangKD.Data.QuanLyBanHangContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuanLyCuaHangThoiTrangKD.Data.QuanLyBanHangContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //TaiKhoan taiKhoan = new TaiKhoan() { Tentaikhoan = "tranvankhai", Matkhau = "123abc", Loai = "Quản lý" };
            //context.TaiKhoans.AddOrUpdate(taiKhoan);
            //context.NhanViens.AddOrUpdate(new NhanVien() { MaNV = "VK2122", Hovaten = "Trần Văn Khải", Diachi = "12/45 Nguyễn Văn Bảo, Gò vấp", Chucvu = "Quản lý", Email = "tranvankhai123@gmail.com", Gioitinh = "Nam", Luongtheogio = 40000, TaiKhoan = taiKhoan, Ngaysinh = DateTime.Parse("10/10/1998"), Phucap = 300000, SDT = "0354988651" });
        }
    }
}
