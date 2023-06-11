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
using QuanLyCuaHangThoiTrangKD.Forms;

namespace QuanLyCuaHangThoiTrangKD.Forms
{
    public partial class FormDangNhap : Form
    {
        TaiKhoanController ctrlTK = new TaiKhoanController();
        public FormDangNhap()
        {
            InitializeComponent();
        }

        //Người thực hiện: Văn Khải
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                TaiKhoan taiKhoan = ctrlTK.Dangnhap(txtUser.Text, txtPassword.Text);

                //if(DateTime.Now.Hour > 9 )
                if (taiKhoan != null)
                {
                    //LichLamViec llv = new LichLamViec()
                    //{
                    //    NhanVien = ctrlTK.getNhanVien(taiKhoan.MaNV),
                    //    Ngaylamviec = DateTime.Today
                    //};

                    //ctrlTK.LuuLichlamviec(llv);

                    FrmMain frm = new FrmMain(taiKhoan.MaTK);
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Tài khoản không hợp lệ ! Vui lòng nhập lại", "Thông báo");
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void txtUser_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                e.Cancel = true;
                txtUser.Focus();
                errorTentaikhoan.SetError(txtUser, "Vui lòng nhập tên tài khoản");
            }
            else
            {
                e.Cancel = false;
                errorTentaikhoan.SetError(txtUser, null);
            }
        }

        //Người thực hiện: Văn Khải
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorMatkhau.SetError(txtPassword, "Vui lòng nhập mật khẩu");
            }
            else
            {
                e.Cancel = false;
                errorMatkhau.SetError(txtPassword, null);
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {

        }

        //Người thực hiện: Văn Khải
        private void chkHienthiMK_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if(chkHienthiMK.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            errorTentaikhoan.Clear();
            errorMatkhau.Clear();
            this.Close();
        }

        private void txtPassword_Validated(object sender, EventArgs e)
        {

        }
    }
}
