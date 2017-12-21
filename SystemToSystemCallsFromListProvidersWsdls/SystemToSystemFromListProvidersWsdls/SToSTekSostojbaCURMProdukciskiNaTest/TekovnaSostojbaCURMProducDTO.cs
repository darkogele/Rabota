using System.Collections.Generic;
using System.Xml.Serialization;

namespace SToSTekSostojbaCURMProdukciskiNaTest
{
    public class TekovnaSostojbaCURMProducDTO
    {
        public List<CU11> Info { get; set; }
        public List<CU12> Owners { get; set; }
        public List<CU13> Actors { get; set; }
        public string Message { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class CU11
    {

        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("LEFullName")]
        public string LEFullName { get; set; }

        [XmlElement("TaxPayerNumber")]
        public string TaxPayerNumber { get; set; }

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

    }
    [XmlType("CrmResultItem")]
    public class CU12
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("ActorID")]
        public string ActorID { get; set; }

        [XmlElement("PersonType")]
        public string PersonType { get; set; }



        [XmlElement("ActorName")]
        public string ActorName { get; set; }

        [XmlElement("ActorSurname")]
        public string ActorSurname { get; set; }

        [XmlElement("Authorisation")]
        public string Authorisation { get; set; }

        [XmlElement("Restrictions")]
        public string Restrictions { get; set; }

        [XmlElement("AuthTypeID")]
        public string AuthTypeID { get; set; }

        [XmlElement("AuthTypeDesc")]
        public string AuthTypeDesc { get; set; }

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

        [XmlElement("CountryCode")]
        public string CountryCode { get; set; }

        [XmlElement("Country")]
        public string Country { get; set; }

        [XmlElement("Phone")]
        public string Phone { get; set; }

        [XmlElement("Fax")]
        public string Fax { get; set; }

        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("WebAddress")]
        public string WebAddress { get; set; }


    }
    [XmlType("CrmResultItem")]
    public class CU13
    {
        [XmlElement("LEID")]
        public string LEID { get; set; }

        [XmlElement("BankruptcyCourtName")]
        public string BankruptcyCourtName { get; set; }

        [XmlElement("CourtJournalID")]
        public string CourtJournalID { get; set; }

        [XmlElement("DecisionDate")]
        public string DecisionDate { get; set; }

        [XmlElement("ValidityDate")]
        public string ValidityDate { get; set; }

        [XmlElement("ValidityTime")]
        public string ValidityTime { get; set; }

        [XmlElement("StageID")]
        public string StageID { get; set; }

        [XmlElement("StageDesc")]
        public string StageDesc { get; set; }

        [XmlElement("Disposition")]
        public string Disposition { get; set; }

        [XmlElement("TypeID")]
        public string TypeID { get; set; }

        [XmlElement("TypeDesc")]
        public string TypeDesc { get; set; }
    }
}
