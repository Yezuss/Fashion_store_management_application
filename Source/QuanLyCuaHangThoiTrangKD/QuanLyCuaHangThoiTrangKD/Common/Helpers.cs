using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Common
{
    class Helpers
    {
        /// <summary>
        /// Phát sinh ngẫu nhiên chuỗi 6 ký tự gồm 2 chữ cái đầu và 4 chữ số 
        /// </summary>
        public static string RandomID(string text)
        {
            Random num = new Random();

            string[] textArr = text.ToUpper().Split(' ');
            string key = "";
            for (int i = 0; i < 2; i++)
            {
                key += textArr[i].First();
            }

            string ID = key + num.Next(999, 10000);
            return ID;
        }

        //public static int TaoMatheoThutuTrongngay()
        //{            
        //    return 0;
        //}

        /// <summary>
        /// Kiểm tra ca làm việc hiện tại 
        /// </summary>
        public static int KiemtraCalamviecHientai()
        {
            int calamviec = 0;
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 16)
                calamviec = 1;
            else if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 22)
                calamviec = 2;
            return calamviec;
        }

        /// <summary>
        ///  Chuyển kiểu dữ liệu hình ảnh (byte[])  sang kiểu string
        /// </summary>
        public static byte[] converImgToByte(string srcImage)
        {
            FileStream fs;
            fs = new FileStream(srcImage, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }

        /// <summary>
        /// Chuyển kiểu dữ liệu chuỗi sang kiểu hình ảnh (byte[])  
        /// </summary>
        public static Image convertByteToImg(string byteString)
        {
            byte[] imgBytes = Convert.FromBase64String(byteString);
            MemoryStream ms = new MemoryStream(imgBytes, 0, imgBytes.Length);
            ms.Write(imgBytes, 0, imgBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
