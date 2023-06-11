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
    public partial class FormChiTietPhieuNhap : Form
    {
        PhieuNhapController ctrlPN = new PhieuNhapController();

        //Người thực hiện: Thanh Đức
        public FormChiTietPhieuNhap(int mapn)
        {
            InitializeComponent();

            PhieuNhap pn = ctrlPN.getPhieunhap(mapn);
            string bosung = "";

            if (pn != null)
            {
                for (int i = 0; i < 5 - pn.MaPN.ToString().Count(); i++)
                {
                    bosung += "0";
                }

                if (pn.Tinhtrang == "Đã hủy")
                {
                    lblTinhtrangPN.Text = "(Đã hủy)";
                    lblTinhtrangPN.ForeColor = Color.Red;
                }
                else
                {
                    lblTinhtrangPN.Visible = false;
                }

                lblMaPN.Text = bosung + pn.MaPN.ToString();
                lblNgaylap.Text = pn.Ngaylap.ToShortDateString();
                NhaCungCap ncc = ctrlPN.getNhaCungCap(pn.MaNCC);
                NhanVien nv = ctrlPN.getNhanVien(pn.MaNV);
                if (ncc != null)
                {
                    lblTenNCC.Text = ncc.TenNCC;
                    lblSDTNCC.Text = ncc.SDT;
                }

                if (nv != null)
                {
                    lblTenNV.Text = nv.Hovaten;
                }

                List<ChiTietPhieuNhap> cthds = ctrlPN.getCTPNbyMaPN(pn.MaPN);
                LoadTTCTPhieunhap(cthds);

                float tongthanhtien = 0, thue = 0;
                try
                {
                    foreach (var ct in cthds)
                    {
                        tongthanhtien += (ct.Soluong * ctrlPN.getSanPham(ct.MaSP).Dongianhap);
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

            void LoadTTCTPhieunhap(List<ChiTietPhieuNhap> ds)
            {
                int i = 0;
                grdDsCTPN.Rows.Clear();
                grdDsCTPN.AllowUserToAddRows = true;

                foreach (var ctpn in ds)
                {
                    grdDsCTPN.Rows.Add(ConvertCTPNtoGridRow(i, ctpn));
                    i++;
                }

                grdDsCTPN.AllowUserToAddRows = false;
            }

            DataGridViewRow ConvertCTPNtoGridRow(int stt, ChiTietPhieuNhap cthd)
            {
                DataGridViewRow row = (DataGridViewRow)grdDsCTPN.Rows[0].Clone();

                row.Cells[0].Value = stt;
                SanPham sp = ctrlPN.getSanPham(cthd.MaSP);
                if (sp != null)
                {
                    row.Cells[1].Value = sp.TenSP;
                }

                row.Cells[2].Value = cthd.Soluong;
                row.Cells[3].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongia);
                row.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", cthd.Soluong * sp.Dongia);

                return row;
            }
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
