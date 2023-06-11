using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;
using QuanLyCuaHangThoiTrangKD.Common;
using QuanLyCuaHangThoiTrangKD.Controller;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Diagnostics;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public delegate void GuiThongtinHoadon(string mahd, DateTime ngaylap, int calamviec);
    public delegate void GuiThongtinKhachhang(string makh);

    public partial class FormLapHoaDon : Form
    {
        TaiKhoan taikhoan;

        HoaDonController ctrlHD = new HoaDonController();

        int pageNumber = 1;
        int numberRecord = 4;

        int pageNumberCT = 1;
        int numberRecordCT = 4;

        private HoaDon hoadonTam = new HoaDon();

        private List<ChiTietHoaDon> dsSPHD = new List<ChiTietHoaDon>();
        private List<SanPham> dsSPHDSess = new List<SanPham>();

        private List<CTByMaSP> dsCTHDbySP = new List<CTByMaSP>();
        private List<CTByMaSP> dsCTPNbySP = new List<CTByMaSP>();

        private List<CTPNByMaSP> dsSP_SLton = new List<CTPNByMaSP>();

        float tongtien = 0;
        Random random = new Random();

        List<SanPham> dsTam = new List<SanPham>();

        public FormLapHoaDon()
        {
            InitializeComponent();

            btnThemvaoHoadon.Enabled = false;
            if (grdThongtinSP.Rows.Count == 1)
            {
                btnThemvaoHoadon.Enabled = false;
                btnLuuhoadon.Enabled = false;
                btnXoakhoiHD.Enabled = false;
            }
        }

        //Người thực hiện: Văn Khải
        public FormLapHoaDon(int MaTK)
        {
            InitializeComponent();

            this.taikhoan = ctrlHD.getTaiKhoanByMaTK(MaTK);
            nudSoluongSP.Value = 1;
            if (grdThongtinSP.Rows.Count == 1)
            {
                btnThemvaoHoadon.Enabled = false;
                btnLuuhoadon.Enabled = false;
                btnXoakhoiHD.Enabled = false;
            }

            string subGiohientai = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
            string[] giohientai = subGiohientai.Split(':');
            
            hoadonTam.MaHD = "" + giohientai[0] + giohientai[1] + giohientai[2] + random.Next(99, 1000);

            lblMaHD.Text = hoadonTam.MaHD;

            lblNgayhientai.Text = DateTime.Today.ToShortDateString();
            txtSotrangCT.Text = 1 + " / ?";
            txtSoTrang.Text = 1 + " / ?";
            txtTenKH.Enabled = false;

            chkInHD.Checked = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {

        }

        //Người thực hiện: Thanh Đức
        private void btnTimkiemTTSP_Click(object sender, EventArgs e)
        {
            grdThongtinSP.Rows.Clear();
            dsTam.Clear();
            //dsSP_SLton.Clear();

            dsCTPNbySP = ctrlHD.LoadDanhsachCTPNByMaSP();
            dsCTHDbySP = ctrlHD.LoadDanhsachCTHDByMaSP();

            List<SanPham> danhsachSP = new List<SanPham>();
            if (txtThongtinSPTK.Text == "")
            {
                danhsachSP = ctrlHD.getTatcaSanpham();
                btnThemvaoHoadon.Enabled = true;
                btnLuuhoadon.Enabled = true;
            }
            else
            {
                //danhsachSP = Common.Common.Intance.SanPhams.Where(x => x.TenSP == txtThongtinSPTK.Text || x.MaSP == txtThongtinSPTK.Text).ToList();
                danhsachSP = ctrlHD.TimkiemDanhsachSanPham(txtThongtinSPTK.Text);
                if (danhsachSP.Count != 0)
                {
                    btnThemvaoHoadon.Enabled = true;
                    btnLuuhoadon.Enabled = true;
                }
            }

            //danhsachSP = danhsachSP.Where(x => x.Tinhtrang == "Còn hàng").ToList();

            int slNhap = 0, slTon = 0;
            foreach (var sp in danhsachSP)
            {
                var ctpn = dsCTPNbySP.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                var cthd = dsCTHDbySP.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                CTPNByMaSP ct;

                if (!dsSP_SLton.Any(x => x.Masp == sp.MaSP))
                {
                    ct = new CTPNByMaSP();
                }
                else
                {
                    ct = dsSP_SLton.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                }

                if (ctpn != null && cthd != null)
                {
                    slTon = ctpn.TongSL - cthd.TongSL;
                    ct.Masp = sp.MaSP; ct.TongSL = slTon;
                    dsSP_SLton.Add(ct);
                }
                else
                {
                    if (ctpn != null)
                    {
                        slNhap = ctpn.TongSL;
                        ct.Masp = sp.MaSP; ct.TongSL = slNhap;
                        dsSP_SLton.Add(ct);
                    }
                }
            }

            if (dsSP_SLton.Count > 0)
            {
                danhsachSP = danhsachSP.Where(x => dsSP_SLton.Any(y => y.Masp == x.MaSP)).ToList();
            }

            LoadRecordSP(danhsachSP, pageNumber, numberRecord);
            int tongST = (danhsachSP.Count / numberRecord) + 1;
            if (danhsachSP.Count % numberRecord == 0)
            {
                tongST = (danhsachSP.Count / numberRecord);
            }
            txtSoTrang.Text = 1 + " / " + tongST.ToString();
            dsTam = danhsachSP;
            btnThemvaoHoadon.Enabled = true;
        }

        //Người thực hiện: Văn Khải
        private void btnThemvaoHoadon_Click(object sender, EventArgs e)
        {
            if (grdThongtinSP.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để thêm vào hóa đơn !", "Thông báo");
            }
            else
            {
                int soluongSPHT = 0; int soluong = 0;
                string masp = grdThongtinSP.CurrentRow.Cells[0].Value.ToString();
                int.TryParse(grdThongtinSP.CurrentRow.Cells[6].Value.ToString(), out soluongSPHT);
                SanPham sanPham = ctrlHD.getSanPham(masp);

                if (int.TryParse(nudSoluongSP.Text.ToString(), out soluong))
                {
                    if (soluong <= soluongSPHT)
                    {
                        ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                        chiTietHoaDon.SanPham = sanPham;
                        chiTietHoaDon.Soluong = soluong;
                        if (dsSPHD.Count == 0)
                        {
                            ChiTietHoaDon cthdInit = new ChiTietHoaDon();
                            cthdInit.SanPham = sanPham;
                            cthdInit.Soluong = 0;
                            dsSPHD.Add(cthdInit);
                            dsSPHDSess.Add(sanPham);
                        }

                        if (dsSPHDSess.Contains(sanPham))
                        {
                            if (dsSPHD.Count != 1)
                            {
                                foreach (var cthd in dsSPHD)
                                {
                                    if (cthd.SanPham.MaSP == masp)
                                    {
                                        cthd.Soluong += soluong;
                                    }
                                }
                            }
                            else
                            {
                                dsSPHD[0].Soluong += soluong;
                            }
                        }
                        else
                        {
                            dsSPHD.Add(chiTietHoaDon);
                            dsSPHDSess.Add(sanPham);
                        }

                        if (dsSPHD.Count != 0)
                        {
                            btnXoakhoiHD.Enabled = true;
                        }

                        CTPNByMaSP ct = dsSP_SLton.Where(x => x.Masp == sanPham.MaSP).FirstOrDefault();
                        ct.TongSL = ct.TongSL - soluong;

                        nudSoluongSP.Value = 1;
                        LoadRecordCTHD(dsSPHD, pageNumberCT, numberRecordCT);
                        LoadRecordSP(dsTam, pageNumber, numberRecord);
                        int tongsotrang = (dsSPHD.Count / numberRecordCT) + 1;
                        if (dsSPHD.Count % numberRecordCT == 0)
                        {
                            tongsotrang = (dsSPHD.Count / numberRecordCT);
                        }
                        txtSotrangCT.Text = 1 + " / " + tongsotrang.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Số lượng sản phẩm thêm vào hóa đơn phải nhỏ hơn số lượng sản phẩm tồn !", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng sản phẩm thêm vào hóa đơn không được rỗng !", "Thông báo");
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void btnXoakhoiHD_Click(object sender, EventArgs e)
        {
            if (dsSPHD.Count > 0)
            {
                int soluongGiam = 0;
                int index = 0;
                string tenspcthd = grdDanhsachSPHD.CurrentRow.Cells[0].Value.ToString();
                string strslSPHD = grdDanhsachSPHD.CurrentRow.Cells[2].Value.ToString();
                string strsoluong = nudSoluongSP.Value.ToString();

                SanPham spSess = ctrlHD.getSanPham(tenspcthd);
                CTPNByMaSP ct = dsSP_SLton.Where(x => x.Masp == spSess.MaSP).FirstOrDefault();
                ChiTietHoaDon cthd;

                if (int.TryParse(strsoluong, out soluongGiam))
                {
                    if (dsSPHD.Any(x => x.SanPham.TenSP == tenspcthd))
                    {
                        if (int.Parse(strslSPHD) > soluongGiam)
                        {
                            cthd = dsSPHD.Where(x => x.SanPham.TenSP == tenspcthd).FirstOrDefault();
                            cthd.Soluong = int.Parse(strslSPHD) - soluongGiam;
                            ct.TongSL += soluongGiam;
                        }
                        else if (int.Parse(strslSPHD) <= soluongGiam)
                        {
                            if (MessageBox.Show("Xác nhận xóa sản phẩm khỏi danh sách sản phẩm hóa đơn ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                foreach (var sphd in dsSPHD)
                                {
                                    if (sphd.SanPham.TenSP == tenspcthd)
                                    {
                                        index = dsSPHD.IndexOf(sphd);
                                    }
                                }

                                dsSPHD.RemoveAt(index);
                                dsSPHDSess.Remove(spSess);
                                grdDanhsachSPHD.Rows.Remove(grdDanhsachSPHD.CurrentRow);
                                ct.TongSL += soluongGiam;
                            }
                        }
                        LoadRecordCTHD(dsSPHD, pageNumberCT, numberRecordCT);
                        LoadRecordSP(dsTam, pageNumber, numberRecord);
                    }

                    if (dsSPHD.Count == 0)
                    {
                        txtSotrangCT.Text = 1 + " / ?";
                    }
                    else
                    {
                        if (dsSPHD.Count % numberRecordCT == 0)
                        {
                            txtSotrangCT.Text = pageNumberCT + " / " + (dsSPHD.Count / numberRecordCT).ToString();
                            btnPrevCT_Click(sender, e);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng sản phẩm thêm vào hóa đơn không được rỗng !", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Danh sách sản phẩm của hóa đơn đã rỗng !", "Thông báo");
            }
        }

        DataGridViewRow ConvertSanPhamtoGridViewRow(SanPham sp, int slTon)
        {
            DataGridViewRow dgvr = (DataGridViewRow)grdThongtinSP.Rows[0].Clone();
            dgvr.Cells[0].Value = sp.MaSP.ToString();
            dgvr.Cells[1].Value = sp.TenSP.ToString();
            dgvr.Cells[2].Value = sp.Mausac.ToString();
            dgvr.Cells[3].Value = sp.Chatlieu.ToString();
            dgvr.Cells[4].Value = sp.Kichthuoc.ToString();
            dgvr.Cells[5].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongia);
            dgvr.Cells[6].Value = slTon;
            return dgvr;
        }

        void LoadDanhsachTTSP(List<SanPham> dsspht)
        {
            grdThongtinSP.Rows.Clear();
            grdThongtinSP.AllowUserToAddRows = true;

            foreach (var sp in dsspht)
            {
                var spSlton = dsSP_SLton.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                if (spSlton != null)
                {
                    grdThongtinSP.Rows.Add(ConvertSanPhamtoGridViewRow(sp, spSlton.TongSL));
                }

            }

            grdThongtinSP.AllowUserToAddRows = false;
        }

        void LoadRecordSP(List<SanPham> ds, int page, int recordNum)
        {
            List<SanPham> dsSP = new List<SanPham>();

            dsSP = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTSP(dsSP);
        }

        DataGridViewRow ConvertCTHDtoGridViewRow(ChiTietHoaDon ct)
        {
            DataGridViewRow dgvr = (DataGridViewRow)grdDanhsachSPHD.Rows[0].Clone();

            dgvr.Cells[0].Value = ct.SanPham.TenSP.ToString();
            dgvr.Cells[1].Value = ct.SanPham.Donvi.ToString();
            dgvr.Cells[2].Value = ct.Soluong.ToString();
            dgvr.Cells[3].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ct.SanPham.Dongia);
            dgvr.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ct.SanPham.Dongia * ct.Soluong);
            tongtien += (ct.SanPham.Dongia * ct.Soluong);
            return dgvr;
        }

        void LoadDanhsachTTCTHD(List<ChiTietHoaDon> dsCTHD)
        {
            tongtien = 0;
            grdDanhsachSPHD.Rows.Clear();
            grdDanhsachSPHD.AllowUserToAddRows = true;

            foreach (var ct in dsCTHD)
            {
                grdDanhsachSPHD.Rows.Add(ConvertCTHDtoGridViewRow(ct));
            }

            lblTongtienHD.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);
            grdThongtinSP.AllowUserToAddRows = false;
            grdDanhsachSPHD.AllowUserToAddRows = false;
        }

        void LoadRecordCTHD(List<ChiTietHoaDon> ds, int page, int recordNum)
        {
            List<ChiTietHoaDon> dscthd = new List<ChiTietHoaDon>();

            dscthd = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTCTHD(dscthd);
        }

        //Người thực hiện: Văn Khải
        private void btnLuuhoadon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận thanh toán hóa đơn !", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dsSPHD.Count == 0)
                {
                    MessageBox.Show("Chưa có thông tin sản phẩm cần lập hóa đơn ! Vui lòng quay lại", "Thông báp", MessageBoxButtons.OK);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtTenKH.Text))
                    {
                        MessageBox.Show("Chưa có thông tin khách hàng lập hóa đơn ! Vui lòng quay lại", "Thông báp", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        KhachHang khmoi = new KhachHang();

                        if (ctrlHD.TimkiemKhachhang(txtSDTKH.Text) != null)
                        {
                            khmoi = ctrlHD.TimkiemKhachhang(txtSDTKH.Text);
                        }
                        else
                        {
                            khmoi.MaKH = Helpers.RandomID(txtTenKH.Text);
                            khmoi.Hovaten = txtTenKH.Text;
                            khmoi.SDT = txtSDTKH.Text.Trim();
                        }

                        foreach (var spHethang in dsSP_SLton)
                        {
                            if (spHethang.TongSL == 0)
                            {
                                var spCapnhat = ctrlHD.getSanPham(spHethang.Masp);
                                spCapnhat.Tinhtrang = "Hết hàng";
                                ctrlHD.CapnhatSanpham();
                            }
                        }

                        NhanVien nv = ctrlHD.getNhanVien(taikhoan.MaNV);

                        if (ctrlHD.getHoadon(hoadonTam.MaHD) != null)
                        {
                            hoadonTam.Tinhtrang = "Đã thanh toán";
                            hoadonTam.ChiTietHoaDons = dsSPHD.ToList();
                            hoadonTam.NhanVien = nv;

                            ctrlHD.CapnhatHoadon();
                        }
                        else
                        {
                            hoadonTam.KhachHang = khmoi;
                            hoadonTam.Ngaylap = DateTime.Today;

                            hoadonTam.NhanVien = nv;
                            hoadonTam.Tinhtrang = "Đã thanh toán";
                            hoadonTam.ChiTietHoaDons = dsSPHD.ToList();

                            hoadonTam.Calamviec = Helpers.KiemtraCalamviecHientai();

                            ctrlHD.LuuHoaDon(hoadonTam);
                        }

                        MessageBox.Show("Lưu hóa đơn thành công !", "Thông báo");
                        if(chkInHD.Checked == true)
                        {
                            if (MessageBox.Show("Xem thông tin hóa đơn ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                string path = XuatHDTam(hoadonTam);
                                Process.Start(path);
                            }
                        }

                        string subGiohientai = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                        string[] giohientai = subGiohientai.Split(':');

                        hoadonTam = new HoaDon();
                        hoadonTam.MaHD = "" + giohientai[0] + giohientai[1] + giohientai[2] + random.Next(99, 1000);
                        lblMaHD.Text = hoadonTam.MaHD;
                        LammoiThongtinHD();
                        txtSDTKH.Enabled = true;
                        btnThemKH.Enabled = true;
                        btnTimkiemKH.Enabled = true;
                    }
                }
            }
        }

        void LammoiThongtinHD()
        {
            grdDanhsachSPHD.Rows.Clear();
            grdThongtinSP.Rows.Clear();
            dsSPHD.Clear();
            dsSPHDSess.Clear();
            txtThongtinSPTK.Clear();
            txtTenKH.Clear();
            txtSDTKH.Clear();
            lblTongtienHD.ResetText();
            lblTongvoiVAT.ResetText();
            lblTienthua.ResetText();
            nudTienmat.ResetText();
            nudSoluongSP.Value = 1;
            chkInHD.Checked = false;
        }

        //Người thực hiện: Văn Khải =====================================
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 > 0)
            {
                pageNumber--;
                int tongsotrang = (dsTam.Count / numberRecord) + 1;
                if (dsTam.Count % numberRecord == 0)
                {
                    tongsotrang = (dsTam.Count / numberRecord);
                }
                txtSoTrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordSP(dsTam, pageNumber, numberRecord);
            }
        }

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
                LoadRecordSP(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            LoadRecordSP(dsTam, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordSP(dsTam, tongsotrang, numberRecord);
        }
        //================================================================

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            //if(dsSPHD.Count > 0)
            //{
            //    if(MessageBox.Show("Xác nhận rời chức năng lập hóa đơn", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        this.Close();
            //    }
            //}
            this.Close();
        }

        //Người thực hiện: Văn Khải
        private void btnTimkiemKH_Click(object sender, EventArgs e)
        {
            KhachHang kh = ctrlHD.TimkiemKhachhang(txtSDTKH.Text);
            if (kh != null)
            {
                if (MessageBox.Show("Thông tin khách hàng tìm được: \n\n" + "Tên: " + kh.Hovaten + "\n\n" + "Số điện thoại: " + kh.SDT + "\n\n" + "Xác nhận chọn khách hàng ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    txtTenKH.Text = kh.Hovaten.ToUpper();
                }
                else
                {
                    txtSDTKH.Focus();
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng !", "Thông báo");
            }
        }

        //Người thực hiện: Thanh Đức
        private void FormLapHoaDon_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void txtSoTrang_TextChanged(object sender, EventArgs e)
        {

        }

        //Người thực hiện: Văn Khải
        private void dgvDanhsachSPHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string soluongSPHD = grdDanhsachSPHD.CurrentRow.Cells[2].Value.ToString();
            //decimal sl = 0;
            //if(decimal.TryParse(soluongSPHD, out sl))
            //{
            //    nudSoluongSP.Value = sl;
            //}
        }

        //Người thực hiện: Văn Khải =====================================
        private void btnPrevCT_Click(object sender, EventArgs e)
        {
            if (pageNumberCT - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumberCT--;

                if (dsSPHD.Count % numberRecordCT == 0)
                {
                    tongsotrang = (dsSPHD.Count / numberRecordCT);
                }
                else
                {
                    tongsotrang = (dsSPHD.Count / numberRecordCT) + 1;
                }

                //tongsotrang = (dsSPHD.Count / numberRecordCT) + 1;
                txtSotrangCT.Text = pageNumberCT.ToString() + " / " + tongsotrang;
                LoadRecordCTHD(dsSPHD, pageNumberCT, numberRecordCT);
            }
        }

        private void btnNextCT_Click(object sender, EventArgs e)
        {
            if (pageNumberCT == 1 && dsSPHD.Count == 4)
            {
                return;
            }
            if (pageNumberCT - 1 < dsSPHD.Count / numberRecordCT)
            {
                int tongsotrang = 0;
                pageNumberCT++;
                tongsotrang = (dsSPHD.Count / numberRecordCT) + 1;
                if (dsSPHD.Count % numberRecordCT == 0)
                {
                    tongsotrang = (dsSPHD.Count / numberRecordCT);
                }
                txtSotrangCT.Text = pageNumberCT.ToString() + " / " + tongsotrang;
                LoadRecordCTHD(dsSPHD, pageNumberCT, numberRecordCT);
            }

        }

        private void btnFirstCT_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsSPHD.Count / numberRecordCT) + 1;
            txtSotrangCT.Text = 1 + " / " + tongsotrang;
            LoadRecordCTHD(dsSPHD, 1, numberRecordCT);
        }

        private void btnLastCT_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsSPHD.Count / numberRecordCT) + 1;
            txtSotrangCT.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordCTHD(dsSPHD, tongsotrang, numberRecordCT);
        }
        //================================================================

        //Người thực hiện: Văn Khải
        private void nudTienmat_ValueChanged_1(object sender, EventArgs e)
        {
            //float tienmat = 0, tienthua = 0, congvat = 0;
            //if (float.TryParse(nudTienmat.Value.ToString(), out tienmat) && float.TryParse(lblTongvoiVAT.Text.ToString(), out congvat))
            //{
            //    tienthua = tienmat - congvat;
            //    lblTienthua.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tienthua);
            //}
            float tienmat = 0, tienthua = 0, congvat = 0;
            if (float.TryParse(nudTienmat.Value.ToString(), out tienmat))
            {
                string[] arrVAT = lblTongvoiVAT.Text.Split('.');
                string strVAT = "";
                for(int i = 0; i < arrVAT.Count(); i++)
                {
                    strVAT += arrVAT[i].ToString();
                }
                if(float.TryParse(strVAT, out congvat))
                {
                    tienthua = tienmat - congvat;
                    lblTienthua.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tienthua);
                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void FormLapHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F3) btnTimkiemKH.PerformClick(); }
            { if (e.KeyCode == Keys.F1) btnTimkiemTTSP.PerformClick(); }
            { if (e.KeyCode == Keys.F5) btnThemvaoHoadon.PerformClick(); }
            { if (e.KeyCode == Keys.F6) btnXoakhoiHD.PerformClick(); }
            { if (e.KeyCode == Keys.F7) btnLuuhoadon.PerformClick(); }
        }

        //Người thực hiện: Văn Khải
        private void lblTongtienHD_TextChanged(object sender, EventArgs e)
        {
            float congVAT = tongtien + ((tongtien / 100) * 10);
            lblTongvoiVAT.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", congVAT);
        }

        private void FormLapHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dsSPHD.Count > 0)
            {
                if (MessageBox.Show("Xác nhận rời chức năng lập hóa đơn ? Tiếp tục ? \n\n Thông tin hóa đơn sẽ không được lưu !", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        void LammoiDanhsachHangcho()
        {
            var dsHDDangcho = ctrlHD.getHoadonbyTinhtrang("Đang chờ");
            dsHDDangcho = dsHDDangcho.Where(x => x.Ngaylap.Day == DateTime.Today.Day && x.Ngaylap.Month == DateTime.Today.Month && x.Ngaylap.Year == DateTime.Today.Year && x.Calamviec == Helpers.KiemtraCalamviecHientai()).ToList();
            foreach (var hd in dsHDDangcho)
            {
                ctrlHD.HuyHoaDon(hd);
            }
            ctrlHD.CapnhatHoadon();
        }

        //Người thực hiện: Văn Khải
        private void btnThemvaohangcho_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenKH.Text) && dsSPHD.Count > 0)
            {
                KhachHang khmoi = new KhachHang();

                if (ctrlHD.geTKhachhang(txtTenKH.Text) != null)
                {
                    khmoi = ctrlHD.geTKhachhang(txtTenKH.Text);
                }
                else
                {
                    khmoi.MaKH = Helpers.RandomID(txtTenKH.Text);
                    khmoi.Hovaten = txtTenKH.Text;
                    khmoi.SDT = txtSDTKH.Text.Trim();
                }

                foreach (var spHethang in dsSP_SLton)
                {
                    if (spHethang.TongSL == 0)
                    {
                        var spCapnhat = ctrlHD.getSanPham(spHethang.Masp);
                        spCapnhat.Tinhtrang = "Hết hàng";
                        ctrlHD.CapnhatSanpham();
                    }
                }
                NhanVien nv = ctrlHD.getNhanVien(taikhoan.MaNV);

                if (ctrlHD.getHoadon(hoadonTam.MaHD) != null)
                {
                    hoadonTam.NhanVien = nv;
                    hoadonTam.ChiTietHoaDons = dsSPHD.ToList();
                    ctrlHD.CapnhatHoadon();
                }
                else
                {
                    hoadonTam.KhachHang = khmoi;
                    hoadonTam.Ngaylap = DateTime.Today;

                    hoadonTam.NhanVien = nv;
                    hoadonTam.Tinhtrang = "Đang chờ";
                    hoadonTam.ChiTietHoaDons = dsSPHD.ToList();

                    hoadonTam.Calamviec = Helpers.KiemtraCalamviecHientai();

                    ctrlHD.LuuHoaDon(hoadonTam);
                }

                MessageBox.Show("Đã thêm thông tin hóa đơn vào hàng chờ !", "Thông báo");
                LammoiThongtinHD();

                hoadonTam = new HoaDon();
                string subGiohientai = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                string[] giohientai = subGiohientai.Split(':');

                hoadonTam.MaHD = "" + giohientai[0] + giohientai[1] + giohientai[2] + random.Next(99, 1000);
                lblMaHD.Text = hoadonTam.MaHD;
            }
            else
            {
                MessageBox.Show("Thông tin khách hàng hoặc danh sách sản phẩm cần lập hóa đơn không được rỗng !", "Thông báo");
            }
        }

        //Người thực hiện: Văn Khải
        private void SetValueThongtinKhachhang(string makh)
        {
            KhachHang kh;
            if (!string.IsNullOrEmpty(makh))
            {
                kh = ctrlHD.geTKhachhang(makh);
                if(kh != null)
                {
                    txtTenKH.Text = kh.Hovaten.ToUpper();
                    txtSDTKH.Text = kh.SDT;                    
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void SetValueThongtinHoadon(string mahd, DateTime ngaylap, int calamviec)
        {
            HoaDon hd = ctrlHD.getHoadon(mahd);
            if (hd != null)
            {
                dsSPHDSess.Clear();
                dsSPHD.Clear();

                hoadonTam = hd;
                lblMaHD.Text = hd.MaHD;
                txtTenKH.Text = hd.KhachHang.Hovaten;
                txtSDTKH.Text = hd.KhachHang.SDT;

                var dsCTHD_HDDangchon = ctrlHD.getCTHDbyMaHD(hd.MaHD);
                try
                {
                    foreach (var cthd in dsCTHD_HDDangchon)
                    {
                        dsSPHD.Add(cthd);
                        dsSPHDSess.Add(cthd.SanPham);
                    }
                    LoadRecordCTHD(dsSPHD, pageNumberCT, numberRecordCT);
                }
                catch
                {

                }

                grdThongtinSP.Rows.Clear();
                btnThemKH.Enabled = false;
                btnTimkiemKH.Enabled = false;
                txtSDTKH.Enabled = false;
            }            
        }

        //Người thực hiện: Văn Khải
        private void btnXemhangcho_Click(object sender, EventArgs e)
        {
            FormDanhsachHangchoHoadon frm = new FormDanhsachHangchoHoadon(SetValueThongtinHoadon);
            frm.ShowDialog();
        }

        //Người thực hiện: Thanh Đức
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            FormThemKhachHang frm = new FormThemKhachHang(SetValueThongtinKhachhang);
            frm.ShowDialog();
        }

        //Người thực hiện: Thanh Đức
        private void btnLamMoiTTHD_Click(object sender, EventArgs e)
        {
            if (ctrlHD.getHoadon(lblMaHD.Text) != null)
            {
                string subGiohientai = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                string[] giohientai = subGiohientai.Split(':');

                hoadonTam = new HoaDon();
                hoadonTam.MaHD = "" + giohientai[0] + giohientai[1] + giohientai[2] + random.Next(99, 1000);
                lblMaHD.Text = hoadonTam.MaHD;
            }
            LammoiThongtinHD();            
        }

        //Người thực hiện: Văn Khải
        string XuatHDTam(HoaDon hd)
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            //SaveFileDialog dialog = new SaveFileDialog();

            //dialog.FileName = "E:\\Phat trien ung dung\\pham mem\\Excel\\Hoa don tam\\";

            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    NhanVien nv = ctrlHD.getNhanvienbyMaTK(taikhoan);

                    // đặt tên người tạo file
                    if (nv != null)
                    {
                        p.Workbook.Properties.Author = nv.Chucvu + " " + nv.Hovaten;
                    }

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Hóa đơn";

                    ////Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("Sheet 1");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[1];

                    ws.DefaultColWidth = 25;

                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 13;
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    // Tạo danh sách các column header
                    string[] arrColumnHeader = {
                                                "Số thứ tự",
                                                "Tên sản phẩm",
                                                "Số lượng",
                                                "Đơn giá",
                                                "Thành tiền"
                        };

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();

                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                    ws.Cells[2, 1].Value = "Tên cửa hàng:";
                    ws.Cells[2, 1].Style.Font.Bold = true;
                    ws.Cells[2, 2].Value = "K&D SHOP";
                    ws.Cells[2, 2].Style.Font.Size = 17;
                    ws.Cells[2, 2, 2, 5].Merge = true;

                    ws.Cells[3, 1].Value = "Địa chỉ:";
                    ws.Cells[3, 1].Style.Font.Bold = true;
                    ws.Cells[3, 2].Value = "12 Nguyễn Văn Bảo,  Phường 4, Quận Gò Vấp, TP.HCM";
                    ws.Cells[3, 2].Style.Font.Size = 17;
                    ws.Cells[3, 2, 3, 5].Merge = true;

                    ws.Cells[4, 1].Value = "Mã hóa đơn:";
                    ws.Cells[4, 1].Style.Font.Bold = true;
                    ws.Cells[4, 2].Value = hoadonTam.MaHD;
                    ws.Cells[4, 2].Style.Font.Size = 17;
                    ws.Cells[4, 2, 4, 5].Merge = true;

                    ws.Cells[6, 1].Value = "HÓA ĐƠN BÁN HÀNG";
                    ws.Cells[6, 1].Style.Font.Bold = true;
                    ws.Cells[6, 1, 6, 5].Merge = true;
                    ws.Cells[6, 1, 6, 5].Style.Font.Size = 30;
                    ws.Cells[6, 1, 6, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[8, 1].Value = "Tên khách hàng:";
                    ws.Cells[8, 1].Style.Font.Bold = true;
                    ws.Cells[8, 2].Value = hd.KhachHang.Hovaten;
                    ws.Cells[8, 2].Style.Font.Size = 17;

                    ws.Cells[8, 4].Value = "Ngày lập:";
                    ws.Cells[8, 4].Style.Font.Bold = true;
                    ws.Cells[8, 5].Value = hd.Ngaylap.ToShortDateString();
                    ws.Cells[8, 5].Style.Font.Size = 17;

                    ws.Cells[9, 1].Value = "Tên nhân viên:";
                    ws.Cells[9, 1].Style.Font.Bold = true;
                    ws.Cells[9, 2].Value = hd.NhanVien.Hovaten;
                    ws.Cells[9, 2].Style.Font.Size = 17;

                    int colIndex = 1;
                    int rowIndex = 11;

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

                    int j = 0;
                    //với mỗi item trong danh sách sẽ ghi trên 1 dòng
                    foreach (var ct in dsSPHD)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;

                        // rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;

                        //gán giá trị cho từng cell                      
                        var cell0 = ws.Cells[rowIndex, colIndex++];
                        cell0.Value = "" + j + 1;
                        var border0 = cell0.Style.Border;
                        border0.Bottom.Style =
                            border0.Top.Style =
                            border0.Left.Style =
                            border0.Right.Style = ExcelBorderStyle.Thin;

                        var cell1 = ws.Cells[rowIndex, colIndex++];
                        cell1.Value = ct.SanPham.TenSP.ToString();
                        var border1 = cell1.Style.Border;
                        border1.Bottom.Style =
                            border1.Top.Style =
                            border1.Left.Style =
                            border1.Right.Style = ExcelBorderStyle.Thin;

                        var cell2 = ws.Cells[rowIndex, colIndex++];
                        cell2.Value = ct.Soluong.ToString();
                        var border2 = cell2.Style.Border;
                        border2.Bottom.Style =
                            border2.Top.Style =
                            border2.Left.Style =
                            border2.Right.Style = ExcelBorderStyle.Thin;

                        var cell3 = ws.Cells[rowIndex, colIndex++];
                        cell3.Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ct.SanPham.Dongia);
                        var border3 = cell3.Style.Border;
                        border3.Bottom.Style =
                            border3.Top.Style =
                            border3.Left.Style =
                            border3.Right.Style = ExcelBorderStyle.Thin;

                        var cell4 = ws.Cells[rowIndex, colIndex++];
                        cell4.Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ct.SanPham.Dongia * ct.Soluong);
                        var border4 = cell4.Style.Border;
                        border4.Bottom.Style =
                            border4.Top.Style =
                            border4.Left.Style =
                            border4.Right.Style = ExcelBorderStyle.Thin;

                        j++;
                    }

                    rowIndex += 2;
                    ws.Cells[rowIndex, 1].Value = "Tổng thanh toán (vnđ):";
                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
                    ws.Cells[rowIndex , 2].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien) + " vnđ";
                    ws.Cells[rowIndex, 2].Style.Font.Size = 17;

                    ws.Cells[rowIndex , 4].Value = "Tổng cộng (vnđ):";
                    ws.Cells[rowIndex, 4].Style.Font.Bold = true;
                    ws.Cells[rowIndex , 5].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien + ((tongtien / 100) * 10)) + " vnđ";
                    ws.Cells[rowIndex, 5].Style.Font.Size = 17;

                    rowIndex++;
                    ws.Cells[rowIndex, 1].Value = "Thuế VAT 10% (vnđ):";
                    ws.Cells[rowIndex, 1].Style.Font.Bold = true;
                    ws.Cells[rowIndex, 2].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ((tongtien / 100) * 10)) + " vnđ";
                    ws.Cells[rowIndex, 2].Style.Font.Size = 17;

                    //Lưu file lại
                    string fileName = "HD-" + hd.MaHD + "-" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + "-" + Helpers.KiemtraCalamviecHientai() + "-" + nv.Hovaten + ".xls";
                    filePath = "E:\\Phat trien ung dung\\pham mem\\Excel\\Hoa don tam\\";
                    filePath += fileName;
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                //MessageBox.Show("Xuất excel thành công!");

            }
            catch (Exception EE)
            {
                MessageBox.Show("Có lỗi khi lưu file!");
            }
            return filePath;
        }
    }
}
