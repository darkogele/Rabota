using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class SingleCustomsDocumentDTO : BasicPrintInfoDTO
    {
        public SCDGeneralDataDTO GeneralData { get; set; }
        public List<SCDItemDataDTO> ItemData { get; set; }
        public SCDExporterDataDTO ExporterData { get; set; }
        public SCDImporterDataDTO ImporterData { get; set; }
        public string Message { get; set; }
        public string FileXMLNameDocument { get; set; }
        public string FilePDFNameDocument { get; set; }
    }

    public class SCDGeneralDataDTO
    {
        public string ExciseStoreCode { get; set; }
        public string DeclarantCode { get; set; }
        public string ReferentNumber { get; set; }
        public string ImportExport { get; set; }
        public string DeclatarionType { get; set; }
        public string ProcedureType { get; set; }
        public string SenderEDB { get; set; }
        public string RegistrationSeries { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string ItemNumber { get; set; }
        public string ImporterEDB { get; set; }
        public string ValueData { get; set; }
        public string ExportingCountry { get; set; }
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }
        public string ExportingConditionCode { get; set; }
        public string ExportingConditionPlace { get; set; }
        public string DeliveryTermsSituationCode { get; set; }
        public string RegistrationOfVehicle { get; set; }
        public string NationalityOfVehicle { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalInvoiceAmount { get; set; }
    }

    public class SCDItemDataDTO
    {
        public string TariffTagPart1 { get; set; }
        public string TariffTagPart2 { get; set; }
        public string DescriptionOfGoodsPart1 { get; set; }
        public string DescriptionOfGoodsPart2 { get; set; }
        public string DescriptionOfGoodsPart3 { get; set; }
        public string CountryOfOrigin { get; set; }
        public string GrossMass { get; set; }
        public string Preference { get; set; }
        public string StatisticalValue { get; set; }
    }

    public class SCDExporterDataDTO
    {
        public string SenderName { get; set; }
        public string SenderAddressPart1 { get; set; }
        public string SenderAddressPart2 { get; set; }
        public string SenderPlace { get; set; }
        public string SenderPostalCode { get; set; }
    }

    public class SCDImporterDataDTO
    {
        public string ImporterName { get; set; }
        public string ImporterAddressPart1 { get; set; }
        public string ImporterAddressPart2 { get; set; }
        public string ImporterPlace { get; set; }
        public string ImporterPostalCode { get; set; }
    }
}
