using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace CocoCreative
{
    internal class ClassKoneksi
    {
        MySql.Data.MySqlClient.MySqlConnection con;
        String myConnectionString;
        static string host = "localhost";
        static string database = "gudang";
        static string userDB = "root";
        static string password = "";
        public static string sql = "server=" + host + ";Database=" + database + ";User ID=" + userDB + ";" + "Password=" + password;

        public string dbconnect()
        {
            string conn = "server=localhost;user id=root;persistsecurityinfo=True;database=gudang";
            return conn; 

        }

        public bool Open()
        {
            try
            {
                sql = "server=" + host + ";Database=" + database + ";User ID=" + userDB + ";Password=" + password;
                con = new MySqlConnection(sql);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error !" + ex.Message, "Information");
            }
            return false;
        }

        public void Close()
        {
            con.Close();
            con.Dispose();
        }

        public DataSet ExecuteDataSet(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                da.Fill(ds, "result");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public MySqlDataReader ExecuteReader(string sql)
        {
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(sql, con);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public int ExecuteNonQuery(string sql)
        {
            try
            {
                int affacted;
                MySqlTransaction mySqlTransaction = con.BeginTransaction();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = sql;
                affacted = cmd.ExecuteNonQuery();
                mySqlTransaction.Commit();
                return affacted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return -1;
        }




    }
}
