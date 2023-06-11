namespace QuanLyCuaHangThoiTrangKD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KDSHOP_Migra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietHoaDon",
                c => new
                    {
                        MaCTHD = c.Int(nullable: false, identity: true),
                        Soluong = c.Int(nullable: false),
                        MaSP = c.String(maxLength: 128),
                        MaHD = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaCTHD)
                .ForeignKey("dbo.HoaDon", t => t.MaHD)
                .ForeignKey("dbo.SanPham", t => t.MaSP)
                .Index(t => t.MaSP)
                .Index(t => t.MaHD);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        MaHD = c.String(nullable: false, maxLength: 128),
                        Ngaylap = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Calamviec = c.Int(nullable: false),
                        Tinhtrang = c.String(maxLength: 30),
                        MaNV = c.String(maxLength: 10),
                        MaKH = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaHD)
                .ForeignKey("dbo.KhachHang", t => t.MaKH)
                .ForeignKey("dbo.NhanVien", t => t.MaNV)
                .Index(t => t.MaNV)
                .Index(t => t.MaKH);
            
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        MaKH = c.String(nullable: false, maxLength: 128),
                        Hovaten = c.String(maxLength: 50),
                        Gioitinh = c.String(maxLength: 10),
                        SDT = c.String(maxLength: 20),
                        Diachi = c.String(maxLength: 200),
                        Email = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MaKH);
            
            CreateTable(
                "dbo.NhanVien",
                c => new
                    {
                        MaNV = c.String(nullable: false, maxLength: 10),
                        Hovaten = c.String(nullable: false, maxLength: 50),
                        SDT = c.String(maxLength: 20),
                        Ngaysinh = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Diachi = c.String(maxLength: 200),
                        Email = c.String(maxLength: 30),
                        Gioitinh = c.String(maxLength: 10),
                        Luongtheogio = c.Single(nullable: false),
                        Phucap = c.Single(nullable: false),
                        Chucvu = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.MaNV);
            
            CreateTable(
                "dbo.LichLamViec",
                c => new
                    {
                        MaLLV = c.Int(nullable: false, identity: true),
                        Ngaylamviec = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Tongsocalamviec = c.Int(nullable: false),
                        MaNV = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.MaLLV)
                .ForeignKey("dbo.NhanVien", t => t.MaNV)
                .Index(t => t.MaNV);
            
            CreateTable(
                "dbo.PhieuNhap",
                c => new
                    {
                        MaPN = c.Int(nullable: false, identity: true),
                        Ngaylap = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Calamviec = c.Int(nullable: false),
                        Tinhtrang = c.String(maxLength: 30),
                        MaNV = c.String(maxLength: 10),
                        MaNCC = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaPN)
                .ForeignKey("dbo.NhaCungCap", t => t.MaNCC)
                .ForeignKey("dbo.NhanVien", t => t.MaNV)
                .Index(t => t.MaNV)
                .Index(t => t.MaNCC);
            
            CreateTable(
                "dbo.ChiTietPhieuNhap",
                c => new
                    {
                        MaCTPN = c.Int(nullable: false, identity: true),
                        Soluong = c.Int(nullable: false),
                        MaSP = c.String(maxLength: 128),
                        MaPN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaCTPN)
                .ForeignKey("dbo.PhieuNhap", t => t.MaPN, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.MaSP)
                .Index(t => t.MaSP)
                .Index(t => t.MaPN);
            
            CreateTable(
                "dbo.SanPham",
                c => new
                    {
                        MaSP = c.String(nullable: false, maxLength: 128),
                        TenSP = c.String(maxLength: 50),
                        Chatlieu = c.String(maxLength: 50),
                        Kichthuoc = c.String(maxLength: 5),
                        Mausac = c.String(maxLength: 20),
                        LoaiSP = c.String(maxLength: 30),
                        Donvi = c.String(maxLength: 20),
                        Tinhtrang = c.String(maxLength: 30),
                        Dongianhap = c.Single(nullable: false),
                        Dongia = c.Single(nullable: false),
                        Hinhanh = c.Binary(),
                    })
                .PrimaryKey(t => t.MaSP);
            
            CreateTable(
                "dbo.NhaCungCap",
                c => new
                    {
                        MaNCC = c.String(nullable: false, maxLength: 128),
                        TenNCC = c.String(maxLength: 50),
                        SDT = c.String(maxLength: 20),
                        Diachi = c.String(maxLength: 200),
                        Email = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MaNCC);
            
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        MaTK = c.Int(nullable: false, identity: true),
                        Tentaikhoan = c.String(maxLength: 20),
                        Matkhau = c.String(maxLength: 20),
                        Loai = c.String(maxLength: 20),
                        MaNV = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.MaTK)
                .ForeignKey("dbo.NhanVien", t => t.MaNV)
                .Index(t => t.MaNV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiKhoan", "MaNV", "dbo.NhanVien");
            DropForeignKey("dbo.PhieuNhap", "MaNV", "dbo.NhanVien");
            DropForeignKey("dbo.PhieuNhap", "MaNCC", "dbo.NhaCungCap");
            DropForeignKey("dbo.ChiTietPhieuNhap", "MaSP", "dbo.SanPham");
            DropForeignKey("dbo.ChiTietHoaDon", "MaSP", "dbo.SanPham");
            DropForeignKey("dbo.ChiTietPhieuNhap", "MaPN", "dbo.PhieuNhap");
            DropForeignKey("dbo.LichLamViec", "MaNV", "dbo.NhanVien");
            DropForeignKey("dbo.HoaDon", "MaNV", "dbo.NhanVien");
            DropForeignKey("dbo.HoaDon", "MaKH", "dbo.KhachHang");
            DropForeignKey("dbo.ChiTietHoaDon", "MaHD", "dbo.HoaDon");
            DropIndex("dbo.TaiKhoan", new[] { "MaNV" });
            DropIndex("dbo.ChiTietPhieuNhap", new[] { "MaPN" });
            DropIndex("dbo.ChiTietPhieuNhap", new[] { "MaSP" });
            DropIndex("dbo.PhieuNhap", new[] { "MaNCC" });
            DropIndex("dbo.PhieuNhap", new[] { "MaNV" });
            DropIndex("dbo.LichLamViec", new[] { "MaNV" });
            DropIndex("dbo.HoaDon", new[] { "MaKH" });
            DropIndex("dbo.HoaDon", new[] { "MaNV" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "MaHD" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "MaSP" });
            DropTable("dbo.TaiKhoan");
            DropTable("dbo.NhaCungCap");
            DropTable("dbo.SanPham");
            DropTable("dbo.ChiTietPhieuNhap");
            DropTable("dbo.PhieuNhap");
            DropTable("dbo.LichLamViec");
            DropTable("dbo.NhanVien");
            DropTable("dbo.KhachHang");
            DropTable("dbo.HoaDon");
            DropTable("dbo.ChiTietHoaDon");
        }
    }
}
