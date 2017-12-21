using InteropServices.CURM.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Data.Services;

namespace InteropServices.CURM.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/", IncludeExceptionDetailInFaults = true)]
    public class SingleCustDoc: ISingleCustDoc
    {
        OracleConnection _connection = null;
        string _connectionString = "User Id=interopera;Password=Int3R0pEr@;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.10.0.80)(PORT=1521))(CONNECT_DATA=(SID=db10g)));";
        List<string> _sqls = new List<string>
        {
            "SELECT * FROM SAD_GEN WHERE SAD_GEN.KEY_YEAR=:pKeyYear AND SAD_GEN.KEY_CUO=:pKeyCuo AND SAD_GEN.KEY_DEC=:pKeyDec AND SAD_GEN.KEY_NBER=:pKeyNber",
            "SELECT * FROM SAD_ITM WHERE SAD_ITM.KEY_YEAR=:pKeyYear AND SAD_ITM.KEY_CUO=:pKeyCuo AND SAD_ITM.KEY_DEC=:pKeyDec AND SAD_ITM.KEY_NBER=:pKeyNber",
            "SELECT * FROM SAD_OCC_EXP WHERE SAD_OCC_EXP.KEY_YEAR=:pKeyYear AND SAD_OCC_EXP.KEY_CUO=:pKeyCuo AND SAD_OCC_EXP.KEY_DEC=:pKeyDec AND SAD_OCC_EXP.KEY_NBER=:pKeyNber",
            "SELECT * FROM SAD_OCC_CNS WHERE SAD_OCC_CNS.KEY_YEAR=:pKeyYear AND SAD_OCC_CNS.KEY_CUO=:pKeyCuo AND SAD_OCC_CNS.KEY_DEC=:pKeyDec AND SAD_OCC_CNS.KEY_NBER=:pKeyNber"
        };
        string sql = "SELECT * FROM CARINSKI_ISPOSTAVI";
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.UseVerboseErrors = true;
        }
        public SingleCustomsDocumentOutput GetSingleCustDoc(int year, long EDBOfShippingCompany,  int NumberOfCustomsOffice, int regNumber)
        {
            SingleCustomsDocumentOutput output = new SingleCustomsDocumentOutput();
            try
            {
                if (EDBOfShippingCompany < 1000000 || EDBOfShippingCompany >= 10000000000000)
                    throw new System.ArgumentException("Parametarot EDB e nevaliden");
                if (year < 1800 || year > 5000)
                    throw new System.ArgumentException("Parametarot Godina e nevaliden");
                OpenConnection();
                List<OracleDataReader> readers = GetData(_connection, _sqls, year, EDBOfShippingCompany, NumberOfCustomsOffice, regNumber);
                output.GeneralData = ObjectGeneratorHelper.GetGeneralData(readers[0]);
                output.ItemData = ObjectGeneratorHelper.GetItemData(readers[1]);
                output.ExporterData = ObjectGeneratorHelper.GetExporterData(readers[2]);
                output.ImporterData = ObjectGeneratorHelper.GetImporterData(readers[3]);
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        public List<CustomsOfficeNumbers> GetCustomsOfficeNumbers()
        {
            OpenConnection();
            OracleCommand command = new OracleCommand(sql, _connection);
            OracleDataReader reader = command.ExecuteReader();
            List<CustomsOfficeNumbers> output = new List<CustomsOfficeNumbers>();
            while (reader.Read())
            {
                CustomsOfficeNumbers temp = new CustomsOfficeNumbers()
                {
                    CustomsOfficeNumber = (string)reader["KEY_CUO"].ToString(),
                    CustomsOfficeName = (string)reader["NAZIV"].ToString()
                };
                output.Add(temp);
            }
            CloseConnection();
            return output;
        }
        void OpenConnection()
        {
            _connection = new OracleConnection(_connectionString);
            _connection.Open();
        }
        void CloseConnection()
        {
            _connection.Close();
        }
        List<OracleDataReader> GetData(OracleConnection connection, List<string> sqls, int year, long EDBOfShippingCompany, int NumberOfCustomsOffice, int regNumber)
        {
            List<OracleDataReader> output = new List<OracleDataReader>();
            for (int i = 0; i < sqls.Count; i++)
            {
                OracleCommand command = new OracleCommand(sqls[i], connection);
                command.Parameters.Add(new OracleParameter("pKeyYear", year.ToString()));
                command.Parameters.Add(new OracleParameter("pKeyCuo", NumberOfCustomsOffice.ToString()));
                command.Parameters.Add(new OracleParameter("pKeyDec", EDBOfShippingCompany.ToString()));
                command.Parameters.Add(new OracleParameter("pKeyNber", regNumber.ToString()));
                OracleDataReader reader = command.ExecuteReader();
                output.Add(reader);
            }
            return output;
        }
    }
}