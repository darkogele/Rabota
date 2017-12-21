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
    public class DataExeExp : IDataExeExp
    {
        string connString = "Server=10.10.1.50;Port=5432;User Id=test;Password=test;Database=carina";
        public List<ExecutedExportOutput> GetDataExeExp(ExecutedExportInput input)
        {
            List<ExecutedExportOutput> output = new List<ExecutedExportOutput>();
            if (input.EDB < 1000000 || input.EDB >= 10000000000000)
                throw new System.ArgumentException("Parametarot EDB e nevaliden");
            if (input.MonthOfExportFrom < 1 || input.MonthOfExportFrom > 12)
                throw new System.ArgumentException("Parametarot Mesec od e nevaliden");
            if (input.MonthOfExportTo < 1 || input.MonthOfExportTo > 12)
                throw new System.ArgumentException("Parametarot Mesec do e nevaliden");
            if (input.YearOfExportFrom < 1800 || input.YearOfExportFrom > 5000)
                throw new System.ArgumentException("Parametarot Godina od e nevaliden");
            if (input.YearOfExportTo < 1800 || input.YearOfExportTo > 5000)
                throw new System.ArgumentException("Parametarot Godina do e nevaliden");
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();
            string sql = "SELECT * FROM export_data where edb = @_edb";
            NpgsqlCommand command = new NpgsqlCommand();
            command.Parameters.Add(new NpgsqlParameter("_edb", input.EDB.ToString()));
            SetCommand(ref command, ref sql, input);
            command.CommandText = sql;
            command.Connection = conn;
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ExecutedExportOutput temp = new ExecutedExportOutput()
                {
                    EDB = reader["edb"].ToString(),
                    ExportAmount = (double)reader["export_amount"],
                    ExportYear = (int)reader["year"],
                    ExportMonth = (int)reader["month"]
                };
                output.Add(temp);
            }
            conn.Close();
            return output;
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
