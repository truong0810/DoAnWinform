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
    public partial class DatPhong : Form
    {
        public  string truockhisua;
        public DatPhong()
        {
            InitializeComponent();
        }

        private void DatPhong_Load(object sender, EventArgs e)
        {
            DsDatPhong.DataSource = TruyXuatCSDL.LayBang("select *from DatPhong");
            DsLoaiPhong.DataSource = TruyXuatCSDL.LayBang("select *from LoaiPhong");
            DsDatPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DsDatPhong.Columns[0].HeaderText = "Mã Phòng";
            DsDatPhong.Columns[1].HeaderText = "Mã KH";
            DsDatPhong.Columns[2].HeaderText = "Ngày Đến";
            DsDatPhong.Columns[3].HeaderText = "Ngày Đi";
            DsDatPhong.Columns[4].HeaderText = "Giá Phòng";
            DsDatPhong.Columns[5].HeaderText = "Số Người";
            DsDatPhong.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           
            //căn cho cot head
            DsLoaiPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DsLoaiPhong.Columns[0].HeaderText = "Loại Phòng";
            DsLoaiPhong.Columns[1].HeaderText = "Số Người Tối Đa";
            DsLoaiPhong.Columns[2].HeaderText = "Giá Phòng";
            //căn chính giữa cho cột
            DsLoaiPhong.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
         

            DsDatPhong.Columns[0].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            DsDatPhong.Columns[1].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            DsDatPhong.Columns[2].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            DsDatPhong.Columns[3].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            DsDatPhong.Columns[4].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            DsDatPhong.Columns[5].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            DsLoaiPhong.Columns[0].AutoSizeMode =
               DataGridViewAutoSizeColumnMode.Fill;
            DsLoaiPhong.Columns[1].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
            DsLoaiPhong.Columns[2].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;
        }
        public bool kiemtra()
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Vui lòng điền thông tin vào mã phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return false;
            }
            else if (TxtKhachHang.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtKhachHang.Focus();
                return false;
            }
            else if (txtGiaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập giá phong", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaPhong.Focus();
                return false;
            }
            else if (txtSoNguoi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số người", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoNguoi.Focus();
                return false;
            }
            else { }
            return true;
        }
        public bool KiemtraMaPhong()
        {
            //kiemtra
            SqlConnection conn = TruyXuatCSDL.TaoKetNoi();
            string query = "select count(*) from DatPhong where MaPhong = @MaPhong";
            SqlCommand Command = new SqlCommand(query, conn);
            Command.Parameters.AddWithValue("@MaPhong", txtMaPhong.Text);
            conn.Open();
            int count = (int)Command.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Mã phòng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else { }
            conn.Close();
            return true;
        }
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            if (kiemtra() && KiemtraMaPhong())
            {
                string sql = "insert into DatPhong values(N'" +
                txtMaPhong.Text + "',N' " +
                TxtKhachHang.Text + "',N' " +
                txtNgayDen.Value.ToShortDateString() + "', N'" +
                txtNgayDi.Value.ToShortDateString() + "', " +
               txtGiaPhong.Text + "," + txtSoNguoi.Text + ")";
                TruyXuatCSDL.ThemSuaXoa(sql);
                DsDatPhong.DataSource =
                    TruyXuatCSDL.LayBang("select * from DatPhong");
                MessageBox.Show("Đặt phòng thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete  from DatPhong where MaPhong=N'" +
               truockhisua + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            DsDatPhong.DataSource = TruyXuatCSDL.LayBang("select * from DatPhong");
            MessageBox.Show("Đã xóa bản ghi!");
        }

        private void DsDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DsDatPhong.CurrentRow != null)
                truockhisua = DsDatPhong.CurrentRow.Cells[0].Value.ToString();
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
