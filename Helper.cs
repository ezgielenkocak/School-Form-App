using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Helper
    {
        SqlConnection cn;
        SqlCommand cmd;

        public int ExecuteNonQuery(string cmdtext)
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cstr"].ConnectionString);
                cmd = new SqlCommand(cmdtext, cn);
                cn.Open();
                int sonuc = cmd.ExecuteNonQuery();
                cn.Close();
                return sonuc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SqlDataReader ExecuteReader(string cmdtext)
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cstr"].ConnectionString);
            cmd = new SqlCommand(cmdtext, cn);
            cn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);

        }
    }
}
