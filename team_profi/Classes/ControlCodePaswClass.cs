using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace team_profi.Classes
{
    public class ControlCodePaswClass
    {
        private static string codePasw = "noap";

        public static string CodePasw
        {
            get { return codePasw; }
            set { codePasw = value; }
        }
    }
}
