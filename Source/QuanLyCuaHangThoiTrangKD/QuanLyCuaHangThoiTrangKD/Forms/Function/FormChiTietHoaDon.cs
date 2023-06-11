using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Common;
using QuanLyCuaHangThoiTrangKD.Models;
using System.Globalization;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormChiTietHoaDon : Form
    {
        HoaDonController ctrlHD = new HoaDonController();

        public FormChiTietHoaDon(string mahd)
        {
            InitializeComponent();

            HoaDon hd = ctrlHD.getHoadon(mahd);
            string bosung = "";

            if(hd != null)
            {
                for (int i = 0; i < 5 - hd.MaHD.ToString().Count(); i++)
                {
                    bosung += "0";
                }

                if(hd.Tinhtrang == "Đã hủy")
                {
                    lblTinhtrangHD.Text = "(Đã hủy)";
                    lblTinhtrangHD.ForeColor = Color.Red;
                }
                else
                {
                    lblTinhtrangHD.Visible = false;
                }

                lblMaHD.Text = bosung + hd.MaHD.ToString();
                lblNgaylap.Text = hd.Ngaylap.ToShortDateString();
                KhachHang kh = ctrlHD.geTKhachhang(hd.MaKH);
                NhanVien nv = ctrlHD.getNhanVien(hd.MaNV);
                if (kh != null)
                {
                    lblTenKH.Text = kh.Hovaten.ToUpper();
                }

                if (nv != null)
                {
                    lblTenNV.Text = nv.Hovaten;
                }

                List<ChiTietHoaDon> cthds = ctrlHD.getCTHDbyMaHD(hd.MaHD);
                LoadTTCTHoadon(cthds);

                float tongthanhtien = 0, thue = 0;
                try
                {
                    foreach (var ct in cthds)
                    {
                        tongthanhtien += (ct.Soluong * ctrlHD.getSanPham(ct.MaSP).Dongia);
                    }
                    lblTongthanhtien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongthanhtien) + " vnđ";
                    thue += ((tongthanhtien / 100) * 10);
                    lblThueVAT.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", thue) + " vnđ";
                    lblTongcong.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongthanhtien + thue) + " vnđ";
                }
                catch
                {

                }
            }
        }

        //Người thực hiện: Văn Khải
        void LoadTTCTHoadon(List<ChiTietHoaDon> ds)
        {
            int i = 0;
            grdDsCTHD.Rows.Clear();
            grdDsCTHD.AllowUserToAddRows = true;

            foreach (var hd in ds)
            {
                grdDsCTHD.Rows.Add(ConvertCTHDtoGridRow(i, hd));
                i++;
            }

            grdDsCTHD.AllowUserToAddRows = false;
        }

        //Người thực hiện: Văn Khải
        DataGridViewRow ConvertCTHDtoGridRow(int stt, ChiTietHoaDon cthd)
        {
            DataGridViewRow row = (DataGridViewRow)grdDsCTHD.Rows[0].Clone();

            row.Cells[0].Value = stt;
            SanPham sp = ctrlHD.getSanPham(cthd.MaSP);
            if (sp != null)
            {
                row.Cells[1].Value = sp.TenSP;
            }

            row.Cells[2].Value = cthd.Soluong;
            row.Cells[3].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongia);
            row.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", cthd.Soluong * sp.Dongia);

            return row;
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
