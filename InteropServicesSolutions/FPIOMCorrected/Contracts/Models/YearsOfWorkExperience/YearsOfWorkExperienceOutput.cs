using System.Collections.Generic;

namespace Contracts.Models.YearsOfWorkExperience
{
    public class YearsOfWorkExperienceOutput
    {
        public YWEGeneralData GeneralData;
        public List<YWEInsuranceData> InsuranceData;
        public List<YWEOldAndForegnExperience> OldAndForeignExperience;
        public List<YWEM4> M4;
        public List<YWEInvalidM4> InvalidM4;
    }
}
