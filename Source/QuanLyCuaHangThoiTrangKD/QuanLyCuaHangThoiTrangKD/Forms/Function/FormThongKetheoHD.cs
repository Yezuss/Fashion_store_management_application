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
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongKetheoHD : Form
    {
        ThongKeController ctrlThongke = new ThongKeController();
        TaiKhoan taikhoan;

        float TongGTHD = 0, TongGTPN = 0;

        int pageNumberHD = 1, numberRecordHD = 4, pageNumberPN = 1, numberRecordPN = 4;

        List<HoaDon> dsHDTam = new List<HoaDon>();
        List<PhieuNhap> dsPNTam = new List<PhieuNhap>();

        //Người thực hiện: Văn Khải
        public FormThongKetheoHD(int matk)
        {
            InitializeComponent();

            taikhoan = ctrlThongke.getTaiKhoanByMaTK(matk);

            grdThongtinHD.AllowUserToAddRows = false;
            grdThongtinPN.AllowUserToAddRows = false;

            cboTenNV.Items.Add("--Chọn tên nhân viên--");
            foreach (var nv in ctrlThongke.getTatcaNhanvien())
            {
                cboTenNV.Items.Add(nv.Hovaten);
            }

            cboCalamviec.Items.Add("--Chọn ca làm việc--");
            cboCalamviec.Items.Add("Ca 1");
            cboCalamviec.Items.Add("Ca 2");

            cboTenSP.Items.Add("--Chọn tên sản phẩm--");
            foreach (var sp in ctrlThongke.getTatcaSanpham())
            {
                cboTenSP.Items.Add(sp.TenSP);
            }

            cboTenNCC.Items.Add("--Chọn tên nhà cung cấp--");
            foreach (var ncc in ctrlThongke.getTatcaNhaCungCap())
            {
                cboTenNCC.Items.Add(ncc.TenNCC);
            }

            cboTenNV.Text = cboTenNV.Items[0].ToString();
            cboCalamviec.Text = cboCalamviec.Items[0].ToString();
            cboTenSP.Text = cboTenSP.Items[0].ToString();
            cboTenNCC.Text = cboTenNCC.Items[0].ToString();

            for (int i = 1; i <= 5; i++)
            {
                cboNam.Items.Add(DateTime.Today.Year - 5 + i);
            }

            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i);
            }

            var dsTatcaHD = ctrlThongke.getHoadonbyNam(DateTime.Today.Year);
            var dsTatcaPN = ctrlThongke.getPhieunhapbyNam(DateTime.Today.Year);

            cboThang.SelectedItem = DateTime.Today.Month;
            cboNam.SelectedItem = DateTime.Today.Year;

            LoadBieudo(int.Parse(cboNam.Text));
            LoadThongtinDoanhthu(dsTatcaHD, dsTatcaPN);
        }

        void LoadRecordHD(List<HoaDon> ds, int page, int recordNum)
        {
            List<HoaDon> dshd = new List<HoaDon>();

            dshd = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadTTHoadon(dshd);
        }

        void LoadTTHoadon(List<HoaDon> ds)
        {
            grdThongtinHD.Rows.Clear();
            grdThongtinHD.AllowUserToAddRows = true;

            foreach (var hd in ds)
            {
                grdThongtinHD.Rows.Add(ConvertHoadontoGridRow(hd));
            }

            grdThongtinHD.AllowUserToAddRows = false;
        }

        DataGridViewRow ConvertHoadontoGridRow(HoaDon hd)
        {
            float tongtien = 0;
            DataGridViewRow row = (DataGridViewRow)grdThongtinHD.Rows[0].Clone();

            var dscthd = ctrlThongke.getCTHDbyMaHD(hd.MaHD);
            if (dscthd.Count > 0)
            {
                foreach (var ct in dscthd)
                {
                    var spct = ctrlThongke.getSanPham(ct.MaSP);
                    tongtien += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                }
            }

            row.Cells[0].Value = hd.MaHD.ToString();
            row.Cells[1].Value = hd.Ngaylap.ToShortDateString();
            row.Cells[2].Value = ctrlThongke.geTKhachhang(hd.MaKH).Hovaten;
            row.Cells[3].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);

            return row;
        }

        void LoadRecordPN(List<PhieuNhap> ds, int page, int recordNum)
        {
            List<PhieuNhap> dspn = new List<PhieuNhap>();

            dspn = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadTTPhieunhap(dspn);
        }

        void LoadTTPhieunhap(List<PhieuNhap> ds)
        {
            grdThongtinPN.Rows.Clear();
            grdThongtinPN.AllowUserToAddRows = true;

            foreach (var pn in ds)
            {
                grdThongtinPN.Rows.Add(ConvertPhieunhaptoGridRow(pn));
            }

            grdThongtinPN.AllowUserToAddRows = false;
            lblTongSLPN.Text = ds.Count.ToString();
            lblTongtienTatcaPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGTPN);
        }

        DataGridViewRow ConvertPhieunhaptoGridRow(PhieuNhap pn)
        {
            string bosung = "";
            float tongtien = 0;
            DataGridViewRow row = (DataGridViewRow)grdThongtinPN.Rows[0].Clone();

            for (int i = 0; i < 5 - pn.MaPN.ToString().Count(); i++)
            {
                bosung += "0";
            }

            var dsctpn = ctrlThongke.getCTPNbyMaPN(pn.MaPN);
            if (dsctpn.Count > 0)
            {
                foreach (var ct in dsctpn)
                {
                    var spct = ctrlThongke.getSanPham(ct.MaSP);
                    tongtien += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                }
            }
            TongGTPN += tongtien;

            row.Cells[0].Value = bosung + pn.MaPN.ToString();
            row.Cells[2].Value = pn.Ngaylap.ToShortDateString();
            row.Cells[1].Value = ctrlThongke.getNhanVien(pn.MaNV).Hovaten;
            row.Cells[3].Value = pn.Calamviec.ToString();
            row.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);

            return row;
        }

        //Người thực hiện: Văn Khải
        private void cbTenNV_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboThang.SelectedIndex != -1 && cboNam.SelectedIndex != -1)
            {
                try
                {
                    TongGTHD = 0;
                    LoadThongkeHDTenNV(cboTenNV.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                    gbThongtindoanhthu.Text = "Thông tin doanh thu tháng " + cboThang.SelectedItem.ToString() + "/ " + cboNam.SelectedItem.ToString();
                    lblTongSLHD.Text = dsHDTam.Count.ToString();
                    lblTongtienTatcaHD.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGTHD);
                }
                catch
                {

                }
            }
        }

        //Người thực hiện: Văn Khải
        private void cbCalamviec_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboThang.SelectedIndex != -1 && cboNam.SelectedIndex != -1)
            {
                try
                {
                    TongGTHD = 0;
                    LoadThongkeHDCalamviec(cboCalamviec.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                    gbThongtindoanhthu.Text = "Thông tin doanh thu tháng " + cboThang.SelectedItem.ToString() + "/ " + cboNam.SelectedItem.ToString();
                    lblTongSLHD.Text = dsHDTam.Count.ToString();
                    lblTongtienTatcaHD.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGTHD);
                }
                catch
                {

                }
            }
        }

        //Người thực hiện: Văn Khải
        private void cbTenSP_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboThang.SelectedIndex != -1 && cboNam.SelectedIndex != -1)
            {
                try
                {
                    TongGTPN = 0;
                    LoadThongkePNTenSP(cboTenSP.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                    gbThongtindoanhthu.Text = "Thông tin doanh thu tháng " + cboThang.SelectedItem.ToString() + "/ " + cboNam.SelectedItem.ToString();
                    lblTongSLPN.Text = dsPNTam.Count.ToString();
                    lblTongtienTatcaPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGTPN);
                }
                catch
                {

                }
            }
        }

        //Người thực hiện: Văn Khải
        private void cboTenNCC_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboThang.SelectedIndex != -1 && cboNam.SelectedIndex != -1)
            {
                try
                {
                    TongGTPN = 0;
                    LoadThongkePNTenNCC(cboTenNCC.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                    gbThongtindoanhthu.Text = "Thông tin doanh thu tháng " + cboThang.SelectedItem.ToString() + "/ " + cboNam.SelectedItem.ToString();
                    lblTongSLPN.Text = dsPNTam.Count.ToString();
                    lblTongtienTatcaPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGTPN);
                }
                catch
                {

                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTenNV.SelectedIndex != -1)
            {
                gbThongtindoanhthu.Text = "Thông tin doanh thu";
                gbThongtindoanhthu.Text += " tháng " + cboThang.SelectedItem.ToString() + " / " + cboNam.SelectedItem.ToString();
                LoadThongkeHDTenNV(cboTenNV.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
            if (cboCalamviec.SelectedIndex != -1)
            {
                gbThongtindoanhthu.Text = "Thông tin doanh thu";
                gbThongtindoanhthu.Text += " tháng " + cboThang.SelectedItem.ToString() + " / " + cboNam.SelectedItem.ToString();
                LoadThongkeHDCalamviec(cboCalamviec.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
            if (cboTenNCC.SelectedIndex != -1)
            {
                gbThongtindoanhthu.Text = "Thông tin doanh thu";
                gbThongtindoanhthu.Text += " tháng " + cboThang.SelectedItem.ToString() + " / " + cboNam.SelectedItem.ToString();
                LoadThongkePNTenNCC(cboTenNCC.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
            if (cboTenSP.SelectedIndex != -1)
            {
                gbThongtindoanhthu.Text = "Thông tin doanh thu";
                gbThongtindoanhthu.Text += " tháng " + cboThang.SelectedItem.ToString() + " / " + cboNam.SelectedItem.ToString();
                LoadThongkePNTenSP(cboTenSP.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
        }

        //Người thực hiện: Thanh Đức
        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gbThongtindoanhthu.Text += " năm " + cboNam.SelectedItem.ToString();
            LoadBieudo(int.Parse(cboNam.Text));
            if (cboTenNV.SelectedIndex != -1)
            {
                LoadThongkeHDTenNV(cboTenNV.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
            if (cboCalamviec.SelectedIndex != -1)
            {
                LoadThongkeHDCalamviec(cboCalamviec.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
            if (cboTenNCC.SelectedIndex != -1)
            {
                LoadThongkePNTenNCC(cboTenNCC.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
            if (cboTenSP.SelectedIndex != -1)
            {
                LoadThongkePNTenSP(cboTenSP.Text, int.Parse(cboThang.Text), int.Parse(cboNam.Text));
            }
        }

        //Người thực hiện: Thanh Đức
        void LoadBieudo(int nam)
        {
            chaThongkeHD.Series["Tổng giá trị hóa đơn"].Points.Clear();
            //chaThongkeHD.Series["Tổng giá trị phiếu nhập"].Points.Clear();

            for (int i = 1; i <= 12; i++)
            {
                float tonghd = 0, tongpn = 0;
                var dsHDTheoThang = ctrlThongke.getHoadonbyThangNam(i, nam);
                //var dsPNTheoThang = ctrlThongke.getPhieunhapbyThangNam(i, nam);

                if(dsHDTheoThang.Count > 0)
                {
                    foreach (var hd in dsHDTheoThang)
                    {
                        var dscthd = ctrlThongke.getCTHDbyMaHD(hd.MaHD);
                        float tong1hd = 0;
                        if (dscthd.Count > 0)
                        {
                            foreach (var ct in dscthd)
                            {
                                var spct = ctrlThongke.getSanPham(ct.MaSP);
                                tong1hd += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                            }
                        }
                        tonghd += tong1hd;
                    }
                }

                //if(dsPNTheoThang.Count > 0)
                //{
                //    foreach (var pn in dsPNTheoThang)
                //    {
                //        var dsctpn = ctrlThongke.getCTPNbyMaPN(pn.MaPN);
                //        float tong1pn = 0;
                //        if (dsctpn.Count > 0)
                //        {
                //            foreach (var ct in dsctpn)
                //            {
                //                var spct = ctrlThongke.getSanPham(ct.MaSP);
                //                tong1pn += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                //            }
                //        }
                //        tongpn += tong1pn;
                //    }
                //}

                if(tonghd > 0)
                {
                    chaThongkeHD.Series["Tổng giá trị hóa đơn"].Points.AddXY(i, tonghd);
                }
                else
                {
                    chaThongkeHD.Series["Tổng giá trị hóa đơn"].Points.AddXY(i, 0);
                }

                //if(tongpn > 0)
                //{
                //    chaThongkeHD.Series["Tổng giá trị phiếu nhập"].Points.AddXY(i, tongpn);
                //}
                //else
                //{
                //    chaThongkeHD.Series["Tổng giá trị phiếu nhập"].Points.AddXY(i, 0);
                //}
            }
        }

        //Người thực hiện: Văn Khải - Thanh Đức
        void LoadThongtinDoanhthu(List<HoaDon> dshd, List<PhieuNhap> dspn)
        {
            float tonghd = 0, tongpn = 0;
            if(dshd.Count > 0)
            {
                foreach (var hd in dshd)
                {
                    var dscthd = ctrlThongke.getCTHDbyMaHD(hd.MaHD);
                    float tong1hd = 0;
                    if (dscthd.Count > 0)
                    {
                        foreach (var ct in dscthd)
                        {
                            var spct = ctrlThongke.getSanPham(ct.MaSP);
                            tong1hd += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                        }
                    }
                    tonghd += tong1hd;
                }
            }

            if(dspn.Count > 0)
            {
                foreach (var pn in dspn)
                {
                    var dsctpn = ctrlThongke.getCTPNbyMaPN(pn.MaPN);
                    float tong1pn = 0;
                    if (dsctpn.Count > 0)
                    {
                        foreach (var ct in dsctpn)
                        {
                            var spct = ctrlThongke.getSanPham(ct.MaSP);
                            tong1pn += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                        }
                    }
                    tongpn += tong1pn;
                }
            }

            lblTongSLHD.Text = dshd.Count + "";
            lblTongtienTatcaHD.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tonghd);
            lblTongSLPN.Text = dspn.Count + "";
            lblTongtienTatcaPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongpn);
        }

        //Người thực hiện: Văn Khải - Thanh Đức
        void LoadThongkeHDTenNV(string tennv, int thang, int nam)
        {
            if (!string.IsNullOrEmpty(tennv))
            {
                List<HoaDon> dsHDNV = ctrlThongke.getHoadonbyMaNVhoacten(tennv);
                var dsHDSP = ctrlThongke.getHoadonbyThangNam(thang, nam);
                dsHDSP = dsHDSP.Where(x => dsHDNV.Any(y => y.MaHD == x.MaHD) == true).ToList();

                if (cboCalamviec.SelectedIndex <= 0 && cboTenNV.SelectedIndex >= 0)
                {
                    if (cboTenNV.SelectedIndex == 0)
                    {
                        return;
                    }
                    LoadRecordHD(dsHDSP, pageNumberHD, numberRecordHD);
                    dsHDTam = dsHDSP.ToList();
                }
                else if (cboCalamviec.SelectedIndex > 0 && cboTenNV.SelectedIndex >= 0)
                {
                    if (cboTenNV.SelectedIndex == 0)
                    {
                        return;
                    }
                    string[] strCalamviec = cboCalamviec.Text.TrimEnd().Split(' ');
                    dsHDSP = dsHDSP.Where(x => x.Calamviec == int.Parse(strCalamviec[1])).ToList();
                    LoadRecordHD(dsHDSP, pageNumberHD, numberRecordHD);
                    dsHDTam = dsHDSP.ToList();
                }

                foreach(var hd in dsHDSP)
                {
                    float tongtien1hd = 0;
                    var dscthd = ctrlThongke.getCTHDbyMaHD(hd.MaHD);
                    if (dscthd.Count > 0)
                    {
                        foreach (var ct in dscthd)
                        {
                            var spct = ctrlThongke.getSanPham(ct.MaSP);
                            tongtien1hd += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                        }
                    }
                    TongGTHD += tongtien1hd;
                }

                int tongsotrang = (dsHDTam.Count / numberRecordHD) + 1;
                if (dsHDTam.Count % numberRecordHD == 0)
                {
                    tongsotrang = (dsHDTam.Count / numberRecordHD);
                }
                txtSotrangHD.Text = 1 + " / " + tongsotrang;
            }
        }

        //Người thực hiện: Văn Khải - Thanh Đức
        void LoadThongkeHDCalamviec(string calamviec, int thang, int nam)
        {
            string[] strCalamviec = calamviec.TrimEnd().Split(' ');
            List<HoaDon> dsHDCLV = ctrlThongke.getHoadonbyCalamviec(int.Parse(strCalamviec[1]));
            var dsHDSP = ctrlThongke.getHoadonbyThangNam(thang, nam);
            dsHDSP = dsHDSP.Where(x => dsHDCLV.Any(y => y.MaHD == x.MaHD) == true).ToList();

            if (cboCalamviec.SelectedIndex >= 0 && cboTenNV.SelectedIndex <= 0)
            {
                if (cboCalamviec.SelectedIndex == 0)
                {
                    return;
                }
                dsHDTam = dsHDSP.ToList();
                LoadRecordHD(dsHDTam, pageNumberHD, numberRecordHD);
            }

            else if (cboCalamviec.SelectedIndex >= 0 && cboTenNV.SelectedIndex > 0)
            {
                if (cboCalamviec.SelectedIndex == 0)
                {
                    return;
                }
                NhanVien nv = ctrlThongke.getNhanVien(cboTenNV.Text);
                dsHDTam = dsHDSP.Where(x => x.MaNV == nv.MaNV).ToList();
                LoadRecordHD(dsHDTam, pageNumberHD, numberRecordHD);
            }

            foreach (var hd in dsHDSP)
            {
                float tongtien1hd = 0;
                var dscthd = ctrlThongke.getCTHDbyMaHD(hd.MaHD);
                if (dscthd.Count > 0)
                {
                    foreach (var ct in dscthd)
                    {
                        var spct = ctrlThongke.getSanPham(ct.MaSP);
                        tongtien1hd += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                    }
                }
                TongGTHD += tongtien1hd;
            }

            int tongsotrang = (dsHDTam.Count / numberRecordHD) + 1;
            if (dsHDTam.Count % numberRecordHD == 0)
            {
                tongsotrang = (dsHDTam.Count / numberRecordHD);
            }
            txtSotrangHD.Text = 1 + " / " + tongsotrang;
        }

        //Người thực hiện: Văn Khải - Thanh Đức
        void LoadThongkePNTenSP(string tensp, int thang, int nam)
        {
            TongGTPN = 0;
            var dsctpn = ctrlThongke.getCTPNbyTenSP(tensp);
            var dsPNSP = ctrlThongke.getPhieunhapbyThangNam(thang, nam);
            dsPNSP = dsPNSP.Where(x => dsctpn.Any(y => y.MaPN == x.MaPN) == true).ToList();

            if (cboTenSP.SelectedIndex >= 0 && cboTenNCC.SelectedIndex <= 0)
            {
                if(cboTenSP.SelectedIndex == 0)
                {
                    return;
                }
                dsPNTam = dsPNSP.ToList();
                LoadRecordPN(dsPNSP, pageNumberPN, numberRecordPN);
            }

            else if (cboTenSP.SelectedIndex >= 0 && cboTenNCC.SelectedIndex > 0)
            {
                if (cboTenSP.SelectedIndex == 0)
                {
                    return;
                }
                NhaCungCap ncc = ctrlThongke.getNhaCungCap(cboTenNCC.Text);
                dsPNTam = dsPNSP.Where(x => x.MaNCC == ncc.MaNCC).ToList();
                LoadRecordPN(dsPNTam, pageNumberPN, numberRecordPN);
            }
        }

        //Người thực hiện: Văn Khải - Thanh Đức
        void LoadThongkePNTenNCC(string tenncc, int thang, int nam)
        {
            TongGTPN = 0;
            var dsPNNCC = ctrlThongke.getPhieunhapbyTenNCC(tenncc);
            List<ChiTietPhieuNhap> dsctpnsp;
            var dsPNSP = ctrlThongke.getPhieunhapbyThangNam(thang, nam);
            dsPNSP = dsPNSP.Where(x => dsPNNCC.Any(y => y.MaPN == x.MaPN) == true).ToList();

            if (cboTenNCC.SelectedIndex >= 0 && cboTenSP.SelectedIndex <= 0)
            {
                if(cboTenNCC.SelectedIndex == 0)
                {
                    return;
                }
                dsPNTam = dsPNSP.ToList();
                LoadRecordPN(dsPNTam, pageNumberPN, numberRecordPN);
            }
            else if (cboTenSP.SelectedIndex > 0 && cboTenNCC.SelectedIndex >= 0)
            {
                if (cboTenNCC.SelectedIndex == 0)
                {
                    return;
                }
                SanPham sp = ctrlThongke.getSanPham(cboTenSP.Text);
                if (sp.MaSP != null)
                {
                    dsctpnsp = ctrlThongke.getCTPNbyTenSP(sp.TenSP);
                    dsPNTam = dsPNSP.Where(x => dsctpnsp.Any(y => y.MaPN == x.MaPN) == true).ToList();
                    LoadRecordPN(dsPNTam, pageNumberPN, numberRecordPN);
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void btnXuatTKHD_Click(object sender, EventArgs e)
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            if (MessageBox.Show("Xác nhận xuất Excel thông tin thống kê hóa đơn ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                    if (string.IsNullOrEmpty(filePath))
                    {
                        MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                        return;
                    }
                }
                else
                {
                    return;
                }

                try
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        p.Workbook.Properties.Author = "Quản lý " + ctrlThongke.getNhanvienbyMaTK(taikhoan).Hovaten;

                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Báo cáo thống kê hóa đơn";

                        ////Tạo một sheet để làm việc trên đó
                        p.Workbook.Worksheets.Add("Sheet 1");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = p.Workbook.Worksheets[1];

                        ws.DefaultColWidth = 30;

                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 15;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                "Mã hóa đơn",
                                                "Ngày lập",
                                                "Tên khách hàng",
                                                "Giá trị hóa đơn",
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        if (cboTenNV.SelectedIndex > 0 && cboCalamviec.SelectedIndex > 0)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin hóa đơn tháng " + cboThang.Text + "/" + cboNam.Text + " theo " + cboTenNV.Text + " - " + cboCalamviec.Text;
                        }
                        else if (cboTenNV.SelectedIndex > 0)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin hóa đơn tháng " + cboThang.Text + "/" + cboNam.Text + " theo " + cboTenNV.Text;
                        }
                        else if (cboCalamviec.SelectedIndex > 0)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin hóa đơn tháng " + cboThang.Text + "/" + cboNam.Text + " theo " + cboCalamviec.Text;
                        }

                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = 22;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        int colIndex = 1;
                        int rowIndex = 2;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        //với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        for (int i = 0; i < grdThongtinHD.Rows.Count; i++)
                        {
                            int j = 0;

                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            var cell0 = ws.Cells[rowIndex, colIndex++];
                            cell0.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border0 = cell0.Style.Border;
                            border0.Bottom.Style =
                                border0.Top.Style =
                                border0.Left.Style =
                                border0.Right.Style = ExcelBorderStyle.Thin;

                            var cell1 = ws.Cells[rowIndex, colIndex++];
                            cell1.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border1 = cell1.Style.Border;
                            border1.Bottom.Style =
                                border1.Top.Style =
                                border1.Left.Style =
                                border1.Right.Style = ExcelBorderStyle.Thin;

                            var cell2 = ws.Cells[rowIndex, colIndex++];
                            cell2.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border2 = cell2.Style.Border;
                            border2.Bottom.Style =
                                border2.Top.Style =
                                border2.Left.Style =
                                border2.Right.Style = ExcelBorderStyle.Thin;

                            var cell3 = ws.Cells[rowIndex, colIndex++];
                            cell3.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border3 = cell3.Style.Border;
                            border3.Bottom.Style =
                                border3.Top.Style =
                                border3.Left.Style =
                                border3.Right.Style = ExcelBorderStyle.Thin;
                        }

                        colIndex = 1;
                        var endCol0 = ws.Cells[rowIndex += 1, colIndex, rowIndex, colIndex + 2];
                        endCol0.Value = "Tổng giá trị hóa đơn (vnđ):";
                        endCol0.Merge = true;
                        endCol0.Style.Font.Bold = true;
                        endCol0.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        var border7 = endCol0.Style.Border;
                        border7.Bottom.Style =
                            border7.Top.Style =
                            border7.Left.Style =
                            border7.Right.Style = ExcelBorderStyle.Thin;

                        var endCol1 = ws.Cells[rowIndex, colIndex + 3];
                        endCol1.Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGTHD);
                        endCol1.Style.Font.Bold = true;
                        endCol1.Style.Font.Size = 22;
                        var border8 = endCol1.Style.Border;
                        border8.Bottom.Style =
                            border8.Top.Style =
                            border8.Left.Style =
                            border8.Right.Style = ExcelBorderStyle.Thin;

                        //Lưu file lại
                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất excel thành công!");

                }
                catch (Exception EE)
                {
                    MessageBox.Show("Có lỗi khi lưu file!");
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void btnXuatTKPN_Click(object sender, EventArgs e)
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            if (MessageBox.Show("Xác nhận xuất Excel thông tin thống kê hóa đơn ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                    if (string.IsNullOrEmpty(filePath))
                    {
                        MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                        return;
                    }
                }
                else
                {
                    return;
                }

                try
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        p.Workbook.Properties.Author = "Quản lý " + ctrlThongke.getNhanvienbyMaTK(taikhoan).Hovaten;

                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Báo cáo thống kê phiếu nhập";

                        ////Tạo một sheet để làm việc trên đó
                        p.Workbook.Worksheets.Add("Sheet 1");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = p.Workbook.Worksheets[1];

                        ws.DefaultColWidth = 30;

                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 15;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                "Mã phiếu nhập",
                                                "Tên nhân viên",
                                                "Ngày lập",
                                                "Ca làm việc",
                                                "Giá trị phiếu nhập"
                };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        if (cboTenNCC.SelectedIndex > 0 && cboTenSP.SelectedIndex > 0)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin phiếu nhập tháng " + cboThang.Text + "/" + cboNam.Text + " theo " + cboTenNCC.Text + " - " + cboTenSP.Text;
                        }
                        else if (cboTenSP.SelectedIndex > 0)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin phiếu nhập tháng " + cboThang.Text + "/" + cboNam.Text + " theo " + cboTenSP.Text;
                        }
                        else if (cboTenNCC.SelectedIndex > 0)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin phiếu nhập tháng " + cboThang.Text + "/" + cboNam.Text + " theo " + cboTenNCC.Text;
                        }

                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = 22;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        int colIndex = 1;
                        int rowIndex = 2;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        //với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        for (int i = 0; i < grdThongtinPN.Rows.Count; i++)
                        {
                            int j = 0;

                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            var cell0 = ws.Cells[rowIndex, colIndex++];
                            cell0.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border0 = cell0.Style.Border;
                            border0.Bottom.Style =
                                border0.Top.Style =
                                border0.Left.Style =
                                border0.Right.Style = ExcelBorderStyle.Thin;

                            var cell1 = ws.Cells[rowIndex, colIndex++];
                            cell1.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border1 = cell1.Style.Border;
                            border1.Bottom.Style =
                                border1.Top.Style =
                                border1.Left.Style =
                                border1.Right.Style = ExcelBorderStyle.Thin;

                            var cell2 = ws.Cells[rowIndex, colIndex++];
                            cell2.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border2 = cell2.Style.Border;
                            border2.Bottom.Style =
                                border2.Top.Style =
                                border2.Left.Style =
                                border2.Right.Style = ExcelBorderStyle.Thin;

                            var cell3 = ws.Cells[rowIndex, colIndex++];
                            cell3.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border3 = cell3.Style.Border;
                            border3.Bottom.Style =
                                border3.Top.Style =
                                border3.Left.Style =
                                border3.Right.Style = ExcelBorderStyle.Thin;

                            var cell4 = ws.Cells[rowIndex, colIndex++];
                            cell4.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border4 = cell4.Style.Border;
                            border4.Bottom.Style =
                                border4.Top.Style =
                                border4.Left.Style =
                                border4.Right.Style = ExcelBorderStyle.Thin;
                        }

                        colIndex = 1;
                        var endCol0 = ws.Cells[rowIndex += 1, colIndex, rowIndex, colIndex + 3];
                        endCol0.Value = "Tổng giá trị phiếu nhập (vnđ):";
                        endCol0.Merge = true;
                        endCol0.Style.Font.Bold = true;
                        endCol0.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        var border7 = endCol0.Style.Border;
                        border7.Bottom.Style =
                            border7.Top.Style =
                            border7.Left.Style =
                            border7.Right.Style = ExcelBorderStyle.Thin;

                        var endCol1 = ws.Cells[rowIndex, colIndex + 4];
                        endCol1.Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGTPN);
                        endCol1.Style.Font.Bold = true;
                        endCol1.Style.Font.Size = 22;
                        var border8 = endCol1.Style.Border;
                        border8.Bottom.Style =
                            border8.Top.Style =
                            border8.Left.Style =
                            border8.Right.Style = ExcelBorderStyle.Thin;

                        //Lưu file lại
                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất excel thành công!");

                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi khi lưu file!");
                }
            }
        }

        //Người thực hiện: Thanh Đức ====================================
        private void btnPrevHD_Click(object sender, EventArgs e)
        {
            if (pageNumberHD - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumberHD--;

                if (dsHDTam.Count % numberRecordHD == 0)
                {
                    tongsotrang = (dsHDTam.Count / numberRecordHD);
                }
                else
                {
                    tongsotrang = (dsHDTam.Count / numberRecordHD) + 1;
                }

                txtSotrangHD.Text = pageNumberHD.ToString() + " / " + tongsotrang;
                LoadRecordHD(dsHDTam, pageNumberHD, numberRecordHD);
            }
        }

        private void btnNextHD_Click(object sender, EventArgs e)
        {
            if (pageNumberHD == 1 && dsHDTam.Count == numberRecordHD)
            {
                return;
            }
            if (pageNumberHD - 1 < dsHDTam.Count / numberRecordHD)
            {
                pageNumberHD++;
                int TongSotrang = (dsHDTam.Count / numberRecordHD) + 1;
                if (dsHDTam.Count % numberRecordHD == 0)
                {
                    TongSotrang = (dsHDTam.Count / numberRecordHD);
                }
                txtSotrangHD.Text = pageNumberHD.ToString() + " / " + TongSotrang;
                LoadRecordHD(dsHDTam, pageNumberHD, numberRecordHD);
            }
        }

        private void btnFirstHD_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsHDTam.Count / numberRecordHD) + 1;
            txtSotrangHD.Text = 1 + " / " + tongsotrang;
            LoadRecordHD(dsHDTam, 1, numberRecordHD);
        }

        private void btnLastHD_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsHDTam.Count / numberRecordHD) + 1;
            txtSotrangHD.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordHD(dsHDTam, tongsotrang, numberRecordHD);
        }

        private void btnPrevPN_Click(object sender, EventArgs e)
        {
            if (pageNumberPN - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumberPN--;

                if (dsPNTam.Count % numberRecordPN == 0)
                {
                    tongsotrang = (dsPNTam.Count / numberRecordPN);
                }
                else
                {
                    tongsotrang = (dsPNTam.Count / numberRecordPN) + 1;
                }

                txtSotrangPN.Text = pageNumberPN.ToString() + " / " + tongsotrang;
                LoadRecordPN(dsPNTam, pageNumberPN, numberRecordPN);
            }
        }

        private void btnNextPN_Click(object sender, EventArgs e)
        {
            if (pageNumberPN == 1 && dsPNTam.Count == numberRecordPN)
            {
                return;
            }
            if (pageNumberPN - 1 < dsPNTam.Count / numberRecordPN)
            {
                pageNumberPN++;
                int TongSotrang = (dsPNTam.Count / numberRecordPN) + 1;
                if (dsPNTam.Count % numberRecordPN == 0)
                {
                    TongSotrang = (dsPNTam.Count / numberRecordPN);
                }
                txtSotrangPN.Text = pageNumberPN.ToString() + " / " + TongSotrang;
                LoadRecordPN(dsPNTam, pageNumberPN, numberRecordPN);
            }
        }

        private void btnFirstPN_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsPNTam.Count / numberRecordPN) + 1;
            txtSotrangPN.Text = 1 + " / " + tongsotrang;
            LoadRecordPN(dsPNTam, 1, numberRecordPN);
        }

        private void btnLastPN_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsHDTam.Count / numberRecordPN) + 1;
            txtSotrangPN.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordPN(dsPNTam, tongsotrang, numberRecordPN);
        }
        //===============================================================

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
