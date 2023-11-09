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
    public partial class HoaDon : Form
    {
        private static string Truockhisua;
        public HoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            dsHoaDon.DataSource = TruyXuatCSDL.LayBang("select *from HoaDon");
            dsHoaDon.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dsHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
            dsHoaDon.Columns[1].HeaderText = "Ngày Thanh Toán";
            dsHoaDon.Columns[2].HeaderText = "Tổng tiền";
            dsHoaDon.Columns[3].HeaderText = "Mã Phòng";
            dsHoaDon.Columns[4].HeaderText = "Mã Khách Hàng";

            dsHoaDon.Columns[0].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsHoaDon.Columns[1].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsHoaDon.Columns[2].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsHoaDon.Columns[3].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsHoaDon.Columns[4].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            dsHoaDon.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dsHoaDon.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dsHoaDon.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dsHoaDon.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete  from HoaDon where MaHD=N'" +
             Truockhisua + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            dsHoaDon.DataSource = TruyXuatCSDL.LayBang("select * from HoaDon");
            MessageBox.Show("Đã xóa bản ghi!");
        }

        private void dsHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dsHoaDon.CurrentRow != null)
                Truockhisua = dsHoaDon.CurrentRow.Cells[0].Value.ToString();
            txtMaHoaDon.Text = dsHoaDon.CurrentRow.Cells[0].Value.ToString();
            datetNgayTT.Format = DateTimePickerFormat.Short;
            datetNgayTT.Text = dsHoaDon.CurrentRow.Cells[1].Value.ToString();
            txtTongTien.Text = dsHoaDon.CurrentRow.Cells[2].Value.ToString();
            txtMaPhong.Text = dsHoaDon.CurrentRow.Cells[3].Value.ToString();
            txtMaKH.Text = dsHoaDon.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaHoaDon.Clear();
            txtMaKH.Clear();
            txtMaPhong.Clear();
            txtTongTien.Clear();
            datetNgayTT.CustomFormat = " ";//chưa reset đc

        }
        public bool KiemtraMaKH()
        {
            
            SqlConnection conn = TruyXuatCSDL.TaoKetNoi();
            string query = "select count(*) from HoaDon where MaHD = @MaHD";
            SqlCommand Command = new SqlCommand(query, conn);
            Command.Parameters.AddWithValue("@MaHD", txtMaHoaDon.Text);
            conn.Open();
            int count = (int)Command.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Mã hóa đơn đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else { }
            conn.Close();
            return true;
        }
        public bool kiemtra()
        {
            if (txtMaHoaDon.Text == "")
            {
                MessageBox.Show("Vui lòng điền thông tin vào mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHoaDon.Focus();
                return false;
            }
            else if (txtTongTien.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tổng tiền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTongTien.Focus();
                return false;
            }
            else if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return false;
            }
            else if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return false;
            }
            else { }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (kiemtra() && KiemtraMaKH())
            {
                string sql = "insert into HoaDon values(N'" +
                txtMaHoaDon.Text + "',N' " +
                datetNgayTT.Value.ToShortDateString() + "',N' " +
                txtTongTien.Text + "',N' " +//+trưa xử lý
               txtMaPhong.Text + "',N'" +txtMaKH.Text + "')";
                TruyXuatCSDL.ThemSuaXoa(sql);
                dsHoaDon.DataSource =
                    TruyXuatCSDL.LayBang("select * from HoaDon");
                MessageBox.Show("Đã thêm hóa đơn thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kiemtra())
            {
                string sql = " update HoaDon set MaHD=N'" + txtMaHoaDon.Text + "',NgayThanhToan=N'" + datetNgayTT.Value.ToShortDateString() + "',TongTien=N'" +txtTongTien.Text + "',MaPhong=N'"
                    + txtMaPhong.Text + "' ,MaKH=N'" + txtMaKH.Text + "'  where MaHD = N'" + Truockhisua + "'";
                TruyXuatCSDL.ThemSuaXoa(sql);
                dsHoaDon.DataSource = TruyXuatCSDL.LayBang("select * from HoaDon");
                MessageBox.Show("Đã sửa hóa đơn thành công", "Thông báo");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Menu nu = new Menu();
            this.Visible = false;
            nu.ShowDialog();
            Application.Exit();
        }
    }
}
