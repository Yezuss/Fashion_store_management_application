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
using QuanLyCuaHangThoiTrangKD.Common;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Models;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormDanhMucSanPham : Form
    {
        SanPhamController ctrlSP = new SanPhamController();

        Bitmap bm = new Bitmap(Application.StartupPath + "\\Resources\\" + "logo_transparent" + ".png");

        int pageNumber = 1;
        int numberRecord = 6;
        List<SanPham> dsTam;

        public FormDanhMucSanPham()
        {
            InitializeComponent();
            LoadComboboxSP();
            //var dsSPHT = ctrlSP.getTatcaSanpham();
            //dsTam = dsSPHT;
            //LoadRecordSP(dsSPHT, pageNumber, numberRecord);

            picHinhanh.Image = bm;
            grdThongtinSP.AllowUserToAddRows = false;
            txtSoTrang.Text = 1 + " / ?";
        }

        //Người thực hiện: Văn Khải
        private void btnTimkiemSP_Click(object sender, EventArgs e)
        {
            grdThongtinSP.Rows.Clear();
            List<int> indexs = new List<int>();
            List<SanPham> itemRemoves = new List<SanPham>();
            List<int> thutuThuoctinh = new List<int>();

            var dsSPTK = ctrlSP.getTatcaSanpham();
            var dsTenhoacma = ctrlSP.Timkiem(txtTenHoacMa.Text.TrimStart().TrimEnd());
            var dsChatlieu = ctrlSP.TimkiemtheoChatlieu(txtChatlieuSP.Text);
            var dsMausac = ctrlSP.TimkiemtheoMausac(cboMausacSP.Text);
            var dsKichthuoc = ctrlSP.TimkiemtheoKichthuoc(cboKichthuocSP.Text);
            var dsLoaiSp = ctrlSP.TimkiemtheoLoaiSP(cboLoaiSP.Text);

            if(dsTenhoacma.Count > 0)
            {
                thutuThuoctinh.Add(1);
            }
            if (dsChatlieu.Count > 0)
            {
                thutuThuoctinh.Add(2);
            }
            if (dsMausac.Count > 0)
            {
                thutuThuoctinh.Add(3);
            }
            if (dsKichthuoc.Count > 0)
            {
                thutuThuoctinh.Add(4);
            }
            if (dsLoaiSp.Count > 0)
            {
                thutuThuoctinh.Add(5);
            }

            foreach(int thutu in thutuThuoctinh)
            {
                switch (thutu)
                {
                    case 1:
                        dsSPTK = dsSPTK.Where(x => dsTenhoacma.Any(y => y.MaSP == x.MaSP)).ToList();
                        break;
                    case 2:
                        dsSPTK = dsSPTK.Where(x => dsChatlieu.Any(y => y.MaSP == x.MaSP)).ToList();
                        break;
                    case 3:
                        dsSPTK = dsSPTK.Where(x => dsMausac.Any(y => y.MaSP == x.MaSP)).ToList();
                        break;
                    case 4:
                        dsSPTK = dsSPTK.Where(x => dsKichthuoc.Any(y => y.MaSP == x.MaSP)).ToList();
                        break;
                    case 5:
                        dsSPTK = dsSPTK.Where(x => dsLoaiSp.Any(y => y.MaSP == x.MaSP)).ToList();
                        break;
                }
            }

            if(thutuThuoctinh.Count == 0)
            {
                dsSPTK = new List<SanPham>();
            }

            dsTam = dsSPTK;
            int tongsotrang = (dsSPTK.Count / numberRecord) + 1;
            if (dsSPTK.Count % numberRecord == 0)
            {
                tongsotrang = (dsSPTK.Count / numberRecord) + 1;
            }
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            LoadRecordSP(dsSPTK, pageNumber, numberRecord);
        }

        void LoadDanhsachTTSP(List<SanPham> dsSP)
        {
            grdThongtinSP.Rows.Clear();
            grdThongtinSP.AllowUserToAddRows = true;

            foreach (var sp in dsSP)
            {
                grdThongtinSP.Rows.Add(ConvertSanPhamtoGridViewRow(sp));
            }

            grdThongtinSP.AllowUserToAddRows = false;
        }

        //Người thực hiện: Văn Khải
        private void btnXoarong_Click(object sender, EventArgs e)
        {
            txtTenHoacMa.Clear();
            txtChatlieuSP.Clear();
            cboKichthuocSP.SelectedIndex = -1;
            cboMausacSP.SelectedIndex = -1;
            cboLoaiSP.SelectedIndex = -1;

            picHinhanh.Image = bm;

            grdThongtinSP.Rows.Clear();
        }

        DataGridViewRow ConvertSanPhamtoGridViewRow(SanPham sp)
        {
            DataGridViewRow row = (DataGridViewRow)grdThongtinSP.Rows[0].Clone();
            row.Cells[0].Value = sp.MaSP.ToString();
            row.Cells[1].Value = sp.TenSP.ToString();
            row.Cells[2].Value = sp.Chatlieu.ToString();
            row.Cells[3].Value = sp.Kichthuoc.ToString();
            row.Cells[4].Value = sp.Mausac.ToString();
            row.Cells[5].Value = sp.LoaiSP.ToString();
            row.Cells[6].Value = sp.Donvi.ToString();
            row.Cells[7].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongianhap);
            row.Cells[8].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongia);
            row.Cells[9].Value = sp.Tinhtrang.ToString();

            return row;
        }

        void LoadComboboxSP()
        {
            cboKichthuocSP.Items.Add("S");
            cboKichthuocSP.Items.Add("M");
            cboKichthuocSP.Items.Add("L");
            cboKichthuocSP.Items.Add("XL");
            cboKichthuocSP.Items.Add("XXL");

            cboLoaiSP.Items.Add("Quần");
            cboLoaiSP.Items.Add("Áo");
            cboLoaiSP.Items.Add("Áo khoát");
            cboLoaiSP.Items.Add("Đặc biệt");
            cboLoaiSP.Items.Add("Phụ kiện");
            cboLoaiSP.Items.Add("Quần ngắn");

            cboMausacSP.Items.Add("Cam");
            cboMausacSP.Items.Add("Trắng");
            cboMausacSP.Items.Add("Đen");
            cboMausacSP.Items.Add("Vàng");
            cboMausacSP.Items.Add("Trắng xám");
            cboMausacSP.Items.Add("Xanh xám");
            cboMausacSP.Items.Add("Xanh dương");
            cboMausacSP.Items.Add("Hồng");
            cboMausacSP.Items.Add("Xanh lá");
            cboMausacSP.Items.Add("Nâu");
            cboMausacSP.Items.Add("Xanh rêu");
        }

        //Người thực hiện: Văn Khải
        private void dgvThongtinSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string masp = grdThongtinSP.CurrentRow.Cells[0].Value.ToString();
            SanPham SPdangchon = ctrlSP.getSanPham(masp);

            for (int i = 0; i < grdThongtinSP.CurrentRow.Cells.Count; i++)
            {
                if (grdThongtinSP.CurrentRow.Cells[i].Value.ToString() != "")
                {
                    switch (i)
                    {
                        case 1:
                            txtTenHoacMa.Text = grdThongtinSP.CurrentRow.Cells[1].Value.ToString();
                            break;
                        case 2:
                            txtChatlieuSP.Text = grdThongtinSP.CurrentRow.Cells[2].Value.ToString();
                            break;
                        case 3:
                            cboKichthuocSP.Text = grdThongtinSP.CurrentRow.Cells[4].Value.ToString();
                            break;
                        case 4:
                            cboMausacSP.Text = grdThongtinSP.CurrentRow.Cells[3].Value.ToString();
                            break;
                        case 5:
                            cboLoaiSP.Text = grdThongtinSP.CurrentRow.Cells[5].Value.ToString();
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            txtTenHoacMa.Text = "";
                            break;
                        case 2:
                            txtChatlieuSP.Text = "";
                            break;
                        case 3:
                            cboMausacSP.ResetText();
                            break;
                        case 4:
                            cboKichthuocSP.ResetText();
                            break;
                        case 5:
                            cboLoaiSP.ResetText();
                            break;
                    }
                }
            }

            if (SPdangchon.Hinhanh != null)
            {
                picHinhanh.Image = Helpers.convertByteToImg(Convert.ToBase64String(SPdangchon.Hinhanh));
            }
            else
            {
                picHinhanh.Image = bm;
            }
        }

        void LoadRecordSP(List<SanPham> ds, int page, int recordNum)
        {
            List<SanPham> dsSP = new List<SanPham>();

            dsSP = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTSP(dsSP);
        }

        //Người thực hiện: Văn Khải=========================================
        private void btnNext_Click_1(object sender, EventArgs e)
        {
            if (dsTam.Count % numberRecord != 0)
            {
                if (pageNumber - 1 < dsTam.Count / numberRecord)
                {
                    pageNumber++;
                    int TongSotrang = (dsTam.Count / numberRecord) + 1;
                    txtSoTrang.Text = pageNumber.ToString() + " / " + TongSotrang;
                    LoadRecordSP(dsTam, pageNumber, numberRecord);
                }
            }
            if (dsTam.Count % numberRecord == 0)
            {
                if (pageNumber < dsTam.Count / numberRecord)
                {
                    int tongsotrang = 0;
                    pageNumber++;
                    tongsotrang = (dsTam.Count / numberRecord);
                    txtSoTrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                    LoadRecordSP(dsTam, pageNumber, numberRecord);
                }
            }
        }

        private void btnPrev_Click_1(object sender, EventArgs e)
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
        //=================================================================

        //Người thực hiện: Thanh Đức
        private void FormDanhMucSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnXoarong.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnTimkiemSP.PerformClick(); }
        }

        //Người thực hiện: Thanh Đức
        private void FormDanhMucSanPham_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdThongtinSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string masp = "";
            if (grdThongtinSP.CurrentRow.Cells[0].Value != null)
            {
                masp = grdThongtinSP.CurrentRow.Cells[0].Value.ToString();
            }
            
            if(dsTam.Count > 0)
            {
                SanPham sp = dsTam.Where(x => x.MaSP == masp).FirstOrDefault();
                if(sp != null)
                {
                    if(sp.Hinhanh != null)
                    {
                        picHinhanh.Image = Helpers.convertByteToImg(Convert.ToBase64String(sp.Hinhanh));
                    }
                    else
                    {
                        picHinhanh.Image = bm;
                    }
                }
            }
        }
    }
}
