using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team_profi.Classes
{
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
}
