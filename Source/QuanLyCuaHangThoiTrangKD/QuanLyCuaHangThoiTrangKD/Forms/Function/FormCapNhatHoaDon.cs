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
using QuanLyCuaHangThoiTrangKD;

using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormCapNhatHoaDon : Form
    {
        HoaDonController ctrlHD = new HoaDonController();
        TaiKhoan taikhoan;

        int pageNumber = 1;
        int numberRecord = 7;

        float tongGT = 0;

        List<HoaDon> dsHDTam = new List<HoaDon>();

        public FormCapNhatHoaDon(int matk)
        {
            InitializeComponent();
            this.taikhoan = ctrlHD.getTaiKhoanByMaTK(matk);

            rdoTheongaylap.Checked = true;

            cboTenNV.Items.Add("--Tên nhân viên--");
            cboTenNV.SelectedIndex = 0;

            foreach (var nv in ctrlHD.getTatcaNhanvien())
            {
                cboTenNV.Items.Add(nv.Hovaten.ToString());
            }
            //LoadTTHoadon(ctrlHD.getTatcaHoadon());

            if (taikhoan.Loai == "Quản lý")
            {
                cboTenNV.Enabled = true;
            }
            else
            {
                cboTenNV.Enabled = false;
                NhanVien nv = ctrlHD.getNhanVien(taikhoan.MaNV);
                if (nv != null)
                {
                    cboTenNV.SelectedIndex = cboTenNV.Items.IndexOf(nv.Hovaten);
                }
            }

            if(grdThongtinHD.Rows.Count == 0)
            {
                btnXuatThongtinHD.Enabled = false;
                btnXemCT.Enabled = false;
            }
            txtSoTrang.Text = 1 + " / ?";
        }

        //Người thực hiện: Văn Khải
        private void btnTimkiemTTHD_Click(object sender, EventArgs e)
        {
            tongGT = 0;
            List<HoaDon> dsHD = new List<HoaDon>();

            //List<HoaDon> dsHDTtheoKH;
            //List<HoaDon> dsHDtheoMaNV;
            List<HoaDon> dsHDtheoNgaylap;
            

            if(rdoTheongaylap.Checked == true)
            {
                dsHDtheoNgaylap = ctrlHD.getHoadonbyNgaylap(dpNgaylap.Value);
                foreach (var pn in dsHDtheoNgaylap)
                {
                    dsHD.Add(pn);
                }
                if (cboTenNV.SelectedIndex > 0)
                {
                    NhanVien nv = ctrlHD.getNhanVien(cboTenNV.Text);
                    dsHD = dsHD.Where(x => x.MaNV == nv.MaNV).ToList();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTenKH.Text))
                {
                    dsHD = ctrlHD.getHoadonbySDThoacTenKH(txtTenKH.Text);
                }
            }

            dsHD = dsHD.Where(x => x.Tinhtrang == "Đã thanh toán").ToList();

            dsHDTam = dsHD;
            LoadRecordHD(dsHD, pageNumber, numberRecord);
            txtSoTrang.Text = 1 + " / " + ((dsHD.Count / numberRecord) + 1).ToString();
            lblTongSLHD.Text = dsHD.Count.ToString();
            foreach (var hd in dsHD)
            {
                float tong1HD = 0;
                var dsct = ctrlHD.getCTHDbyMaHD(hd.MaHD);
                if (dsct.Count > 0)
                {
                    foreach (var ct in dsct)
                    {
                        var spct = ctrlHD.getSanPham(ct.MaSP);
                        if (spct != null)
                        {
                            tong1HD += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                        }
                    }
                }
                tongGT += tong1HD;
            }

            if (dsHD.Count > 0)
            {
                btnXuatThongtinHD.Enabled = true;
                btnXemCT.Enabled = true;
            }
            else
            {
                btnXuatThongtinHD.Enabled = false;
                btnXemCT.Enabled = false;
            }

            lblTongGTHD.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongGT);
        }

        void LoadTTHoadon(List<HoaDon> ds)
        {
            grdThongtinHD.Rows.Clear();
            grdThongtinHD.AllowUserToAddRows = true;

            foreach(var hd in ds)
            {
                grdThongtinHD.Rows.Add(ConvertHoadontoGridRow(hd));
            }

            grdThongtinHD.AllowUserToAddRows = false;
        }

        DataGridViewRow ConvertHoadontoGridRow(HoaDon hd)
        {
            float tongtien = 0;
            DataGridViewRow row = (DataGridViewRow)grdThongtinHD.Rows[0].Clone();

            var dscthd = ctrlHD.getCTHDbyMaHD(hd.MaHD);
            if(dscthd.Count > 0)
            {
                foreach (var ct in dscthd)
                {
                    var spct = ctrlHD.getSanPham(ct.MaSP);
                    tongtien += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia)/100) * 10);
                }
            }
            tongGT += tongtien;
            row.Cells[0].Value = hd.MaHD.ToString();

            row.Cells[1].Value = hd.Ngaylap.ToShortDateString();
            row.Cells[2].Value = ctrlHD.geTKhachhang(hd.MaKH).Hovaten;
            row.Cells[3].Value = ctrlHD.getNhanVien(hd.MaNV).Hovaten;
            row.Cells[4].Value = hd.Calamviec; 
            row.Cells[5].Value = ctrlHD.getSolongCTHD(hd);
            row.Cells[6].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);

            return row;
        }

        void LoadRecordHD(List<HoaDon> ds, int page, int recordNum)
        {
            List<HoaDon> dshd = new List<HoaDon>();

            dshd = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadTTHoadon(dshd);
        }

        //Người thực hiện: Văn Khải
        private void btnXemCT_Click(object sender, EventArgs e)
        {
            string mahd = "";
            if(grdThongtinHD.CurrentRow.Cells[0].Value != null)
            {
                mahd = grdThongtinHD.CurrentRow.Cells[0].Value.ToString();
            }
            FormChiTietHoaDon frm = new FormChiTietHoaDon(mahd);
            frm.ShowDialog();
        }

        //Người thực hiện: Thanh Đức
        private void FormCapNhatHoaDon_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        //Người thực hiện: Văn Khải ===================================================
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pageNumber == 1 && dsHDTam.Count == 7)
            {
                return;
            }
            if (pageNumber - 1 < dsHDTam.Count / numberRecord)
            {
                pageNumber++;
                int TongSotrang = (dsHDTam.Count / numberRecord) + 1;
                if (dsHDTam.Count % numberRecord == 0)
                {
                    TongSotrang = (dsHDTam.Count / numberRecord);
                }
                txtSoTrang.Text = pageNumber.ToString() + " / " + TongSotrang;
                LoadRecordHD(dsHDTam, pageNumber, numberRecord);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumber--;

                if (dsHDTam.Count % numberRecord == 0)
                {
                    tongsotrang = (dsHDTam.Count / numberRecord);
                }
                else
                {
                    tongsotrang = (dsHDTam.Count / numberRecord) + 1;
                }

                txtSoTrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordHD(dsHDTam, pageNumber, numberRecord);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsHDTam.Count / numberRecord) + 1;
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            LoadRecordHD(dsHDTam, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsHDTam.Count / numberRecord) + 1;
            txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordHD(dsHDTam, tongsotrang, numberRecord);
        }
        //=============================================================================

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Người thực hiện: Văn Khải
        private void btnXuatThongtinHD_Click(object sender, EventArgs e)
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
                        NhanVien nv = ctrlHD.getNhanvienbyMaTK(taikhoan);

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

                        ws.DefaultColWidth = 25;

                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 15;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                "Mã hóa đơn","Ngày lập",
                                                "Tên khách hàng",
                                                "Tên nhân viên",                                               
                                                "Ca làm việc",
                                                "Tổng số sản phẩm",
                                                "Giá trị hóa đơn (vnđ)"
                        };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        if (nv != null && cboTenNV.SelectedIndex > 0 && dpNgaylap.Value != null)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin hóa đơn ngày " + dpNgaylap.Value.ToShortDateString() + " theo nhân viên: " + nv.Hovaten;
                        }
                        if (nv != null && cboTenNV.SelectedIndex > 0 && dpNgaylap.Value != null && txtTenKH.Text != null)
                        {
                            string tenkh = "";
                            if (grdThongtinHD.CurrentRow.Cells[3].Value != null)
                            {
                                tenkh = grdThongtinHD.CurrentRow.Cells[3].Value.ToString();
                            }
                            ws.Cells[1, 1].Value = "Thống kê thông tin hóa đơn ngày " + dpNgaylap.Value.ToShortDateString() + " theo khách hàng: " + tenkh + " - nhân viên: " + nv.Hovaten;
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
                        var endCol0 = ws.Cells[rowIndex += 1, colIndex, rowIndex, colIndex + 5];
                        endCol0.Merge = true;
                        endCol0.Value = "Tổng giá trị hóa đơn:";
                        endCol0.Style.Font.Bold = true;
                        endCol0.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        var border7 = endCol0.Style.Border;
                        border7.Bottom.Style =
                            border7.Top.Style =
                            border7.Left.Style =
                            border7.Right.Style = ExcelBorderStyle.Thin;

                        var endCol1 = ws.Cells[rowIndex, colIndex + 6];
                        endCol1.Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongGT);
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

        //Người thực hiện: Thanh Đức
        private void FormCapNhatHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnTimkiemTTHD.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnXemCT.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnXuatThongtinHD.PerformClick(); }
        }

        private void tbTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        //Người thực hiện: Văn Khải
        private void rdoTheongaylap_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if(rdoTheongaylap.Checked == true)
            {
                lblNVTitle.Visible = true;
                cboTenNV.Visible = true;
                shaNV.Visible = true;

                lblNgaylapTitle.Visible = true;
                dpNgaylap.Visible = true;
                shaNgaylap.Visible = true;

                lblKHTitle.Visible = false;
                txtTenKH.Visible = false;
                shaKH.Visible = false;

                grdThongtinHD.Rows.Clear();
                lblTongSLHD.Text = "0";
                lblTongGTHD.Text = "0";
            }
            else
            {
                lblNVTitle.Visible = false;
                cboTenNV.Visible = false;
                shaNV.Visible = false;

                lblNgaylapTitle.Visible = false;
                dpNgaylap.Visible = false;
                shaNgaylap.Visible = false;

                lblKHTitle.Visible = true;
                txtTenKH.Visible = true;
                shaKH.Visible = true;

                grdThongtinHD.Rows.Clear();
                lblTongSLHD.Text = "0";
                lblTongGTHD.Text = "0";
            }
        }
    }
}
