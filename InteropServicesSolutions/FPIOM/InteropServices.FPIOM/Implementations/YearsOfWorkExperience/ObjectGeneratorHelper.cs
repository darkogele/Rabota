using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.FPIOM.Implementations
{
    public static class ObjectGeneratorHelper
    {
        private static string ToStringOrEmpty(this object readerData)
        {
            try
            {
                return readerData.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static YWEGeneralData GetGeneralData(OracleDataReader reader)
        {
            YWEGeneralData output = new YWEGeneralData();
            while (reader.Read())
            {
                output.Surname = reader["PREZIME"].ToStringOrEmpty();
                output.Name = reader["IME"].ToStringOrEmpty();
                output.Gender = reader["POL"].ToStringOrEmpty();
                output.DateOfBirht = reader["DATUM_RAGANJE"].ToStringOrEmpty();
            }
            return output;
        }
        public static List<YWEInsuranceData> GetInsuranseData(OracleDataReader reader)
        {
            List<YWEInsuranceData> output = new List<YWEInsuranceData>();
            while (reader.Read())
            {
                output.Add(new YWEInsuranceData
                {
                    CompanyRegistrationNumber = reader["REG_BR"].ToStringOrEmpty(),
                    CompanyName = reader["NAZIV"].ToStringOrEmpty(),
                    EDB = reader["DANOCEN_BROJ"].ToStringOrEmpty(),
                    EMB = reader["MATICEN_BROJ"].ToStringOrEmpty(),
                    StartData = reader["OD_DATUM"].ToStringOrEmpty(),
                    EndDate = reader["DO_DATUM"].ToStringOrEmpty(),
                    WeeklyHours = Convert.ToInt32(reader["CASOVI_NEDELNO"].ToStringOrEmpty())
                }
                );
            }
            return output;
        }
        public static List<YWEOldAndForegnExperience> GetOldAndForeignExperience(OracleDataReader reader)
        {
            List<YWEOldAndForegnExperience> output = new List<YWEOldAndForegnExperience>();
            while (reader.Read())
            {
                output.Add(new YWEOldAndForegnExperience
                {
                    PS = reader["'ПС'"].ToStringOrEmpty(),
                    Year = reader["GODINA"].ToStringOrEmpty(),
                    Country = reader["DRZAVA"].ToStringOrEmpty(),
                    PeriodFrom = reader["PERIOD_OD"].ToStringOrEmpty(),
                    PeriodTo = reader["PERIOD_DO"].ToStringOrEmpty(),
                    DurationWorkExperience = reader["TRAST"].ToStringOrEmpty(),
                    TypeOfWorkExperience = reader["VSTAZ"].ToStringOrEmpty(),
                    DegreeOfIncreaseOfWorkExperience = reader["STEPEN"].ToStringOrEmpty(),
                    WorkExperience = reader["TRAENJE"].ToStringOrEmpty()
                }
                );
            }
            return output;
        }
        public static List<YWEM4> GetM4(OracleDataReader reader)
        {
            List<YWEM4> output = new List<YWEM4>();
            while (reader.Read())
            {
                output.Add(new YWEM4()
                {
                    TypeOfForm = reader["OBRAZEC"].ToStringOrEmpty(),
                    Year = reader["GODINA"].ToStringOrEmpty(),
                    RegistrationNumber = reader["REGB"].ToStringOrEmpty(),
                    PeriodFrom = reader["PEROD"].ToStringOrEmpty(),
                    PeriodTo = reader["PERDO"].ToStringOrEmpty(),
                    DurationWorkExperience = reader["TRAST"].ToStringOrEmpty(),
                    WorkingHours = reader["CASLD"].ToStringOrEmpty(),
                    Salary = reader["DINLD"].ToStringOrEmpty(),
                    YearOfSickLeave = reader["GOD_BOL"].ToStringOrEmpty(),
                    EffectiveDuration = reader["EFTRA"].ToStringOrEmpty(),
                    DegreeOfIncrease = reader["STEPEN"].ToStringOrEmpty(),
                    Meseci = reader.GetOracleDecimal(11).ToStringOrEmpty()
                }
                );
            }
            return output;
        }
        public static List<YWEInvalidM4> GetInvalidM4(OracleDataReader reader)
        {
            List<YWEInvalidM4> output = new List<YWEInvalidM4>();
            while (reader.Read())
            {
                output.Add(new YWEInvalidM4()
                {
                    TypeOfForm = reader["OBRAZEC"].ToStringOrEmpty(),
                    Year = reader["GODINA"].ToStringOrEmpty(),
                    RegistrationNumber = reader["REGB"].ToStringOrEmpty(),
                    PeriodFrom = reader["PEROD"].ToStringOrEmpty(),
                    PeriodTo = reader["PERDO"].ToStringOrEmpty(),
                    DurationWorkExperience = reader["TRAST"].ToStringOrEmpty(),
                    WorkingHours = reader["CASLD"].ToStringOrEmpty(),
                    Salary = reader["DINLD"].ToStringOrEmpty(),
                    YearOfSickLeave = reader["GOD_BOL"].ToStringOrEmpty(),
                    EffectiveDuration = reader["EFTRA"].ToStringOrEmpty(),
                    DegreeOfIncrease = reader["STEPEN"].ToStringOrEmpty()
                }
                );
            }
            return output;
        }
    }
}
