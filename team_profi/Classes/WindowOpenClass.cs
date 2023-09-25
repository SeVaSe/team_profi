using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace team_profi.Classes
{
    public class WindowOpenClass
    {
        public static void OpenWindow<T>() where T : Window, new()
        {
            Window wind = new T();
            wind.Show();
        }
    }
}
