using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediLine_FrontEnd.Utilities
{
    public enum UserStatus
    {
        Active,         // 0
        Inactive,       // 1
        Suspended,      // 2
        DataInputError, // 3
        Unknown         // 4
    }

    public enum UserRole
    {
        Patient,           // 0
        SpecialistDoctor,  // 1
        MedicalAssistant,  // 2
        Administrator      // 3
    }
}
