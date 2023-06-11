using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Models;
using OfficeOpenXml;
using System.IO;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormLuongNhanVien : Form
    {
        NhanVienController ctrlNV = new NhanVienController();
        Random ran = new Random();

        List<LichLamViec> dsTamLLV;
        //List<Luong> dsTamLuong;

        int pageNumberLLV = 1;
        int numberRecordLLV = 6;

        int pageNumberLuong = 1;
        int numberRecordLuong = 4;

        public FormLuongNhanVien()
        {
            InitializeComponent();
            LoadCombobox();

            btnTinhluongNV.Enabled = false;
            btnLuuTTLuong.Enabled = false;
            btnXoaTTLuong.Enabled = false;
            btnLuuDSlLuong.Enabled = false;
        }

        void LoadDanhsachTTLLV(List<LichLamViec> ds)
        {
            grdThongtinLLV.Rows.Clear();

            grdThongtinLLV.AllowUserToAddRows = true;

            if (ds.Count == 0)
            {
                btnLuuTTLuong.Enabled = false;
                btnTinhluongNV.Enabled = false;
            }
            else
            {
                btnTinhluongNV.Enabled = true;
                foreach (var llv in ds)
                {
                    grdThongtinLLV.Rows.Add(ConvertLLVtoGridViewRow(llv));
                }
            }
            grdThongtinLLV.AllowUserToAddRows = false;
        }

        DataGridViewRow ConvertLLVtoGridViewRow(LichLamViec llv)
        {
            DataGridViewRow row = (DataGridViewRow)grdThongtinLLV.Rows[0].Clone();
            row.Cells[0].Value = llv.Ngaylamviec.ToShortDateString();
            row.Cells[1].Value = llv.Tongsocalamviec;

            return row;
        }

        //void LoadDanhsachTTLuong(List<Luong> ds)
        //{
        //    grdThongtinLuongNV.Rows.Clear();

        //    grdThongtinLuongNV.AllowUserToAddRows = true;
        //    foreach (var luong in ds)
        //    {
        //        //grdThongtinLuongNV.Rows.Add(ConvertLuongtoGridViewRow(luong));
        //    }

        //    grdThongtinLuongNV.AllowUserToAddRows = false;
        //}

        //DataGridViewRow ConvertLuongtoGridViewRow(Luong luong)
        //{
        //    DataGridViewRow row = (DataGridViewRow)grdThongtinLuongNV.Rows[0].Clone();
        //    row.Cells[0].Value = luong.NhanVien.Hovaten.ToString();
        //    row.Cells[1].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", luong.NhanVien.Luongtheogio);
        //    if(luong.NhanVien != null && luong.NhanVien.Phucap > 0)
        //    {
        //        row.Cells[2].Value = luong.NhanVien.Phucap;
        //    }
        //    row.Cells[3].Value = int.Parse(luong.Thang.ToString());
        //    row.Cells[4].Value = int.Parse(luong.Nam.ToString());
        //    //row.Cells[5].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", luong.Tienluong);
        //    row.Cells[6].Value = luong.Tinhtrang.ToString();

        //    return row;
        //}

        void LoadCombobox()
        {
            var dsNV = ctrlNV.getTatcaNhanvien();
            foreach (var nv in dsNV)
            {
                cboTenNV.Items.Add(nv.Hovaten);
            }

            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i);
            }
        }

        //Người thực hiện: Văn Khải 
        private void cbThang_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboTenNV.SelectedItem.ToString() != "")
            {
                NhanVien nv = ctrlNV.getNhanVien(cboTenNV.SelectedItem.ToString());
                var dsLLV = ctrlNV.getLLVbyNhanVien(nv, int.Parse(cboThang.Text));

                if (dsLLV.Count > 0)
                {
                    gbDanhsachLLV.Text += " " + cboThang.Text;
                    dsTamLLV = dsLLV;
                    LoadRecordLLV(dsLLV, pageNumberLLV, numberRecordLLV);
                }
            }
        }

        //Người thực hiện: Văn Khải 
        private void btnTinhluongNV_Click(object sender, EventArgs e)
        {
            int tongSoCalamviec = 0, thanghientai = 0;

            if (cboTenNV.SelectedIndex > -1 && cboThang.SelectedIndex > -1)
            {
                NhanVien nv = ctrlNV.getNhanVien(cboTenNV.SelectedItem.ToString());
                double tienluong1ca = double.Parse(nv.Luongtheogio.ToString()) * 4;
                int.TryParse(cboThang.SelectedItem.ToString(), out thanghientai);

                foreach (var llv in dsTamLLV)
                {
                    if (thanghientai > 0 && llv.Ngaylamviec.Month == thanghientai)
                    {
                        tongSoCalamviec += int.Parse(llv.Tongsocalamviec.ToString());
                    }
                }

                cboTenNV.Enabled = false;
                cboThang.Enabled = false;
                lblThanhtien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", (tienluong1ca * tongSoCalamviec) + nv.Phucap);
                btnLuuTTLuong.Enabled = true;
            }
        }

        //Người thực hiện: Văn Khải 
        private void cbTenNV_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cboTenNV.SelectedItem != null)
            {
                var nv = ctrlNV.getNhanVien(cboTenNV.SelectedItem.ToString());
                if(nv != null)
                {
                    //var dsLuong = ctrlNV.getLuongbyNhanVien(nv);
                    //dsTamLuong = dsLuong;
                    //LoadRecordLuong(dsLuong, pageNumberLuong, numberRecordLuong);
                }
            }
        }

        //Người thực hiện: Văn Khải 
        private void btnLuuTTLuong_Click(object sender, EventArgs e)
        {
            //if (cboTenNV.SelectedIndex > -1 && cboThang.SelectedIndex > -1)
            //{
            //    NhanVien nv = ctrlNV.getNhanVien(cboTenNV.SelectedItem.ToString());
            //    Luong luong; bool isExixt = false;

            //    luong = new Luong()
            //    {
            //        NhanVien = nv,
            //        Thang = int.Parse(cboThang.Text),
            //        Nam = DateTime.Today.Year,
            //        //Tienluong = float.Parse(lblThanhtien.Text),
            //        Tinhtrang = "Xem trước"
            //    };

            //    foreach (var l in dsTamLuong)
            //    {
            //        if (l.Thang == luong.Thang && l.Nam == luong.Nam && l.NhanVien.MaNV == luong.NhanVien.MaNV)
            //        {
            //            isExixt = true;
            //        }
            //    }

            //    if (!isExixt)
            //    {
            //        //ctrlNV.LuuLuong(luong);
            //        dsTamLuong.Add(luong);
            //        MessageBox.Show("Thông tin lương lưu thành công !", "Thông báo");
            //        LoadRecordLuong(dsTamLuong, pageNumberLuong, numberRecordLuong);
            //        btnXoaTTLuong.Enabled = true;
            //        btnLuuDSlLuong.Enabled = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Thông tin lương đã tồn tại !", "Thông báo");
            //    }
            //}
        }

        //Người thực hiện: Văn Khải 
        private void btnXoaTTLuong_Click(object sender, EventArgs e)
        {
            try
            {
                //if(MessageBox.Show("Xác nhận xóa thông tin lương đã chọn ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                //    string strnam = grdThongtinLuongNV.CurrentRow.Cells[4].Value.ToString();
                //    string strthang = grdThongtinLuongNV.CurrentRow.Cells[3].Value.ToString();
                //    int thang = 0, nam = 0;
                //    int.TryParse(strthang, out thang);
                //    int.TryParse(strnam, out nam);
                //    Luong luongRemove = dsTamLuong.Where(x => x.Thang == thang && x.Nam == nam).FirstOrDefault();
                //    if (luongRemove != null && luongRemove.Tinhtrang != "Xác nhận")
                //    {
                //        dsTamLuong.Remove(luongRemove);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Không thể xóa thông tin lương nhân viên đã xác nhận", "Thông báo");
                //    }

                //    if(dsTamLuong.Count > 0)
                //    {
                //        LoadRecordLuong(dsTamLuong, pageNumberLuong, numberRecordLuong);
                //        int tongsotrang = (dsTamLuong.Count / numberRecordLuong) + 1;
                //        if (dsTamLuong.Count % numberRecordLuong == 0)
                //        {
                //            tongsotrang = (dsTamLuong.Count / numberRecordLuong);
                //        }
                //        txtSotrangLuong.Text = 1 + " / " + tongsotrang;
                //    }
                //    else
                //    {
                //        btnLuuDSlLuong.Enabled = false;
                //        btnXoaTTLuong.Enabled = false;
                //    }
                //}
            }
            catch
            {

            }
        }

        void LoadRecordLLV(List<LichLamViec> ds, int page, int recordNum)
        {
            List<LichLamViec> dsLLV = new List<LichLamViec>();

            dsLLV = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTLLV(dsLLV);
        }

        //void LoadRecordLuong(List<Luong> ds, int page, int recordNum)
        //{
        //    List<Luong> dsLuong = new List<Luong>();

        //    dsLuong = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
        //    LoadDanhsachTTLuong(dsLuong);
        //}

        //Người thực hiện: Văn Khải ==========================================
        private void btnPrevLLV_Click(object sender, EventArgs e)
        {
            if (pageNumberLLV - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumberLLV--;

                if (dsTamLLV.Count % numberRecordLLV == 0)
                {
                    tongsotrang = (dsTamLLV.Count / numberRecordLLV);
                }
                else
                {
                    tongsotrang = (dsTamLLV.Count / numberRecordLLV) + 1;
                }

                txtSoTrangLLV.Text = pageNumberLLV.ToString() + " / " + tongsotrang;
                LoadRecordLLV(dsTamLLV, pageNumberLLV, numberRecordLLV);
            }
        }

        private void btnNextLLV_Click(object sender, EventArgs e)
        {
            if (pageNumberLLV - 1 < dsTamLLV.Count / numberRecordLLV)
            {
                pageNumberLLV++;

                int tongsotrang = (dsTamLLV.Count / numberRecordLLV) + 1;
                if (dsTamLLV.Count % numberRecordLLV == 0)
                {
                    tongsotrang = (dsTamLLV.Count / numberRecordLLV);
                }

                txtSoTrangLLV.Text = pageNumberLLV.ToString() + " / " + tongsotrang;
                LoadRecordLLV(dsTamLLV, pageNumberLLV, numberRecordLLV);
            }
        }

        private void btnPrevLuong_Click(object sender, EventArgs e)
        {
            //if (pageNumberLuong - 1 > 0)
            //{
            //    int tongsotrang = 0;
            //    pageNumberLuong--;

            //    if (dsTamLuong.Count % numberRecordLuong == 0)
            //    {
            //        tongsotrang = (dsTamLuong.Count / numberRecordLuong);
            //    }
            //    else
            //    {
            //        tongsotrang = (dsTamLuong.Count / numberRecordLuong) + 1;
            //    }

            //    txtSotrangLuong.Text = pageNumberLuong.ToString() + " / " + tongsotrang;
            //    //LoadRecordLuong(dsTamLuong, pageNumberLuong, numberRecordLuong);
            //}
        }

        private void btnNextLuong_Click(object sender, EventArgs e)
        {
            //if (pageNumberLuong - 1 < dsTamLuong.Count / numberRecordLuong)
            //{
            //    pageNumberLuong++;

            //    int tongsotrang = (dsTamLuong.Count / numberRecordLuong) + 1;
            //    if (dsTamLLV.Count % numberRecordLLV == 0)
            //    {
            //        tongsotrang = (dsTamLuong.Count / numberRecordLuong);
            //    }

            //    txtSotrangLuong.Text = pageNumberLuong.ToString() + " / " + tongsotrang;
            //    //LoadRecordLuong(dsTamLuong, pageNumberLuong, numberRecordLuong);
            //}
        }

        private void btnFirstLLV_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTamLLV.Count / numberRecordLLV) + 1;
            txtSoTrangLLV.Text = 1 + " / " + tongsotrang;
            LoadRecordLLV(dsTamLLV, 1, numberRecordLLV);
        }

        private void btnLastLLV_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTamLLV.Count / numberRecordLLV) + 1;
            txtSoTrangLLV.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordLLV(dsTamLLV, tongsotrang, numberRecordLLV);
        }

        private void btnFirstLuong_Click(object sender, EventArgs e)
        {
            //int tongsotrang = (dsTamLuong.Count / numberRecordLuong) + 1;
            //txtSotrangLuong.Text = 1 + " / " + tongsotrang;
            //LoadRecordLuong(dsTamLuong, 1, numberRecordLuong);
        }

        private void btnLastLuong_Click(object sender, EventArgs e)
        {
            //int tongsotrang = (dsTamLuong.Count / numberRecordLuong) + 1;
            //txtSotrangLuong.Text = tongsotrang + " / " + tongsotrang;
            //LoadRecordLuong(dsTamLuong, tongsotrang, numberRecordLuong);
        }
        //=====================================================================

        //Người thực hiện: Văn Khải 
        private void btnImportLLV_Click(object sender, EventArgs e)
        {
            lblThanhtien.ResetText();
            List<LichLamViec> dsLLV = new List<LichLamViec>();
            List<int> dsSLnhap = new List<int>();

            if (MessageBox.Show("Xác nhận nhập thông tin lịch làm việc từ tệp ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenFileDialog openFileFormBangluong = new OpenFileDialog();
                openFileFormBangluong.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileFormBangluong.FilterIndex = 1;
                openFileFormBangluong.RestoreDirectory = true;
                if (openFileFormBangluong.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string Tongsocalamviec = "", Manv = "";

                        var package = new ExcelPackage(new FileInfo(openFileFormBangluong.FileName));

                        ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                        for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                        {
                            try
                            {
                                int j = 1;

                                var Ngaylamviec = workSheet.Cells[i, j++].Value;
                                DateTime ngaylv = new DateTime();
                                if (Ngaylamviec != null)
                                {
                                    ngaylv = (DateTime)Ngaylamviec;
                                }

                                Tongsocalamviec = workSheet.Cells[i, j++].Value.ToString();
                                Manv = workSheet.Cells[i, j++].Value.ToString();

                                LichLamViec llv = new LichLamViec();
                                int Tongsocalv = 0;
                                if (ngaylv != null)
                                {
                                    llv.Ngaylamviec = ngaylv;
                                    NhanVien nv = ctrlNV.getNhanVien(Manv);
                                    if (nv != null)
                                    {
                                        llv.NhanVien = nv;
                                    }

                                    if (int.TryParse(Tongsocalamviec, out Tongsocalv))
                                    {
                                        llv.Tongsocalamviec = Tongsocalv;
                                    }

                                    if (!dsLLV.Any(x => x.Ngaylamviec.Day == ngaylv.Day))
                                    {
                                        dsLLV.Add(llv);
                                    }                                    
                                }

       
                            }
                            catch (Exception)
                            {

                            }
                        }
                        LoadRecordLLV(dsLLV, pageNumberLLV, numberRecordLLV);
                        dsTamLLV = dsLLV;
                        int tongsotrang = (dsLLV.Count / numberRecordLLV) + 1;
                        if (dsLLV.Count % numberRecordLLV == 0)
                        {
                            tongsotrang = (dsLLV.Count / numberRecordLLV);
                        }
                        txtSoTrangLLV.Text = 1 + " / " + tongsotrang;

                        if (dsLLV.Count > 0)
                        {
                            NhanVien nv = ctrlNV.getNhanVien(dsLLV.FirstOrDefault().NhanVien.MaNV);
                            if(nv != null)
                            {
                                cboTenNV.SelectedItem = nv.Hovaten;
                                cboThang.SelectedItem = dsLLV.FirstOrDefault().Ngaylamviec.Month;
                                lblTongsongayLV.Text = dsLLV.Count.ToString();
                                lblLuongtheogio.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", nv.Luongtheogio);
                                lblPhucap.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", nv.Phucap);
                                btnTinhluongNV.Enabled = true;                                
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("File excel đang được mở !");
                    }
                }

            }
        }

        //Người thực hiện: Văn Khải 
        private void btnLuuDSlLuong_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Xác nhận lưu thông tin cập nhật lương ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    foreach (Luong luong in dsTamLuong)
            //    {
            //        luong.Tinhtrang = "Xác nhận";
            //        ctrlNV.LuuLuong(luong);
            //    }
            //    var nv = ctrlNV.getNhanVien(cboTenNV.SelectedItem.ToString());
            //    var dsLuong = ctrlNV.getLuongbyNhanVien(nv);
            //    dsTamLuong = dsLuong;
            //    LoadRecordLuong(dsTamLuong, pageNumberLuong, numberRecordLuong);
            //}
        }

        private void bunifuGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblLuongtheogio_Click(object sender, EventArgs e)
        {

        }
    }
}
