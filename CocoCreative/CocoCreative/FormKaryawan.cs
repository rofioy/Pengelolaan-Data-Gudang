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
    public partial class FormKaryawan : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        int i = 0;

        ClassKoneksi koneksi = new ClassKoneksi();
        public FormKaryawan()
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
                string query = "SELECT kd_karyawan, gudang, nama, borongan, bon, DATE_FORMAT(tanggal, '%Y-%m-%d') AS tanggal FROM karyawan"; // Menampilkan tanggal
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();

                dgvKaryawan.Rows.Clear();

                while (dr.Read())
                {
                    dgvKaryawan.Rows.Add(
                        dr["kd_karyawan"].ToString(),
                        dr["gudang"].ToString(),
                        dr["nama"].ToString(),
                        dr["borongan"].ToString(),
                        dr["bon"].ToString(),
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
            txtKodeKaryawan.Clear();
            txtGudang.Clear();
            txtNama.Clear();
            txtBorongan.Clear();
            txtBon.Clear();
        }
        //save
        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtKodeKaryawan.Text == string.Empty) || (txtGudang.Text == string.Empty) || (txtNama.Text == string.Empty) || (txtBorongan.Text == string.Empty) || (txtBon.Text == string.Empty))
            {
                MessageBox.Show("Silahkan Masukan Data !", "crud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO karyawan(kd_karyawan,gudang,nama,borongan,bon) VALUES (@kd_karyawan,@gudang,@nama,@borongan,@bon)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kd_karyawan", txtKodeKaryawan.Text);
                cmd.Parameters.AddWithValue("@gudang", txtGudang.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@borongan", txtBorongan.Text);
                cmd.Parameters.AddWithValue("@bon", txtBon.Text);

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

        //delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new MySqlCommand("delete from karyawan where kd_karyawan = @kd_karyawan", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@kd_karyawan", txtKodeKaryawan.Text);

            i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Data Berhasil Dihapus !", "Data Karyawan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Gagal Dihapus !", "Data Karyawan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conn.Close();
            LoadRecord();
            clear();
        }
        private void dgvKaryawan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKodeKaryawan.Text = dgvKaryawan.CurrentRow.Cells[0].Value.ToString();
            txtGudang.Text = dgvKaryawan.CurrentRow.Cells[1].Value.ToString();
            txtNama.Text = dgvKaryawan.CurrentRow.Cells[2].Value.ToString();
            txtBorongan.Text = dgvKaryawan.CurrentRow.Cells[3].Value.ToString();
            txtBon.Text = dgvKaryawan.CurrentRow.Cells[4].Value.ToString();
        }
        //delete done


        //Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKodeKaryawan.Clear();
            txtGudang.Clear();
            txtNama.Clear();
            txtBorongan.Clear();
            txtBon.Clear();
        }
        //Clear End

        //Edit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if ((txtKodeKaryawan.Text == string.Empty) || (txtGudang.Text == string.Empty) || (txtNama.Text == string.Empty) || (txtBorongan.Text == string.Empty) || (txtBon.Text == string.Empty))
            {
                MessageBox.Show("Silahkan Masukan Data!", "crud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                conn.Open();
                cmd = new MySqlCommand("UPDATE karyawan SET gudang=@gudang, nama=@nama, borongan=@borongan, bon=@bon WHERE kd_karyawan=@kd_karyawan", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kd_karyawan", txtKodeKaryawan.Text);
                cmd.Parameters.AddWithValue("@gudang", txtGudang.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@borongan", txtBorongan.Text);
                cmd.Parameters.AddWithValue("@bon", txtBon.Text);


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
        private void FormKaryawan_Load(object sender, EventArgs e)
        {

        }

    }
}
