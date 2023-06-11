using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Common;
using System.Globalization;
using QuanLyCuaHangThoiTrangKD.Models;
using System.Text.RegularExpressions;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongTinSanPham : Form
    {
        SanPhamController ctrlSP = new SanPhamController();
        SanPham SPdangchon = new SanPham();

        List<SanPham> dsTam;
        byte[] tempHinhanh;

        int pageNumber = 1;
        int numberRecord = 4;

        Bitmap bitmap = new Bitmap(Application.StartupPath + "\\Resources\\" + "logo_transparent" + ".png");

        //Người thực hiện: Văn Khải
        public FormThongTinSanPham()
        {
            InitializeComponent();

            LoadComboboxSP();
            DisableTTSP();
            btnLuuTTSP.Enabled = false;
            btnXoarongTTSP.Enabled = false;
            var dsSPHT = ctrlSP.getTatcaSanpham();
            dsTam = dsSPHT;
            LoadRecordSP(dsSPHT, pageNumber, numberRecord);

            int tongsotrang = (dsSPHT.Count / numberRecord) + 1;
            if(dsSPHT.Count % numberRecord == 0)
            {
                tongsotrang = (dsSPHT.Count / numberRecord) + 1;
            }
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            Bitmap bm = new Bitmap(Application.StartupPath + "\\Resources\\" + "logo_transparent" + ".png");
            picHinhanhSP.Image = bm;
        }

        //Người thực hiện: Văn Khải
        private void btnTimkiemSP_Click(object sender, EventArgs e)
        {
            var dsSanpham = new List<SanPham>();
            if (txtThongtinTKSP.Text == "")
            {
                //MessageBox.Show("Vui lòng nhập thông tin tìm kiếm !");
                dsSanpham = ctrlSP.getTatcaSanpham();
            }
            else
            {
                dsSanpham = ctrlSP.Timkiem(txtThongtinTKSP.Text);
            }

            LoadRecordSP(dsSanpham, pageNumber, numberRecord);
            dsTam = dsSanpham;
        }

        //Người thực hiện: Văn Khải
        private void dgvDanhsachTTSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string masp = grdDanhsachTTSP.CurrentRow.Cells[0].Value.ToString();
            SPdangchon = ctrlSP.getSanPham(masp);
            decimal gianhap = 0, giaban = 0;

            for (int i = 0; i < grdDanhsachTTSP.CurrentRow.Cells.Count; i++)
            {
                if (grdDanhsachTTSP.CurrentRow.Cells[i].Value != null)
                {
                    switch (i)
                    {
                        case 1:
                            txtTenSP.Text = grdDanhsachTTSP.CurrentRow.Cells[1].Value.ToString();
                            break;
                        case 2:
                            txtChatlieuSP.Text = grdDanhsachTTSP.CurrentRow.Cells[2].Value.ToString();
                            break;
                        case 3:
                            cboMausacSP.Text = grdDanhsachTTSP.CurrentRow.Cells[3].Value.ToString();
                            break;
                        case 4:
                            cboKichthuocSP.Text = grdDanhsachTTSP.CurrentRow.Cells[4].Value.ToString();
                            break;
                        case 5:
                            cboLoaiSP.Text = grdDanhsachTTSP.CurrentRow.Cells[5].Value.ToString();
                            break;
                        case 6:
                            cboDonviSP.Text = grdDanhsachTTSP.CurrentRow.Cells[6].Value.ToString();
                            break;
                        case 7:
                            if (SPdangchon != null && SPdangchon.Dongianhap > 0)
                            {
                                decimal.TryParse(SPdangchon.Dongianhap.ToString(), out gianhap);
                                nudDongianhapSP.Value = gianhap;
                            }
                            else
                            {
                                nudDongianhapSP.ResetText();
                            }
                            break;
                        case 8:
                            if(SPdangchon != null && SPdangchon.Dongia > 0)
                            {
                                decimal.TryParse(SPdangchon.Dongia.ToString(), out giaban);
                                nudDongiaSP.Value = giaban;
                            }
                            else
                            {
                                nudDongiaSP.ResetText();
                            }
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            txtTenSP.Text = "";
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
                        case 6:
                            cboDonviSP.ResetText();
                            break;
                        case 7:
                            nudDongianhapSP.ResetText();
                            break;
                        case 8:
                            nudDongiaSP.ResetText();
                            break;
                    }
                }
            }

            if (SPdangchon.Hinhanh != null)
            {
                picHinhanhSP.Image = Helpers.convertByteToImg(Convert.ToBase64String(SPdangchon.Hinhanh));
            }
            else
            {
                Bitmap bm = new Bitmap(Application.StartupPath + "\\Resources\\" + "logo_transparent" + ".png");
                picHinhanhSP.Image = bm;
            }
        }

        //Người thực hiện: Văn Khải
        private void btnCapnhatTTSP_Click(object sender, EventArgs e)
        {
            if (btnCapnhatTTSP.Text == "Cập nhật (F2)")
            {
                EnableTTSP();
                btnCapnhatTTSP.Text = "Hủy";
                btnLuuTTSP.Enabled = true;
                btnXoarongTTSP.Enabled = true;
            }
            else
            {
                DisableTTSP();
                btnCapnhatTTSP.Text = "Cập nhật (F2)";
                btnLuuTTSP.Enabled = false;
                btnXoarongTTSP.Enabled = false;
            }
        }

        //Người thực hiện: Văn Khải
        private void btnLuuTTSP_Click(object sender, EventArgs e)
        {
            float gianhap = 0, giaban = 0;
            //MessageBox.Show("Ten sp: " + SPdangchon.TenSP + " - " + Convert.ToBase64String(SPdangchon.Hinhanh), "thông báo");
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                SPdangchon.TenSP = txtTenSP.Text;
                SPdangchon.Chatlieu = txtChatlieuSP.Text;
                SPdangchon.Mausac = cboMausacSP.Text;
                SPdangchon.Kichthuoc = cboKichthuocSP.Text;
                SPdangchon.LoaiSP = cboLoaiSP.Text;
                SPdangchon.Donvi = cboDonviSP.Text;
                if (!string.IsNullOrEmpty(nudDongianhapSP.Value.ToString()))
                {
                    float.TryParse(nudDongianhapSP.Value.ToString(), out gianhap);
                    SPdangchon.Dongianhap = gianhap;
                }
                if (!string.IsNullOrEmpty(nudDongiaSP.Value.ToString()))
                {
                    float.TryParse(nudDongiaSP.Value.ToString(), out giaban);
                    SPdangchon.Dongia = float.Parse(nudDongiaSP.Value.ToString());
                }                            
                SPdangchon.Hinhanh = tempHinhanh;

                //SPdangchon.Tinhtrang = cbTinhtrangSP.Text;

                ctrlSP.CapnhatSanpham(SPdangchon);

                var dsSPHT = ctrlSP.getTatcaSanpham();

                if (MessageBox.Show("Cập nhật thông tin sản phẩm thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    DisableTTSP();
                    btnCapnhatTTSP.Text = "Cập nhật";
                    btnLuuTTSP.Enabled = false;
                    txtSoTrang.Text = "1";
                    pageNumber = 1;
                    dsTam = dsSPHT;
                    LoadRecordSP(dsSPHT, pageNumber, numberRecord);
                    btnXoarongTTSP.Enabled = false;
                    XoarongThongtinSP();
                }
                else
                {
                    dsTam = dsSPHT;
                    LoadRecordSP(dsSPHT, pageNumber, numberRecord);
                }
            }
        }

        //Người thực hiện: Văn Khải
        private void btnXoarongTTSP_Click(object sender, EventArgs e)
        {
            XoarongThongtinSP();
        }

        void XoarongThongtinSP()
        {
            txtTenSP.ResetText();
            txtChatlieuSP.ResetText();
            cboMausacSP.ResetText();
            cboKichthuocSP.ResetText();
            cboLoaiSP.ResetText();
            cboDonviSP.ResetText();
            nudDongianhapSP.ResetText();
            nudDongiaSP.ResetText();
            picHinhanhSP.Image = bitmap;
            //cbTinhtrangSP.ResetText();
        }

        DataGridViewRow ConvertSanPhamtoGridViewRow(SanPham sp)
        {
            DataGridViewRow row = (DataGridViewRow)grdDanhsachTTSP.Rows[0].Clone();
            row.Cells[0].Value = sp.MaSP.ToString();
            row.Cells[1].Value = sp.TenSP.ToString();
            row.Cells[2].Value = sp.Chatlieu.ToString();
            row.Cells[3].Value = sp.Mausac.ToString();
            row.Cells[4].Value = sp.Kichthuoc.ToString();
            row.Cells[5].Value = sp.LoaiSP.ToString();
            row.Cells[6].Value = sp.Donvi.ToString();
            row.Cells[7].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongianhap);
            row.Cells[8].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongia);
            row.Cells[9].Value = sp.Tinhtrang.ToString();

            return row;
        }

        void LoadComboboxSP()
        {
            //cbTinhtrangSP.Items.Add("Còn hàng");
            //cbTinhtrangSP.Items.Add("Hết hàng");
            //cbTinhtrangSP.Items.Add("Ngừng kinh doanh");

            cboKichthuocSP.Items.Add("S");
            cboKichthuocSP.Items.Add("M");
            cboKichthuocSP.Items.Add("L");
            cboKichthuocSP.Items.Add("XL");
            cboKichthuocSP.Items.Add("XXL");

            cboDonviSP.Items.Add("Đôi");
            cboDonviSP.Items.Add("Chiếc");
            cboDonviSP.Items.Add("Bộ");

            cboLoaiSP.Items.Add("Quần");
            cboLoaiSP.Items.Add("Áo");
            cboLoaiSP.Items.Add("Áo khoát");
            cboLoaiSP.Items.Add("Đặc biệt");
            cboLoaiSP.Items.Add("Phụ kiện");

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

        void DisableTTSP()
        {
            txtTenSP.Enabled = false;
            txtChatlieuSP.Enabled = false;
            cboMausacSP.Enabled = false;
            cboKichthuocSP.Enabled = false;
            cboLoaiSP.Enabled = false;
            cboDonviSP.Enabled = false;
            nudDongianhapSP.Enabled = false;
            nudDongiaSP.Enabled = false;
            //cbTinhtrangSP.Enabled = false;
            btnChonanhSP.Enabled = false;
        }

        void EnableTTSP()
        {
            txtTenSP.Enabled = true;
            txtChatlieuSP.Enabled = true;
            cboMausacSP.Enabled = true;
            cboKichthuocSP.Enabled = true;
            cboLoaiSP.Enabled = true;
            cboDonviSP.Enabled = true;
            nudDongianhapSP.Enabled = true;
            nudDongiaSP.Enabled = true;
            //cbTinhtrangSP.Enabled = true;
            btnChonanhSP.Enabled = true;
        }

        void LoadDanhsachTTSP(List<SanPham> ds)
        {
            grdDanhsachTTSP.Rows.Clear();
            grdDanhsachTTSP.AllowUserToAddRows = true;

            foreach (var sp in ds)
            {
                grdDanhsachTTSP.Rows.Add(ConvertSanPhamtoGridViewRow(sp));
            }

            grdDanhsachTTSP.AllowUserToAddRows = false;
        }

        private void btnThoatTTSP_Click(object sender, EventArgs e)
        {
            this.Close();
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

        void LoadRecordSP(List<SanPham> ds, int page, int recordNum)
        {
            List<SanPham> dsSP = new List<SanPham>();

            dsSP = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTSP(dsSP);
        }

        //Người thực hiện: Văn Khải ====================================================
        private void tbTenSP_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "[a-zA-ZÀ-Ỵà-ỵ]+(([',. -][a-zA-ZÀ-Ỵà-ỵ ])?[a-zA-ZÀ-Ỵà-ỵ ]*)*$";

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

        private void tbChatlieuSP_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "[a-zA-Z0-9À-Ỵà-ỵ]+(([',. -][a-zA-Z0-9À-Ỵà-ỵ ])?[a-zA-Z0-9À-Ỵà-ỵ ]*)*$";

            if (!string.IsNullOrEmpty(txtChatlieuSP.Text))
            {
                e.Cancel = false;
                errTenSP.SetError(this.txtChatlieuSP, null);
            }
            else
            {
                e.Cancel = true;
                txtChatlieuSP.Focus();
                errChatlieu.SetError(this.txtChatlieuSP, "Chât liệu không được rỗng!");
            }
        }

        private void FormThongTinSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnTimkiemSP.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnCapnhatTTSP.PerformClick(); }
            { if (e.KeyCode == Keys.F3) btnLuuTTSP.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnXoarongTTSP.PerformClick(); }
            { if (e.KeyCode == Keys.F5) btnChonanhSP.PerformClick(); }
        }

        private void FormThongTinSanPham_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        //===============================================================================

        //Người thực hiện: Văn Khải =================================================
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
        //==========================================================================
    }
}
