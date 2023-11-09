using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace BaiTapLon
{
    internal class TruyXuatCSDL
    {
        private static string diachi = @"Data Source=LAPTOP-LF1193DU;Initial Catalog=DangNhap;Integrated Security=True";
        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(diachi);
        }
        // lấy ra một bảng (table)
        public static DataTable LayBang(string sql)
        {
            SqlConnection OngHut = TaoKetNoi();
            OngHut.Open();
            SqlDataAdapter MayBom = new SqlDataAdapter(sql, OngHut);
            DataTable ThungChua = new DataTable();
            MayBom.Fill(ThungChua);
            OngHut.Close();
            MayBom.Dispose();
            return ThungChua;
        }
        // Phương thức thêm sửa xóa
        public static void ThemSuaXoa(string sql)
        {
            SqlConnection OngHut = TaoKetNoi();
            OngHut.Open();
            SqlCommand Lenh = new SqlCommand(sql, OngHut);
            Lenh.ExecuteNonQuery();
            OngHut.Close();
            Lenh.Dispose();
        }
    }
}

