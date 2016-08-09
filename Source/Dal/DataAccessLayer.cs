using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HaloBoSach.Dal
{
    public static class DataAccessLayer
    {
        private static string cnnString = ConfigurationManager.ConnectionStrings["HaloBoSach"].ConnectionString;
        public static bool ExcuteNoneQuery(string storeName,params SqlParameter[] param)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            try
            {
                SqlCommand cmd = new SqlCommand(storeName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in param)
                {
                    cmd.Parameters.Add(sqlParameter);
                }
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception exception)
            {
                Utilities.WriteLog(exception.Message + exception.StackTrace);
                return false;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static string ExcuteNoneQueryHasOutput(string storeName,string OutputParamName, params SqlParameter[] param)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            try
            {
                SqlCommand cmd = new SqlCommand(storeName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in param)
                {
                    cmd.Parameters.Add(sqlParameter);
                }
                cnn.Open();
                cmd.ExecuteNonQuery();
                string value = cmd.Parameters[OutputParamName].Value.ToString();
                cnn.Close();
                return value;
            }
            catch (Exception exception)
            {
                Utilities.WriteLog(exception.Message + exception.StackTrace);
                return "";
            }
            finally
            {
                cnn.Close();
            }
        }
        public static DataSet ExecuteDataSet(string storeName, params SqlParameter[] param)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            try
            {
                SqlCommand cmd = new SqlCommand(storeName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null && param.Length>0)
                {
                    foreach (SqlParameter sqlParameter in param)
                    {
                        cmd.Parameters.Add(sqlParameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds=new DataSet();
                cnn.Open();
                da.Fill(ds);
                cnn.Close();
                return ds;
            }
            catch (Exception exception)
            {
                Utilities.WriteLog(exception.Message + exception.StackTrace);
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}