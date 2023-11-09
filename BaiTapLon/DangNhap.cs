using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BaiTapLon
{
    public partial class DangNhap : Form
    {
        
        public DangNhap()
        {
            InitializeComponent();
        }

        private static string diachi = @"Data Source=LAPTOP-LF1193DU;Initial Catalog=DangNhap;Integrated Security=True";

        private bool Kiemtra()
        {
            if (txtTaiKhoan.Text.Trim() == ""|| txtTaiKhoan.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin tài khoản và mật khẩu."
                    ,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                txtTaiKhoan.Focus();
                return false;
            }
            else { }   
            return true;
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(diachi);
            try
            {
                conn.Open();
                string sql = "select * from DangNhap where TaiKhoan=N'" + txtTaiKhoan.Text.Trim() + "'AND MatKhau=N'" + txtMatKhau.Text.Trim() + "' ";
                SqlCommand data = new SqlCommand(sql, conn);//xác định những thao tác
                SqlDataReader rdr = data.ExecuteReader();
                if (Kiemtra() == true)
                {
                    if (rdr.Read() == true)
                    {
                        MessageBox.Show("Đăng nhập thành công","Thông báo",MessageBoxButtons.OKCancel);
                        Menu nu = new Menu();
                        this.Visible = false;
                        nu.ShowDialog();
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin Tài khoản hoặc mật khẩu sai", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết lỗi", "Thông báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rd = MessageBox.Show("Bạn thực sự muốn thoát", "Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (rd == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            DangKy dangKy = new DangKy();
            this.Visible = false;
            dangKy.ShowDialog();
            Application.Exit();
        }
    }
}
