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
namespace WindowsFormsApp1
{
    public partial class De10 : Form
    {
        string str = @"Data Source=DESKTOP-F7MRBFM\SQLEXPRESS;Initial Catalog=QlySV;Integrated Security=True";
        public De10()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            string query = "select Hoten,GioiTinh,LoaiPhong,SoPhongThue from QLKhachHang";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter dta = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            dta.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            

            
        }

        private void De10_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            bool gioitinh = rdNam.Checked ? true : false;
            int kq;
            if(Int32.TryParse(txtSPT.Text,out kq))
            {
                string query = "insert into QLKhachHang values(N'" + txtTenKH.Text + "','" + gioitinh + "',N'" + cbLP.Text + "','" + txtSPT.Text + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                txtTenKH.Text = txtSPT.Text = cbLP.Text = cbLP.Text = "";
                rdNam.Checked = rdNu.Checked = false;
                LoadData();
            }
            else
            {
                MessageBox.Show("Số phòng thuê phải là chữ số");
            }

        }
        int id;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0) 
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                
                txtTenKH.Text = row.Cells["Column2"].Value.ToString();
                if (row.Cells["Column3"].Value.ToString() == "True")
                {
                    rdNam.Checked = true;
                }    
                else
                {
                    rdNu.Checked= true;
                }
                cbLP.Text = row.Cells["Column4"].Value.ToString();
                txtSPT.Text = row.Cells["Column5"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            bool gioitinh = rdNam.Checked ? true : false;

            string query = "update QLKhachHang set HoTen = N'" + txtTenKH.Text + "', GioiTinh = '" + gioitinh + "',LoaiPhong = N'" + cbLP.Text + "',SoPhongThue = '" + txtSPT.Text + "' where MaKH = '"+id+"'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            txtTenKH.Text = txtSPT.Text = cbLP.Text = cbLP.Text = "";
            rdNam.Checked = rdNu.Checked = false;
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            bool gioitinh = rdNam.Checked ? true : false;

            string query = "delete QlKhachHang where Hoten = '"+txtTenKH.Text+"' and GioiTinh = '"+gioitinh+"' and LoaiPhong = '"+cbLP.Text+"' and SoPhongThue = "+txtSPT.Text+"";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            txtTenKH.Text = txtSPT.Text = cbLP.Text = cbLP.Text = "";
            rdNam.Checked = rdNu.Checked = false;
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            string query = "select Hoten,GioiTinh,LoaiPhong,SoPhongThue from QLKhachHang where Hoten like '%" + txtTimKiem.Text+"%'";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter dta = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            dta.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }
    }
}
