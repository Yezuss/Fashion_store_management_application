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
using QuanLyCuaHangThoiTrangKD.Common;
using System.Globalization;
using QuanLyCuaHangThoiTrangKD.Controller;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormChiTietSanPham : Form
    {
        public GuiThongtinSanpham sendThongtin;

        SanPhamController ctrlSP = new SanPhamController();
        SanPham sanpham;

        byte[] tempHinhanh;

        //Người thực hiện: Văn Khải
        public FormChiTietSanPham(GuiThongtinSanpham sender, string tensp)
        {
            InitializeComponent();
            LoadComboboxSP();
            DisableTTSP();
            this.sanpham = ctrlSP.getSanPham(tensp);
            decimal gianhap = 0;

            this.sendThongtin = sender;

            if (sanpham != null)
            {
                txtTenSP.Text = sanpham.TenSP;
                txtChatlieu.Text = sanpham.Chatlieu;
                cboMausac.SelectedItem = sanpham.Mausac;
                cboKichthuoc.SelectedItem = sanpham.Kichthuoc;
                cboLoaiSP.SelectedItem = sanpham.LoaiSP;
                cboDonvi.SelectedItem = sanpham.Donvi;
                if (decimal.TryParse(sanpham.Dongianhap.ToString(), out gianhap))
                {
                    nudDongianhap.Value = gianhap;
                }

                if (sanpham.Hinhanh != null)
                {
                    picHinhanhSP.Image = Helpers.convertByteToImg(Convert.ToBase64String(sanpham.Hinhanh));
                }
                else
                {
                    Bitmap bm = new Bitmap(Application.StartupPath + "\\Resources\\" + "logo_transparent" + ".png");
                    picHinhanhSP.Image = bm;
                }
            }

            btnLuu.Enabled = false;
            btnNhapTT.Enabled = false;
            btnXoarongTTSP.Enabled = false;
            btnChonanhSP.Enabled = false;
        }

        //Người thực hiện: Văn Khải
        public FormChiTietSanPham(GuiThongtinSanpham sender, string Tensp, string Chatlieu, string Mausac, string Kichthuoc, string Loaisp, string Donvi, string Dongianhap, string Dongia, byte[] Hinhanh)
        {
            InitializeComponent();
            //this.sanpham = ctrlSP.getSanPham(tensp);
            LoadComboboxSP();
            DisableTTSP();
            decimal gianhap = 0, giaban = 0;

            this.sendThongtin = sender;

            txtTenSP.Text = Tensp;
            txtChatlieu.Text = Chatlieu;
            cboMausac.SelectedItem = Mausac;
            cboKichthuoc.SelectedItem = Kichthuoc;
            cboLoaiSP.SelectedItem = Loaisp;
            cboDonvi.SelectedItem = Donvi;
            if (decimal.TryParse(Dongianhap, out gianhap))
            {
                nudDongianhap.Value = gianhap;
            }
            if (decimal.TryParse(Dongia, out giaban))
            {
                if(giaban >= 50000)
                {
                    nudDongiaban.Value = giaban;
                }
            }

            if(Hinhanh != null)
            {
                //picHinhanhSP.Image = Helpers.convertByteToImg(Convert.ToBase64String(SPdangchon.Hinhanh));
                Image hinhanhsp = Helpers.convertByteToImg(Convert.ToBase64String(Hinhanh));
                if (hinhanhsp  != null)
                {
                    picHinhanhSP.Image = hinhanhsp;
                }
            }
            else
            {
                Bitmap bm = new Bitmap(Application.StartupPath + "\\Resources\\" + "logo_transparent" + ".png");
                picHinhanhSP.Image = bm;
            }

            btnLuu.Enabled = false;
            btnXoarongTTSP.Enabled = false;
            btnChonanhSP.Enabled = false;
        }

        private void brnLuu_Click(object sender, EventArgs e)
        {           
        
        }

        void LoadComboboxSP()
        {

            cboKichthuoc.Items.Add("S");
            cboKichthuoc.Items.Add("M");
            cboKichthuoc.Items.Add("L");
            cboKichthuoc.Items.Add("XL");
            cboKichthuoc.Items.Add("XXL");

            cboDonvi.Items.Add("Đôi");
            cboDonvi.Items.Add("Chiếc");
            cboDonvi.Items.Add("Bộ");

            cboLoaiSP.Items.Add("Quần");
            cboLoaiSP.Items.Add("Áo");
            cboLoaiSP.Items.Add("Áo khoát");
            cboLoaiSP.Items.Add("Đặc biệt");
            cboLoaiSP.Items.Add("Phụ kiện");
            cboLoaiSP.Items.Add("Quần ngắn");

            cboMausac.Items.Add("Cam");
            cboMausac.Items.Add("Trắng");
            cboMausac.Items.Add("Đen");
            cboMausac.Items.Add("Vàng");
            cboMausac.Items.Add("Trắng xám");
            cboMausac.Items.Add("Xanh xám");
            cboMausac.Items.Add("Xanh dương");
            cboMausac.Items.Add("Hồng");
            cboMausac.Items.Add("Xanh lá");
            cboMausac.Items.Add("Nâu");
            cboMausac.Items.Add("Xanh rêu");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Người thực hiện: Văn Khải
        private void btnXoarongTTSP_Click(object sender, EventArgs e)
        {
            txtTenSP.ResetText();
            txtChatlieu.ResetText();
            cboMausac.SelectedItem = "";
            cboKichthuoc.SelectedItem = "";
            cboDonvi.SelectedItem = "";
            cboLoaiSP.SelectedItem = "";
            nudDongianhap.Value = 50000;
            nudDongiaban.Value = 50000;
            //nudSoluong.Value = 1;
        }

        void DisableTTSP()
        {
            txtTenSP.Enabled = false;
            txtChatlieu.Enabled = false;
            cboMausac.Enabled = false;
            cboKichthuoc.Enabled = false;
            cboLoaiSP.Enabled = false;
            cboDonvi.Enabled = false;
            nudDongianhap.Enabled = false;
            nudDongiaban.Enabled = false;
            //btnChonanhSP.Enabled = false;
        }

        void EnableTTSP()
        {
            txtTenSP.Enabled = true;
            txtChatlieu.Enabled = true;
            cboMausac.Enabled = true;
            cboKichthuoc.Enabled = true;
            cboLoaiSP.Enabled = true;
            cboDonvi.Enabled = true;
            nudDongianhap.Enabled = true;
            btnChonanhSP.Enabled = true;
            nudDongiaban.Enabled = true;
        }

        //Người thực hiện: Văn Khải
        private void btnNhapTT_Click(object sender, EventArgs e)
        {
            if (btnNhapTT.Text == "Nhập thông tin (F3)")
            {
                btnNhapTT.Text = "Hủy";
                EnableTTSP();
                btnXoarongTTSP.Enabled = true;
                btnLuu.Enabled = true;
                btnChonanhSP.Enabled = true;
            }
            else if (btnNhapTT.Text == "Hủy")
            {
                btnNhapTT.Text = "Nhập thông tin (F3)";
                DisableTTSP();
                btnXoarongTTSP.Enabled = false;
                btnLuu.Enabled = false;
                btnChonanhSP.Enabled = false;
            }
        }

        //Người thực hiện: Văn Khải
        private void btnChonanhSP_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                tempHinhanh = Helpers.converImgToByte(openFile.FileName);
                picHinhanhSP.Image = new Bitmap(openFile.FileName);
            }
        }

        //Người thực hiện: Văn Khải
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (sendThongtin != null)
                {
                    string mausac = "", kichthuoc = "", loai = "", donvi = "";

                    if (cboMausac.SelectedItem != null)
                    {
                        mausac = cboMausac.SelectedItem.ToString();
                    }
                    if (cboKichthuoc.SelectedItem != null)
                    {
                        kichthuoc = cboKichthuoc.SelectedItem.ToString();
                    }
                    if (cboLoaiSP.SelectedItem != null)
                    {
                        loai = cboLoaiSP.SelectedItem.ToString();
                    }
                    if (cboDonvi.SelectedItem != null)
                    {
                        donvi = cboDonvi.SelectedItem.ToString();
                    }

                    if (tempHinhanh != null)
                    {
                        this.sendThongtin(txtTenSP.Text, txtChatlieu.Text, mausac, kichthuoc, loai, donvi, nudDongianhap.Value.ToString(), nudDongiaban.Value.ToString(), tempHinhanh);
                    }
                    else
                    {
                        this.sendThongtin(txtTenSP.Text, txtChatlieu.Text, mausac, kichthuoc, loai, donvi, nudDongianhap.Value.ToString(), nudDongiaban.Value.ToString(), null);
                    }
                    MessageBox.Show("Lưu thông tin sản phẩm phiếu nhập thành công !", "Thông báo");
                }

                this.Close();
            }
        }

        //Người thực hiện: Thanh Đức
        private void FormChiTietSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnChonanhSP.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnXoarongTTSP.PerformClick(); }
            { if (e.KeyCode == Keys.F3) btnNhapTT.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnLuu.PerformClick(); }
        }

        //Người thực hiện: Thanh Đức
        private void FormChiTietSanPham_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        //Người thực hiện: Thanh Đức
        private void txtTenSP_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenSP.Text))
            {
                e.Cancel = false;
                errTenSP.SetError(this.txtTenSP, null);
            }
            else
            {
                e.Cancel = true;
                txtTenSP.Focus();
                errTenSP.SetError(this.txtTenSP, "Tên sản phẩm không được rỗng!");
            }
        }
    }
}
