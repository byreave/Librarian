using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public class dbHelper
    {
        public static MySqlConnection conn;
        public static MySqlConnection Conn()
        {
            string strConn = "server=localhost;user id=root;password=;database=Library;Convert Zero Datetime=True;";
                //string strConn = "Data Source=.;Initial Catalog=MyLibraryDB;Integrated Security=True";//ConfigurationManager.AppSettings["conn"];
                if (conn == null)
                {
                    conn = new MySqlConnection(strConn);
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Broken)
                {
                    conn.Close();
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return conn;
        }
        public static int ExecuteCommand(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, Conn());
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public static int ExecuteCommand(MySqlCommand cmd)
        {
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        public static object GetScalar(string sql)
        {
            //MessageBox.Show("123");
            MySqlCommand cmd = new MySqlCommand(sql, Conn());
            object o = cmd.ExecuteScalar();
            //MessageBox.Show("12345");
            conn.Close();
            //MessageBox.Show("123456");

            return o;
        }
        public static DataSet GetDataSet(string sql)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter(sql, Conn());
            DataSet ds = new DataSet();
            adp.Fill(ds);
            conn.Close();
            return ds;
        }

    }
}
