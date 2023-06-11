using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormCapNhatPhieuNhap : Form
    {
        PhieuNhapController ctrlPN = new PhieuNhapController();

        int pageNumber = 1;
        int numberRecord = 7;

        TaiKhoan taikhoan;

        List<PhieuNhap> dsPNTam = new List<PhieuNhap>();

        float TongGT = 0;

        public FormCapNhatPhieuNhap(int matk)
        {
            InitializeComponent();
            this.taikhoan = ctrlPN.getTaiKhoanByMaTK(matk);

            cboTenNV.Items.Add("--Tên nhân viên--");
            cboTenNV.Text = cboTenNV.Items[0].ToString();
            foreach (var nv in ctrlPN.getTatcaNhanvien())
            {
                if (nv.Chucvu == "Nhân viên quản lý")
                {
                    cboTenNV.Items.Add(nv.Hovaten.ToString());
                }              
            }

            //LoadTTPhieunhap(ctrlPN.getTatcaPhieunhap());
            if(grdThongtinHD.Rows.Count == 0)
            {
                btnXuatThongtinPN.Enabled = false;
                btnXemCTPN.Enabled = false;
            }
        }

        void LoadTTPhieunhap(List<PhieuNhap> ds)
        {
            grdThongtinHD.Rows.Clear();
            grdThongtinHD.AllowUserToAddRows = true;

            foreach (var pn in ds)
            {
                grdThongtinHD.Rows.Add(ConvertPhieunhaptoGridRow(pn));
            }

            grdThongtinHD.AllowUserToAddRows = false;
        }

        DataGridViewRow ConvertPhieunhaptoGridRow(PhieuNhap pn)
        {
            string bosung = "";
            float tongtien = 0;
            DataGridViewRow row = (DataGridViewRow)grdThongtinHD.Rows[0].Clone();
            for (int i = 0; i < 5 - pn.MaPN.ToString().Count(); i++)
            {
                bosung += "0";
            }
            row.Cells[0].Value = bosung + pn.MaPN.ToString();

            var dsctpn = ctrlPN.getCTPNbyMaPN(pn.MaPN);
            if (dsctpn.Count > 0)
            {
                foreach (var ct in dsctpn)
                {
                    var spct = ctrlPN.getSanPham(ct.MaSP);
                    tongtien += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                }
            }

            TongGT += tongtien;

            row.Cells[1].Value = pn.Ngaylap.ToShortDateString();
            row.Cells[2].Value = ctrlPN.getNhaCungCap(pn.MaNCC).TenNCC;
            row.Cells[3].Value = ctrlPN.getNhanVien(pn.MaNV).Hovaten;
            row.Cells[4].Value = pn.Calamviec.ToString();
            row.Cells[5].Value = ctrlPN.getSolongCTPN(pn);
            row.Cells[6].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);

            return row;
        }

        void LoadRecordPN(List<PhieuNhap> ds, int page, int recordNum)
        {
            List<PhieuNhap> dspn = new List<PhieuNhap>();

            dspn = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadTTPhieunhap(dspn);
        }

        //Người thực hiện: Văn Khải
        private void btnTimkiemTTPN_Click(object sender, EventArgs e)
        {
            TongGT = 0;
            List<PhieuNhap> dsPN = new List<PhieuNhap>();

            List<PhieuNhap> dsPNTtheoncc = new List<PhieuNhap>();
            List<PhieuNhap> dsPNtheoNgaylap;

            if (rdoTheongaylap.Checked == true)
            {
                dsPNtheoNgaylap = ctrlPN.getPhieunhapbyNgaylap(dpNgaylap.Value);
                foreach (var pn in dsPNtheoNgaylap)
                {
                    dsPN.Add(pn);
                }
                if (cboTenNV.SelectedIndex > 0)
                {
                    NhanVien nv = ctrlPN.getNhanVien(cboTenNV.Text);
                    dsPN = dsPN.Where(x => x.MaNV == nv.MaNV).ToList();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTenNCC.Text))
                {
                    dsPN = ctrlPN.getPhieunhapbySDThoacTenNCC(txtTenNCC.Text);
                }
            }

            dsPNTam = dsPN;
            LoadRecordPN(dsPN, pageNumber, numberRecord);
            txtSoTrang.Text = 1 + " / " + ((dsPN.Count / numberRecord) + 1).ToString();
            lblTongSLPN.Text = dsPN.Count.ToString();

            if (dsPN.Count > 0)
            {
                btnXuatThongtinPN.Enabled = true;
                btnXemCTPN.Enabled = true;
            }
            else
            {
                btnXuatThongtinPN.Enabled = false;
                btnXemCTPN.Enabled = false;
            }

            lblTongGTPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGT);
        }

        //Người thực hiện: Văn Khải
        private void btnXemCTPN_Click(object sender, EventArgs e)
        {
            int ma = 0;
            string mapn = grdThongtinHD.CurrentRow.Cells[0].Value.ToString();
            int.TryParse(mapn, out ma);
            FormChiTietPhieuNhap frm = new FormChiTietPhieuNhap(ma);
            frm.ShowDialog();
        }

        private void tbTenNCC_TextChanged(object sender, EventArgs e)
        {

        }

        //Người thực hiện: Thanh Đức
        private void FormCapNhatPhieuNhap_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        //Người thực hiện: Văn Khải========================================
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumber--;

                if (dsPNTam.Count % numberRecord == 0)
                {
                    tongsotrang = (dsPNTam.Count / numberRecord);
                }
                else
                {
                    tongsotrang = (dsPNTam.Count / numberRecord) + 1;
                }

                txtSoTrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordPN(dsPNTam, pageNumber, numberRecord);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pageNumber == 1 && dsPNTam.Count == 7)
            {
                return;
            }
            if (pageNumber - 1 < dsPNTam.Count / numberRecord)
            {
                pageNumber++;
                int TongSotrang = (dsPNTam.Count / numberRecord) + 1;
                if (dsPNTam.Count % numberRecord == 0)
                {
                    TongSotrang = (dsPNTam.Count / numberRecord);
                }
                txtSoTrang.Text = pageNumber.ToString() + " / " + TongSotrang;
                LoadRecordPN(dsPNTam, pageNumber, numberRecord);
            }

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsPNTam.Count / numberRecord) + 1;
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            LoadRecordPN(dsPNTam, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsPNTam.Count / numberRecord) + 1;
            txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordPN(dsPNTam, tongsotrang, numberRecord);
        }
        //=================================================================

        //Người thực hiện: Văn Khải
        private void btnXuatThongtinPN_Click(object sender, EventArgs e)
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
                        NhanVien nv = ctrlPN.getNhanvienbyMaTK(taikhoan);

                        // đặt tên người tạo file
                        if (nv != null)
                        {
                            p.Workbook.Properties.Author = nv.Chucvu + " " + nv.Hovaten;
                        }

                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Báo cáo thống kê hóa đơn";

                        ////Tạo một sheet để làm việc trên đó
                        p.Workbook.Worksheets.Add("Sheet 1");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = p.Workbook.Worksheets[1];

                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 11;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                "Mã phiếu nhập","Ngày lập",
                                                "Tên nhân viên",
                                                "Tên nhà cung cấp",
                                                "Giá trị phiếu nhập",
                                                "Ca làm việc",
                                                "Tình trạng",
                                                "Tổng số sản phẩm"
                        };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        if (nv != null && cboTenNV.SelectedIndex > 0 && dpNgaylap.Value != null)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin phiếu nhập ngày " + dpNgaylap.Value.ToShortDateString() + " theo nhân viên: " + nv.Hovaten;
                        }
                        if (nv != null && cboTenNV.SelectedIndex > 0 && dpNgaylap.Value != null && txtTenNCC.Text != null)
                        {
                            string tenkh = "";
                            if (grdThongtinHD.CurrentRow.Cells[3].Value != null)
                            {
                                tenkh = grdThongtinHD.CurrentRow.Cells[3].Value.ToString();
                            }
                            ws.Cells[1, 1].Value = "Thống kê thông tin phiếu nhập ngày " + dpNgaylap.Value.ToShortDateString() + " theo nhà cung câp: " + tenkh + " - nhân viên: " + nv.Hovaten;
                        }

                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
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

                            var cell4 = ws.Cells[rowIndex, colIndex++];
                            cell4.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border4 = cell4.Style.Border;
                            border4.Bottom.Style =
                                border4.Top.Style =
                                border4.Left.Style =
                                border4.Right.Style = ExcelBorderStyle.Thin;

                            var cell5 = ws.Cells[rowIndex, colIndex++];
                            cell5.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border5 = cell5.Style.Border;
                            border5.Bottom.Style =
                                border5.Top.Style =
                                border5.Left.Style =
                                border5.Right.Style = ExcelBorderStyle.Thin;

                            var cell6 = ws.Cells[rowIndex, colIndex++];
                            cell6.Value = grdThongtinHD.Rows[i].Cells[j++].Value.ToString();
                            var border6 = cell6.Style.Border;
                            border6.Bottom.Style =
                                border6.Top.Style =
                                border6.Left.Style =
                                border6.Right.Style = ExcelBorderStyle.Thin;
                        }

                        colIndex = 1;
                        var endCol0 = ws.Cells[rowIndex++, colIndex, rowIndex, colIndex + 5];
                        endCol0.Value = "Tổng giá trị phiếu nhập:";
                        endCol0.Style.Font.Bold = true;
                        endCol0.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        var border7 = endCol0.Style.Border;
                        border7.Bottom.Style =
                            border7.Top.Style =
                            border7.Left.Style =
                            border7.Right.Style = ExcelBorderStyle.Thin;

                        var endCol1 = ws.Cells[rowIndex, colIndex + 6];
                        endCol1.Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGT);
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

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Người thực hiện: Thanh Đức
        private void FormCapNhatPhieuNhap_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnTimkiemNV.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnXemCTPN.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnXuatThongtinPN.PerformClick(); }
        }

        //Người thực hiện: Văn Khải
        private void rdoTheongaylap_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (rdoTheongaylap.Checked == true)
            {
                lblNVTitle.Visible = true;
                cboTenNV.Visible = true;
                shaNV.Visible = true;

                lblNgaylapTitle.Visible = true;
                dpNgaylap.Visible = true;
                shaNgaylap.Visible = true;

                lblNCCTitle.Visible = false;
                txtTenNCC.Visible = false;
                shaNCC.Visible = false;

                grdThongtinHD.Rows.Clear();
                lblTongSLPN.Text = "0";
                lblTongGTPN.Text = "0";
            }
            else
            {
                lblNVTitle.Visible = false;
                cboTenNV.Visible = false;
                shaNV.Visible = false;

                lblNgaylapTitle.Visible = false;
                dpNgaylap.Visible = false;
                shaNgaylap.Visible = false;

                lblNCCTitle.Visible = true;
                txtTenNCC.Visible = true;
                shaNCC.Visible = true;

                grdThongtinHD.Rows.Clear();
                lblTongSLPN.Text = "0";
                lblTongGTPN.Text = "0";
            }
        }
    }
}
