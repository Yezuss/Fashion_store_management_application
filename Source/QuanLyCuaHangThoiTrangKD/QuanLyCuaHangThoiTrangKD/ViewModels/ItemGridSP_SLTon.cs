using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Models;
using QuanLyCuaHangThoiTrangKD.ViewModels;

namespace QuanLyCuaHangThoiTrangKD.ViewModels
{
    class ItemGridSP_SLTon
    {
        //Thông tin sản phẩm
        public SanPham Sanpham { get; set; }

        //Số lượng sản phẩm tồn
        public int Slton { get; set; }

        //Số lượng sản phẩm đã bán
        public int Slban { get; set; }
    }
}
