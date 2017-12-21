using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.FPIOM.Implementations
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
