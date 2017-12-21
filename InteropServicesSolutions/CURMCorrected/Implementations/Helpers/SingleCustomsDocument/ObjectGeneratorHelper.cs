using System.Collections.Generic;
using Contracts.Models.SingleCustomsDocument;
using Oracle.ManagedDataAccess.Client;

namespace Implementations.Helpers.SingleCustomsDocument
{
    public static class ObjectGeneratorHelper
    {
        public static SCDGeneralData GetGeneralData(OracleDataReader reader)
        {
            var output = new SCDGeneralData();
            while (reader.Read())
            {
                output.ExciseStoreCode = (string)reader["KEY_CUO"].ToString();
                output.DeclarantCode = (string)reader["KEY_DEC"].ToString();
                output.ReferentNumber = (string)reader["KEY_NBER"].ToString();
                output.ImportExport = (string)reader["SAD_FLW"].ToString();
                output.DeclatarionType = (string)reader["SAD_TYP_DEC"].ToString();
                output.ProcedureType = (string)reader["SAD_TYP_PROC"].ToString();
                output.SenderEDB = (string)reader["SAD_EXPORTER"].ToString();
                output.RegistrationSeries = (string)reader["SAD_REG_SERIAL"].ToString();
                output.RegistrationNumber = (string)reader["SAD_REG_NBER"].ToString();
                output.RegistrationDate = (string)reader["SAD_REG_DATE"].ToString();
                output.ItemNumber = (string)reader["SAD_ITM_TOTAL"].ToString();
                output.ImporterEDB = (string)reader["SAD_CONSIGNEE"].ToString();
                output.ValueData = (string)reader["SAD_VAL_DETAILS"].ToString();
                output.ExportingCountry = (string)reader["SAD_CTY_EXPCOD"].ToString();
                output.OriginCountry = (string)reader["SAD_CTY_EXPREG"].ToString();
                output.DestinationCountry = (string)reader["SAD_CTY_DESTCOD"].ToString();
                output.ExportingConditionCode = (string)reader["SAD_TOD_COD"].ToString();
                output.ExportingConditionPlace = (string)reader["SAD_TOD_NAM"].ToString();
                output.DeliveryTermsSituationCode = (string)reader["SAD_TOD_SIT"].ToString();
                output.RegistrationOfVehicle = (string)reader["SAD_TRSP_IDBORD"].ToString();
                output.NationalityOfVehicle = (string)reader["SAD_TRSP_NATBORD"].ToString();
                output.CurrencyCode = (string)reader["SAD_CUR_COD"].ToString();
                output.TotalInvoiceAmount = (string)reader["SAD_TOT_INVOICED"].ToString();
            }
            return output;
        }
        public static List<SCDItemData> GetItemData(OracleDataReader reader)
        {
            var output = new List<SCDItemData>();
            while (reader.Read())
            {
                output.Add(new SCDItemData()
                {
                    TariffTagPart1 = reader["SADITM_HS_COD"].ToString(),
                    TariffTagPart2 = reader["SADITM_HSPREC_COD"].ToString(),
                    DescriptionOfGoodsPart1 = reader["SADITM_GOODS_DESC1"].ToString(),
                    DescriptionOfGoodsPart2 = reader["SADITM_GOODS_DESC2"].ToString(),
                    DescriptionOfGoodsPart3 = reader["SADITM_GOODS_DESC3"].ToString(),
                    CountryOfOrigin = reader["SADITM_CTY_ORIGCOD"].ToString(),
                    GrossMass = reader["SADITM_GROSS_MASS"].ToString(),
                    Preference = reader["SADITM_PREFER_COD"].ToString(),
                    StatisticalValue = reader["SADITM_STAT_VAL"].ToString()
                }
                );
            }
            return output;
        }
        public static SCDExporterData GetExporterData(OracleDataReader reader)
        {
            var output = new SCDExporterData();
            while (reader.Read())
            {
                output.SenderName = (string)reader["SAD_EXP_NAM"].ToString();
                output.SenderAddressPart1 = (string)reader["SAD_EXP_ADD1"].ToString();
                output.SenderAddressPart2 = (string)reader["SAD_EXP_ADD2"].ToString();
                output.SenderPlace = (string)reader["SAD_EXP_CTY"].ToString();
                output.SenderPostalCode = (string)reader["SAD_EXP_ZIP"].ToString();
            }
            return output;
        }
        public static SCDImporterData GetImporterData(OracleDataReader reader)
        {
            var output = new SCDImporterData();
            while (reader.Read())
            {
                output.ImporterName = (string)reader["SAD_CON_NAM"].ToString();
                output.ImporterAddressPart1 = (string)reader["SAD_CON_ADD1"].ToString();
                output.ImporterAddressPart2 = (string)reader["SAD_CON_ADD2"].ToString();
                output.ImporterPlace = (string)reader["SAD_CON_CITY"].ToString();
                output.ImporterPostalCode = (string)reader["SAD_CON_ZIP"].ToString();
            }
            return output;
        }
    }
}
