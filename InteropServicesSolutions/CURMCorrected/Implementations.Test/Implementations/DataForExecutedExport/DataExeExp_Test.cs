using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.DataAccessLibrary.DataForExecutedExport;
using Contracts.Interfaces.IDataForExecutedExport;
using Contracts.Models.DataForExecutedExport;
using Helpers;
using Npgsql;

namespace Implementations.Test.Implementations.DataForExecutedExport
{
    public class DataExeExp_Test : IDataExeExp
    {
        readonly string _connString = ConfigurationManager.AppSettings.Get("DataExeExpConnString");
        public List<ExecutedExportOutput> GetDataExeExp(ExecutedExportInput input)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                //da se proveri dali parametrite smeat da bidat null
                var output = new List<ExecutedExportOutput>();
                if (input.EDB < 1000000 || input.EDB >= 10000000000000)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Вредноста на параметарот 'едб' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                //treba konsultacija dali e potreben uslovot za proverka dali e null, na site parametri podolu
                if (input.MonthOfExportFrom < 1 || input.MonthOfExportFrom > 12) // || input.MonthOfExportFrom == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'месец од' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (input.MonthOfExportTo < 1 || input.MonthOfExportTo > 12) // || input.MonthOfExportTo == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'месец до' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (input.YearOfExportFrom < 1800 || input.YearOfExportFrom > 5000) // || input.YearOfExportFrom == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'година од' е невалиден. Вредноста на параметарот 'година од' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (input.YearOfExportTo < 1800 || input.YearOfExportTo > 5000) // || input.YearOfExportTo == null
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'година до' е невалиден. Вредноста на параметарот 'година до' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region GettingDataFromInstitutionDatabase

                var conn = new NpgsqlConnection(_connString);
                conn.Open();

                string sql = ConfigurationManager.AppSettings["DataExeExportSQL"];
                var command = new NpgsqlCommand();
                command.Parameters.Add(new NpgsqlParameter("_edb", input.EDB.ToString()));
                SetCommand(ref command, ref sql, input);
                command.CommandText = sql;
                command.Connection = conn;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var temp = new ExecutedExportOutput()
                    {
                        EDB = reader["edb"].ToString(),
                        ExportAmount = (double) reader["export_amount"],
                        ExportYear = (int) reader["year"],
                        ExportMonth = (int) reader["month"]
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

        void SetCommand(ref NpgsqlCommand command, ref string sql, ExecutedExportInput input)
        {
            if (input.AmountOfExportFrom != null)
            {
                sql = String.Concat(sql, " and export_amount >= @_export_amount_from");
                command.Parameters.Add(new NpgsqlParameter("_export_amount_from", input.AmountOfExportFrom));
            }
            if (input.AmountOfExportTo != null)
            {
                sql = String.Concat(sql, " and export_amount <= @_export_amount_to");
                command.Parameters.Add(new NpgsqlParameter("_export_amount_to", input.AmountOfExportTo));
            }
            if (input.YearOfExportFrom != null)
            {
                sql = String.Concat(sql, " and year >= @_year_from");
                command.Parameters.Add(new NpgsqlParameter("_year_from", input.YearOfExportFrom));
            }
            if (input.YearOfExportTo != null)
            {
                sql = String.Concat(sql, " and year <= @_year_to");
                command.Parameters.Add(new NpgsqlParameter("_year_to", input.YearOfExportTo));
            }
            if (input.MonthOfExportFrom != null)
            {
                sql = String.Concat(sql, " and month >= @_month_from");
                command.Parameters.Add(new NpgsqlParameter("_month_from", input.MonthOfExportFrom));
            }
            if (input.MonthOfExportTo != null)
            {
                sql = String.Concat(sql, " and month <= @_month_to");
                command.Parameters.Add(new NpgsqlParameter("_month_to", input.MonthOfExportTo));
            }
        }
    }
}
