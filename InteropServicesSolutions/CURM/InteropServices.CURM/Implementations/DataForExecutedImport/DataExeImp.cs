using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.CURM.Interfaces;
using Npgsql;

namespace InteropServices.CURM.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class DataExeImp : IDataExeImp
    {
        string connString = "Server=10.10.1.50;Port=5432;User Id=test;Password=test;Database=carina";
        public List<ExecutedImportOutput> GetDataExeImp(ExecutedImportInput input)
        {
            if (input.EDB < 1000000 || input.EDB >= 10000000000000)
                throw new System.ArgumentException("Parametarot EDB e nevaliden");
            if (input.MonthOfImportFrom < 1 || input.MonthOfImportFrom > 12)
                throw new System.ArgumentException("Parametarot Mesec od e nevaliden");
            if (input.MonthOfImportTo < 1 || input.MonthOfImportTo > 12)
                throw new System.ArgumentException("Parametarot Mesec do e nevaliden");
            if (input.YearOfImportFrom < 1800 || input.YearOfImportFrom > 5000)
                throw new System.ArgumentException("Parametarot Godina od e nevaliden");
            if (input.YearOfImportTo < 1800 || input.YearOfImportTo > 5000)
                throw new System.ArgumentException("Parametarot Godina do e nevaliden");
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();
            string sql = "SELECT * FROM import_data where edb = @_edb";
            NpgsqlCommand command = new NpgsqlCommand();
            command.Parameters.Add(new NpgsqlParameter("_edb", input.EDB.ToString()));
            
            SetCommand(ref command, ref sql, input);
            command.CommandText = sql;
            command.Connection = conn;
            NpgsqlDataReader reader = command.ExecuteReader();
            List<ExecutedImportOutput> output = new List<ExecutedImportOutput>();
            while (reader.Read())
            {
                ExecutedImportOutput temp = new ExecutedImportOutput()
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
            return output;
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
