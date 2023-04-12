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
    public partial class DangNhap : Form
    {
        string str = @"Data Source=DESKTOP-F7MRBFM\SQLEXPRESS;Initial Catalog=QlySV;Integrated Security=True";
       
        public DangNhap()
        {
            InitializeComponent();
        }
        
        private void btnTinh_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                string query = "select * from taikhoan where taikhoan = '" + txbTK.Text + "' and matkhau = '" + txtMK.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dta = cmd.ExecuteReader();
                if(dta.Read())
                {
                    this.Hide();
                    Main main = new Main();
                    main.ShowDialog();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi không kết nối được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        
    }
}
