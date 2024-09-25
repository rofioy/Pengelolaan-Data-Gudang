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
    public partial class FormCluster : Form
    {
        MySqlConnection conn; 
        MySqlCommand cmd;
        MySqlDataReader dr;
        int i = 0;

        ClassKoneksi koneksi = new ClassKoneksi();
        public FormCluster()
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
                string query = "SELECT kd_cluster, alamat, nama, nama_barang, stok, bayar, DATE_FORMAT(tanggal, '%Y-%m-%d') AS tanggal FROM cluster"; // Menampilkan tanggal
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();

                dgvCluster.Rows.Clear();

                while (dr.Read())
                {
                    dgvCluster.Rows.Add(
                        dr["kd_cluster"].ToString(),
                        dr["alamat"].ToString(),
                        dr["nama"].ToString(),
                        dr["nama_barang"].ToString(),
                        dr["stok"].ToString(),
                        dr["bayar"].ToString(),
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
            txtKodeCluster.Clear();
            txtAlamat.Clear();
            txtNama.Clear();
            txtNamaBarang.Clear();
            txtStok.Clear();
            txtBayar.Clear();
        }

        //Simpan
        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtAlamat.Text == string.Empty) || (txtBayar.Text == string.Empty) || (txtNama.Text == string.Empty) 
                || (txtNamaBarang.Text == string.Empty) || (txtStok.Text == string.Empty))
            {
                MessageBox.Show("Silahkan Masukan Data !", "crud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO cluster(kd_cluster,alamat,nama,nama_barang,stok,bayar) " +
                    "VALUES (@kd_cluster,@alamat,@nama,@nama_barang,@stok,@bayar)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kd_cluster", txtKodeCluster.Text);
                cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txtNamaBarang.Text);
                cmd.Parameters.AddWithValue("@stok", txtStok.Text);
                cmd.Parameters.AddWithValue("@bayar", txtBayar.Text);

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
        //Simpan Done

        //Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new MySqlCommand("delete from cluster where kd_cluster = @kd_cluster", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@kd_cluster", txtKodeCluster.Text);

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Data Berhasil Dihapus !", "Data Cluster", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Gagal Dihapus !", "Data Cluster", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conn.Close();
            LoadRecord();
            clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKodeCluster.Clear();
            txtAlamat.Clear();
            txtNama.Clear();
            txtNamaBarang.Clear();
            txtStok.Clear();
            txtBayar.Clear();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if ((txtAlamat.Text == string.Empty) || (txtBayar.Text == string.Empty) || (txtNama.Text == string.Empty) || (txtNamaBarang.Text == string.Empty) || (txtStok.Text == string.Empty))
            {
                MessageBox.Show("Silahkan Masukan Data!", "crud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                conn.Open();
                cmd = new MySqlCommand("UPDATE cluster SET alamat=@alamat, nama=@nama, nama_barang=@nama_barang, stok=@stok, bayar=@bayar WHERE kd_cluster=@kd_cluster", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kd_cluster", txtKodeCluster.Text);
                cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@nama_barang", txtNamaBarang.Text);
                cmd.Parameters.AddWithValue("@stok", txtStok.Text);
                cmd.Parameters.AddWithValue("@bayar", txtBayar.Text);


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

        private void dgvClusterCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKodeCluster.Text = dgvCluster.CurrentRow.Cells[0].Value.ToString();
            txtAlamat.Text = dgvCluster.CurrentRow.Cells[1].Value.ToString();
            txtNama.Text = dgvCluster.CurrentRow.Cells[2].Value.ToString();
            txtNamaBarang.Text = dgvCluster.CurrentRow.Cells[3].Value.ToString();
            txtStok.Text = dgvCluster.CurrentRow.Cells[4].Value.ToString();
            txtBayar.Text = dgvCluster.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
