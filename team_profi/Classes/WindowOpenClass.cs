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
        // Открывает новое окно указанного типа, который является наследником класса Window и имеет публичный конструктор без параметров
        public static void OpenWindow<T>() where T : Window, new()
        {
            // Создание экземпляра окна указанного типа
            Window wind = new T();

            // Отображение созданного окна
            wind.Show();
        }
    }

    public class LoginInfoAll
    {
        private static string login = "net"; // Строка по умолчанию для логина

        // Метод для установки значения логина
        public static void ShowLogin(string log)
        {
            login = log; // Присваивание переданного значения логина переменной
        }

        // Метод для получения текущего значения логина
        public static string GetLogin()
        {
            return login; // Возвращение текущего значения логина
        }
    }

}
