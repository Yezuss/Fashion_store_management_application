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
    public partial class FormDanhsachHangchoHoadon : Form
    {
        public GuiThongtinHoadon sendThongtin;
            
        HoaDonController ctrlHD = new HoaDonController();

        List<HoaDon> dsTam;

        int pageNumber = 1, numberRecord = 8;

        //Người thực hiện: Văn Khải
        public FormDanhsachHangchoHoadon(GuiThongtinHoadon sender)
        {
            InitializeComponent();

            this.sendThongtin = sender;

            var dsHDdangcho = ctrlHD.getHoadonbyTinhtrang("Đang chờ");

            dsHDdangcho = dsHDdangcho.Where(x => x.Ngaylap.Day == DateTime.Today.Day && x.Ngaylap.Month == DateTime.Today.Month && x.Ngaylap.Year == DateTime.Today.Year && x.Calamviec == Helpers.KiemtraCalamviecHientai()).ToList();

            dsHDdangcho.Sort((p1, p2) =>
            {
                string strp1Ma = p1.MaHD.Substring(0, 6);
                string strp2Ma = p2.MaHD.Substring(0, 6);
                int map1 = 0, map2 = 0;
                int.TryParse(strp1Ma, out map1);
                int.TryParse(strp2Ma, out map2);

                if (map1 < map2)
                {
                    return 1;
                }
                else if (map1 == map2)
                {
                    return 0;
                }
                return -1;
            });

            dsTam = dsHDdangcho.ToList();
            LoadRecordHDDangcho(dsHDdangcho, pageNumber, numberRecord);
            int tongST = (dsHDdangcho.Count / numberRecord) + 1;
            if(dsHDdangcho.Count % numberRecord == 0)
            {
                tongST = dsHDdangcho.Count / numberRecord;
            }
            txtSotrang.Text = 1 + " / " + tongST;
        }

        void LoadRecordHDDangcho(List<HoaDon> ds, int page, int recordNum)
        {
            List<HoaDon> dsHD = new List<HoaDon>();

            dsHD = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTHD(dsHD);
        }

        void LoadDanhsachTTHD(List<HoaDon> ds)
        {
            grdDanhsachHDHangcho.Rows.Clear();
            grdDanhsachHDHangcho.AllowUserToAddRows = true;

            foreach (var hd in ds)
            {
                grdDanhsachHDHangcho.Rows.Add(ConvertHoadontoGridViewRow(hd));
            }

            grdDanhsachHDHangcho.AllowUserToAddRows = false;
        }

        //Người thực hiện: Thanh Đức ===================================
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

                txtSotrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordHDDangcho(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(pageNumber == 1 && dsTam.Count == numberRecord)
            {
                return;
            }
            if (pageNumber - 1 < dsTam.Count / numberRecord)
            {
                pageNumber++;
                int TongSotrang = (dsTam.Count / numberRecord) + 1;
                txtSotrang.Text = pageNumber.ToString() + " / " + TongSotrang;
                LoadRecordHDDangcho(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSotrang.Text = 1 + " / " + tongsotrang;
            LoadRecordHDDangcho(dsTam, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSotrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordHDDangcho(dsTam, tongsotrang, numberRecord);
        }
        //==============================================================

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Người thực hiện: Văn Khải
        private void btnThanhtoanHDHangcho_Click(object sender, EventArgs e)
        {
            HoaDon hdChon;
            string mahd = "";
            if (grdDanhsachHDHangcho.CurrentRow.Cells[0].Value != null)
            {
                mahd = grdDanhsachHDHangcho.CurrentRow.Cells[0].Value.ToString();
                hdChon = dsTam.Where(x => x.MaHD == mahd).FirstOrDefault();
                if(hdChon != null)
                {
                    this.sendThongtin(mahd, hdChon.Ngaylap, hdChon.Calamviec);
                }
            }
            this.Close();
        }

        //Người thực hiện: Văn Khải
        private void btnXoaHDHangcho_Click(object sender, EventArgs e)
        {
            HoaDon hdChon;
            string mahd = "";
            if(MessageBox.Show("Xác nhận xóa thông tin hóa đơn chờ thanh toán !", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (grdDanhsachHDHangcho.CurrentRow.Cells[0].Value != null)
                {
                    mahd = grdDanhsachHDHangcho.CurrentRow.Cells[0].Value.ToString();
                    hdChon = dsTam.Where(x => x.MaHD == mahd).FirstOrDefault();
                    if (hdChon != null)
                    {
                        try
                        {
                            ctrlHD.HuyHoaDon(hdChon);
                            ctrlHD.CapnhatHoadon();

                            var dsHangcho = ctrlHD.getHoadonbyTinhtrang("Đang chờ");
                            dsHangcho = dsHangcho.Where(x => x.Ngaylap.Day == DateTime.Today.Day && x.Ngaylap.Month == DateTime.Today.Month && x.Ngaylap.Year == DateTime.Today.Year && x.Calamviec == Helpers.KiemtraCalamviecHientai()).ToList();
                            dsTam = dsHangcho.ToList();
                            LoadRecordHDDangcho(dsHangcho, pageNumber, numberRecord);
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        //Người thực hiện: Thanh Đức
        private void btnXoaTatcaHDHangcho_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Xác nhận xóa tất cả hóa đơn chờ thanh toán ! Tiếp tục ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                grdDanhsachHDHangcho.Rows.Clear();
                foreach (var hd in dsTam)
                {
                    ctrlHD.HuyHoaDon(hd);
                }
                ctrlHD.CapnhatHoadon();
            }
        }

        //Người thực hiện: Thanh Đức
        private void btnTImkiemHDHangcho_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSDTKH.Text))
            {
                List<HoaDon> dshd = ctrlHD.getHoadonbySDThoacTenKH(txtSDTKH.Text);
                dshd = dshd.Where(x => x.Ngaylap.Day == DateTime.Today.Day && x.Ngaylap.Month == DateTime.Today.Month && x.Ngaylap.Year == DateTime.Today.Year && x.Calamviec == Helpers.KiemtraCalamviecHientai()).ToList();
                dsTam = dshd.ToList();
                LoadRecordHDDangcho(dshd, pageNumber, numberRecord);
                int tongST = (dshd.Count / numberRecord) + 1;
                if (dshd.Count % numberRecord == 0)
                {
                    tongST = dshd.Count / numberRecord;
                }
                txtSotrang.Text = 1 + " / " + tongST;
            }
        }

        DataGridViewRow ConvertHoadontoGridViewRow(HoaDon hoaDon)
        {
            KhachHang kh = ctrlHD.geTKhachhang(hoaDon.MaKH);
            DataGridViewRow row = (DataGridViewRow)grdDanhsachHDHangcho.Rows[0].Clone();
            row.Cells[0].Value = hoaDon.MaHD;
            row.Cells[1].Value = kh.Hovaten;
            row.Cells[2].Value = kh.SDT;

            return row;
        }
    }
}
