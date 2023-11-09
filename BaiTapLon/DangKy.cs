using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMatKhau.Text = "";
            txtNhapLaiMK.Clear();
            txtTaiKhoan.Clear();
        }
        public bool KiemtraTrung()
        {
            SqlConnection conn = TruyXuatCSDL.TaoKetNoi();
            string query = "select count(*) from DangNhap where TaiKhoan = @TaiKhoan";
            SqlCommand Command = new SqlCommand(query, conn);
            Command.Parameters.AddWithValue("@TaiKhoan", txtTaiKhoan.Text);
            conn.Open();
            int count = (int)Command.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Tài khoản này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else { }
            conn.Close();
            return true;
        }
        public bool kiemtra()
        {
            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaiKhoan.Focus();
                return false;
            }
            else if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }
            else if (txtNhapLaiMK.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập lại mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLaiMK.Focus();
                return false;
            }
            else if (txtMatKhau.Text != txtNhapLaiMK.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại đã sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }
            else { }
            return true;
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if(kiemtra() && KiemtraTrung())
            {
                string sql = "insert into DangNhap values(N'" +
              txtTaiKhoan.Text + "',N' " +
              txtMatKhau.Text + "')";
                TruyXuatCSDL.ThemSuaXoa(sql);
                MessageBox.Show("Đã đăng ký tài khoản thành công!");
               DangNhap dangNhap = new DangNhap();
                this.Visible = false;
                dangNhap.ShowDialog();
                Application.Exit();
            }
        }

        private void DangKy_Load(object sender, EventArgs e)
        {

        }
    }
}
