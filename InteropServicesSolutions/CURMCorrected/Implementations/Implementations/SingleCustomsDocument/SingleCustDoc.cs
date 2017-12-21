using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Services;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.ISingleCustomsDocument;
using Contracts.Models.SingleCustomsDocument;
using Helpers;
using Implementations.Helpers.SingleCustomsDocument;
using Oracle.ManagedDataAccess.Client;

namespace Implementations.Implementations.SingleCustomsDocument
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/", IncludeExceptionDetailInFaults = true)]
    public class SingleCustDoc : ISingleCustDoc
    {
        OracleConnection _connection;
        readonly string _connectionString = ConfigurationManager.AppSettings.Get("SingleCustDocConnString");
        List<string> _sqls = new List<string>
        {
            "SELECT * FROM SAD_GEN WHERE SAD_GEN.KEY_YEAR=:pKeyYear AND SAD_GEN.KEY_CUO=:pKeyCuo AND SAD_GEN.KEY_DEC=:pKeyDec AND SAD_GEN.KEY_NBER=:pKeyNber",
            "SELECT * FROM SAD_ITM WHERE SAD_ITM.KEY_YEAR=:pKeyYear AND SAD_ITM.KEY_CUO=:pKeyCuo AND SAD_ITM.KEY_DEC=:pKeyDec AND SAD_ITM.KEY_NBER=:pKeyNber",
            "SELECT * FROM SAD_OCC_EXP WHERE SAD_OCC_EXP.KEY_YEAR=:pKeyYear AND SAD_OCC_EXP.KEY_CUO=:pKeyCuo AND SAD_OCC_EXP.KEY_DEC=:pKeyDec AND SAD_OCC_EXP.KEY_NBER=:pKeyNber",
            "SELECT * FROM SAD_OCC_CNS WHERE SAD_OCC_CNS.KEY_YEAR=:pKeyYear AND SAD_OCC_CNS.KEY_CUO=:pKeyCuo AND SAD_OCC_CNS.KEY_DEC=:pKeyDec AND SAD_OCC_CNS.KEY_NBER=:pKeyNber"
        };

        readonly string _sql = ConfigurationManager.AppSettings.Get("SingleCustDocSQL");
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.UseVerboseErrors = true;
        }
        public SingleCustomsDocumentOutput GetSingleCustDoc(int year, long edbOfShippingCompany, int numberOfCustomsOffice, int regNumber)
        {
            var output = new SingleCustomsDocumentOutput();
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (edbOfShippingCompany < 1000000 || edbOfShippingCompany >= 10000000000000)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Вредноста на параметарот 'едб' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (year < 1800 || year > 5000)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Вредноста на параметарот 'година' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region GettingDataFromInstitutionDatabase

                OpenConnection();
                var readers = GetData(_connection, _sqls, year, edbOfShippingCompany, numberOfCustomsOffice, regNumber);
                output.GeneralData = ObjectGeneratorHelper.GetGeneralData(readers[0]);
                output.ItemData = ObjectGeneratorHelper.GetItemData(readers[1]);
                output.ExporterData = ObjectGeneratorHelper.GetExporterData(readers[2]);
                output.ImporterData = ObjectGeneratorHelper.GetImporterData(readers[3]);
                CloseConnection();

                if (String.IsNullOrEmpty(output.GeneralData.DeclarantCode) || output.GeneralData == null)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не постојат податоци за внесените параметри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                return output;
            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (OracleException)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Конекцијата до базата на ЦУРМ не може да се воспостави!");
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
            catch (Exception ex)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Настана грешка во адаптерот или при повикување на сервисот на институцијата:", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
        }

        public List<CustomsOfficeNumbers> GetCustomsOfficeNumbers()
        {
            OpenConnection();
            var command = new OracleCommand(_sql, _connection);
            var reader = command.ExecuteReader();
            var output = new List<CustomsOfficeNumbers>();
            while (reader.Read())
            {
                var temp = new CustomsOfficeNumbers()
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
        List<OracleDataReader> GetData(OracleConnection connection, List<string> sqls, int year, long edbOfShippingCompany, int numberOfCustomsOffice, int regNumber)
        {
            var output = new List<OracleDataReader>();
            for (int i = 0; i < sqls.Count; i++)
            {
                var command = new OracleCommand(sqls[i], connection);
                command.Parameters.Add(new OracleParameter("pKeyYear", year.ToString()));
                command.Parameters.Add(new OracleParameter("pKeyCuo", numberOfCustomsOffice.ToString()));
                command.Parameters.Add(new OracleParameter("pKeyDec", edbOfShippingCompany.ToString()));
                command.Parameters.Add(new OracleParameter("pKeyNber", regNumber.ToString()));
                var reader = command.ExecuteReader();
                output.Add(reader);
            }
            return output;
        }
    }
}
