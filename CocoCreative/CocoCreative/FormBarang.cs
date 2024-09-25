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
using MySql.Data.MySqlClient;

namespace CocoCreative
{
    public partial class FormBarang : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        int i = 0;

        ClassKoneksi koneksi = new ClassKoneksi();
        public FormBarang()
        {
            InitializeComponent();
            conn = new MySqlConnection(koneksi.dbconnect());
            LoadRecord();
        }

        //Menampilkan data pada grid
        public void LoadRecord()
        {
            try
            {
                conn.Open();
                string query = "SELECT kd_barang, nama_barang, stok, DATE_FORMAT(tanggal, '%Y-%m-%d') AS tanggal FROM barang"; // Menampilkan tanggal
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();

                dgvBarang.Rows.Clear();

                while (dr.Read())
                {
                    dgvBarang.Rows.Add(
                        dr["kd_barang"].ToString(),
                        dr["nama_barang"].ToString(),
                        dr["stok"].ToString(),
                        dr["tanggal"].ToString() // Menambahkan kolom tanggal
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //Clear
        public void clear()
        {
            txtKodeBarang.Clear();
            txtNamaBarang.Clear();
            txtStok.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtKodeBarang.Text == string.Empty) || (txtNamaBarang.Text == string.Empty) || (txtStok.Text == string.Empty))
            {
                MessageBox.Show("Silahkan Masukan Data !", "crud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO barang(kd_barang,nama_barang,stok) VALUES (@kd_barang,@nama_barang,@stok)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kd_barang", txtKodeBarang.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txtNamaBarang.Text);
                cmd.Parameters.AddWithValue("@stok", txtStok.Text);

                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Data berhasil disimpan.", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data gagal disimpan.", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                conn.Close();
                LoadRecord();
                clear();
            }
        }
        //save done

        //Delete

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new MySqlCommand("delete from barang where kd_barang = @kd_barang", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@kd_barang", txtKodeBarang.Text);

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Data Berhasil Dihapus !", "Data Barang", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Gagal Dihapus !", "Data Barang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conn.Close();
            LoadRecord();
            clear();
        }
        private void dgvBarangCellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKodeBarang.Text = dgvBarang.CurrentRow.Cells[0].Value.ToString();
            txtNamaBarang.Text = dgvBarang.CurrentRow.Cells[1].Value.ToString();
            txtStok.Text = dgvBarang.CurrentRow.Cells[2].Value.ToString();
        }
        //Delete Done



        private void FormBarang_Load(object sender, EventArgs e)
        {

        }

        //Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKodeBarang.Clear();
            txtNamaBarang.Clear();
            txtStok.Clear();
        }
        //Clear Done

        //Edit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if ((txtKodeBarang.Text == string.Empty) || (txtNamaBarang.Text == string.Empty) || (txtStok.Text == string.Empty))
            {
                MessageBox.Show("Silahkan Masukan Data!", "crud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                conn.Open();
                cmd = new MySqlCommand("UPDATE barang SET nama_barang=@nama_barang, stok=@stok WHERE kd_barang=@kd_barang", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kd_barang", txtKodeBarang.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txtNamaBarang.Text);
                cmd.Parameters.AddWithValue("@stok", txtStok.Text);

                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Data berhasil diedit.", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Data gagal diedit.", "CRUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                conn.Close();
                LoadRecord();
                clear();
            }
        }
        //Edit Done
    }
}
