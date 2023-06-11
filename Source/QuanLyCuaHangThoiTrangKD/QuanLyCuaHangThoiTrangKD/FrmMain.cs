using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Forms.Function;
using QuanLyCuaHangThoiTrangKD.Forms;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Common;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;

namespace QuanLyCuaHangThoiTrangKD
{
    public partial class FrmMain : Form
    {
        TaiKhoanController ctrlTK = new TaiKhoanController();
        ThongKeController ctrlThongke = new ThongKeController();
        TaiKhoan taikhoan;
        List<string> dsSPHethang;

        public FrmMain()
        {
            InitializeComponent();
            customizeDesing();
        }

        //Người thực hiện: Thanh Đức
        public FrmMain(int matk)
        {
            InitializeComponent();
            customizeDesing();
            this.taikhoan = ctrlTK.getTaiKhoanByMaTK(matk);

            NhanVien nv = ctrlTK.getNhanVien(taikhoan.MaNV);
            if (nv != null)
            {
                lblTenNV.Text = nv.Hovaten;
                lblChucvu.Text = nv.Chucvu;
            }

            List<CTByMaSP> dsCTHD = ctrlThongke.LoadDanhsachCTHDByMaSP();
            List<CTByMaSP> dsCTPN = ctrlThongke.LoadDanhsachCTPNByMaSP();
            List<SanPham> dsSPHT = ctrlThongke.getTatcaSanpham();
            //List<ItemGridSP_SLTon> dsSPHethang = new List<ItemGridSP_SLTon>();
            dsSPHethang = new List<string>();

            foreach (var sp in dsSPHT)
            {
                int slTon = 0;
                CTByMaSP cthd = dsCTHD.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                CTByMaSP ctpn = dsCTPN.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                if (cthd != null && ctpn != null)
                {
                    slTon = ctpn.TongSL - cthd.TongSL;
                    if(slTon == 0)
                    {
                        sp.Tinhtrang = "Hết hàng";
                        dsSPHethang.Add(sp.MaSP);
                    }
                    else
                    {
                        if(slTon < 5)
                        {
                            dsSPHethang.Add(sp.MaSP);
                        }
                        sp.Tinhtrang = "Còn hàng";
                    }

                }
            }
            ctrlThongke.CapnhatSanpham();

        }

        //Người thực hiện: Thanh Đức
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (dsSPHethang.Count > 0)
            {
                FormDanhsachSPHethang frm = new FormDanhsachSPHethang(dsSPHethang, taikhoan.MaTK);
                frm.ShowDialog();
            }
        }

        private void customizeDesing()
        {
            //btnThongtinKH.Visible = false;
            //panelNhanvien.Visible = false;
            panelHoadon.Visible = false;
            panelSanpham.Visible = false;
            panelTaikhoan.Visible = false;
            panelThongke.Visible = false;
        }
        private void hideSubMenu()
        {
         
            //if (panelNhanvien.Visible == true)
            //    panelNhanvien.Visible = false;
            if (panelSanpham.Visible == true)
                panelSanpham.Visible = false;
            if (panelHoadon.Visible == true)
                panelHoadon.Visible = false;
            if (panelTaikhoan.Visible == true)
                panelTaikhoan.Visible = false;
            if (panelThongke.Visible == true)
                panelThongke.Visible = false;

        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private Form activeForm = null;

        //Người thực hiện: Thanh Đức
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelchildForm.Controls.Add(childForm);
            panelchildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        //Người thực hiện: Văn Khải - Thanh Đức ====================================
        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận đăng xuất khỏi ứng dụng ? Tiếp tục", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                FormDangNhap frm = new FormDangNhap();
                frm.ShowDialog();
            }
        }

        private void btnThongtinTK_Click(object sender, EventArgs e)
        {
            if (taikhoan.Loai == "Quản lý")
            {
                openChildForm(new FormThongTinTaiKhoan());
                hideSubMenu();
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được cấp quyền sử dụng chức năng này");
            }
        }

        private void btnDoimatkhau_Click(object sender, EventArgs e)
        {
            //openChildForm(new FormThayDoiMatKhau());
            //hideSubMenu();
            FormThayDoiMatKhau frm = new FormThayDoiMatKhau(taikhoan.MaTK);
            frm.ShowDialog();
        }

        private void btnTaikhoan_Click(object sender, EventArgs e)
        {
            showSubMenu(panelTaikhoan);
        }

        private void btnThongkeHD_Click(object sender, EventArgs e)
        {
            openChildForm(new FormThongKetheoHD(taikhoan.MaTK));
            hideSubMenu();
        }

        private void btnThongkeSP_Click(object sender, EventArgs e)
        {
            openChildForm(new FormThongKetheoSP(taikhoan.MaTK));
            hideSubMenu();
        }

        private void btnSanpham_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSanpham);
        }

        private void btnThongtinSP_Click(object sender, EventArgs e)
        {
            openChildForm(new FormThongTinSanPham());
            hideSubMenu();
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            openChildForm(new FormThongTinKhachHang());
        }

        private void btnThongtinNCC_Click(object sender, EventArgs e)
        {               
            if (taikhoan.Loai == "Quản lý")
            {
                openChildForm(new FormThongTinNCC());
                hideSubMenu();
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được cấp quyền sử dụng chức năng này");
            }
        }

        private void btnLapphieunhap_Click(object sender, EventArgs e)
        {
            if (taikhoan.Loai == "Quản lý")
            {
                openChildForm(new FormLapPhieuNhap(taikhoan.MaTK));
                hideSubMenu();
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được cấp quyền sử dụng chức năng này");
            }
        }

        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            if (taikhoan.Loai == "Quản lý")
            {
                openChildForm(new FormThongTinNhanVien());
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được cấp quyền sử dụng chức năng này");
            }
        }

        private void btnLuongNV_Click(object sender, EventArgs e)
        {
            //openChildForm(new FormLuongNhanVien());
            //hideSubMenu();
        }

        private void btnThongtinNV_Click(object sender, EventArgs e)
        {
            //openChildForm(new FormThongTinNhanVien());
            //hideSubMenu();
        }

        private void btnHoadon_Click(object sender, EventArgs e)
        {
            showSubMenu(panelHoadon);
        }

        private void btnLaphoadon_Click(object sender, EventArgs e)
        {
            openChildForm(new FormLapHoaDon(taikhoan.MaTK));
            hideSubMenu();
        }

        private void btnCapnhatHD_Click(object sender, EventArgs e)
        {
            openChildForm(new FormCapNhatHoaDon(taikhoan.MaTK));
            hideSubMenu();
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            if (taikhoan.Loai == "Quản lý")
            {
                showSubMenu(panelThongke);
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được cấp quyền sử dụng chức năng này");
            }
        }

        private void btnDanhmucsanpham_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDanhMucSanPham());
            hideSubMenu();
        }

        private void btnThongtinPhieunhap_Click(object sender, EventArgs e)
        {
            if (taikhoan.Loai == "Quản lý")
            {
                openChildForm(new FormCapNhatPhieuNhap(taikhoan.MaTK));
                hideSubMenu();
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được cấp quyền sử dụng chức năng này");
            }
        }
        //============================================================================

    }
}
