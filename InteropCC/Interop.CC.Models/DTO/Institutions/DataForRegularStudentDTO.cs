using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Interop.CC.Models.DTO.Institutions
{
    [XmlType("student")]
    public class DataForRegularStudentDTO : BasicPrintInfoDTO
    {
        [XmlElement("municipality")]
        public string Municipality { get; set; }

        [XmlElement("school")]
        public string School { get; set; }

        [XmlElement("schoolClass")]
        public string SchoolClass { get; set; }

        [XmlElement("schoolYear")]
        public string SchoolYear { get; set; }

        [XmlElement("grade")]
        public string Grade { get; set; }

        [XmlElement("profession")]
        public string Profession { get; set; }

        [XmlElement("profile")]
        public string Profile { get; set; }

        [XmlElement("programAssociation")]
        public string ProgramAssociation { get; set; }

        [XmlElement("languageEducation")]
        public string LanguageEducation { get; set; }

        [XmlElement("fullName")]
        public string FullName { get; set; }

        [XmlElement("fatherName")]
        public string FatherName { get; set; }

        [XmlElement("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [XmlElement("placeOfBirth")]
        public string PlaceOfBirth { get; set; }

        [XmlElement("embg")]
        public string Embg { get; set; }

        [XmlElement("ethnicity")]
        public string Ethnicity { get; set; }

        [XmlElement("nativeLanguage")]
        public string NativeLanguage { get; set; }

        [XmlElement("place")]
        public string Place { get; set; }

        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("mobile")]
        public string Mobile { get; set; }

        [XmlElement("schoolClassStatus")]
        public string SchoolClassStatus { get; set; }

        [XmlElement("schoolStatus")]
        public string SchoolStatus { get; set; }

        public string Message { get; set; }
        public string FilePdfName { get; set; }
        public string FileXMLName { get; set; }

    }
}
