using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Data.SqlClient;

namespace NawareKulCore.DataBL
{
    public class Response
    {

        public Response()
        {
            this.UserErrorList = new List<string>();
            this.UserWarningList = new List<string>();
        }

        // Indication that the function call was successful

        public bool Success { get; set; }

        // Message to return

        public string Message { get; set; }

        // List of strings to be displayed to a user when multiple issues are present.

        public List<string> UserErrorList { get; set; }

        // List of strings to be displayed to a user when multiple warnings are present. 

        public List<string> UserWarningList { get; set; }

    }
    public class Response<Type> : Response
    {
        // Object to return
        public Type Data { get; set; }

    }
    public class ResponseList<Type> : Response
    {

        public ResponseList()
        {
            this.Data = new List<Type>();
        }
        // List containing objects to return
        public List<Type> Data { get; set; }
    }

    public static class ConnectionStringProvider
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetConnectionString()
        {
            return _configuration.GetConnectionString("SqlDBConnection2");
        }
    }

    internal static class CommonDAL
    {
        private static string connectionString = ConnectionStringProvider.GetConnectionString();
        private static StringBuilder _infoBuilder;

        static CommonDAL()
        {
            _infoBuilder = new StringBuilder();
        }

        internal enum DatabaseConnection
        {
            Local = 1,
        }

        static void myConnection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            _infoBuilder.Append(e.Message);
        }

        private static SqlConnection getConnection(DatabaseConnection dbase)
        {
            SqlConnection sqlCon;
            switch (dbase)
            {
                case DatabaseConnection.Local:
                    sqlCon = new SqlConnection(connectionString);
                    break;
                default:
                    sqlCon = new SqlConnection(connectionString);
                    break;
            }

            return sqlCon;
        }

        internal static Response ExecuteSql(string sql, DatabaseConnection dbase, CommandType commandType,
            ref List<SqlParameter> paramList, bool logException = true)
        {
            Response response = new Response();
            SqlConnection sqlCon = getConnection(dbase);
            sqlCon.InfoMessage += new SqlInfoMessageEventHandler(myConnection_InfoMessage);
            SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
            sqlCmd.CommandTimeout = 120;
            if (paramList != null) sqlCmd.Parameters.AddRange(paramList.ToArray());
            sqlCmd.CommandType = commandType;

            if (commandType == CommandType.Text) sql = sql.Replace("'", "''");

            try
            {
                sqlCmd.Connection.Open();
                sqlCmd.ExecuteNonQuery();
                response.Success = true;

                if (!string.IsNullOrEmpty(_infoBuilder.ToString()))
                {
                    string[] warnings = _infoBuilder.ToString().Split('~');
                    foreach (string warning in warnings)
                    {
                        response.UserWarningList.Add(warning);
                    }
                }
            }
            catch (SqlException se)
            {
                response.Message = se.ToString();
                //response.UserErrorList.Add(ExceptionHelper.GetErrorMessage(se));
                response.Success = false;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }
            finally
            {
                if (sqlCmd.Connection.State != ConnectionState.Closed) sqlCmd.Connection.Close();
            }
            return response;
        }

        internal static Response<object> ExecuteSql_Scalar(string sql, DatabaseConnection dbase, CommandType commandType, ref List<SqlParameter> paramList, bool logException = true)
        {
            SqlConnection sqlCon = getConnection(dbase);
            sqlCon.InfoMessage += new SqlInfoMessageEventHandler(myConnection_InfoMessage);

            SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
            if (paramList != null) sqlCmd.Parameters.AddRange(paramList.ToArray());
            sqlCmd.CommandType = commandType;
            object result;
            Response<object> response = new Response<object>();
            try
            {
                sqlCmd.Connection.Open();
                result = sqlCmd.ExecuteScalar();
                response.Data = result;
                response.Success = true;

                if (!string.IsNullOrEmpty(_infoBuilder.ToString()))
                {
                    string[] warnings = _infoBuilder.ToString().Split('~');
                    foreach (string warning in warnings)
                    {
                        response.UserWarningList.Add(warning);
                    }
                }
            }
            catch (SqlException se)
            {
                response.Message = se.ToString();
                response.Success = false;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }
            finally
            {
                if (sqlCmd.Connection.State != ConnectionState.Closed) sqlCmd.Connection.Close();
            }

            return response;
        }

        internal static Response<DataSet> ExecuteSql_DataSet(string sql, DatabaseConnection dbase, CommandType commandType, ref List<SqlParameter> paramList, bool logException = true)
        {
            SqlConnection sqlCon = getConnection(dbase);
            sqlCon.InfoMessage += new SqlInfoMessageEventHandler(myConnection_InfoMessage);

            SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
            Response<DataSet> response = new Response<DataSet>();

            if (paramList != null) sqlCmd.Parameters.AddRange(paramList.ToArray());

            sqlCmd.CommandType = commandType;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            try
            {
                da.Fill(ds);
                response.Data = ds;
                response.Success = true;

                if (!string.IsNullOrEmpty(_infoBuilder.ToString()))
                {
                    string[] warnings = _infoBuilder.ToString().Split('~');
                    foreach (string warning in warnings)
                    {
                        response.UserWarningList.Add(warning);
                    }
                }
            }
            catch (SqlException se)
            {
                response.Message = se.ToString();
                response.Success = false;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }
            return response;
        }

        internal static Response<DataSet> ExecuteReader(string sql, DatabaseConnection dbase, CommandType commandType,
            ref List<SqlParameter> paramList, bool logException = true)
        {
            SqlConnection sqlCon = getConnection(dbase);
            sqlCon.InfoMessage += new SqlInfoMessageEventHandler(myConnection_InfoMessage);

            Response<DataSet> response = new Response<DataSet>();
            response.Data = new DataSet();
            try
            {
                using (sqlCon)
                {
                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    cmd.CommandType = commandType;
                    if (paramList != null) cmd.Parameters.AddRange(paramList.ToArray());
                    sqlCon.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        do
                        {
                            var table = new DataTable();
                            table.Load(reader);
                            response.Data.Tables.Add(table);
                        } while (!reader.IsClosed);
                    }
                    sqlCon.Close();

                    if (!string.IsNullOrEmpty(_infoBuilder.ToString()))
                    {
                        string[] warnings = _infoBuilder.ToString().Split('~');
                        foreach (string warning in warnings)
                        {
                            response.UserWarningList.Add(warning);
                        }
                    }
                    response.Success = true;
                }
            }
            catch (SqlException se)
            {
                response.Success = false;
                response.Message = se.ToString();
                
            }
            catch (Exception e)
            {
                response.Message = e.ToString();
                response.Success = false;
            }
            return response;
        }

        private static string GetParamListString(List<SqlParameter> plist)
        {
            string result = "";

            foreach (SqlParameter param in plist)
            {
                string val = (object.Equals(param.Value, null) ? "" : param.Value.ToString());

                result += param.ParameterName + ":" + val + ";";
            }

            return result;
        }
    }
}