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
using System.Text.RegularExpressions;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongTinNhanVien : Form
    {
        NhanVienController ctrlNV = new NhanVienController();

        int pageNumber = 1, numberRecord = 4;
        List<NhanVien> dsTam;

        //Người thực hiện: Thanh Đức
        public FormThongTinNhanVien()
        {
            InitializeComponent();

            btnXoa.Enabled = false;
            DisableTTNV();

            cboGioitinh.Items.Add("Nam");
            cboGioitinh.Items.Add("Nữ");
            cboGioitinh.Items.Add("Khác");

            cboChucvu.Items.Add("Nhân viên quản lý");
            cboChucvu.Items.Add("Nhân viên bán hàng");

        }
        void LoadDanhsachTTNV(List<NhanVien> ds)
        {
            dgvThongtinNV.Rows.Clear();
            dgvThongtinNV.AllowUserToAddRows = true;

            foreach (var nv in ds)
            {
                dgvThongtinNV.Rows.Add(ConvertNhanVientoGridViewRow(nv));
            }

            dgvThongtinNV.AllowUserToAddRows = false;
        }
        void DisableTTNV()
        {
            cboChucvu.Enabled = false;
            txtDiachi.Enabled = false;
            txtEmail.Enabled = false;
            nudLuongtheogio.Enabled = false;
            //txtmaNV.Enabled = false;
            dpNgaysinh.Enabled = false;
            //.Enabled = false;
            txtSDT.Enabled = false;
            txtTenNV.Enabled = false;
            cboGioitinh.Enabled = false;

        }

        void EnableTTNV()
        {
            cboChucvu.Enabled = true;
            txtDiachi.Enabled = true;
            txtEmail.Enabled = true;
            nudLuongtheogio.Enabled = true;
            //txtmaNV.Enabled = true;
            dpNgaysinh.Enabled = true;
            nudPhucap.Enabled = true;
            txtSDT.Enabled = true;
            txtTenNV.Enabled = true;
            cboGioitinh.Enabled = true;

        }
        DataGridViewRow ConvertNhanVientoGridViewRow(NhanVien nv)
        {
            DataGridViewRow row = (DataGridViewRow)dgvThongtinNV.Rows[0].Clone();
            row.Cells[0].Value = nv.MaNV.ToString();
            row.Cells[1].Value = nv.Hovaten.ToString();
            row.Cells[2].Value = nv.Ngaysinh.ToShortDateString();
            row.Cells[3].Value = nv.Chucvu.ToString();
            row.Cells[4].Value = nv.SDT.ToString();
            row.Cells[5].Value = nv.Diachi.ToString();
            row.Cells[6].Value = nv.Email.ToString();
            row.Cells[7].Value = nv.Gioitinh.ToString();
            row.Cells[8].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", nv.Phucap);
            row.Cells[9].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", nv.Luongtheogio);
            return row;
        }

        //Người thực hiện: Thanh Đức
        private void dgvThongtinNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(btnCapnhatNV.Text == "Hủy")
            {
                for (int i = 0; i < dgvThongtinNV.CurrentRow.Cells.Count; i++)
                {
                    decimal luong = 0, phucap = 0;

                    decimal.TryParse(dgvThongtinNV.CurrentRow.Cells[8].Value.ToString(), out phucap);
                    decimal.TryParse(dgvThongtinNV.CurrentRow.Cells[9].Value.ToString(), out luong);

                    if (dgvThongtinNV.CurrentRow.Cells[i].Value.ToString() != "")
                    {
                        switch (i)
                        {
                            case 1:
                                txtTenNV.Text = dgvThongtinNV.CurrentRow.Cells[1].Value.ToString();
                                break;
                            case 2:
                                dpNgaysinh.Value = DateTime.Parse(dgvThongtinNV.CurrentRow.Cells[2].Value.ToString());
                                break;
                            case 3:
                                cboChucvu.Text = dgvThongtinNV.CurrentRow.Cells[3].Value.ToString();
                                break;
                            case 4:
                                txtSDT.Text = dgvThongtinNV.CurrentRow.Cells[4].Value.ToString();
                                break;
                            case 5:
                                txtDiachi.Text = dgvThongtinNV.CurrentRow.Cells[5].Value.ToString();
                                break;
                            case 6:
                                txtEmail.Text = dgvThongtinNV.CurrentRow.Cells[6].Value.ToString();
                                break;
                            case 7:
                                cboGioitinh.Text = dgvThongtinNV.CurrentRow.Cells[7].Value.ToString();
                                break;
                            case 8:
                                nudPhucap.Value = phucap;
                                break;
                            case 9:
                                nudLuongtheogio.Value = luong;
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 1:
                                txtTenNV.Text = "";
                                break;
                            case 2:
                                dpNgaysinh.ResetText();
                                break;
                            case 3:
                                cboChucvu.ResetText();
                                break;
                            case 4:
                                txtSDT.Text = "";
                                break;
                            case 5:
                                txtDiachi.ResetText();
                                break;
                            case 6:
                                txtEmail.ResetText();
                                break;
                            case 7:
                                cboGioitinh.ResetText();
                                break;
                            case 8:
                                nudPhucap.ResetText();
                                break;
                            case 9:
                                nudLuongtheogio.ResetText();
                                break;
                        }
                    }
                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void btnTimkiemNV_Click(object sender, EventArgs e)
        {
            var dsNhanvien = new List<NhanVien>();
            if (txtTimkiem.Text == "")
            {
                //MessageBox.Show("Vui lòng nhập thông tin tìm kiếm !");
                dsNhanvien = ctrlNV.getTatcaNhanvien();
            }
            else
            {
                dsNhanvien = ctrlNV.Timkiem(txtTimkiem.Text);
            }

            dsTam = dsNhanvien.ToList();
            LoadRecordNV(dsNhanvien, pageNumber, numberRecord);
        }

        //Người thực hiện: Thanh Đức
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (btnThemNV.Text == "Thêm (F3)")
            {
                btnCapnhatNV.Text = "Lưu";
                btnThemNV.Text = "Hủy";
                btnXoa.Enabled = true;
                XoaRongThongtinSP();
                EnableTTNV();
            }
            else if (btnThemNV.Text == "Hủy")
            {
                btnCapnhatNV.Text = "Cập nhật (F4)";
                btnThemNV.Text = "Thêm (F3)";
                btnXoa.Enabled = false;
                XoaRongThongtinSP();
                DisableTTNV();
            }
            else
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    string manv = dgvThongtinNV.CurrentRow.Cells[0].Value.ToString();
                    NhanVien nv = ctrlNV.getNhanVien(manv);
                    nv.Hovaten = txtTenNV.Text;
                    nv.Ngaysinh = dpNgaysinh.Value;
                    nv.Email = txtEmail.Text;
                    nv.Gioitinh = cboGioitinh.Text;
                    nv.Diachi = txtDiachi.Text;
                    nv.Phucap = float.Parse(nudPhucap.Text.ToString());
                    nv.Luongtheogio = float.Parse(nudLuongtheogio.Value.ToString());
                    nv.SDT = txtSDT.Text;
                    nv.Chucvu = cboChucvu.Text;

                    ctrlNV.CapnhatNhanvien(nv);

                    var dsNhanVien = ctrlNV.getTatcaNhanvien();
                    LoadRecordNV(dsNhanVien, pageNumber, numberRecord);
                    dsTam = dsNhanVien.ToList();

                    if (MessageBox.Show("Cập nhật thông tin sản phẩm thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        DisableTTNV();
                        btnCapnhatNV.Text = "Cập nhật (F4)";
                        btnThemNV.Enabled = false;
                    }
                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void btnCapnhatNV_Click(object sender, EventArgs e)
        {
            if (btnCapnhatNV.Text == "Cập nhật (F4)")
            {
                EnableTTNV();
                btnCapnhatNV.Text = "Hủy";
                btnThemNV.Text = "Lưu";
                btnXoa.Enabled = true;
            }
            else if (btnCapnhatNV.Text == "Hủy")
            {
                DisableTTNV();
                btnCapnhatNV.Text = "Cập nhật (F4)";
                btnThemNV.Text = "Thêm (F3)";
                btnXoa.Enabled = false;
                errTenNV.SetError(txtTenNV, null);
                XoaRongThongtinSP();
            }
            else if (btnCapnhatNV.Text == "Lưu")
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    NhanVien nv = new NhanVien();
                    nv.Hovaten = txtTenNV.Text;
                    nv.Ngaysinh = dpNgaysinh.Value;
                    nv.MaNV = Helpers.RandomID(txtTenNV.Text);
                    nv.Email = txtEmail.Text;
                    nv.Gioitinh = cboGioitinh.Text;
                    nv.Diachi = txtDiachi.Text;
                    nv.Phucap = float.Parse(nudPhucap.Text.ToString());
                    nv.Luongtheogio = float.Parse(nudLuongtheogio.Value.ToString());
                    nv.SDT = txtSDT.Text;
                    nv.Chucvu = cboChucvu.Text;

                    ctrlNV.LuuNhanvien(nv);

                    MessageBox.Show("Thêm thông tin nhà cung cấp thành công !", "Thông báo");

                    DisableTTNV();
                    btnThemNV.Text = "Thêm (F3)";
                }
            }

        }

        //Người thực hiện: Văn Khải
        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaRongThongtinSP();
        }
    
        private void dgvThongtinNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        void LoadRecordNV(List<NhanVien> ds, int page, int recordNum)
        {
            List<NhanVien> dsNV = new List<NhanVien>();

            dsNV = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTNV(dsNV);
        }

        void XoaRongThongtinSP()
        {
            txtTenNV.Clear();
            dpNgaysinh.ResetText();
            cboChucvu.ResetText();
            //txtmaNV.ResetText();
            txtSDT.Clear();
            txtDiachi.ResetText();
            txtEmail.ResetText();
            cboGioitinh.ResetText();
            nudPhucap.ResetText();
            nudLuongtheogio.ResetText();
        }

        //Người thực hiện: Thanh Đức ==============================================
        private void txtTenNV_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "[a-zA-ZÀ-Ỵà-ỵ]+(([',. -][a-zA-ZÀ-Ỵà-ỵ ])?[a-zA-ZÀ-Ỵà-ỵ ]*)*$";

            if (Regex.IsMatch(txtTenNV.Text, pattern))
            {
                e.Cancel = false;
                errTenNV.SetError(this.txtTenNV, null);
            }
            else
            {
                e.Cancel = true;
                txtTenNV.Focus();
                errTenNV.SetError(this.txtTenNV, "Tên nhân viên phải là chuỗi chữ cái và không chứa chữ số !");

            }
        }

        private void txtSDT_Validating(object sender, CancelEventArgs e)
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
                errSDT.SetError(this.txtSDT, "Số điện thoại phải là chuỗi chữ số và không chứa chữ cái !");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "^[A-Za-z][A-Za-z0-9]{5,32}@[a-z0-9]{2,}(\\.[a-z0-9]{2,4}){1,2}$";

            if (Regex.IsMatch(txtEmail.Text, pattern))
            {
                e.Cancel = false;
                errEmail.SetError(this.txtEmail, null);
            }
            else
            {
                e.Cancel = true;
                txtEmail.Focus();
                errEmail.SetError(this.txtEmail, "Email phải là kiểu định dạng Email (abc@xyz.com)");

            }
        }

        private void txtDiachi_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "[a-zA-Z0-9À-Ỵà-ỵ]+(([',. -][a-zA-Z0-9À-Ỵà-ỵ ])?[a-zA-Z0-9À-Ỵà-ỵ ]*)*$";

            if (Regex.IsMatch(txtDiachi.Text, pattern))
            {
                e.Cancel = false;
                errDiachi.SetError(this.txtDiachi, null);
            }
            else
            {
                e.Cancel = true;
                txtDiachi.Focus();
                errDiachi.SetError(this.txtDiachi, "Địa chỉ không hợp lệ ");
            }
        }
        //=========================================================================

        //Người thực hiện: Văn Khải ============================================
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
                LoadRecordNV(dsTam, pageNumber, numberRecord);
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
                    LoadRecordNV(dsTam, pageNumber, numberRecord);
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if(dsTam != null)
            {
                int tongsotrang = (dsTam.Count / numberRecord) + 1;
                txtSoTrang.Text = 1 + " / " + tongsotrang;
                LoadRecordNV(dsTam, 1, numberRecord);
            }
        }

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (dsTam != null)
            {
                int tongsotrang = (dsTam.Count / numberRecord) + 1;
                txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
                LoadRecordNV(dsTam, tongsotrang, numberRecord);
            }
        }
        //======================================================================
    }
}
