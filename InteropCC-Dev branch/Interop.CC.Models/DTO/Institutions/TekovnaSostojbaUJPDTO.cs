using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Interop.CC.Models.DTO.Institutions
{
    public class TekovnaSostojbaUJPDTO : BasicPrintInfoDTO
    {
        public List<CVLEInfoUJP> Info { get; set; }
        public List<CVUnitsUJP> Units { get; set; }
        public List<CVActorsUJP> Actors { get; set; }
        public List<CVOwnersUJP> Owners { get; set; }
        public List<CVActivitiesUJP> Activities { get; set; }
        public List<CVMembershipUJP> Membership { get; set; }
        public List<CVFoundingUJP> Founding { get; set; }
        public List<CVCourtProc> Court { get; set; }
        public string Message { get; set; }
        public string TekovnaSostojbaXML { get; set; }
        public string TekovnaSostojbaPDF { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CVLEInfoUJP
    {
        [XmlElement("IsLETerminated")]
        public string IsLETerminated { get; set; }

        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("LEFullName")]
        public string LEFullName { get; set; }

        [XmlElement("TaxPayerNumber")]
        public string TaxPayerNumber { get; set; }

        [XmlElement("ShortName")]
        public string ShortName { get; set; }

        [XmlElement("TerminationTypeID")]
        public string TerminationTypeID { get; set; }

        [XmlElement("TerminationTypeDesc")]
        public string TerminationTypeDesc { get; set; }

        [XmlElement("TerminationBasis")]
        public string TerminationBasis { get; set; }

        [XmlElement("DateDeletedInCR")]
        public string DateDeletedInCR { get; set; }

        [XmlElement("LETypeID")]
        public string LETypeID { get; set; }

        [XmlElement("LETypeDesc")]
        public string LETypeDesc { get; set; }

        [XmlElement("LESizeID")]
        public string LESizeID { get; set; }

        [XmlElement("LESizeDesc")]
        public string LESizeDesc { get; set; }

        [XmlElement("Municipality")]
        public string Municipality { get; set; }

        [XmlElement("MunicipalityCode")]
        public string MunicipalityCode { get; set; }

        [XmlElement("Place")]
        public string Place { get; set; }

        [XmlElement("PlaceCode")]
        public string PlaceCode { get; set; }

        [XmlElement("Street")]
        public string Street { get; set; }

        [XmlElement("StreetCode")]
        public string StreetCode { get; set; }

        [XmlElement("HouseNo")]
        public string HouseNo { get; set; }

        [XmlElement("EntranceNo")]
        public string EntranceNo { get; set; }

        [XmlElement("FlatNo")]
        public string FlatNo { get; set; }

        [XmlElement("OrganisationTypeCode")]
        public string OrganisationTypeCode { get; set; }

        [XmlElement("OrganisationTypeDesc")]
        public string OrganisationTypeDesc { get; set; }

        [XmlElement("AuthorisedRegisterID")]
        public string AuthorisedRegisterID { get; set; }

        [XmlElement("AuthorisedRegister")]
        public string AuthorisedRegister { get; set; }

        [XmlElement("OwnershipTypeID")]
        public string OwnershipTypeID { get; set; }

        [XmlElement("OwnershipTypeDesc")]
        public string OwnershipTypeDesc { get; set; }

        [XmlElement("IsForeignAct")]
        public string IsForeignAct { get; set; }

        [XmlElement("IsActivityNoLicence")]
        public string IsActivityNoLicence { get; set; }

        [XmlElement("CoreActivityCode")]
        public string CoreActivityCode { get; set; }

        [XmlElement("CoreActivityDesc")]
        public string CoreActivityDesc { get; set; }

        [XmlElement("Licence")]
        public string Licence { get; set; }

        [XmlElement("ForeignActivity")]
        public string ForeignActivity { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("AdditionalInfo")]
        public string AdditionalInfo { get; set; }

        [XmlElement("IsDataConfirmed")]
        public string IsDataConfirmed { get; set; }

        [XmlElement("IsAAActive")]
        public string IsAAActive { get; set; }

        [XmlElement("ActingPeriod")]
        public string ActingPeriod { get; set; }

       
    }
    [XmlType("CrmResultItem")]
    public class CVUnitsUJP
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("UnitNo")]
        public string UnitNo { get; set; }

        [XmlElement("UnitName")]
        public string UnitName { get; set; }

        [XmlElement("UnitTypeID")]
        public string UnitTypeID { get; set; }

        [XmlElement("UnitTypeDescr")]
        public string UnitTypeDescr { get; set; }

        [XmlElement("UnitDescr")]
        public string UnitDescr { get; set; }

        [XmlElement("OtherInfo")]
        public string OtherInfo { get; set; }

        [XmlElement("CountryCode")]
        public string CountryCode { get; set; }

        [XmlElement("Municipality")]
        public string Municipality { get; set; }

        [XmlElement("MunicipalityCode")]
        public string MunicipalityCode { get; set; }

        [XmlElement("Place")]
        public string Place { get; set; }

        [XmlElement("PlaceCode")]
        public string PlaceCode { get; set; }

        [XmlElement("Street")]
        public string Street { get; set; }

        [XmlElement("StreetCode")]
        public string StreetCode { get; set; }

        [XmlElement("HouseNo")]
        public string HouseNo { get; set; }

        [XmlElement("EntranceNo")]
        public string EntranceNo { get; set; }

        [XmlElement("FlatNo")]
        public string FlatNo { get; set; }

        [XmlElement("ActivityCode")]
        public string ActivityCode { get; set; }

        [XmlElement("ActivityDesc")]
        public string ActivityDesc { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CVActorsUJP
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("UnitNo")]
        public string UnitNo { get; set; }

        [XmlElement("PersonOrLEID")]
        public string PersonOrLEID { get; set; }

        [XmlElement("PersonOrLEDesc")]
        public string PersonOrLEDesc { get; set; }

        [XmlElement("ActorID")]
        public string ActorID { get; set; }

        [XmlElement("ActorTypeID")]
        public string ActorTypeID { get; set; }

        [XmlElement("ActorTypeDesc")]
        public string ActorTypeDesc { get; set; }

        [XmlElement("ActorName")]
        public string ActorName { get; set; }

        [XmlElement("ActorSurname")]
        public string ActorSurname { get; set; }

        [XmlElement("CountryCode")]
        public string CountryCode { get; set; }

        [XmlElement("Municipality")]
        public string Municipality { get; set; }

        [XmlElement("Place")]
        public string Place { get; set; }

        [XmlElement("Street")]
        public string Street { get; set; }

        [XmlElement("HouseNo")]
        public string HouseNo { get; set; }

        [XmlElement("EntranceNo")]
        public string EntranceNo { get; set; }

        [XmlElement("FlatNo")]
        public string FlatNo { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Restrictions")]
        public string Restrictions { get; set; }

        [XmlElement("AuthorisationTypeID")]
        public string AuthorisationTypeID { get; set; }

        [XmlElement("AuthorisationTypeDesc")]
        public string AuthorisationTypeDesc { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CVOwnersUJP
    {
      
        [XmlElement("PersonOrLEID")]
        public string PersonOrLEID { get; set; }

        [XmlElement("PersonOrLEDesc")]
        public string PersonOrLEDesc { get; set; }

        [XmlElement("OwnerID")]
        public string OwnerID { get; set; }

        [XmlElement("OwnerTypeID")]
        public string OwnerTypeID { get; set; }

        [XmlElement("OwnerTypeDesc")]
        public string OwnerTypeDesc { get; set; }

        [XmlElement("LiabilityID")]
        public string LiabilityID { get; set; }

        [XmlElement("LiabilityDesc")]
        public string LiabilityDesc { get; set; }

        [XmlElement("OwnerName")]
        public string OwnerName { get; set; }

        [XmlElement("OwnerSurname")]
        public string OwnerSurname { get; set; }

        [XmlElement("CountryCode")]
        public string CountryCode { get; set; }

        [XmlElement("Municipality")]
        public string Municipality { get; set; }

        [XmlElement("Place")]
        public string Place { get; set; }

        [XmlElement("Street")]
        public string Street { get; set; }

        [XmlElement("HouseNo")]
        public string HouseNo { get; set; }

        [XmlElement("EntranceNo")]
        public string EntranceNo { get; set; }

        [XmlElement("FlatNo")]
        public string FlatNo { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("FCCode")]
        public string FCCode { get; set; }

        [XmlElement("ParticipationFC_Cash")]
        public string ParticipationFC_Cash { get; set; }

        [XmlElement("ParticipationFC_NonCash")]
        public string ParticipationFC_NonCash { get; set; }

        [XmlElement("ParticipationFC_Payd")]
        public string ParticipationFC_Payd { get; set; }

        [XmlElement("ParticipationFC_Total")]
        public string ParticipationFC_Total { get; set; }
        [XmlElement("AddInfo")]
        public string AddInfo { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CVActivitiesUJP
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("ActivityCode")]
        public string ActivityCode { get; set; }

        [XmlElement("ActivityDesc")]
        public string ActivityDesc { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CVMembershipUJP
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("MemberOf")]
        public string MemberOf { get; set; }

        [XmlElement("Members")]
        public string Members { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CVFoundingUJP
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("FoundingDate")]
        public string FoundingDate { get; set; }

        [XmlElement("CapitalOriginID")]
        public string CapitalOriginID { get; set; }

        [XmlElement("CapitalOriginDesc")]
        public string CapitalOriginDesc { get; set; }

        [XmlElement("FCCode")]
        public string FCCode { get; set; }

        [XmlElement("CapitalFC_Cash")]
        public string CapitalFC_Cash { get; set; }

        [XmlElement("CapitalFC_NonCash")]
        public string CapitalFC_NonCash { get; set; }

        [XmlElement("CapitalFC_Payd")]
        public string CapitalFC_Payd { get; set; }

        [XmlElement("SharesTotal")]
        public string SharesTotal { get; set; }

        [XmlElement("SharesPayd")]
        public string SharesPayd { get; set; }

        [XmlElement("CapitalFC_Total")]
        public string CapitalFC_Total { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CVCourtProc
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("CourtJournalID")]
        public string CourtJournalID { get; set; }

        [XmlElement("DecisionDate")]
        public string DecisionDate { get; set; }

        [XmlElement("ValidityDate")]
        public string ValidityDate { get; set; }

        [XmlElement("ValidityHour")]
        public string ValidityHour { get; set; }

        [XmlElement("StageID")]
        public string StageID { get; set; }

        [XmlElement("StageDesc")]
        public string StageDesc { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("BankruptcyCourtName")]
        public string BankruptcyCourtName { get; set; }

        [XmlElement("TypeID")]
        public string TypeID { get; set; }

        [XmlElement("TypeDesc")]
        public string TypeDesc { get; set; }

    }
}
