using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CocoCreative
{
    public partial class FormLogin : Form
    {
        ClassKoneksi con = new ClassKoneksi();
        string username, password;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == false)
                txtPass.UseSystemPasswordChar = true;
            else
                txtPass.UseSystemPasswordChar = false;
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPass.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtPass.Text != "")
                {
                    con.Open();
                    string query = "select username, pass from tb_login where username = '" + txtName.Text + "' AND pass = '" + txtPass.Text + "'";
                    MySqlDataReader row;
                    row = con.ExecuteReader(query);

                    if (row.HasRows)
                    {
                        while (row.Read())
                        {
                            username = row["username"].ToString();
                            password = row["pass"].ToString();
                        }
                        MainForm main = new MainForm();
                        main.ShowDialog();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login gagal!", "Information");
                    }
                }
                else
                {
                    MessageBox.Show("Username or Password is empty!", "Information");
                }
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }
    }
}
