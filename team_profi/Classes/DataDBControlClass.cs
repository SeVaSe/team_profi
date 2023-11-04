using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team_profi.Classes
{
    // определение, что за админ щас
    public class DataDBControlClass
    {
        private static string nameAdmin = "noap";

        public static void SetName(string name)
        {
            nameAdmin = name;
        }
        
        public static string GetName()
        {
            return nameAdmin;
        }
    }

    // определение, что за студент щас
    public class DataGetIDStudentClass
    {
        private static string nameStudent = "noap";

        public static void SetIDStud(string name)
        {
            nameStudent = name;
        }

        public static string GetName()
        {
            return nameStudent;
        }
    }

    // определение, что за задание щас
    public class DataGetIDAssigmentClass
    {
        private static string nameAssig = "noap";

        public static void SetIDAssig(string name)
        {
            nameAssig = name;
        }

        public static string GetName()
        {
            return nameAssig;
        }
    }
}
