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
using LiveCharts;
using LiveCharts.Wpf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongKetheoSP : Form
    {
        ThongKeController ctrlThongke = new ThongKeController();
        TaiKhoan taikhoan;

        List<SanPham> dsSP;
        List<ITemGridThongkeSP> dsThongke = new List<ITemGridThongkeSP>();

        int pageNumber = 1, numberRecord = 6;
        float TongGT = 0;

        int isChecked = 0;

        //Người thực hiện: Văn Khải
        public FormThongKetheoSP(int MaTK)
        {
            InitializeComponent();
            taikhoan = ctrlThongke.getTaiKhoanByMaTK(MaTK);

            var dsCTPNLoaisp = ctrlThongke.LoadDanhsachCTPNByLoaiSP();

            var dsCTHDLoaisp = ctrlThongke.LoadDanhsachCTHDByLoaiSP();

            var dsCTHDbysp = ctrlThongke.LoadDanhsachCTHDByTenSP();

            dsSP = ctrlThongke.getTatcaSanpham();

            var loaisps = new List<string>();
            loaisps.Add("Quần");
            loaisps.Add("Áo");
            loaisps.Add("Áo khoát");
            loaisps.Add("Đặc biệt");
            loaisps.Add("Phụ kiện");
            loaisps.Add("Quần ngắn");

            grdDSThongkeSP.AllowUserToAddRows = false;
            cboLoaiTKe.Items.Add("--Theo tình trạng sản phẩm--");
            cboLoaiTKe.Items.Add("Sản phẩm gần hết hàng");
            cboLoaiTKe.Items.Add("Sản phẩm bán chạy tháng " + DateTime.Today.Month + " / " + DateTime.Today.Year);
            //cboLoaiTKe.Items.Add("Sản phẩm chưa có đơn hàng");
            cboLoaiTKe.Text = cboLoaiTKe.Items[0].ToString();

            chaTKSPBanchay.DataSource = dsCTHDLoaisp.ToList();
            chaTKSPBanchay.Series["LoaiSP"].XValueMember = "Loaisp";
            chaTKSPBanchay.Series["LoaiSP"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chaTKSPBanchay.Series["LoaiSP"].YValueMembers = "Tong";
            chaTKSPBanchay.Series["LoaiSP"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;

            List<CTHD_BySP> cthdsp = new List<CTHD_BySP>();
            foreach (var ct in dsCTHDbysp)
            {
                cthdsp.Add(new CTHD_BySP() { Tensp = ct.Loaisp, Tong = ct.Tong });
            }

            foreach (var ct in cthdsp)
            {
                SanPham sp = ctrlThongke.getSanPham(ct.Tensp);
                ct.Tong = (ct.Tong * sp.Dongia) + (((ct.Tong * sp.Dongia)/100))*10;
            }

            cthdsp.Sort((p1, p2) =>
            {
                if (p1.Tong < p2.Tong)
                {
                    return 1;
                }
                else if (p1.Tong == p2.Tong)
                {
                    return 0;
                }
                return -1;
            });

            cthdsp = cthdsp.Take(5).ToList();

            int k = 0;
            foreach (var ct in cthdsp)
            {
                chaTKTopSPBanchay.Series["TenSP"].Points.Add(ct.Tong);
                chaTKTopSPBanchay.Series["TenSP"].Points[k].AxisLabel = ct.Tensp;
                chaTKTopSPBanchay.Series["TenSP"].Points[k].LegendText = ct.Tensp;
                chaTKTopSPBanchay.Series["TenSP"].Points[k].Label = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ct.Tong);
                k++;
            }
            chaTKTopSPBanchay.Titles["Title1"].Text += " " + DateTime.Today.Month + "/" + DateTime.Today.Year;

            List<LoaiSP_SLTon> loaiSP_SLTons = new List<LoaiSP_SLTon>();

            int g = 0;
            foreach (var loai in loaisps)
            {
                if (dsCTPNLoaisp.Any(x => x.Loaisp == loai) && dsCTHDLoaisp.Any(x => x.Loaisp == loai))
                {
                    int tongslsphd = dsCTHDLoaisp.Where(x => x.Loaisp == loai).FirstOrDefault().Tong;
                    int tongslsppn = dsCTPNLoaisp.Where(x => x.Loaisp == loai).FirstOrDefault().Tong;
                    loaiSP_SLTons.Add(new LoaiSP_SLTon() { Loaisp = loai, Tong = tongslsppn - tongslsphd });

                }
                else if (dsCTPNLoaisp.Any(x => x.Loaisp == loai) && !dsCTHDLoaisp.Any(x => x.Loaisp == loai))
                {
                    int tongslsppn = dsCTPNLoaisp.Where(x => x.Loaisp == loai).FirstOrDefault().Tong;
                    loaiSP_SLTons.Add(new LoaiSP_SLTon() { Loaisp = loai, Tong = tongslsppn });
                }
                else if (!dsCTPNLoaisp.Any(x => x.Loaisp == loai))
                {
                    loaiSP_SLTons.Add(new LoaiSP_SLTon() { Loaisp = loai, Tong = 0 });

                }
                g++; 
            }

            loaiSP_SLTons.Sort((p1, p2) =>
            {
                if (p1.Tong < p2.Tong)
                {
                    return 1;
                }
                else if (p1.Tong == p2.Tong)
                {
                    return 0;
                }
                return -1;
            });

            int d = 0;
            foreach(var loai in loaiSP_SLTons)
            {
                chaTKSPHethang.Series["LoaiSP"].Points.Add(loai.Tong);
                chaTKSPHethang.Series["LoaiSP"].Points[d].AxisLabel = loai.Loaisp;
                chaTKSPHethang.Series["LoaiSP"].Points[d].LegendText = loai.Loaisp;
                chaTKSPHethang.Series["LoaiSP"].Points[d].Label = loai.Tong.ToString();
                d++;
            }

            //grdDSThongkeSP.CurrentTheme.AlternatingRowsStyle.BackColor = Color.White;
        }

        //Người thực hiện: Văn Khải
        private void cboLoaiTKe_SelectedValueChanged(object sender, EventArgs e)
        {
            TongGT = 0;
            isChecked = 0;
            dsThongke.Clear();
            //List<CTHD_ByMaSPMaHD> dsCTHDMahd = new List<CTHD_ByMaSPMaHD>();

            var dsCTPN = ctrlThongke.LoadDanhsachCTPNByMaSP();

            var dsCTHDMahd = ctrlThongke.LoadDanhsachCTHDByMaSP();

            int slton = 0, slBan = 0, slnhap = 0;
            if (cboLoaiTKe.SelectedIndex == 1)
            {
                //grdDSThongkeSP.Columns[7].DefaultCellStyle.BackColor = Color.Gray;
                isChecked = 1;
                foreach (var sp in dsSP)
                {
                    var ctpn = dsCTPN.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                    var cthd = dsCTHDMahd.Where(x => x.Masp == sp.MaSP).FirstOrDefault();

                    if (ctpn != null && cthd != null)
                    {
                        slBan = cthd.TongSL;
                        slnhap = ctpn.TongSL;
                        dsThongke.Add(new ITemGridThongkeSP() { Sanpham = sp, Slton = slnhap - slBan, TongGTSPDaban = 0 });
                    }
                    else 
                    {
                        if (ctpn != null)
                        {
                            slnhap = ctpn.TongSL;
                        }
                        dsThongke.Add(new ITemGridThongkeSP() { Sanpham = sp, Slton = slnhap, TongGTSPDaban = 0 });
                    }
                }

                dsThongke.Sort((p1, p2) =>
                {
                    if (p1.Slton > p2.Slton)
                    {
                        return 1;
                    }
                    else if (p1.Slton == p2.Slton)
                    {
                        return 0;
                    }
                    return -1;
                });
            }
            else if (cboLoaiTKe.SelectedIndex == 2)
            {
                isChecked = 2;
                //grdDSThongkeSP.Columns[6].DefaultCellStyle.BackColor = Color.Gray;
                List<HoaDon> dsHDtheothang = ctrlThongke.getHoadonbyThangNam(DateTime.Today.Month, DateTime.Today.Year);
                List<ChiTietHoaDon> dsCTHDTheothang = new List<ChiTietHoaDon>();

                foreach(var hd in dsHDtheothang)
                {
                    dsCTHDTheothang.AddRange(ctrlThongke.getCTHDbyMaHD(hd.MaHD));
                }

                foreach (var sp in dsSP)
                {
                    if (dsCTHDMahd.Any(x => x.Masp == sp.MaSP && dsCTHDTheothang.Any(y => y.MaSP == x.Masp)))
                    {
                        slBan = dsCTHDMahd.Where(x => x.Masp == sp.MaSP).FirstOrDefault().TongSL;
                        dsThongke.Add(new ITemGridThongkeSP() { Sanpham = sp, Slton = 0, TongGTSPDaban = (slBan * sp.Dongia) + (((slBan * sp.Dongia)/100)*10) });
                    }
                }
                dsThongke.Sort((p1, p2) =>
                {
                    if (p1.TongGTSPDaban < p2.TongGTSPDaban)
                    {
                        return 1;
                    }
                    else if (p1.TongGTSPDaban == p2.TongGTSPDaban)
                    {
                        return 0;
                    }
                    return -1;
                });

                foreach (var sp in dsThongke)
                {
                    TongGT += sp.TongGTSPDaban;
                }
            }

            LoadRecordSP(dsThongke, pageNumber, numberRecord);
            int tongsotrang = (dsThongke.Count / numberRecord) + 1;
            if(dsThongke.Count % numberRecord == 0)
            {
                tongsotrang = (dsThongke.Count / numberRecord) + 1;
            }
            txtSotrang.Text = 1 + " / " + tongsotrang;
        }

        //Người thực hiện: Thanh Đức
        void LoadRecordSP(List<ITemGridThongkeSP> ds, int page, int recordNum)
        {
            List<ITemGridThongkeSP> dsSP = new List<ITemGridThongkeSP>();

            dsSP = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();

            grdDSThongkeSP.Rows.Clear();
            grdDSThongkeSP.AllowUserToAddRows = true;

            foreach (var sp in dsSP)
            {
                grdDSThongkeSP.Rows.Add(ConvertSanPhamtoGridViewRow(sp));
            }

            grdDSThongkeSP.AllowUserToAddRows = false;
        }

        DataGridViewRow ConvertSanPhamtoGridViewRow(ITemGridThongkeSP sp)
        {
            DataGridViewRow row = (DataGridViewRow)grdDSThongkeSP.Rows[0].Clone();
            row.Cells[0].Value = sp.Sanpham.TenSP.ToString();
            row.Cells[1].Value = sp.Sanpham.Chatlieu.ToString();
            row.Cells[2].Value = sp.Sanpham.Mausac.ToString();
            row.Cells[3].Value = sp.Sanpham.Kichthuoc.ToString();
            row.Cells[4].Value = sp.Sanpham.LoaiSP.ToString();
            row.Cells[5].Value = sp.Sanpham.Donvi.ToString();
            if (isChecked == 1)
            {
                row.Cells[6].Value = sp.Slton;
                row.Cells[7].Value = "###";
            }
            if (isChecked == 2)
            {
                row.Cells[6].Value = "###";
                row.Cells[7].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.TongGTSPDaban);
            }

            return row;
        }

        //Người thực hiện: Văn Khải
        private void btnXuatTKSP_Click(object sender, EventArgs e)
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
                        ws.Cells.Style.Font.Size = 13;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                                                "Tên sản phẩm",
                                                "Chất liệu",
                                                "Màu sắc",
                                                "Kích thước",
                                                "Loại sản phẩm",
                                                "Đơn vị",
                                                "Số lượng tồn",
                                                "Tổng giá trị đã bán"};

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        if (cboLoaiTKe.Text != null && cboLoaiTKe.SelectedIndex > 0)
                        {
                            ws.Cells[1, 1].Value = "Thống kê thông tin sản phẩm tháng " + DateTime.Today.Month + "/" + DateTime.Today.Year + " theo " + cboLoaiTKe.Text;
                        }

                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = 25;
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
                        for (int i = 0; i < grdDSThongkeSP.Rows.Count; i++)
                        {
                            int j = 0;

                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            var cell0 = ws.Cells[rowIndex, colIndex++];
                            cell0.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border0 = cell0.Style.Border;
                            border0.Bottom.Style =
                                border0.Top.Style =
                                border0.Left.Style =
                                border0.Right.Style = ExcelBorderStyle.Thin;

                            var cell1 = ws.Cells[rowIndex, colIndex++];
                            cell1.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border1 = cell1.Style.Border;
                            border1.Bottom.Style =
                                border1.Top.Style =
                                border1.Left.Style =
                                border1.Right.Style = ExcelBorderStyle.Thin;

                            var cell2 = ws.Cells[rowIndex, colIndex++];
                            cell2.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border2 = cell2.Style.Border;
                            border2.Bottom.Style =
                                border2.Top.Style =
                                border2.Left.Style =
                                border2.Right.Style = ExcelBorderStyle.Thin;

                            var cell3 = ws.Cells[rowIndex, colIndex++];
                            cell3.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border3 = cell3.Style.Border;
                            border3.Bottom.Style =
                                border3.Top.Style =
                                border3.Left.Style =
                                border3.Right.Style = ExcelBorderStyle.Thin;

                            var cell4 = ws.Cells[rowIndex, colIndex++];
                            cell4.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border4 = cell4.Style.Border;
                            border4.Bottom.Style =
                                border4.Top.Style =
                                border4.Left.Style =
                                border4.Right.Style = ExcelBorderStyle.Thin;

                            var cell5 = ws.Cells[rowIndex, colIndex++];
                            cell5.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border5 = cell5.Style.Border;
                            border5.Bottom.Style =
                                border5.Top.Style =
                                border5.Left.Style =
                                border5.Right.Style = ExcelBorderStyle.Thin;

                            var cell6 = ws.Cells[rowIndex, colIndex++];
                            cell6.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border6 = cell6.Style.Border;
                            border6.Bottom.Style =
                                border6.Top.Style =
                                border6.Left.Style =
                                border6.Right.Style = ExcelBorderStyle.Thin;

                            var cell7 = ws.Cells[rowIndex, colIndex++];
                            cell7.Value = grdDSThongkeSP.Rows[i].Cells[j++].Value.ToString();
                            var border7 = cell7.Style.Border;
                            border7.Bottom.Style =
                                border7.Top.Style =
                                border7.Left.Style =
                                border7.Right.Style = ExcelBorderStyle.Thin;
                        }

                        if(isChecked == 2)
                        {
                            colIndex = 1;
                            var endCol0 = ws.Cells[rowIndex += 1, colIndex, rowIndex, colIndex + 6];
                            endCol0.Value = "Tổng giá trị đã bán (vnđ):";
                            endCol0.Merge = true;
                            endCol0.Style.Font.Bold = true;
                            endCol0.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            var border7 = endCol0.Style.Border;
                            border7.Bottom.Style =
                                border7.Top.Style =
                                border7.Left.Style =
                                border7.Right.Style = ExcelBorderStyle.Thin;

                            var endCol1 = ws.Cells[rowIndex, colIndex + 7];
                            endCol1.Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", TongGT);
                            endCol1.Style.Font.Bold = true;
                            endCol1.Style.Font.Size = 22;
                            var border8 = endCol1.Style.Border;
                            border8.Bottom.Style =
                                border8.Top.Style =
                                border8.Left.Style =
                                border8.Right.Style = ExcelBorderStyle.Thin;
                        }

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

        //Người thực hiện: Thanh Đức ========================================
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumber--;

                if (dsThongke.Count % numberRecord == 0)
                {
                    tongsotrang = (dsThongke.Count / numberRecord);
                }
                else
                {
                    tongsotrang = (dsThongke.Count / numberRecord) + 1;
                }

                txtSotrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordSP(dsThongke, pageNumber, numberRecord);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dsThongke.Count % numberRecord != 0)
            {
                if (pageNumber - 1 < dsThongke.Count / numberRecord)
                {
                    pageNumber++;
                    int TongSotrang = (dsThongke.Count / numberRecord) + 1;
                    txtSotrang.Text = pageNumber.ToString() + " / " + TongSotrang;
                    LoadRecordSP(dsThongke, pageNumber, numberRecord);
                }
            }
            if (dsThongke.Count % numberRecord == 0)
            {
                if (pageNumber < dsThongke.Count / numberRecord)
                {
                    int tongsotrang = 0;
                    pageNumber++;
                    tongsotrang = (dsThongke.Count / numberRecord);
                    txtSotrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                    LoadRecordSP(dsThongke, pageNumber, numberRecord);
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsThongke.Count / numberRecord) + 1;
            txtSotrang.Text = 1 + " / " + tongsotrang;
            LoadRecordSP(dsThongke, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsThongke.Count / numberRecord) + 1;
            txtSotrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordSP(dsThongke, tongsotrang, numberRecord);
        }

        //===================================================================

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
