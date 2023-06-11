using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Controller;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.Common;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongTinNCC : Form
    {
        SanPhamController ctrlSP = new SanPhamController();

        int pageNumber = 1, numberRecord = 5;

        List<NhaCungCap> dsTam;

        public FormThongTinNCC()
        {
            InitializeComponent();


            btnXoarongTTNCC.Enabled = false;
            DisableTTNCC();
        }
        void EnableTTNCC()
        {
            txtDiachiNCC.Enabled = true;
            txtTenNCC.Enabled = true;
            txtEmailNCC.Enabled = true;
            txtSDTNCC.Enabled = true;
        }

        void DisableTTNCC()
        {
            txtDiachiNCC.Enabled = false;
            txtTenNCC.Enabled = false;
            txtEmailNCC.Enabled = false;
            txtSDTNCC.Enabled = false;
        }

        //Người thực hiện: Thanh Đức
        private void dgvDanhsachTTNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnCapnhatNCC.Text == "Hủy") 
            {
                for (int i = 0; i < grdDanhsachTTNCC.CurrentRow.Cells.Count; i++)
                {
                    if (grdDanhsachTTNCC.CurrentRow.Cells[i].Value.ToString() != "")
                    {
                        switch (i)
                        {
                            case 1:
                                txtTenNCC.Text = grdDanhsachTTNCC.CurrentRow.Cells[1].Value.ToString();
                                break;
                            case 2:
                                txtSDTNCC.Text = grdDanhsachTTNCC.CurrentRow.Cells[2].Value.ToString();
                                break;
                            case 3:
                                txtDiachiNCC.Text = grdDanhsachTTNCC.CurrentRow.Cells[3].Value.ToString();
                                break;
                            case 4:
                                txtEmailNCC.Text = grdDanhsachTTNCC.CurrentRow.Cells[4].Value.ToString();
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 1:
                                txtTenNCC.Text = "";
                                break;
                            case 2:
                                txtSDTNCC.Text = "";
                                break;
                            case 3:
                                txtDiachiNCC.ResetText();
                                break;
                            case 4:
                                txtEmailNCC.ResetText();
                                break;
                        }
                    }
                }
            }
        }

        void LoadRecordNCC(List<NhaCungCap> ds, int page, int recordNum)
        {
            List<NhaCungCap> dsKH = new List<NhaCungCap>();

            dsKH = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachNCC(dsKH);
        }

        void LoadDanhsachNCC(List<NhaCungCap> ds)
        {
            grdDanhsachTTNCC.Rows.Clear();
            grdDanhsachTTNCC.AllowUserToAddRows = true;

            foreach (var ncc in ds)
            {
                DataGridViewRow row = (DataGridViewRow)grdDanhsachTTNCC.Rows[0].Clone();
                row.Cells[0].Value = ncc.MaNCC.ToString();
                row.Cells[1].Value = ncc.TenNCC.ToString();
                row.Cells[2].Value = ncc.SDT.ToString();
                row.Cells[3].Value = ncc.Diachi.ToString();
                row.Cells[4].Value = ncc.Email.ToString();

                grdDanhsachTTNCC.Rows.Add(row);
            }

            grdDanhsachTTNCC.AllowUserToAddRows = false;
        }

        //Người thực hiện: Thanh Đức
        private void btnThemTTNCC_Click(object sender, EventArgs e)
        {
            if (btnThemTTNCC.Text == "Thêm (F3)")
            {
                btnCapnhatNCC.Text = "Lưu";
                btnThemTTNCC.Text = "Hủy";
                btnXoarongTTNCC.Enabled = true;
                XoaRongThongtinNCC();
                EnableTTNCC();
            }
            else if (btnThemTTNCC.Text == "Hủy")
            {
                btnCapnhatNCC.Text = "Cập nhật (F4)";
                btnThemTTNCC.Text = "Thêm (F3)";
                btnXoarongTTNCC.Enabled = false;

                DisableTTNCC();
            }
            else if (btnThemTTNCC.Text == "Lưu")
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    string mancc = grdDanhsachTTNCC.CurrentRow.Cells[0].Value.ToString();
                    NhaCungCap ncc = ctrlSP.getNhaCungCap(mancc);
                    ncc.TenNCC = txtDiachiNCC.Text;
                    ncc.SDT = txtTenNCC.Text;
                    ncc.Diachi = txtEmailNCC.Text;
                    ncc.Email = txtSDTNCC.Text;

                    ctrlSP.CapnhatNhacungcap();

                    if (MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        DisableTTNCC();
                        btnCapnhatNCC.Text = "Cập nhật";
                        btnThemTTNCC.Text = "Thêm";
                        btnXoarongTTNCC.Enabled = false;

                        LoadRecordNCC(dsTam, pageNumber, numberRecord);
                        int TongSotrang = (dsTam.Count / numberRecord) + 1;
                        if (dsTam.Count % numberRecord == 0)
                        {
                            TongSotrang = (dsTam.Count / numberRecord);
                        }
                        txtSoTrang.Text = 1 + " / " + TongSotrang;
                    }
                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void btnCapnhatNCC_Click(object sender, EventArgs e)
        {
            if (btnCapnhatNCC.Text == "Cập nhật (F4)")
            {
                btnCapnhatNCC.Text = "Hủy";
                btnThemTTNCC.Text = "Lưu";
                btnXoarongTTNCC.Enabled = true;

                EnableTTNCC();
            }
            else if (btnCapnhatNCC.Text == "Hủy")
            {
                btnCapnhatNCC.Text = "Cập nhật (F4)";
                btnThemTTNCC.Text = "Thêm (F3)";
                btnXoarongTTNCC.Enabled = false;
                XoaRongThongtinNCC();
                DisableTTNCC();
            }
            else if (btnCapnhatNCC.Text == "Lưu")
            {
                Random ran = new Random();
                NhaCungCap ncc = new NhaCungCap();
                ncc.MaNCC = "NCC" + ran.Next(99, 1000);
                ncc.TenNCC = txtTenNCC.Text;
                ncc.SDT = txtSDTNCC.Text;
                ncc.Diachi = txtDiachiNCC.Text;
                ncc.Email = txtEmailNCC.Text;

                ctrlSP.LuuNhacungcap(ncc);

                MessageBox.Show("Thêm thông tin nhà cung cấp thành công !", "Thông báo");

                var dsNCC = ctrlSP.getTatcaNhaCungCap();
                dsTam = dsNCC.ToList();
                LoadRecordNCC(dsTam, pageNumber, numberRecord);

                int TongSotrang = (dsTam.Count / numberRecord) + 1;
                if(dsTam.Count % numberRecord == 0)
                {
                    TongSotrang = (dsTam.Count / numberRecord);
                }
                txtSoTrang.Text = 1 + " / " + TongSotrang;
            }
        }

        //Người thực hiện: Thanh Đức - Văn Khải ==========================
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
                LoadRecordNCC(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dsTam.Count % numberRecord != 0)
            {
                if (pageNumber - 1 < dsTam.Count / numberRecord)
                {
                    pageNumber++;
                    int TongSotrang = (dsTam.Count / numberRecord) + 1;
                    txtSoTrang.Text = pageNumber.ToString() + " / " + TongSotrang;
                    LoadRecordNCC(dsTam, pageNumber, numberRecord);
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
                    LoadRecordNCC(dsTam, pageNumber, numberRecord);
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            if(dsTam.Count % numberRecord == 0)
            {
                tongsotrang = (dsTam.Count / numberRecord);
            }
            txtSoTrang.Text = 1 + " / " + tongsotrang;
            LoadRecordNCC(dsTam, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSoTrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordNCC(dsTam, tongsotrang, numberRecord);
        }
        //================================================================

        //Người thực hiện: Thanh Đức
        private void btnTimkiemNCC_Click(object sender, EventArgs e)
        {
            var dsNCC = ctrlSP.getTatcaNhaCungCap();

            if (!string.IsNullOrEmpty(txtTimkiemNCC.Text))
            {
                dsNCC = dsNCC.Where(x => x.TenNCC.ToLower().Contains(txtTimkiemNCC.Text.ToLower()) || x.MaNCC == txtTimkiemNCC.Text.ToUpper()).ToList();
            }

            dsTam = dsNCC;
            LoadRecordNCC(dsNCC, pageNumber, numberRecord);

            int TongSotrang = (dsTam.Count / numberRecord) + 1;
            if (dsTam.Count % numberRecord == 0)
            {
                TongSotrang = (dsTam.Count / numberRecord);
            }
            txtSoTrang.Text = 1 + " / " + TongSotrang;
        }

        private void btnXoarongTTNCC_Click(object sender, EventArgs e)
        {
            XoaRongThongtinNCC();
        }

        void XoaRongThongtinNCC()
        {
            txtTenNCC.Clear();
            txtSDTNCC.Clear();
            txtEmailNCC.Clear();
            txtDiachiNCC.Clear();
        }

        //Người thực hiện: Thanh Đức
        private void FormThongTinNCC_KeyDown(object sender, KeyEventArgs e)
        {
            { if (e.KeyCode == Keys.F1) btnTimkiemNCC.PerformClick(); }
            { if (e.KeyCode == Keys.F2) btnCapnhatNCC.PerformClick(); }
            { if (e.KeyCode == Keys.F3) btnThemTTNCC.PerformClick(); }
            { if (e.KeyCode == Keys.F4) btnXoarongTTNCC.PerformClick(); }
        }

        //Người thực hiện: Thanh Đức
        private void FormThongTinNCC_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        //Người thực hiện: Thanh Đức ==================================================
        private void tbSDTNCC_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "^[0-9]{5,10}$";

            if (Regex.IsMatch(txtSDTNCC.Text, pattern))
            {
                e.Cancel = false;
                errSDTNCC.SetError(this.txtSDTNCC, null);
            }
            else
            {
                e.Cancel = true;
                txtSDTNCC.Focus();
                errSDTNCC.SetError(this.txtSDTNCC, "Số điện thoại phải là chuỗi chữ số và không nhập chứa chữ cái ! Vui lòng nhập lại ");
            }
        }

        private void tbEmailNCC_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "^[A-Za-z][A-Za-z0-9]{5,32}@[a-z0-9]{2,}(\\.[a-z0-9]{2,4}){1,2}$";

            if (Regex.IsMatch(txtEmailNCC.Text, pattern))
            {
                e.Cancel = false;
                errEmailNCC.SetError(this.txtEmailNCC, null);
            }
            else
            {
                e.Cancel = true;
                txtEmailNCC.Focus();
                errEmailNCC.SetError(this.txtEmailNCC, "Email phải là kiểu định dạng Email (abc@xyz.com)");
            }
        }

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //=============================================================================

    }
}
