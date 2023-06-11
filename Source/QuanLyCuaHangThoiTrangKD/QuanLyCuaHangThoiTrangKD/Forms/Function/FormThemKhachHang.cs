using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;
using QuanLyCuaHangThoiTrangKD.Common;
using QuanLyCuaHangThoiTrangKD.Controller;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThemKhachHang : Form
    {
        public GuiThongtinKhachhang sendThongtin;

        KhachHangController ctrlKH = new KhachHangController();

        //Người thực hiện: Thanh Đức
        public FormThemKhachHang(GuiThongtinKhachhang sender)
        {
            InitializeComponent();
            this.sendThongtin = sender;
        }

        //Người thực hiện: Thanh Đức
        private void btnLuuKhachhang_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            if(!string.IsNullOrEmpty(txtTenKH.Text) && !string.IsNullOrEmpty(txtSDT.Text))
            {
                try
                {
                    kh.MaKH = Helpers.RandomID(txtTenKH.Text);
                    kh.Hovaten = txtTenKH.Text;
                    kh.SDT = txtSDT.Text;
                    kh.Diachi = txtDiachi.Text;

                    ctrlKH.LuuKhachhang(kh);
                    this.sendThongtin(kh.MaKH);
                    MessageBox.Show("Lưu thông tin khách hàng thành công !", "Thông báo");
                    this.Close();
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("Thông tin tên và số điện thoại khách hàng không được rỗng !", "Thông báo");
            }
        }

        //Người thực hiện: Thanh Đức
        private void btnXoarong_Click(object sender, EventArgs e)
        {
            txtTenKH.ResetText();
            txtSDT.ResetText();
            txtDiachi.ResetText();
        }

        //Người thực hiện: Thanh Đức
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
