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
    public partial class FormThayDoiMatKhau : Form
    {
        TaiKhoanController ctrlTK = new TaiKhoanController();
        TaiKhoan taikhoan;

        public FormThayDoiMatKhau()
        {
            InitializeComponent();
        }

        public FormThayDoiMatKhau(int matk)
        {
            InitializeComponent();
            this.taikhoan = ctrlTK.getTaiKhoanByMaTK(matk);
        }

        private void tbMatkhauHientai_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatkhauHientai.Text))
            {
                e.Cancel = true;
                txtMatkhauHientai.Focus();
                errMatkhauHientai.SetError(txtMatkhauHientai, "Vui lòng nhập mật khẩu cũ !");
            }
            else
            {
                if (ctrlTK.Dangnhap(taikhoan.Tentaikhoan, txtMatkhauHientai.Text.Trim()) != null)
                {
                    e.Cancel = false;
                    errMatkhauHientai.SetError(txtMatkhauHientai, null);
                }
                else
                {
                    e.Cancel = true;
                    txtMatkhauHientai.Focus();
                    errMatkhauHientai.SetError(txtMatkhauHientai, "Mật khẩu hiện tại không đúng !");
                }
            }
        }

        private void tbMatkhauMoi_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatkhauMoi.Text))
            {
                e.Cancel = true;
                txtMatkhauMoi.Focus();
                errMatkhauMoi.SetError(txtMatkhauMoi, "Vui lòng nhập mật khẩu mới !");
            }
            else
            {
                if (ctrlTK.Dangnhap(taikhoan.Tentaikhoan, txtMatkhauHientai.Text.Trim()) != null)
                {
                    e.Cancel = false;
                    errMatkhauMoi.SetError(txtMatkhauHientai, null);
                }
                else
                {
                    e.Cancel = true;
                    txtMatkhauMoi.Focus();
                    errMatkhauMoi.SetError(txtMatkhauHientai, "Vui lòng nhập mật khẩu mới !");
                }
            }
        }

        private void tbXacnhan_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtXacnhan.Text))
            {
                e.Cancel = true;
                txtXacnhan.Focus();
                errXacnhanMatkhau.SetError(txtXacnhan, "Vui lòng nhập xác nhận mật khẩu !");
            }
            else
            {
                if (txtMatkhauMoi.Text == txtXacnhan.Text)
                {
                    e.Cancel = false;
                    errXacnhanMatkhau.SetError(txtXacnhan, null);
                }
                else
                {
                    e.Cancel = true;
                    txtXacnhan.Focus();
                    errXacnhanMatkhau.SetError(txtXacnhan, "Xác nhận mật khẩu phải khớp với mật khẩu mới !");
                }
            }
        }

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuuThaydoi_Click(object sender, EventArgs e)
        {

        }
    }
}
