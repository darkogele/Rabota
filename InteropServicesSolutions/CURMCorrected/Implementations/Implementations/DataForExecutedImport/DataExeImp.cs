using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.DataAccessLibrary.DataForExecutedImport;
using Contracts.Interfaces.IDataForExecutedImport;
using Contracts.Models.DataForExecutedImport;
using Helpers;
using Npgsql;

namespace Implementations.Implementations.DataForExecutedImport
{
    public class DataExeImp : IDataExeImp
    {
        readonly string _connString = ConfigurationManager.AppSettings.Get("DataExeImportConnString");
        public List<ExecutedImportOutput> GetDataExeImp(ExecutedImportInput input)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                //da se proveri dali parametrite smeat da bidat null
                if (input.EDB < 1000000 || input.EDB >= 10000000000000)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Вредноста на параметарот 'едб' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                //treba konsultacija dali e potreben uslovot za proverka dali e null, na site parametri podolu
                if (input.MonthOfImportFrom < 1 || input.MonthOfImportFrom > 12)// || input.MonthOfImportFrom == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'месец од' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (input.MonthOfImportTo < 1 || input.MonthOfImportTo > 12)// || input.MonthOfImportTo == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'месец до' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (input.YearOfImportFrom < 1800 || input.YearOfImportFrom > 5000)// || input.YearOfImportFrom == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'година од' е невалиден. Вредноста на параметарот 'година од' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (input.YearOfImportTo < 1800 || input.YearOfImportTo > 5000)// || input.YearOfImportTo == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'година до' е невалиден. Вредноста на параметарот 'година до' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region GettingDataFromInstitutionDatabase

                var conn = new NpgsqlConnection(_connString);
                conn.Open();

                string sql = ConfigurationManager.AppSettings.Get("DataExeImportSQL");
                var command = new NpgsqlCommand();
                command.Parameters.Add(new NpgsqlParameter("_edb", input.EDB.ToString()));

                SetCommand(ref command, ref sql, input);
                command.CommandText = sql;
                command.Connection = conn;
                var reader = command.ExecuteReader();
                var output = new List<ExecutedImportOutput>();
                while (reader.Read())
                {
                    var temp = new ExecutedImportOutput()
                    {
                        EDB = reader["edb"].ToString(),
                        ImportAmount = (double)reader["import_amount"],
                        ImportTaxAmount = (double)reader["import_vat"],
                        ImportYear = (int)reader["year"],
                        ImportMonth = (int)reader["month"]
                    };
                    output.Add(temp);
                }
                conn.Close();

                if (output.Count == 0)
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
            catch (SocketException)
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
        void SetCommand(ref NpgsqlCommand command, ref string sql, ExecutedImportInput input)
        {
            if (input.AmountOfImportFrom != null)
            {
                sql = String.Concat(sql, " and import_amount >= @_import_amount_from");
                command.Parameters.Add(new NpgsqlParameter("_import_amount_from", input.AmountOfImportFrom));
            }
            if (input.AmountOfImportTo != null)
            {
                sql = String.Concat(sql, " and import_amount <= @_import_amount_to");
                command.Parameters.Add(new NpgsqlParameter("_import_amount_to", input.AmountOfImportTo));
            }
            if (input.AmountOfImportTaxFrom != null)
            {
                sql = String.Concat(sql, " and import_vat >= @_import_vat_from");
                command.Parameters.Add(new NpgsqlParameter("_import_vat_from", input.AmountOfImportTaxFrom));
            }
            if (input.AmountOfImportTaxTo != null)
            {
                sql = String.Concat(sql, " and import_vat <= @_import_vat_to");
                command.Parameters.Add(new NpgsqlParameter("_import_vat_to", input.AmountOfImportTaxTo));
            }
            if (input.YearOfImportFrom != null)
            {
                sql = String.Concat(sql, " and year >= @_year_from");
                command.Parameters.Add(new NpgsqlParameter("_year_from", input.YearOfImportFrom));
            }
            if (input.YearOfImportTo != null)
            {
                sql = String.Concat(sql, " and year <= @_year_to");
                command.Parameters.Add(new NpgsqlParameter("_year_to", input.YearOfImportTo));
            }
            if (input.MonthOfImportFrom != null)
            {
                sql = String.Concat(sql, " and month >= @_month_from");
                command.Parameters.Add(new NpgsqlParameter("_month_from", input.MonthOfImportFrom));
            }
            if (input.MonthOfImportTo != null)
            {
                sql = String.Concat(sql, " and month <= @_month_to");
                command.Parameters.Add(new NpgsqlParameter("_month_to", input.MonthOfImportTo));
            }
        }
    }
}
