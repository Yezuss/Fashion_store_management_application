using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongTinKhachHang : Form
    {

        KhachHangController ctrlKH = new KhachHangController();

        List<KhachHang> dsTam;

        int pageNumber = 1;
        int numberRecord = 7;

        public FormThongTinKhachHang()
        {
            InitializeComponent();
            //var dsKHHT = ctrlKH.getTatcaKhachhang();
            //dsTam = dsKHHT;
            //LoadDanhsachTTKH(dsKHHT);
            btnXoarong.Enabled = false;
            btnLuuKH.Enabled = false;
            DisableTTKH();
            cboGioitinh.Items.Add("Nam");
            cboGioitinh.Items.Add("Nữ");
            cboGioitinh.Items.Add("Khác");


        }

        private void btnXoarong_Click(object sender, EventArgs e)
        {
            txtTenKH.Text = "";
            cboGioitinh.ResetText();
            txtDiachiKH.Text = "";
            txtEmailKH.Text = "";
            txtSDT.Text = "";
        }

        //Người thực hiện: Văn Khải 
        private void btnCapnhatKH_Click(object sender, EventArgs e)
        {
            if (btnCapnhatKH.Text == "Cập nhật (F4)")
            {
                EnableTTKH();
                btnCapnhatKH.Text = "Hủy";
                btnLuuKH.Enabled = true;
                btnXoarong.Enabled = true;
            }
            else
            {
                DisableTTKH();
                btnCapnhatKH.Text = "Cập nhật (F4)";
                btnLuuKH.Enabled = false;
                btnXoarong.Enabled = false;
            }
        }

        //Người thực hiện: Thanh Đức
        private void dgvThongtinKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            float tong = 0;
            TimeSpan khoangTG;
            DateTime Tam = new DateTime(); 
            int khoangNgannhat = 0;
            var dsHDtheoKH = ctrlKH.getDanhsachHoadon(grdThongtinKH.CurrentRow.Cells[0].Value.ToString());

            if(dsHDtheoKH.Count > 0)
            {
                lblTongsoHD.Text = dsHDtheoKH.Count.ToString();
                foreach (var hd in dsHDtheoKH)
                {
                    float tong1HD = 0;
                    var dsct = ctrlKH.getCTHDbyMaHD(hd.MaHD);
                    if (dsct.Count > 0)
                    {
                        foreach (var ct in dsct)
                        {
                            var spct = ctrlKH.getSanPham(ct.MaSP);
                            if (spct != null)
                            {
                                tong1HD += (ct.Soluong * spct.Dongia) + (((ct.Soluong * spct.Dongia) / 100) * 10);
                            }
                        }
                    }
                    tong += tong1HD;

                    khoangTG = DateTime.Today.Subtract(hd.Ngaylap);
                    if (khoangNgannhat == 0)
                    {
                        khoangNgannhat = khoangTG.Days;
                        Tam = hd.Ngaylap;
                    }
                    else
                    {
                        if (khoangNgannhat > khoangTG.Days)
                        {
                            khoangNgannhat = khoangTG.Days;
                            Tam = hd.Ngaylap;
                        }
                    }
                }

                lbTongthanhtoan.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tong) + " vnđ";
                lbNgayHDGN.Text = Tam.ToShortDateString();
            }
            else
            {
                lblTongsoHD.Text = "0";
                lbTongthanhtoan.Text = "0";
                lbNgayHDGN.Text = "##/##/####";
            }

            if (btnCapnhatKH.Text == "Hủy")
            {
                for (int i = 0; i < grdThongtinKH.CurrentRow.Cells.Count; i++)
                {
                    if (grdThongtinKH.CurrentRow.Cells[i].Value != null)
                    {
                        switch (i)
                        {
                            case 1:
                                txtTenKH.Text = grdThongtinKH.CurrentRow.Cells[1].Value.ToString();
                                break;
                            case 2:
                                txtSDT.Text = grdThongtinKH.CurrentRow.Cells[2].Value.ToString();
                                break;
                            case 3:
                                cboGioitinh.Text = grdThongtinKH.CurrentRow.Cells[3].Value.ToString();
                                break;
                            case 4:
                                txtDiachiKH.Text = grdThongtinKH.CurrentRow.Cells[4].Value.ToString();
                                break;
                            case 5:
                                txtEmailKH.Text = grdThongtinKH.CurrentRow.Cells[5].Value.ToString();
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 1:
                                txtTenKH.Text = "";
                                break;
                            case 2:
                                txtSDT.Text = "";
                                break;
                            case 3:
                                cboGioitinh.ResetText();
                                break;
                            case 4:
                                txtDiachiKH.ResetText();
                                break;
                            case 5:
                                txtEmailKH.ResetText();
                                break;
                        }
                    }
                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void btnTimkiemKH_Click(object sender, EventArgs e)
        {
            var dsKH = new List<KhachHang>();
            if (txtThongtinTKKH.Text == "")
            {
                //MessageBox.Show("Vui lòng nhập thông tin tìm kiếm !");
                dsKH = ctrlKH.getTatcaKhachhang();
            }
            else
            {
                dsKH = ctrlKH.Timkiem(txtThongtinTKKH.Text);
            }

            LoadRecordSP(dsKH, pageNumber, numberRecord);
            int tongsotrang = (dsKH.Count / numberRecord) + 1;
            if (dsKH.Count % numberRecord == 0)
            {
                tongsotrang = (dsKH.Count / numberRecord);
            }
            
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            dsTam = dsKH;
        }

        void LoadDanhsachTTKH(List<KhachHang> ds)
        {
            grdThongtinKH.Rows.Clear();

            grdThongtinKH.AllowUserToAddRows = true;

            foreach (var kh in ds)
            {
                grdThongtinKH.Rows.Add(ConvertKhachHangtoGridViewRow(kh));
            }

            grdThongtinKH.AllowUserToAddRows = false;
        }
        void DisableTTKH()
        {
            txtTenKH.Enabled = false;
            cboGioitinh.Enabled = false;
            txtEmailKH.Enabled = false;
            txtDiachiKH.Enabled = false;
            txtSDT.Enabled = false;
        }

        void EnableTTKH()
        {
            txtTenKH.Enabled = true;
            cboGioitinh.Enabled = true;
            txtEmailKH.Enabled = true;
            txtDiachiKH.Enabled = true;
            txtSDT.Enabled = true;

        }
        DataGridViewRow ConvertKhachHangtoGridViewRow(KhachHang kh)
        {
            DataGridViewRow row = (DataGridViewRow)grdThongtinKH.Rows[0].Clone();
            row.Cells[0].Value = kh.MaKH.ToString();
            row.Cells[1].Value = kh.Hovaten.ToString();

            if(kh.SDT != null)
                row.Cells[2].Value = kh.SDT.ToString();
            if(kh.Gioitinh != null)
                row.Cells[3].Value = kh.Gioitinh.ToString();
            if(kh.Email != null)
                row.Cells[4].Value = kh.Email.ToString();
            if (kh.Diachi != null)
                row.Cells[5].Value = kh.Diachi.ToString();

            return row;
        }

        //Người thực hiện: Thanh Đức
        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                string makh = grdThongtinKH.CurrentRow.Cells[0].Value.ToString();
                KhachHang KH = ctrlKH.geTKhachhang(makh);
                KH.Hovaten = txtTenKH.Text;
                KH.SDT = txtSDT.Text;
                KH.Gioitinh = cboGioitinh.Text;
                KH.Diachi = txtDiachiKH.Text;
                KH.Email = txtEmailKH.Text;

                ctrlKH.CapnhatKhachhang(KH);

                var dsKHHT = ctrlKH.getTatcaKhachhang();
                dsTam = dsKHHT;
                LoadRecordSP(dsKHHT, pageNumber, numberRecord);

                if (MessageBox.Show("Cập nhật thông tin khách hàng thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    DisableTTKH();
                    btnCapnhatKH.Text = "Cập nhật";
                    btnLuuKH.Enabled = false;
                }
            }           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadRecordSP(List<KhachHang> ds, int page, int recordNum)
        {
            List<KhachHang> dsKH = new List<KhachHang>();

            dsKH = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTKH(dsKH);
        }

        //Người thực hiện: Thanh Đức - Văn Khải==========================
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumber--;

                if (dsTam.Count % numberRecord == 0)
                {
                    tongsotrang = (dsTam.Count / numberRecord);
                }
                else
                {
                    tongsotrang = (dsTam.Count / numberRecord) + 1;
                }

                txtSoTrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordSP(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(dsTam != null)
            {
                if (pageNumber == 1 && dsTam.Count == numberRecord)
                {
                    return;
                }
                if (pageNumber - 1 < dsTam.Count / numberRecord)
                {
                    pageNumber++;
                    int TongSotrang = (dsTam.Count / numberRecord) + 1;
                    if (dsTam.Count % numberRecord == 0)
                    {
                        TongSotrang = (dsTam.Count / numberRecord);
                    }
                    txtSoTrang.Text = pageNumber.ToString() + " / " + TongSotrang;
                    LoadRecordSP(dsTam, pageNumber, numberRecord);
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if(dsTam != null)
            {
                int tongsotrang = (dsTam.Count / numberRecord) + 1;
                txtSoTrang.Text = 1 + " / " + tongsotrang;
                LoadRecordSP(dsTam, 1, numberRecord);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (dsTam != null)
            {
                int tongsotrang = (dsTam.Count / numberRecord) + 1;
                txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
                LoadRecordSP(dsTam, tongsotrang, numberRecord);
            }
        }
        //===============================================================

        //Người thực hiện: Thanh Đức ====================================================
        private void tbTenKH_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "[a-zA-ZÀ-Ỵà-ỵ]+(([',. -][a-zA-ZÀ-Ỵà-ỵ ])?[a-zA-ZÀ-Ỵà-ỵ ]*)*$";

            if (Regex.IsMatch(txtTenKH.Text, pattern))
            {
                e.Cancel = false;
                errTenKH.SetError(this.txtTenKH, null);
            }
            else
            {
                e.Cancel = true;
                txtTenKH.Focus();
                errTenKH.SetError(this.txtTenKH, "Tên khách hàng phải là chuỗi chữ cái và không chứa chữ số !");

            }
        }

        private void tbSDT_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "^[0-9]{5,10}$";

            if (Regex.IsMatch(txtSDT.Text, pattern))
            {
                e.Cancel = false;
                errSDT.SetError(this.txtSDT, null);
            }
            else
            {
                e.Cancel = true;
                txtSDT.Focus();
                errSDT.SetError(this.txtSDT, "Số điện thoại phải là chuỗi chữ số và không nhập chứa chữ cái !");
            }
        }

        private void tbEmailKH_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "^[A-Za-z][A-Za-z0-9]{5,32}@[a-z0-9]{2,}(\\.[a-z0-9]{2,4}){1,2}$";

            if (Regex.IsMatch(txtEmailKH.Text, pattern))
            {
                e.Cancel = false;
                errSDT.SetError(this.txtEmailKH, null);
            }
            else
            {
                e.Cancel = true;
                txtEmailKH.Focus();
                errEmail.SetError(this.txtEmailKH, "Email phải là kiểu định dạng Email (abc@xyz.com)");
            }
        }

        private void FormThongTinKhachHang_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void FormThongTinKhachHang_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnTimkiemKH.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnXoarong.PerformClick(); }
            { if (e.KeyCode == Keys.F3) btnLuuKH.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnCapnhatKH.PerformClick(); }
        }
        //===============================================================================
    }
}
