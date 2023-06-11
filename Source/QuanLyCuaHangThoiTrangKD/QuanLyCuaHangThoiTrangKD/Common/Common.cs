using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangThoiTrangKD.Data;

namespace QuanLyCuaHangThoiTrangKD.Common
{
    public class Common
    {
        private static QuanLyBanHangContext _Intance = new QuanLyBanHangContext();

        internal static QuanLyBanHangContext Intance { get => _Intance; set => _Intance = value; }
    }
}
