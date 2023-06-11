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
using QuanLyCuaHangThoiTrangKD.Controller;
using System.Globalization;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormDanhsachSPHethang : Form
    {
        ThongKeController ctrlThongke = new ThongKeController();
        List<string> dsSPGanhet;
        List<ItemGridSP_SLTon> dsTam;
        TaiKhoan taikhoan; 

        int pageNumber = 1, numberRecord = 8;

        public FormDanhsachSPHethang()
        {
            InitializeComponent();
        }

        //Người thực hiện: Văn Khải
        public FormDanhsachSPHethang(List<string> ds, int matk)
        {
            InitializeComponent();
            dsSPGanhet = ds.ToList();
            taikhoan = ctrlThongke.getTaiKhoanByMaTK(matk);

            //if (taikhoan != null)
            //{
            //    if (taikhoan.Loai == "Quản lý")
            //    {
            //        btnLapPhieunhap.Visible = true;
            //    }
            //    else
            //    {
            //        btnLapPhieunhap.Visible = false;
            //    }
            //}

            List<CTByMaSP> dsCTHD = ctrlThongke.LoadDanhsachCTHDByMaSP();
            List<CTByMaSP> dsCTPN = ctrlThongke.LoadDanhsachCTPNByMaSP();
            List<ItemGridSP_SLTon> dsItemGrid = new List<ItemGridSP_SLTon>();
            foreach (var masp in dsSPGanhet)
            {
                int slTon = 0;
                CTByMaSP cthd = dsCTHD.Where(x => x.Masp == masp).FirstOrDefault();
                CTByMaSP ctpn = dsCTPN.Where(x => x.Masp == masp).FirstOrDefault();
                SanPham sp = ctrlThongke.getSanPham(masp);
                if(sp != null)
                {
                    if (cthd != null && ctpn != null)
                    {
                        slTon = ctpn.TongSL - cthd.TongSL;
                        dsItemGrid.Add(new ItemGridSP_SLTon() { Sanpham = sp, Slton = slTon, Slban = 0 });
                    }
                    else
                    {
                        dsItemGrid.Add(new ItemGridSP_SLTon() { Sanpham = sp, Slton = ctpn.TongSL, Slban = 0 });
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

            LoadRecordSP(dsItemGrid, pageNumber, numberRecord);
            dsTam = dsItemGrid.ToList();
        }

        void LoadRecordSP(List<ItemGridSP_SLTon> ds, int page, int recordNum)
        {
            List<ItemGridSP_SLTon> dsSP = new List<ItemGridSP_SLTon>();

            dsSP = ds.Skip((page - 1) * recordNum).Take(recordNum).ToList();
            LoadDanhsachTTSP(dsSP);
        }

        void LoadDanhsachTTSP(List<ItemGridSP_SLTon> dsspht)
        {
            grdThongtinSP.Rows.Clear();
            grdThongtinSP.AllowUserToAddRows = true;

            foreach (var sp in dsspht)
            {
                grdThongtinSP.Rows.Add(ConvertSanPhamtoGridViewRow(sp.Sanpham, sp.Slton));
            }

            grdThongtinSP.AllowUserToAddRows = false;
        }

        DataGridViewRow ConvertSanPhamtoGridViewRow(SanPham sp, int slTon)
        {
            DataGridViewRow dgvr = (DataGridViewRow)grdThongtinSP.Rows[0].Clone();
            dgvr.Cells[0].Value = sp.TenSP.ToString();
            dgvr.Cells[1].Value = slTon;
            dgvr.Cells[2].Value = sp.Chatlieu.ToString();
            dgvr.Cells[3].Value = sp.Mausac.ToString();
            dgvr.Cells[4].Value = sp.Kichthuoc.ToString();
            dgvr.Cells[5].Value = sp.LoaiSP.ToString();

            return dgvr;
        }

        //Người thực hiện: Văn Khải ===================================
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

                //tongsotrang = (dsSPHD.Count / numberRecordCT) + 1;
                txtSotrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordSP(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (pageNumber - 1 < dsTam.Count / numberRecord)
            {
                int tongsotrang = 0;
                pageNumber++;
                tongsotrang = (dsTam.Count / numberRecord) + 1;
                txtSotrang.Text = pageNumber.ToString() + " / " + tongsotrang;
                LoadRecordSP(dsTam, pageNumber, numberRecord);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSotrang.Text = 1 + " / " + tongsotrang;
            LoadRecordSP(dsTam, 1, numberRecord);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int tongsotrang = (dsTam.Count / numberRecord) + 1;
            txtSotrang.Text = tongsotrang + " / " + tongsotrang;
            LoadRecordSP(dsTam, tongsotrang, numberRecord);
        }

        private void btnLapPhieunhap_Click(object sender, EventArgs e)
        {
            //FrmMain frm = new FrmMain(taikhoan.MaTK);
            //frm.Show();
            //frm.
        }

        // ============================================================

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}
