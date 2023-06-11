using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;

namespace QuanLyCuaHangThoiTrangKD.ViewModels
{
    class ITemGridThongkeSP
    {
        //Thông tin sản phẩm
        public SanPham Sanpham { get; set; }

        //Số lượng sản phẩm tồn
        public int Slton { get; set; }

        //Tổng giá trị đã bán của sản phẩm
        public float TongGTSPDaban { get; set; }
    }
}
