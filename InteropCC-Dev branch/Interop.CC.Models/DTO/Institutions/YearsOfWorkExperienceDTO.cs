using System.Collections.Generic;
using System.Xml.Serialization;

namespace Interop.CC.Models.DTO.Institutions
{
    [XmlRoot("YearsOfWorkExperienceOutput")]
    public class YearsOfWorkExperienceDTO : BasicPrintInfoDTO
    {
        public string Embg { get; set; }

        [XmlElement(ElementName = "GeneralData")]
        public List<GeneralData> GeneralData { get; set; }

        [XmlArray("InsuranceData")]
        [XmlArrayItem("YWEInsuranceData")]
        public List<InsuranceData> InsuranceData { get; set; }

        [XmlArray("OldAndForegnExperience")]
        [XmlArrayItem("YWEOldAndForegnExperience")]
        public List<OldAndForegnExperience> OldAndForegnExperience { get; set; }

        [XmlArray("M4")]
        [XmlArrayItem("YWEM4")]
        public List<M4> M4 { get; set; }

        [XmlArray("InvalidM4")]
        [XmlArrayItem("YWEInvalidM4")]
        public List<InvalidM4> InvalidM4 { get; set; }
        public string YearsOfWorkPDF { get; set; }
        public string YearsOfWorkXML { get; set; }
    }
    public class GeneralData
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DateOfBirht { get; set; }
    }

    public class InsuranceData
    {
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyName { get; set; }
        public string EDB { get; set; }
        public string EMB { get; set; }
        public string StartData { get; set; }
        public string EndDate { get; set; }
        public int WeeklyHours { get; set; }
    }
    public class OldAndForegnExperience
    {
        public string PS { get; set; }
        public string Year { get; set; }
        public string Country { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string DurationWorkExperience { get; set; }
        public string TypeOfWorkExperience { get; set; }
        public string DegreeOfIncreaseOfWorkExperience { get; set; }
        public string WorkExperience { get; set; }

    }
    public class M4
    {
        public string TypeOfForm { get; set; }
        public string Year { get; set; }
        public string RegistrationNumber { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string DurationWorkExperience { get; set; }
        public string WorkingHours { get; set; }
        public string Salary { get; set; }
        public string YearOfSickLeave { get; set; }
        public string EffectiveDuration { get; set; }
        public string DegreeOfIncrease { get; set; }
        public string Meseci { get; set; }
    }
    public class InvalidM4
    {
        public string TypeOfForm { get; set; }
        public string Year { get; set; }
        public string RegistrationNumber { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string DurationWorkExperience { get; set; }
        public string WorkingHours { get; set; }
        public string Salary { get; set; }
        public string YearOfSickLeave { get; set; }
        public string EffectiveDuration { get; set; }
        public string DegreeOfIncrease { get; set; }
    }
}
