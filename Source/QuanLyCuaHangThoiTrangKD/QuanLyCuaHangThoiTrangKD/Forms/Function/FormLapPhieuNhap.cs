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
using System.Globalization;
using QuanLyCuaHangThoiTrangKD.Controller;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public delegate void GuiTenSanpham(string tensp);
    public delegate void GuiThongtinSanpham(string Tensp, string Chatlieu, string Mausac, string Kichthuoc, string Loaisp, string Donvi, string Dongianhap, string Dongiaban, byte[] Hinhanh);

    public partial class FormLapPhieuNhap : Form
    {
        PhieuNhapController ctrlPN = new PhieuNhapController();

        TaiKhoan taikhoan;

        int pageNumber = 1;
        int numberRecord = 4;

        int pageNumberCT = 1;
        int numberRecordCT = 6;

        int tongslSP = 0;

        SanPham sanphamSess = new SanPham();

        private List<SanPham> dsSPPNSess = new List<SanPham>();
        private List<ChiTietPhieuNhap> dsCTPN = new List<ChiTietPhieuNhap>();
        float tongtien = 0;

        List<SanPham> dsTam;
        byte[] temp;

        //Người thực hiện: Văn Khải
        public FormLapPhieuNhap(int MaTK)
        {
            InitializeComponent();

            cboTenNCC.Items.Add("--Chọn nhà cung cấp--");
            cboTenNCC.SelectedItem = cboTenNCC.Items[0];

            LoadComboboxSP();
            //btnXoarongTTSP.Enabled = false;
            taikhoan = ctrlPN.getTaiKhoanByMaTK(MaTK);
            List<SanPham> ds = ctrlPN.getTatcaSanpham();

            if (dsCTPN.Count == 0)
            {
                btnLuuPN.Enabled = false;
            }

            lblNgaylap.Text = DateTime.Today.ToShortDateString();
            grdThongtinSPHT.AllowUserToAddRows = false;
            grdThongtinSPHT.ScrollBars = ScrollBars.None;
            dsTam = ds;
            txtSoTrang.Text = 1 + " / " + ((ds.Count / numberRecord) + 1).ToString();
            txtSotrangDSCT.Text = 1 + " / ?";
            LoadRecordSP(ds, pageNumber, numberRecord);

            btnXemCTSP.Enabled = false;
        }

        void LoadDanhsachSPHT(List<ItemGridSP_SLTon> ds)
        {
            grdThongtinSPHT.Rows.Clear();

            foreach (var ct in ds)
            {
                grdThongtinSPHT.Rows.Add(ConvertSanPhamtoGridRow(ct.Sanpham, ct.Slton, ct.Slban));
            }

            grdThongtinSPHT.AllowUserToAddRows = false;
        }

        //Người thực hiện: Văn Khải
        private void btnThemvaoPN_Click(object sender, EventArgs e)
        {
            float dongia = float.Parse(nudDongianhap.Value.ToString());

            int slNhap = int.Parse(nudSoluongnhap.Value.ToString());
            
            ThemSPvaoPN(sanphamSess, slNhap);

            btnLuuPN.Enabled = true;
        }

        void ThemSPvaoPN(SanPham sp, int slNhap)
        {
            if (ctrlPN.KiemtraSanphamTontai(sp) == false)
            {
                if (string.IsNullOrEmpty(sp.TenSP) && string.IsNullOrEmpty(sp.MaSP))
                {
                    sp.MaSP = Helpers.RandomID(sp.TenSP);
                }
            }

            ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
            ctpn.SanPham = sp;
            ctpn.Soluong = slNhap;

            if (dsCTPN.Count == 0)
            {
                ChiTietPhieuNhap ctpnInit = new ChiTietPhieuNhap();
                ctpnInit.SanPham = sp;
                ctpnInit.Soluong = 0;
                dsSPPNSess.Add(sp);
                dsCTPN.Add(ctpnInit);
            }

            if (dsSPPNSess.Contains(sp))
            {
                if (dsCTPN.Count != 1)
                {
                    foreach (var cthd in dsCTPN)
                    {
                        if (cthd.SanPham.MaSP == sp.MaSP)
                        {
                            cthd.Soluong += slNhap;
                        }
                    }
                }
                else
                {
                    dsCTPN[0].Soluong += slNhap;
                }
            }
            else
            {
                dsCTPN.Add(ctpn);
                dsSPPNSess.Add(sp);
            }

            if (dsCTPN.Count != 0)
            {
                btnXoakhoiPN.Enabled = true;
            }

            lblTongSLSPNhap.Text = dsCTPN.Count + "";

            LoadRecordCTPN(dsCTPN, pageNumberCT, numberRecordCT);
            txtSotrangDSCT.Text = 1 + " / " + ((dsCTPN.Count / numberRecordCT) + 1).ToString();
        }

        //Người thực hiện: Văn Khải
        private void btnLuuPN_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Xác nhận thanh toán phiếu nhập !", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dsCTPN.Count == 0 && cboTenNCC.Text == null)
                {
                    MessageBox.Show("Chưa có thông tin sản phẩm phiếu nhập, thông tin nhà cung cấp ! Vui lòng quay lại");
                }

                else if (dsCTPN.Count == 0 || cboTenNCC.SelectedIndex == 0)
                {
                    MessageBox.Show("Chưa có thông tin sản phẩm phiếu nhập hoặc thông tin nhà cung cấp ! Vui lòng quay lại");
                }

                else if (dsCTPN.Count != 0 && cboTenNCC.Text != null)
                {
                    //NhaCungCap ncc = Common.Common.Intance.NhaCungCaps.Where(x => x.TenNCC == cbTenNCC.SelectedItem.ToString().Trim()).FirstOrDefault();
                    NhaCungCap ncc = ctrlPN.getNhaCungCap(cboTenNCC.SelectedItem.ToString().Trim());
                    Random ran = new Random();
                    PhieuNhap phieu = new PhieuNhap();
                    NhanVien nv = ctrlPN.getNhanVien(taikhoan.MaNV);

                    var sps = ctrlPN.getTatcaSanpham();

                    foreach (var item in dsSPPNSess)
                    {
                        if (!sps.Contains(item))
                        {
                            ctrlPN.LuuSanpham(item);
                        }
                    }

                    phieu.Ngaylap = DateTime.Today;
                    phieu.Tinhtrang = "Đã thanh toán";
                    //phieu.GiatriPhieunhap = tongtien;
                    phieu.ChiTietPhieuNhaps = dsCTPN.ToList();
                    phieu.NhaCungCap = ncc;
                    phieu.NhanVien = nv;
                    phieu.Calamviec = Helpers.KiemtraCalamviecHientai();

                    ctrlPN.LuuPhieunhap(phieu);

                    MessageBox.Show("Phiếu nhập lưu thành công");
                    LammoiThongtinPN();
                }
            }
        }

        void LammoiThongtinPN()
        {
            grdDanhsachSPPN.Rows.Clear();
            dsCTPN.Clear();
            dsSPPNSess.Clear();
            var dssanpham = ctrlPN.getTatcaSanpham();
            LoadRecordSP(dssanpham, pageNumber, numberRecord);
            dsTam = dssanpham;
            cboTenNCC.ResetText();
            lblSDTNCC.ResetText();
            lblTongtienPN.ResetText();
            lblTongtienVAT.ResetText();
            lblTienthua.ResetText();
            nudTienmat.ResetText();
            lblTongSLSPNhap.ResetText();
            nudSoluongnhap.Value = 1;
            txtTenSP.Clear();
            cboDonvi.ResetText();
            nudDongianhap.ResetText();
        }

        void LoadComboboxSP()
        {
            var dsNCC = ctrlPN.getTatcaNhaCungCap();
            foreach (var ncc in dsNCC)
            {
                cboTenNCC.Items.Add(ncc.TenNCC);
            }

            //cboKichthuoc.Items.Add("S");
            //cboKichthuoc.Items.Add("M");
            //cboKichthuoc.Items.Add("L");
            //cboKichthuoc.Items.Add("XL");
            //cboKichthuoc.Items.Add("XXL");

            cboDonvi.Items.Add("Đôi");
            cboDonvi.Items.Add("Chiếc");
            cboDonvi.Items.Add("Bộ");
        }

        private void btnXoarongTTSP_Click(object sender, EventArgs e)
        {
            //tbTenSP.Text = "";
            //tbChatlieu.Text = "";
            //cbMausac.SelectedItem = "";
            //cbKichthuoc.SelectedItem = "";
            //cbDonvi.SelectedItem = "";
            //cbLoaiSP.SelectedItem = "";
            //nudDongianhap.Value = 100000;
            //nudSoluongnhap.Value = 1;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Người thực hiện: Văn Khải
        private void btnChonanhSP_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFile = new OpenFileDialog();
            //openFile.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
            //openFile.FilterIndex = 1;
            //openFile.RestoreDirectory = true;
            //if (openFile.ShowDialog() == DialogResult.OK)
            //{
            //    temp = Helpers.converImgToByte(openFile.FileName);
            //    pbHinhanhSP.Image = new Bitmap(openFile.FileName);
            //}
        }

        //Người thực hiện: Văn Khải
        private void btnXoakhoiPN_Click(object sender, EventArgs e)
        {
            if (dsCTPN.Count > 0)
            {
                int slSPTon = 0, soluonggiam = 0;
                int index = 0;
                string tenspctpn = grdDanhsachSPPN.CurrentRow.Cells[0].Value.ToString();
                string strslSPPN = grdDanhsachSPPN.CurrentRow.Cells[2].Value.ToString();
                string strsoluong = nudSoluongnhap.Value.ToString();

                SanPham spSess = dsSPPNSess.Where(x => x.TenSP == tenspctpn).FirstOrDefault();
                ChiTietPhieuNhap ctpn;

                if (int.TryParse(strsoluong, out soluonggiam))
                {
                    if (dsCTPN.Any(x => x.SanPham.TenSP == tenspctpn))
                    {
                        if (int.Parse(strslSPPN) > soluonggiam)
                        {
                            ctpn = dsCTPN.Where(x => x.SanPham.TenSP == tenspctpn).FirstOrDefault();
                            ctpn.Soluong = int.Parse(strslSPPN) - soluonggiam;
                        }
                        else if (int.Parse(strslSPPN) <= soluonggiam)
                        {
                            if(MessageBox.Show("Xác nhận xóa sản phẩm khỏi danh sách sản phẩm phiếu nhập ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                foreach (var sphd in dsCTPN)
                                {
                                    if (sphd.SanPham.TenSP == tenspctpn)
                                    {
                                        index = dsCTPN.IndexOf(sphd);
                                    }
                                }

                                dsCTPN.RemoveAt(index);
                                dsSPPNSess.Remove(spSess);
                                grdDanhsachSPPN.Rows.Remove(grdDanhsachSPPN.CurrentRow);
                            }
                        }
                        LoadRecordCTPN(dsCTPN, pageNumberCT, numberRecordCT);
                    }

                    lblTongSLSPNhap.Text = dsCTPN.Count + "";
                    if (dsCTPN.Count == 0)
                    {
                        txtSotrangDSCT.Text = 1 + " / ?";
                    }
                    else
                    {
                        if (dsCTPN.Count == numberRecordCT)
                        {
                            txtSotrangDSCT.Text = pageNumberCT + " / " + (dsCTPN.Count / numberRecordCT).ToString();
                            btnPrevDSCT_Click(sender, e);
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

        DataGridViewRow ConvertSanPhamtoGridRow(SanPham sp, int SLTon, int SLBan)
        {
            DataGridViewRow row = (DataGridViewRow)grdThongtinSPHT.Rows[0].Clone();
            row.Cells[0].Value = sp.MaSP.ToString();
            row.Cells[1].Value = sp.TenSP.ToString();            
            row.Cells[2].Value = SLTon;
            return row;
        }

        DataGridViewRow ConvertCTPNtoGridRow(ChiTietPhieuNhap ctpn)
        {
            DataGridViewRow row = (DataGridViewRow)grdDanhsachSPPN.Rows[0].Clone();
            row.Cells[0].Value = ctpn.SanPham.TenSP.ToString();
            row.Cells[1].Value = ctpn.SanPham.Donvi.ToString();
            row.Cells[2].Value = ctpn.Soluong;
            row.Cells[3].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ctpn.SanPham.Dongianhap);
            row.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ctpn.Soluong * ctpn.SanPham.Dongianhap);

            tongtien += ctpn.Soluong * ctpn.SanPham.Dongianhap;

            return row;
        }

        void LoadDanhsachTTCTPN(List<ChiTietPhieuNhap> dsCTPN)
        {
            tongtien = 0;
            grdDanhsachSPPN.Rows.Clear();
            grdDanhsachSPPN.AllowUserToAddRows = true;

            foreach (var ct in dsCTPN)
            {
                grdDanhsachSPPN.Rows.Add(ConvertCTPNtoGridRow(ct));
            }

            lblTongtienPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);
            //dgvThongtinSP.AllowUserToAddRows = false;
            grdDanhsachSPPN.AllowUserToAddRows = false;
        }

        void LoadRecordCTPN(List<ChiTietPhieuNhap> ds, int page, int recordNum)
        {
            List<ChiTietPhieuNhap> dsctpn = new List<ChiTietPhieuNhap>();

            dsctpn = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTCTPN(dsctpn);
        }

        //Người thực hiện: Văn Khải
        private void dgvThongtinSPHT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisableTTSP();
            btnXemCTSP.Enabled = true;

            string masp = grdThongtinSPHT.CurrentRow.Cells[0].Value.ToString();
            SanPham SPdangchon = ctrlPN.getSanPham(masp);
            decimal gianhap = 0;

            if (SPdangchon != null)
            {
                txtTenSP.Text = SPdangchon.TenSP;
                //tbChatlieu.Text = SPdangchon.Chatlieu;
                //cbMausac.SelectedItem = SPdangchon.Mausac;
                //cboKichthuoc.SelectedItem = SPdangchon.Kichthuoc;
                //cbLoaiSP.SelectedItem = SPdangchon.LoaiSP;
                cboDonvi.SelectedItem = SPdangchon.Donvi;
                if (decimal.TryParse(SPdangchon.Dongianhap.ToString(), out gianhap))
                {
                    nudDongianhap.Value = gianhap;
                }
            }
            else
            {
                //tbChatlieu.Text = "";
                //cbMausac.ResetText();
                //cbKichthuoc.ResetText();
                //tbChatlieu.Text = "";
                //cbLoaiSP.ResetText();
                cboDonvi.ResetText();
                nudDongianhap.ResetText();
            }
            sanphamSess = SPdangchon;
        }

        void DisableTTSP()
        {
            txtTenSP.Enabled = false;
            cboDonvi.Enabled = false;
        }

        void EnableTTSP()
        {
            txtTenSP.Enabled = true;
            cboDonvi.Enabled = true;
        }

        //Người thực hiện: Văn Khải
        void LoadRecordSP(List<SanPham> ds, int page, int recordNum)
        {
            List<SanPham> dsSP = new List<SanPham>();

            List<CTByMaSP> dsCTPN = ctrlPN.LoadDanhsachCTPNByMaSP();
            List<CTByMaSP> dsCTHD = ctrlPN.LoadDanhsachCTHDByMaSP();
            List<ItemGridSP_SLTon> dsItemGrid = new List<ItemGridSP_SLTon>();

            grdThongtinSPHT.AllowUserToAddRows = true;
            foreach (var sp in ds)
            {
                var ctpn = dsCTPN.Where(x => x.Masp == sp.MaSP).FirstOrDefault();
                var cthd = dsCTHD.Where(x => x.Masp == sp.MaSP).FirstOrDefault();

                if (dsCTPN.Any(x => x.Masp == sp.MaSP) && dsCTHD.Any(x => x.Masp == sp.MaSP))
                {
                    //dgvThongtinSPHT.Rows.Add(ConvertSanPhamtoGridRow(sp, ctpn.TongSL, cthd.TongSL));
                    dsItemGrid.Add(new ItemGridSP_SLTon() { Sanpham = sp, Slton = ctpn.TongSL - cthd.TongSL, Slban = cthd.TongSL });
                }
                else
                {
                    if (dsCTPN.Any(x => x.Masp == sp.MaSP) && ctpn != null)
                    {
                        //dgvThongtinSPHT.Rows.Add(ConvertSanPhamtoGridRow(sp, ctpn.TongSL, 0));
                        dsItemGrid.Add(new ItemGridSP_SLTon() { Sanpham = sp, Slton = ctpn.TongSL, Slban = 0 });
                    }
                    else
                    {
                        //dgvThongtinSPHT.Rows.Add(ConvertSanPhamtoGridRow(sp, 0, 0));
                        dsItemGrid.Add(new ItemGridSP_SLTon() { Sanpham = sp, Slton = 0, Slban = 0 });
                    }
                }
            }

            dsItemGrid.Sort((p1, p2) =>
            {
                if (p1.Slton > p2.Slton)
                {
                    return 1;
                }
                else if (p1.Slton == p2.Slton)
                {
                    return 0;
                }
                return -1;
            });

            dsItemGrid = dsItemGrid.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachSPHT(dsItemGrid);
        }

        //Người thực hiện: Văn Khải =========================================
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 > 0)
            {
                pageNumber--;
                int tongsotrang = (dsTam.Count / numberRecord) + 1;
                txtSoTrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordSP(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 < dsTam.Count / numberRecord)
            {
                pageNumber++;
                int tongsotrang = (dsTam.Count / numberRecord) + 1;
                if(dsTam.Count % numberRecord == 0)
                {
                    tongsotrang = (dsTam.Count / numberRecord);
                }
                txtSoTrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordSP(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnFirstDSSP_Click(object sender, EventArgs e)
        {
            LoadRecordSP(dsTam, 1, numberRecord);
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSoTrang.Text = 1 + " / " + tongsotrang;
        }

        private void btnLastDSSP_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            LoadRecordSP(dsTam, tongsotrang, numberRecord);
            txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
        }
        // ==================================================================

        //Người thực hiện: Văn Khải
        private void btnImportDSSP_Click(object sender, EventArgs e)
        {
            List<SanPham> dsSanPham = new List<SanPham>();
            List<int> dsSLnhap = new List<int>();

            if (MessageBox.Show("Xác nhận nhập thông tin sản phẩm từ tệp ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    string Tensp, Chatlieu, Mausac, Kichthuoc, Loaisp, Donvi, Dongianhap, SLNhap;

                    var package = new ExcelPackage(new FileInfo(openFile.FileName));

                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                    try
                    {
                        for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                        {
                            try
                            {
                                int j = 1;

                                Tensp = workSheet.Cells[i, j++].Value.ToString();
                                Chatlieu = workSheet.Cells[i, j++].Value.ToString();
                                Mausac = workSheet.Cells[i, j++].Value.ToString();
                                Kichthuoc = workSheet.Cells[i, j++].Value.ToString();
                                Loaisp = workSheet.Cells[i, j++].Value.ToString();
                                Donvi = workSheet.Cells[i, j++].Value.ToString();
                                Dongianhap = workSheet.Cells[i, j++].Value.ToString();
                                SLNhap = workSheet.Cells[i, j++].Value.ToString();

                                SanPham sanpham = new SanPham();
                                float gianhap = 0;
                                int slnhap = 0;
                                if (Tensp != "")
                                {
                                    //if(!)
                                    sanpham.MaSP = Helpers.RandomID(Tensp);
                                    sanpham.TenSP = Tensp;
                                    sanpham.Chatlieu = Chatlieu;
                                    sanpham.Mausac = Mausac;
                                    sanpham.Kichthuoc = Kichthuoc;
                                    sanpham.LoaiSP = Loaisp;
                                    sanpham.Donvi = Donvi;
                                    if (float.TryParse(Dongianhap, out gianhap) == true)
                                    {
                                        sanpham.Dongianhap = gianhap;
                                    }
                                    sanpham.Tinhtrang = "Còn hàng";
                                }

                                if (int.TryParse(SLNhap, out slnhap) == true)
                                {
                                    dsSLnhap.Add(slnhap);
                                }

                                dsSanPham.Add(sanpham);
                            }
                            catch (Exception)
                            {

                            }
                        }

                        int y = 0;
                        foreach (var sp in dsSanPham)
                        {
                            ThemSPvaoPN(sp, dsSLnhap[y]);
                            y++;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error!");
                    }
                }
            
            }
            //if(grdDanhsachSPPN.Rows.Count > 0)
            //{
            //    lblTongSLSPNhap.Text = grdDanhsachSPPN.Rows.Count + "";
            //}
            //else
            //{
            //    lblTongSLSPNhap.Text = "0";
            //}
            btnLuuPN.Enabled = true;
        }

        //Người thực hiện: Văn Khải
        private void dgvDanhsachSPPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisableTTSP();
            btnXemCTSP.Enabled = true;
            string tensp = grdDanhsachSPPN.CurrentRow.Cells[0].Value.ToString();
            SanPham SPdangchon = dsSPPNSess.Where(x => x.TenSP == tensp).FirstOrDefault();
            decimal gianhap = 0, slnhapsp = 0;

            if(SPdangchon != null)
            {
                txtTenSP.Text = SPdangchon.TenSP;
                cboDonvi.Text = SPdangchon.Donvi.ToString();
                if(decimal.TryParse(SPdangchon.Dongianhap.ToString(), out gianhap))
                {
                    nudDongianhap.Value = gianhap;
                }
            }
            else
            {
                cboDonvi.ResetText();
                nudDongianhap.ResetText();
            }

            sanphamSess = SPdangchon;
            nudSoluongnhap.Value = 1;
        }

        //Người thực hiện: Thanh Đức
        private void FormLapPhieuNhap_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        //Người thực hiện: Văn Khải
        private void cbTenNCC_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cboTenNCC.SelectedIndex != 0)
            {
                string sdt = ctrlPN.getNhaCungCap(cboTenNCC.SelectedItem.ToString().Trim()).SDT;
                lblSDTNCC.Text = sdt;
            }
        }

        //Người thực hiện: Thanh Đức
        private void nudTienmat_ValueChanged(object sender, EventArgs e)
        {
            //float tienmat = 0, tienthua = 0, congVAT = 0;
            //if(float.TryParse(nudTienmat.Value.ToString(), out tienmat) && float.TryParse(lblTongtienVAT.Text, out congVAT))
            //{
            //    tienthua = tienmat - congVAT;
            //    lblTienthua.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tienthua);
            //}
            float tienmat = 0, tienthua = 0, congVAT = 0;
            if (float.TryParse(nudTienmat.Value.ToString(), out tienmat))
            {
                string[] arrVAT = lblTongtienVAT.Text.Split('.');
                string strVAT = "";
                for (int i = 0; i < arrVAT.Count(); i++)
                {
                    strVAT += arrVAT[i].ToString();
                }
                if (float.TryParse(strVAT, out congVAT))
                {
                    tienthua = tienmat - congVAT;
                    lblTienthua.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tienthua);
                }
            }
        }

        //Người thực hiện: Văn Khải ========================================
        private void btnPrevDSCT_Click(object sender, EventArgs e)
        {
            if (pageNumberCT - 1 > 0)
            {
                int tongsotrang = 0;
                pageNumberCT--;

                if (dsCTPN.Count % numberRecordCT == 0)
                {
                    tongsotrang = (dsCTPN.Count / numberRecordCT);
                }
                else
                {
                    tongsotrang = (dsCTPN.Count / numberRecordCT) + 1;
                }

                txtSotrangDSCT.Text = pageNumberCT.ToString() + " / " + tongsotrang;
                LoadRecordCTPN(dsCTPN, pageNumberCT, numberRecordCT);
            }
        }

        private void btnNextDSCT_Click(object sender, EventArgs e)
        {
            if(pageNumberCT == 1 && dsCTPN.Count == 6)
            {
                return;
            }
            if (pageNumberCT - 1 < dsCTPN.Count / numberRecordCT)
            {
                pageNumberCT++;
                int TongSotrang = (dsCTPN.Count / numberRecordCT) + 1;
                if (dsCTPN.Count % numberRecordCT == 0)
                {
                    TongSotrang = (dsCTPN.Count / numberRecordCT);
                }                
                txtSotrangDSCT.Text = pageNumberCT.ToString() + " / " + TongSotrang;
                LoadRecordCTPN(dsCTPN, pageNumberCT, numberRecordCT);
            }

        }

        private void btnFirstDSCT_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsCTPN.Count / numberRecordCT) + 1;
            txtSotrangDSCT.Text = 1 + " / " + tongsotrang;
            LoadRecordCTPN(dsCTPN, 1, numberRecordCT);
        }

        private void btnLastDSCT_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsCTPN.Count / numberRecordCT) + 1;
            txtSotrangDSCT.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordCTPN(dsCTPN, tongsotrang, numberRecordCT);
        }
        //==================================================================

        //Người thực hiện: Văn Khải 
        private void SetValueTenSP(string tensp)
        {
            sanphamSess.TenSP = tensp;
        }

        //Người thực hiện: Văn Khải 
        private void SetValueThongtinSP(string Tensp, string Chatlieu, string Mausac, string Kichthuoc, string Loaisp, string Donvi, string Dongianhap, string Dongiaban, byte[] Hinhanh)
        {
            float gianhap = 0, giaban = 0;
            sanphamSess.TenSP = Tensp;
            sanphamSess.Chatlieu = Chatlieu;
            sanphamSess.Mausac = Mausac;
            sanphamSess.Kichthuoc = Kichthuoc;
            sanphamSess.LoaiSP = Loaisp;
            sanphamSess.Donvi = Donvi;
            if(float.TryParse(Dongianhap, out gianhap))
            {
                sanphamSess.Dongianhap = gianhap;
            }
            if(float.TryParse(Dongiaban, out giaban))
            {
                sanphamSess.Dongia = giaban;
            }

            sanphamSess.Hinhanh = Hinhanh;

            if(dsSPPNSess.Any(x => x.TenSP == sanphamSess.TenSP))
            {
                if(!dsTam.Any(x => x.TenSP == sanphamSess.TenSP))
                {
                    SanPham SPsua = dsSPPNSess.Where(x => x.TenSP == sanphamSess.TenSP).FirstOrDefault();
                    SPsua.TenSP = sanphamSess.TenSP;
                    SPsua.Chatlieu = sanphamSess.Chatlieu;
                    SPsua.Mausac = sanphamSess.Mausac;
                    SPsua.Kichthuoc = sanphamSess.Kichthuoc;
                    SPsua.LoaiSP = sanphamSess.LoaiSP;
                    SPsua.Donvi = sanphamSess.Donvi;
                    SPsua.Dongianhap = sanphamSess.Dongianhap;
                    SPsua.Dongia = sanphamSess.Dongia;
                    SPsua.Hinhanh = sanphamSess.Hinhanh;
                }
            }
        }

        //Người thực hiện: Văn Khải 
        private void btnXemCTSP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenSP.Text))
            {
                if (ctrlPN.getSanPham(txtTenSP.Text) != null)
                {
                    FormChiTietSanPham frm = new FormChiTietSanPham(SetValueThongtinSP, txtTenSP.Text);
                    frm.ShowDialog();
                }
                else
                {
                    FormChiTietSanPham frm;
                    SanPham sp = dsSPPNSess.Where(x => x.TenSP == txtTenSP.Text).FirstOrDefault();
                    if(sp.Hinhanh != null)
                    {
                        frm = new FormChiTietSanPham(SetValueThongtinSP, sp.TenSP, sp.Chatlieu, sp.Mausac, sp.Kichthuoc, sp.LoaiSP, sp.Donvi, sp.Dongianhap.ToString(), sp.Dongia.ToString(), sp.Hinhanh);
                        frm.ShowDialog();
                    }
                    else
                    {
                        frm = new FormChiTietSanPham(SetValueThongtinSP, sp.TenSP, sp.Chatlieu, sp.Mausac, sp.Kichthuoc, sp.LoaiSP, sp.Donvi, sp.Dongianhap.ToString(), sp.Dongia.ToString(), null);
                        frm.ShowDialog();
                    }
                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void FormLapPhieuNhap_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnImportDSSP.PerformClick(); }
            { if (e.KeyCode == Keys.F3) btnXoakhoiPN.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnThemvaoPN.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnXemCTSP.PerformClick(); }
            { if (e.KeyCode == Keys.F5) btnLuuPN.PerformClick(); }
        }

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLapPhieuNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(dsCTPN.Count > 0)
            {
                if (MessageBox.Show("Xác nhận rời chức năng lập phiếu nhập ? Tiếp tục ? \n\n Thông tin phiếu nhập sẽ không được lưu !", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        //Người thực hiện: Văn Khải 
        private void lblTongtienPN_TextChanged(object sender, EventArgs e)
        {
            float congVAT = tongtien + ((tongtien / 100) * 10);
            lblTongtienVAT.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", congVAT);
        }

        private void btnLamMoiTTPN_Click(object sender, EventArgs e)
        {
            LammoiThongtinPN();
        }
    }
}
