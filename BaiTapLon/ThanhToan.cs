using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class ThanhToan : Form
    {
        public ThanhToan()
        {
            InitializeComponent();
        }
        public bool kiemtra()
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Vui lòng điền thông tin vào mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return false;
            }
            else  if (txtTenKH.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return false;
            }
            else if (txtNgayO.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số ngày ở", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNgayO.Focus();
                return false;
            }
            else if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return false;
            }
            else if (cobPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập loại phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cobPhong.Focus();
                return false;
            }
            else { }
            return true;
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            
            if (kiemtra())
            {
                
                int SoNgayO = Convert.ToInt32(txtNgayO.Text);
                int kq;
                if (cobPhong.Text == "Loại 1")
                {
                    kq = SoNgayO * 2000000;
                    lbTTThanhToan.Text = kq.ToString();
                }
                else if (cobPhong.Text == "Loại 2")
                {
                    kq = SoNgayO * 500000;
                    lbTTThanhToan.Text = kq.ToString();
                }
                else if (cobPhong.Text == "Loại 3")
                {
                    kq = SoNgayO * 600000;
                    lbTTThanhToan.Text = kq.ToString();
                }
                else if (cobPhong.Text == "Loại 4")
                {
                    kq = SoNgayO * 700000;
                    lbTTThanhToan.Text = kq.ToString();
                }
                else if (cobPhong.Text == "Loại 5")
                {
                    kq = SoNgayO * 800000;
                    lbTTThanhToan.Text = kq.ToString();
                }
                else if (cobPhong.Text == "Loại 6")
                {
                    kq = SoNgayO * 1000000;
                    lbTTThanhToan.Text = kq.ToString();
                }
                else if (cobPhong.Text == "Loại 7")
                {
                    kq = SoNgayO * 1500000;
                    lbTTThanhToan.Text = kq.ToString();
                }
                else if (cobPhong.Text == "Loại 8")
                {
                    kq = SoNgayO * 400000;
                    lbTTThanhToan.Text = kq.ToString();
                }
            }
        }

        private void btnXemHoaDon_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.ShowDialog();
        }
        public bool KiemtraMaHD()
        {

            SqlConnection conn = TruyXuatCSDL.TaoKetNoi();
            string query = "select count(*) from HoaDon where MaHD = @MaHD";
            SqlCommand Command = new SqlCommand(query, conn);
            Command.Parameters.AddWithValue("@MaHD", txtMaHD.Text);
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
       
        private void btnThemHD_Click(object sender, EventArgs e)
        {

            if (kiemtra() && KiemtraMaHD())
            {
                if (txtMaHD.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaHD.Focus();
                }
                else
                {
                    string sql = "insert into HoaDon values(N'" +
                 txtMaHD.Text + "',N' " +
                 datetNgayTT.Value.ToShortDateString() + "',N' " +
                 lbTTThanhToan.Text + "',N' " +
              txtMaPhong.Text + "',N'" + txtMaKH.Text + "')";
                    TruyXuatCSDL.ThemSuaXoa(sql);

                    MessageBox.Show("Đã thêm hóa đơn thành công!");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Menu nu = new Menu();
            this.Visible = false;
            nu.ShowDialog();
            Application.Exit();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            dsThanhToan.DataSource = TruyXuatCSDL.LayBang("select * from ThongTinKhachHang");
            dsThanhToan.Columns[0].HeaderText = "Mã khách hàng";
            dsThanhToan.Columns[1].HeaderText = "Họ Tên khách hàng";
            dsThanhToan.Columns[2].Visible = false;//
            dsThanhToan.Columns[3].HeaderText = "Ngày sinh";
            dsThanhToan.Columns[4].HeaderText = "CCCDC/MND";
            dsThanhToan.Columns[5].Visible = false;//
            dsThanhToan.Columns[6].HeaderText = "Số điện thoại";
            dsThanhToan.Columns[7].Visible = false;//

            dsThanhToan.Columns[0].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThanhToan.Columns[1].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThanhToan.Columns[2].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThanhToan.Columns[3].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThanhToan.Columns[4].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThanhToan.Columns[5].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThanhToan.Columns[6].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThanhToan.Columns[7].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
        }
        private void dsThanhToan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dsThanhToan.CurrentRow != null)
            txtMaKH.Text = dsThanhToan.CurrentRow.Cells[0].Value.ToString();
            txtTenKH.Text = dsThanhToan.CurrentRow.Cells[1].Value.ToString(); 
            
        }
        public bool KTTimKiem()
        {
            if (cobMaKH.Text == "")
            {
                MessageBox.Show("Bạn cần phải nhập mã khách hàng để tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cobMaKH.Focus();
                return false;
            }
            return true;
        }
        private void btnTiemKiem_Click(object sender, EventArgs e)
        {
            if (KTTimKiem())
            {
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LF1193DU;Initial Catalog=DangNhap;Integrated Security=True");
                string Timkiem = "select *from ThongTinKhachHang where MaKH like '%" + cobMaKH.Text + "%'";
                con.Open();
                SqlCommand cmd = new SqlCommand(Timkiem, con);
                cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rd);
                dsThanhToan.DataSource = dt;
                con.Close();
            }
        }

        private void cobMaKH_DropDown(object sender, EventArgs e)
        {
            String sql = "SELECT MaKH FROM ThongTinKhachHang";
            SqlConnection connection = TruyXuatCSDL.TaoKetNoi();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                cobMaKH.DataSource = table;
                cobMaKH.DisplayMember = "TenKH";
                cobMaKH.ValueMember = "MaKH";
            }
            // Refresh ComboBox để hiển thị dữ liệu mới nhất
            cobMaKH.Refresh();
        }

        
    }
}
