using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            DatPhong datPhong = new DatPhong();
            this.Visible = false;
            datPhong.ShowDialog();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThanhToan thanhToan = new ThanhToan();
            this.Visible = false;
            thanhToan.ShowDialog();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HoaDon hoaDon = new HoaDon();
            this.Visible = false;
            hoaDon.ShowDialog();
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            ThongTinKhachHang thongTinKhachHang = new ThongTinKhachHang();
            this.Visible = false;
            thongTinKhachHang.ShowDialog();
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult rd = MessageBox.Show("Bạn có muốn đăng xuất", "Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (rd == DialogResult.OK)
            {
                DangNhap dangNhap = new DangNhap();
                this.Visible = false;
                dangNhap.ShowDialog();
                Application.Exit();
            }
        }
    }
}
