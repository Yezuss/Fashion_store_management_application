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
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongTinTaiKhoan : Form
    {
        TaiKhoanController ctrlTK = new TaiKhoanController();
        TaiKhoan taikhoan;
        //string MaNVTam = "";

        int pageNumber = 1, numberRecord = 8;
        List<TaiKhoan> dsTam;

        //Người thực hiện: Văn Khải
        public FormThongTinTaiKhoan()
        {
            InitializeComponent();

            var dsTK = ctrlTK.getTatcaTaikhoan();

            lblNV.Visible = false;
            shaNV.Visible = false;
            cboNhanVien.Visible = false;

            txtMatkhauTK.Enabled = false;
            txtTenTK.Enabled = false;
            cboLoaiTK.Enabled = false;

            if (txtTenTK.Text == "")
            {
                btnDatlaimatkhau.Enabled = false;
            }

            cboLoaiTK.Items.Add("Quản lý");
            cboLoaiTK.Items.Add("Nhân viên");

            cboNhanVien.Enabled = false;

            dsTam = dsTK.ToList();
            LoadRecordTK(dsTK, pageNumber, numberRecord);
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            if (dsTam.Count % numberRecord == 0)
            {
                tongsotrang = (dsTam.Count / numberRecord);
            }

            txtSoTrang.Text = 1 + " / " + tongsotrang; 
        }

        //Người thực hiện: Văn Khải
        private void dgvThongtinTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnCapnhatTK.Text == "Hủy")
            {
                for (int i = 0; i < grdThongtinTK.CurrentRow.Cells.Count; i++)
                {
                    if (grdThongtinTK.CurrentRow.Cells[i].Value != null)
                    {
                        switch (i)
                        {
                            case 0:
                                txtTenTK.Text = grdThongtinTK.CurrentRow.Cells[0].Value.ToString();
                                break;
                            case 1:
                                txtMatkhauTK.Text = grdThongtinTK.CurrentRow.Cells[1].Value.ToString();
                                break;
                            case 3:
                                cboLoaiTK.Text = grdThongtinTK.CurrentRow.Cells[3].Value.ToString();
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                txtTenTK.Text = "";
                                break;
                            case 1:
                                txtMatkhauTK.Text = "";
                                break;
                            case 3:
                                cboLoaiTK.ResetText();
                                break;
                        }
                    }
                }
            }
        }

        void LoadRecordTK(List<TaiKhoan> ds, int page, int recordNum)
        {
            List<TaiKhoan> dsTK = new List<TaiKhoan>();

            dsTK = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTTK(dsTK);
        }

        void LoadDanhsachTTTK(List<TaiKhoan> ds)
        {
            grdThongtinTK.Rows.Clear();

            grdThongtinTK.AllowUserToAddRows = true;

            foreach (var kh in ds)
            {
                grdThongtinTK.Rows.Add(ConvertTaiKhoantoGridViewRow(kh));
            }

            grdThongtinTK.AllowUserToAddRows = false;
        }

        DataGridViewRow ConvertTaiKhoantoGridViewRow(TaiKhoan tk)
        {
            string hideMatkhau = "";

            for (int i = 0; i < tk.Matkhau.Length; i++)
            {
                hideMatkhau += "*";
            }
            DataGridViewRow row = new DataGridViewRow();

            try
            {
                row = (DataGridViewRow)grdThongtinTK.Rows[0].Clone();
                row.Cells[0].Value = tk.Tentaikhoan.ToString();
                row.Cells[1].Value = hideMatkhau;

                NhanVien nv = ctrlTK.getNhanVien(tk.MaNV);
                row.Cells[2].Value = nv.Hovaten.ToString();

                row.Cells[3].Value = tk.Loai.ToString();
            }
            catch
            {

            }
            return row;
        }

        //Người thực hiện: Văn Khải
        private void btnThemTK_Click(object sender, EventArgs e)
        {
            if (btnThemTK.Text == "Thêm (F1)")
            {
                btnCapnhatTK.Text = "Lưu (F1)";
                btnThemTK.Text = "Hủy";
                btnDatlaimatkhau.Enabled = false;

                lblNV.Visible = true;
                shaNV.Visible = true;
                cboNhanVien.Visible = true;

                foreach (var nv in ctrlTK.getNhanVienChuaCapTaikhoan())
                {
                    cboNhanVien.Items.Add(nv.Hovaten.ToString());
                }

                txtTenTK.Enabled = true;
                cboLoaiTK.Enabled = true;
                cboNhanVien.Enabled = true;
            }
            else if (btnThemTK.Text == "Hủy")
            {
                btnCapnhatTK.Text = "Cập nhật (F2)";
                btnThemTK.Text = "Thêm (F1)";
                btnDatlaimatkhau.Enabled = true;

                lblNV.Visible = false;
                shaNV.Visible = false;
                cboNhanVien.Visible = false;

                txtTenTK.Enabled = false;
                cboLoaiTK.Enabled = false;
                cboNhanVien.Enabled = false;
            }
            else if (btnThemTK.Text == "Lưu (F1)")
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    string tentk = grdThongtinTK.CurrentRow.Cells[0].Value.ToString();
                    TaiKhoan tk = ctrlTK.getTaiKhoan(tentk);

                    tk.Tentaikhoan = txtTenTK.Text;
                    tk.Loai = cboLoaiTK.Text;

                    ctrlTK.CapnhatTaikhoan(tk);

                    if (MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        txtTenTK.Enabled = false;
                        cboLoaiTK.Enabled = false;

                        btnCapnhatTK.Text = "Cập nhật (F2)";
                        btnThemTK.Text = "Thêm (F1)";
                        btnDatlaimatkhau.Enabled = false;
                        cboNhanVien.Enabled = false;
                        LoadRecordTK(ctrlTK.getTatcaTaikhoan(), pageNumber, numberRecord);
                        dsTam = ctrlTK.getTatcaTaikhoan();
                    }
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void btnCapnhatTK_Click(object sender, EventArgs e)
        {
            if (btnCapnhatTK.Text == "Cập nhật (F2)")
            {
                btnCapnhatTK.Text = "Hủy";
                btnThemTK.Text = "Lưu (F1)";
                btnDatlaimatkhau.Enabled = false;

                txtTenTK.Enabled = true;
                cboLoaiTK.Enabled = true;
            }
            else if (btnCapnhatTK.Text == "Hủy")
            {
                btnCapnhatTK.Text = "Cập nhật (F2)";
                btnThemTK.Text = "Thêm (F1)";
                btnDatlaimatkhau.Enabled = true;

                txtTenTK.Enabled = false;
                cboLoaiTK.Enabled = false;

                txtTenTK.Clear();
                cboLoaiTK.ResetText();
            }
            else if (btnCapnhatTK.Text == "Lưu (F1)")
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    NhanVien nv = ctrlTK.getNhanVien(cboNhanVien.Text);
                    TaiKhoan tk = new TaiKhoan();
                    tk.Tentaikhoan = txtTenTK.Text;
                    tk.Matkhau = "123abc";
                    tk.Loai = cboLoaiTK.Text;
                    tk.NhanVien = nv;

                    ctrlTK.LuuTaikhoan(tk);

                    if(MessageBox.Show("Lưu thông tin tài khoản thành công ! Tiếp tục thêm tài khoản", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        btnCapnhatTK.Text = "Cập nhật (F2)";
                        btnThemTK.Text = "Thêm (F1)";
                        txtTenTK.Enabled = false;
                        cboLoaiTK.Enabled = false;

                        lblNV.Visible = false;
                        shaNV.Visible = false;
                        cboNhanVien.Visible = false;
                    }
                    LoadRecordTK(ctrlTK.getTatcaTaikhoan(), pageNumber, numberRecord);
                    dsTam = ctrlTK.getTatcaTaikhoan();
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void btnDatlaimatkhau_Click(object sender, EventArgs e)
        {
            string tentk = grdThongtinTK.CurrentRow.Cells[0].Value.ToString();
            TaiKhoan tk = ctrlTK.getTaiKhoan(tentk);
            ctrlTK.Datlaimatkhau(tk);

            MessageBox.Show("Đặt lại mật khẩu mặc định thành công !", "Thông báo");
        }

        //Người thực hiện: Thanh Đức
        private void txtTenTK_TextChange(object sender, EventArgs e)
        {
            if (txtTenTK.Text != "")
            {
                btnDatlaimatkhau.Enabled = true;
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Người thực hiện: Thanh Đức
        private void FormThongTinTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            //{ if (e.KeyCode == Keys.F1) btnTimkiemTK.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnThemTK.PerformClick(); }
            { if (e.KeyCode == Keys.F3) btnCapnhatTK.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnDatlaimatkhau.PerformClick(); }
            //{ if (e.KeyCode == Keys.F5) btnthoat.PerformClick(); }
        }

        //Người thực hiện: Văn Khải ===================================
        private void btnNext_Click(object sender, EventArgs e)
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
                LoadRecordTK(dsTam, pageNumber, numberRecord);
            }
        }

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
                LoadRecordTK(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            LoadRecordTK(dsTam, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordTK(dsTam, tongsotrang, numberRecord);
        }
        //==============================================================

        private void cbNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var dsTK = ctrlTK.getTatcaTaikhoan();
            //if (cbNhanVien.SelectedIndex == 0)
            //{
            //    dsTK = dsTK.Where(x => x.MaNV != "" || x.MaNV != null).ToList();
            //    LoadRecordTK(dsTK, pageNumber, numberRecord);
            //    dsTam = dsTK;
            //}
            //if (cbNhanVien.SelectedIndex == 1)
            //{
            //    dsTK = dsTK.Where(x => x.MaNV == "" || x.MaNV == null).ToList();
            //    dsTam = dsTK;
            //}
        }

        //Người thực hiện: Thanh Đức
        private void FormThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Người thực hiện: Văn Khải
        private void txtTenTK_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenTK.Text))
            {
                e.Cancel = false;
                errTenTK.SetError(this.txtTenTK, null);
            }
            else
            {
                e.Cancel = true;
                txtTenTK.Focus();
                errTenTK.SetError(this.txtTenTK, "Tên tài khoản không được rỗng! Vui lòng nhập lại ");                
            }
        }
    }
}
