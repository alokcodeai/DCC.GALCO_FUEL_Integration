using Microsoft.Data.SqlClient;
using SAPbobsCOM;
using System.Data;
using System.Reflection;
using System.Text;

namespace DCC.GALCO_FUEL_Integration.Controllers
{
    public class Common
    {
        private readonly IConfiguration _configuration;

        public string LogFilePath = AppDomain.CurrentDomain.BaseDirectory + @"LogFile.txt";
        public SAPbobsCOM.Company SboCompany { get; set; }
        public string Company_Server { get; set; }
        public string Company_DbUserPwd { get; set; }
        public string Company_UserID { get; set; }
        public string Company_Database { get; set; }
        public Int32 DbServerType { get; set; }
        public string Company_LicenseServer { get; set; }
        public string LogFileName { get; set; }
        public string Company_Password { get; internal set; }
        public string Company_DbUserID { get; internal set; }
        public string DBTYPE { get; private set; }

        public Common(IConfiguration configuration)
        {
            _configuration = configuration;
            ValidateCompanyConnection();
        }

        public void Set_RequiredFields()
        {
            Company_Server = _configuration["SAP:Company_Server"];
            Company_DbUserPwd = _configuration["SAP:Company_DbUserPwd"];
            Company_UserID = _configuration["SAP:Company_UserID"];
            Company_Database = _configuration["SAP:Company_Database"];
            DbServerType = int.Parse(_configuration["SAP:DbServerType"]);
            Company_LicenseServer = _configuration["SAP:Company_LicenseServer"];
            Company_Password = _configuration["SAP:Company_Password"];
            Company_DbUserID = _configuration["SAP:Company_DbUserID"];
            DBTYPE = _configuration["SAP:DBTYPE"];
        }

        public int ConnectWithB1Company()
        {
            SboCompany = (SAPbobsCOM.Company)Activator.CreateInstance(Type.GetTypeFromProgID("SAPbobsCOM.Company"));
            SboCompany.Server = Company_Server;
            SboCompany.CompanyDB = Company_Database;
            SboCompany.UserName = Company_UserID;
            SboCompany.Password = Company_Password;
            SboCompany.DbUserName = Company_DbUserID;
            SboCompany.DbPassword = Company_DbUserPwd;
            SboCompany.UseTrusted = false;

            if (Company_LicenseServer.Length > 0)
                SboCompany.LicenseServer = Company_LicenseServer;

            if (DbServerType == 2016)
                SboCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;

           else if (DbServerType == 2017)
                SboCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017;

            else if (DbServerType == 2019)
                SboCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;

            if (DBTYPE == "SAPHANA")
            {
                //SboCompany.Server = Company_Server + ":30015";
                //SboCompany.DbServerType = BoDataServerTypes.dst_HANADB;
                //SboCompany.LicenseServer = Company_LicenseServer + ":30015";
                //SboCompany.SLDServer = Company_LicenseServer + ":40000";
            }

            SboCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
            Int32 result = SboCompany.Connect();
            if (result != 0)
            {
                string message = SboCompany.GetLastErrorDescription();
                ErrorLog(String.Format("Error when connecting to SBO Company - {0}, message - {1}", result, SboCompany.GetLastErrorDescription()));
            }
            return result;
        }

        public static string GetExecutingAssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location.Replace(Assembly.GetExecutingAssembly().GetName().Name + ".exe", "");
        }

        public void ValidateCompanyConnection()
        {
            try
            {
                Set_RequiredFields();

                if (SboCompany != null)
                {
                    if (SboCompany.Connected == false)
                    {
                        if (ConnectWithB1Company() != 0) { };
                    }
                }
                else
                {
                    if (ConnectWithB1Company() != 0) { };
                }
            }
            catch (Exception ex)
            {
                ErrorLog("ValidateCompanyConnection() Error: " + ex.ToString());
            }

        }

        public void ErrorLog(string Message)
        {
            FileStream fs = new FileStream(LogFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(Message);
            sw.Flush();
            sw.Close();
        }

        //public DataTable ConvertToDataTable(Recordset recordset)
        //{
        //    DataTable dataTable = new DataTable();

        //    // Add columns to DataTable based on Recordset fields
        //    for (int i = 0; i < recordset.Fields.Count; i++)
        //    {
        //        dataTable.Columns.Add(recordset.Fields.Item(i).Name);
        //    }

        //    // Add rows to DataTable based on Recordset data
        //    while (!recordset.EoF)
        //    {
        //        DataRow row = dataTable.NewRow();
        //        for (int i = 0; i < recordset.Fields.Count; i++)
        //        {
        //            row[i] = recordset.Fields.Item(i).Value;
        //        }
        //        dataTable.Rows.Add(row);
        //        recordset.MoveNext();
        //    }

        //    return dataTable;
        //}

        public List<Dictionary<string, object>> ConvertToDictionaryList(SAPbobsCOM.Recordset recordset)
        {
            var list = new List<Dictionary<string, object>>();

            // Iterate over each row in the Recordset
            while (!recordset.EoF)
            {
                var dict = new Dictionary<string, object>();

                // Add each field as a key-value pair in the dictionary
                for (int i = 0; i < recordset.Fields.Count; i++)
                {
                    dict[recordset.Fields.Item(i).Name] = recordset.Fields.Item(i).Value;
                }

                list.Add(dict);
                recordset.MoveNext();
            }

            return list;
        }

        public List<T> ConvertToList<T>(Recordset recordset) where T : new()
        {
            var result = new List<T>();

            if (recordset == null || recordset.RecordCount == 0)
                return result;

            recordset.MoveFirst();

            while (!recordset.EoF)
            {
                T item = new T();
                Type type = typeof(T);

                for (int i = 0; i < recordset.Fields.Count; i++)
                {
                    string fieldName = recordset.Fields.Item(i).Name;
                    object value = recordset.Fields.Item(i).Value;

                    PropertyInfo property = type.GetProperty(fieldName);
                    if (property != null && value != null && value != DBNull.Value)
                    {
                        property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                    }
                }

                result.Add(item);
                recordset.MoveNext();
            }

            return result;
        }

        public object ExecuteScalar(string Query, string connectionstring)
        {
            object result = null;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    result = cmd.ExecuteScalar();
                }
            }
            return result;
        }

        public int ExecuteNonQuery(string Query, string connectionstring)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public DataTable ExecuteNonQueryWithDataTable(string Query, string connectionstring)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlDataAdapter adt = new SqlDataAdapter(Query, con))
                {
                    adt.Fill(dt);
                }
            }
            return dt;
        }

    }

    public static class StringExtention
    {
        public static string clean(this string s)
        {
            return new StringBuilder(s)
                  .Replace("&", "and")
                  .Replace(",", "")
                  .Replace("<", "")
                  .Replace(">", "")
                  .Replace("'", "")
                  .Replace(".", "")
                  .Replace("/", "")
                  .Replace("\\", "")
                  .Replace("//", "")
                  .Replace("?", "")
                  .Replace("^", "")
                  .Replace("!", "")
                  .Replace("@", "")
                  .Replace("#", "")
                  .Replace("$", "")
                  .Replace("\"", "")
                  .Replace("eacute;", "é")
                  .ToString()
                  .Trim('"')
                  .ToLower();
        }
    }
}
