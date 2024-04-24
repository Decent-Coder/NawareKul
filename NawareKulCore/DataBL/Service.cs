using System.Data;
using Microsoft.Data.SqlClient;
using NawareKulCore.Models;

namespace NawareKulCore.DataBL
{
    public static class Service
    {
        internal static Response<List<NawarePeopleDTO>> Get_ListOfNawarePeople()
        {
            string sql = "Select ID, Name, Phone, City, Std, Address, Area, Pincode from dbo.userinformation Order By Name asc";
            Response<List<NawarePeopleDTO>> response = new Response<List<NawarePeopleDTO>>();
            response.Data = new List<NawarePeopleDTO>();

            List<SqlParameter> spParams = new List<SqlParameter>()
            {
                //new SqlParameter("@accountID", acct.AccountID),
                //new SqlParameter("@accountTypeID", acct.AccountTypeID),
                //new SqlParameter("@isServiceNow", eventData.isServiceNow),
                //new SqlParameter("@activityLogID", activityLogID),
                //new SqlParameter("@groupID", eventData.groupID),
                //new SqlParameter("@CustID", eventData.customerID),
                //new SqlParameter("@eventSource", "Phone"),
                //new SqlParameter("@typeOfSave", uiSaveType),
                //new SqlParameter("@isMultipleEvent", eventData.isMultipleEvent),
                //new SqlParameter("@isMasterEvent", eventData.isMasterEvent),
                //new SqlParameter("@MasterEventNumber", eventData.masterEventNumber),
                //new SqlParameter("@eventStatusID", 1),
                //new SqlParameter("@callerName", eventData.callerName),
                //new SqlParameter("@callerNumber", eventData.callerPhone),
                //new SqlParameter("@callerNumberExt", eventData.callerExt),
                //new SqlParameter("@callBeginDateTime", eventData.callDate),
                //new SqlParameter("@DriverName", eventData.DriverName),
                //new SqlParameter("@DriverNumber", eventData.DriverNumber),
                //new SqlParameter("@ContactName", eventData.ContactName),
                //new SqlParameter("@ContactNumber", eventData.ContactNumber),
                //new SqlParameter("@EventType", eventData.TypeOfEvent),
                //new SqlParameter("@CallerType", eventData.TypeOfCaller),
                //new SqlParameter("@TypeofRequest", eventData.TypeOfRequest),
                //new SqlParameter("@UserProvidedCompanyName", eventData.UserProvidedCompanyName),
                //new SqlParameter("@eventNumber", 0),
                //new SqlParameter("@eventID", 0)
            };


            Response<DataSet> result = new Response<DataSet>();

            try
            {
                result = CommonDAL.ExecuteSql_DataSet(sql, CommonDAL.DatabaseConnection.Local, CommandType.Text, ref spParams);
                response.Data = FillEventFromDataSet(result.Data);
                response.Success = true;
            }


            catch (SqlException se)
            {
                response.Success = false;
                response.Message = se.ToString();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }

            return response;
        }
        internal static Response<NawarePeopleDTO> Get_UserById(int id)
        {
            string sql = "Select ID, Name, Phone, City, Std, Address, Area, Pincode from dbo.userinformation where Id = @Id";
            Response<NawarePeopleDTO> response = new Response<NawarePeopleDTO>();
            response.Data = new NawarePeopleDTO();

            List<SqlParameter> spParams = new List<SqlParameter>()
            {
                new SqlParameter("@id", id),
            };


            Response<DataSet> result = new Response<DataSet>();

            try
            {
                result = CommonDAL.ExecuteSql_DataSet(sql, CommonDAL.DatabaseConnection.Local, CommandType.Text, ref spParams);
                response.Data = FillUserInfo(result.Data);
                response.Success = true;
            }


            catch (SqlException se)
            {
                response.Success = false;
                response.Message = se.ToString();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }

            return response;
        }

        internal static Response<AncestorInfoDTO> Get_AncestoreInformationById(int id)
        {
            string sql = "Select FatherID, FatherName, GrandFatherID, GrandFatherName, SuperGrandFatherID, SuperGrandFatherName, popularperson, info, kulinfo from dbo.ancestorsname where id = @id";
            Response<AncestorInfoDTO> response = new Response<AncestorInfoDTO>();
            response.Data = new AncestorInfoDTO();

            List<SqlParameter> spParams = new List<SqlParameter>()
            {
                new SqlParameter("@id", id)
            };


            Response<DataSet> result = new Response<DataSet>();

            try
            {
                result = CommonDAL.ExecuteSql_DataSet(sql, CommonDAL.DatabaseConnection.Local, CommandType.Text, ref spParams);
                response.Data = FillAncestorInfo(result.Data);
                response.Success = true;
            }


            catch (SqlException se)
            {
                response.Success = false;
                response.Message = se.ToString();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }

            return response;
        }

        internal static Response<AncestorInfoDTO> Get_BasicInformationById(int id)
        {
            string sql = "Select FatherID, FatherName, GrandFatherID, GrandFatherName, SuperGrandFatherID, SuperGrandFatherName, popularperson, info, kulinfo from dbo.ancestorsname where id = @id";
            Response<AncestorInfoDTO> response = new Response<AncestorInfoDTO>();
            response.Data = new AncestorInfoDTO();

            List<SqlParameter> spParams = new List<SqlParameter>()
            {
                new SqlParameter("@id", id)
            };


            Response<DataSet> result = new Response<DataSet>();

            try
            {
                result = CommonDAL.ExecuteSql_DataSet(sql, CommonDAL.DatabaseConnection.Local, CommandType.Text, ref spParams);
                response.Data = FillAncestorInfo(result.Data);
                response.Success = true;
            }


            catch (SqlException se)
            {
                response.Success = false;
                response.Message = se.ToString();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }

            return response;
        }

        internal static AncestorInfoDTO FillAncestorInfo(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var ancestorInfoDTO = new AncestorInfoDTO();
                    ancestorInfoDTO.FatherId = NullSafeOperations.NullToInt(row["FatherID"]);
                    ancestorInfoDTO.FatherName = NullSafeOperations.NullToString(row["FatherName"]);
                    ancestorInfoDTO.GrandFatherId = NullSafeOperations.NullToInt(row["GrandFatherID"]);
                    ancestorInfoDTO.GrandFatherName = NullSafeOperations.NullToString(row["GrandFatherName"]);
                    ancestorInfoDTO.SuperGrandFatherId = NullSafeOperations.NullToInt(row["SuperGrandFatherID"]);
                    ancestorInfoDTO.SuperGrandFatherName = NullSafeOperations.NullToString(row["SuperGrandFatherName"]);

                    return ancestorInfoDTO;
                }
            }
            return new AncestorInfoDTO();
        }

        internal static NawarePeopleDTO FillUserInfo(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var ancestorInfoDTO = new NawarePeopleDTO();
                    ancestorInfoDTO.Id = NullSafeOperations.NullToInt(row["Id"]);
                    ancestorInfoDTO.Name = NullSafeOperations.NullToString(row["Name"]);
                    ancestorInfoDTO.Area = NullSafeOperations.NullToString(row["Area"]);
                    ancestorInfoDTO.Address = NullSafeOperations.NullToString(row["Address"]);
                    ancestorInfoDTO.Phone = NullSafeOperations.NullToString(row["Phone"]);
                    ancestorInfoDTO.City = NullSafeOperations.NullToString(row["City"]);
                    ancestorInfoDTO.STD = NullSafeOperations.NullToString(row["STD"]);
                    ancestorInfoDTO.PinCode = NullSafeOperations.NullToString(row["PinCode"]);

                    return ancestorInfoDTO;
                }
            }
            return new NawarePeopleDTO();
        }

        internal static List<NawarePeopleDTO> FillEventFromDataSet(DataSet ds)
        {
            List<NawarePeopleDTO> listOfPeople = new List<NawarePeopleDTO>();


            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var people = new NawarePeopleDTO();
                    people.Id = NullSafeOperations.NullToInt(row["ID"]);
                    people.Name = NullSafeOperations.NullToString(row["Name"]);
                    people.Phone = NullSafeOperations.NullToString(row["Phone"]);
                    people.STD = NullSafeOperations.NullToString(row["Std"]);
                    people.PinCode = NullSafeOperations.NullToString(row["PinCode"]);
                    people.Address = NullSafeOperations.NullToString(row["Address"]);
                    people.Area = NullSafeOperations.NullToString(row["Area"]);
                    people.City = NullSafeOperations.NullToString(row["City"]);

                    listOfPeople.Add(people);
                }
            }

            return listOfPeople;
        }
    }

    internal static class NullSafeOperations
    {

        internal static int NullToInt(object valueToCheck, int defaultValue = 0)
        {
            int result; //If you can parse it return it, else default value
            return ((int.TryParse(valueToCheck.ToString(), out result)) ? result : defaultValue);
        }

        internal static decimal NullToDecimal(object valueToCheck, decimal defaultValue = 0)
        {
            decimal result;
            return ((decimal.TryParse(valueToCheck.ToString(), out result)) ? result : defaultValue);
        }

        internal static byte NullToByte(object valueToCheck, byte defaultValue = 0)
        {
            byte result;
            return ((byte.TryParse(valueToCheck.ToString(), out result)) ? result : defaultValue);
        }

        internal static string NullToString(object valueToCheck, string defaultValue = "")
        {
            return ((valueToCheck != DBNull.Value && valueToCheck != null) ? valueToCheck.ToString() : defaultValue);
        }

        internal static bool NullToBool(object valueToCheck, bool defaultValue = false)
        {
            return ((valueToCheck != DBNull.Value && valueToCheck != null) ? Convert.ToBoolean(valueToCheck) : defaultValue);
        }

        internal static bool? NullableBoolValue(object valueToCheck)
        {
            if (valueToCheck != DBNull.Value && valueToCheck != null)
                return Convert.ToBoolean(valueToCheck);
            else
                return null;
        }

        internal static DateTime NullToDateTime(object valueToCheck, DateTime defaultValue = new DateTime())
        {
            DateTime result;
            if (defaultValue == new DateTime()) defaultValue = new DateTime(1900, 1, 1);
            return ((DateTime.TryParse(valueToCheck.ToString(), out result)) ? result : defaultValue);
        }

        internal static byte[] NullToByteArray(object valueToCheck)
        {
            return ((valueToCheck != DBNull.Value && valueToCheck != null) ? (byte[])(valueToCheck) : new byte[0]);
        }

        internal static double NullToDouble(object valueToCheck, double defaultValue = 0)
        {
            double result;
            return ((double.TryParse(valueToCheck.ToString(), out result)) ? result : defaultValue);
        }
    }
}