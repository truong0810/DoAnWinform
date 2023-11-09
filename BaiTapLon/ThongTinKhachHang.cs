using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BaiTapLon
{
    public partial class ThongTinKhachHang : Form
    {
        DateTimePicker dt = new DateTimePicker();
        private static string Truockhisua;
        public ThongTinKhachHang()
        {
            InitializeComponent();
        }
        private void ThongTinKhachHang_Load(object sender, EventArgs e)
        {
            dsThongTinKH.DataSource = TruyXuatCSDL.LayBang("select * from ThongTinKhachHang");
            dsThongTinKH.Columns[0].HeaderText = "Mã khách hàng";
            dsThongTinKH.Columns[1].HeaderText = "Tên khách hàng";
            dsThongTinKH.Columns[2].HeaderText = "Giới tính";
            dsThongTinKH.Columns[3].HeaderText = "Ngày sinh";
            dsThongTinKH.Columns[4].HeaderText = "CCCDC/MND";
            dsThongTinKH.Columns[5].HeaderText = "Địa chỉ";
            dsThongTinKH.Columns[6].HeaderText = "Số điện thoại";
            dsThongTinKH.Columns[7].HeaderText = "Email";

            dsThongTinKH.Columns[0].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThongTinKH.Columns[1].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThongTinKH.Columns[2].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThongTinKH.Columns[3].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThongTinKH.Columns[4].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThongTinKH.Columns[5].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThongTinKH.Columns[6].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            dsThongTinKH.Columns[7].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
          

        }
        public bool KiemtraMaKH()
        {
            //kiemtra
            SqlConnection conn = TruyXuatCSDL.TaoKetNoi();
            string query = "select count(*) from ThongTinKhachHang where MaKH = @MaKH";
            SqlCommand Command = new SqlCommand(query, conn);
            Command.Parameters.AddWithValue("@MaKH", txtMaKhachHang.Text);
            conn.Open();
            int count = (int)Command.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else{}
            conn.Close();
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
                if (kiemtra() && KiemtraMaKH())
                {
                    string sql = "insert into ThongTinKhachHang values(N'" +
                    txtMaKhachHang.Text + "',N' " +
                    txtTenKH.Text + "',N' " +
                    txtGioitinh.Text + "', N'" +
                    txtNamSinh.Value.ToShortDateString() + "',N' " +//+trưa xử lý
                   txtCCCD.Text + "',N'" + txtDiaChi.Text + "',N'" + txtSDT.Text + "',N'" + txtEmail.Text + "')";
                    TruyXuatCSDL.ThemSuaXoa(sql);
                    dsThongTinKH.DataSource =
                        TruyXuatCSDL.LayBang("select * from ThongTinKhachHang");
                    MessageBox.Show("Đã thêm bản ghi!");
                }      
        }

         //Kiểm tra có dữ liệu hay không
        public bool kiemtra()
        {
            if (txtMaKhachHang.Text == "")
            {
                MessageBox.Show("Vui lòng điền thông tin vào mã khách hàng","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtMaKhachHang.Focus();
                return false;   
            }else if (txtTenKH.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return false;
            }else if (txtGioitinh.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập giới tính khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGioitinh.Focus();
                return false;
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            else if (txtCCCD.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập căn cước công dân/Số CMT khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Focus();
                return false;
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập email khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            else{}
            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (kiemtra())
            {
                string sql = " update ThongTinKhachHang set MaKH=N'" + txtMaKhachHang.Text + "',TenKH=N'" + txtTenKH.Text + "',GioiTinh=N'" + txtGioitinh.Text + "',NgaySinh=N'"
                    + txtNamSinh.Value.ToShortDateString() + "' ,CCCD=" + txtCCCD.Text + " ,DiaChi=N'" + txtDiaChi.Text + "',SDT=" + txtSDT.Text + ",Email=N'" + txtEmail.Text + "' where MaKH = N'" + Truockhisua + "'";
                TruyXuatCSDL.ThemSuaXoa(sql);
                dsThongTinKH.DataSource = TruyXuatCSDL.LayBang("select * from ThongTinKhachHang");
                MessageBox.Show("Đã sửa bản ghi thành công", "Thông báo");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete  from ThongTinKhachHang where MaKH=N'" +
              Truockhisua + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            dsThongTinKH.DataSource = TruyXuatCSDL.LayBang("select * from ThongTinKhachHang");
            MessageBox.Show("Đã xóa bản ghi!");

        }

        private void dsThongTinKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dsThongTinKH.CurrentRow != null)
                Truockhisua = dsThongTinKH.CurrentRow.Cells[0].Value.ToString();
            txtMaKhachHang.Text = dsThongTinKH.CurrentRow.Cells[0].Value.ToString();
            txtTenKH.Text = dsThongTinKH.CurrentRow.Cells[1].Value.ToString();
            txtGioitinh.Text = dsThongTinKH.CurrentRow.Cells[2].Value.ToString();
            txtNamSinh.Text = dsThongTinKH.CurrentRow.Cells[3].Value.ToString();
            txtCCCD.Text = dsThongTinKH.CurrentRow.Cells[4].Value.ToString();
            txtDiaChi.Text = dsThongTinKH.CurrentRow.Cells[5].Value.ToString();
            txtSDT.Text = dsThongTinKH.CurrentRow.Cells[6].Value.ToString();
            txtEmail.Text = dsThongTinKH.CurrentRow.Cells[7].Value.ToString();
        }
        public bool KTTimKiem()
        {
            if (txtTim.Text == "")
            {
                MessageBox.Show("Bạn cần phải nhập mã khách hàng để tìm kiếm","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtTim.Focus();
                return false;
            }
            return true;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (KTTimKiem())
            {
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LF1193DU;Initial Catalog=DangNhap;Integrated Security=True");
                string Timkiem = "select *from ThongTinKhachHang where MaKH like '%" + txtTim.Text + "%'";
                con.Open();
                SqlCommand cmd = new SqlCommand(Timkiem, con);
                cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rd);
                dsThongTinKH.DataSource = dt;
                con.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            txtMaKhachHang.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtCCCD.Clear();
            txtGioitinh.Text = "";
            txtTenKH.Clear();
            txtTim.Text = "";
            txtNamSinh.Value = dt.Value;
        }
        //upder
        private void txtTim_DropDown(object sender, EventArgs e)
        {
            string sql= "SELECT MaKH FROM ThongTinKhachHang";
            SqlConnection connection = TruyXuatCSDL.TaoKetNoi();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    txtTim.DataSource = table;
                    txtTim.DisplayMember = "TenKH";
                    txtTim.ValueMember = "MaKH";
                }
            // Refresh ComboBox để hiển thị dữ liệu mới nhất
            txtTim.Refresh();
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
