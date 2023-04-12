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
    public partial class Main : Form
    {
        string str = @"Data Source=DESKTOP-F7MRBFM\SQLEXPRESS;Initial Catalog=QlySV;Integrated Security=True";
        public Main()
        {
            InitializeComponent();
        }

        void LoadKhoa()
        {
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            string query = "select * from khoa";
            SqlCommand cmd = new SqlCommand(query, conn);
            
            SqlDataAdapter dta = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();
            dta.Fill(dataTable);
            cbMaKhoa.DataSource = dataTable;
            cbMaKhoa.DisplayMember = "TenKhoa";
            cbMaKhoa.ValueMember = "MaKhoa";

            Khoa.DataSource = dataTable;
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int makhoa = Convert.ToInt32(txtMaKhoa.Text);
            try
            {
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                string query = "insert into Khoa values ('" + makhoa + "',n'" + txtTenKhoa.Text + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                txtMaKhoa.Text = txtTenKhoa.Text = "";
                LoadKhoa();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi không thêm được!");
            }
            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadKhoa();
        }

        private void Khoa_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtMaKhoa.Text = Khoa.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenKhoa.Text = Khoa.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int makhoa = Convert.ToInt32(txtMaKhoa.Text);
            try
            {
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                string query = "update Khoa  set TenKhoa = N'"+txtTenKhoa.Text+ "' where MaKhoa ='"+txtMaKhoa.Text+"'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                txtMaKhoa.Text = txtTenKhoa.Text = "";
                LoadKhoa();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi không sửa được!");
            }
        }

        private void Khoa_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.Khoa.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex+1).ToString();
        }
    }
}
